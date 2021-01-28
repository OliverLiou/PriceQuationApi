using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using PriceQuationApi.Model;
using PriceQuationApi.Services;
using NPOI.XSSF.UserModel; //XSSF 用來產生Excel 2007檔案（.xlsx）
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using PriceQuationApi.Plm;

namespace PriceQuationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BomController : ControllerBase
    {
        private IBomService _service;

        public BomController(IBomService service)
        {
            _service = service;
        }

        [HttpPost("CreateBom")]
        public async Task<IActionResult> CreateBom([FromForm(Name = "file")] IFormFileCollection excelfiles)
        {
            try
            {
                //判斷丟過來的Bom 是否有資料
                if (excelfiles.Count <= 0)
                {
                    ModelState.AddModelError("Error", "請確認是否有上傳檔案！");
                    return BadRequest(ModelState);
                }

                List<IWorkbook> workbookList = new List<IWorkbook>();
                List<ISheet> sheetList = new List<ISheet>();
                IFormulaEvaluator evaluator = null;
                foreach (var file in excelfiles)
                {
                    await using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        Stream stream = new MemoryStream(ms.ToArray());
                        IWorkbook workbook = null;
                        if (file.FileName.Contains(".xlsx"))
                        {
                            workbook = new XSSFWorkbook(stream);
                            evaluator = new XSSFFormulaEvaluator(workbook);
                        }
                        else
                        {
                            workbook = new HSSFWorkbook(stream);
                            evaluator = new HSSFFormulaEvaluator(workbook);
                        }
                        workbookList.Add(workbook);
                        //讀取工作表
                        ISheet sheet = (ISheet)workbook.GetSheet("BOM續頁");
                        sheetList.Add(sheet);
                    }
                }

                //組成Bom
                List<Bom> Boms = new List<Bom>();
                foreach (var sheet in sheetList)
                {
                    Boms.Add(await SetBomData(sheet, evaluator));
                }
                await _service.CreateBoms(Boms);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetBoms")]
        public async Task<ActionResult<IEnumerable<Bom>>> GetBoms()
        {
            try
            {
                var Boms = await _service.GetBomsAsync();
                return Boms.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetBomDetail/{assemblyPartNumber}")]
        public async Task<ActionResult<Bom>> GetBomDetail(string assemblyPartNumber)
        {
            try
            {
                Bom bom = new Bom();
                bom = await _service.GetBomDetailsAsync(assemblyPartNumber);
                if(bom == null)
                    return NotFound();

                return bom;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error",ex.Message);
                return BadRequest(ModelState);
            }
        }
        
        #region Function
        private async Task<Bom> SetBomData(ISheet sheet, IFormulaEvaluator evaluator)
        {
            string ErrMsg = string.Empty;
            Bom Bom = new Bom();
            List<string> data = new List<string>();
            List<QuoteDetail> quoteDetails = new List<QuoteDetail>();
            try
            {
                for (int i = 2; i <= 6; i++)
                {
                    IRow row = sheet.GetRow(i);
                    ICell cell = row.GetCell(10);
                    var cellValue = evaluator.Evaluate(cell);
                    if (cellValue == null)
                    {
                        data.Add(string.Empty);
                        continue;
                    }
                    if (cellValue.CellType == CellType.Formula)
                    {
                        data.Add(cellValue.StringValue);
                    }
                    else if (cellValue.CellType == CellType.Numeric)
                    {
                        data.Add(cellValue.NumberValue.ToString());
                    }
                    else if (cellValue.CellType == CellType.Blank)
                    {
                        data.Add(string.Empty);
                    }
                    else
                    {
                        data.Add(cellValue.StringValue);
                    }
                }
                Bom.Customer = data[0].Trim();
                Bom.Model = data[1].Trim();
                Bom.AssemblyPartNumber = data[2].Trim();
                Bom.AssemblyName = data[3].Trim();
                Bom.AssemblyNameEng = data[4].Trim();
                Bom.UserId = 1;
                Bom.CreateDate = DateTime.Now;
                Bom.AllFinishTime = DateTime.Now.AddDays(7);

                //組成quoteDetails
                //拿取中間區資料
                PlmMiddle plmMiddle = await _service.GetMiddleData(Bom.AssemblyPartNumber);
                if (plmMiddle == null)
                    throw new Exception("PLM中間區找不到「總成件號」為：" + Bom.AssemblyPartNumber + "的資料！請確認，總成資料是否正確");
                //放入QuoteDetail
                var quoters = plmMiddle.QUOTER.Split(',');
                var quote_Times = plmMiddle.QUOTE_TIME.Split(',');
                if (quoters.Length == 0)
                    throw new Exception(string.Format("PLM中間區，立項單號：{0}，總成件號{1}，未填寫報價單位，請連絡PLM管理者",
                                                      plmMiddle.OPPO, Bom.AssemblyPartNumber));
                else if (quoters.Length != quote_Times.Length)
                    throw new Exception(string.Format("PLM中間區，立項單號：{0}，總成件號{1}，報價單位與報價時間數量不正確！，請連絡PLM管理者",
                                                      plmMiddle.OPPO, Bom.AssemblyPartNumber));
                for (int i = 0; i < quoters.Length; i++)
                {
                    QuoteDetail quoteDetail = new QuoteDetail();
                    quoteDetail.AssemblyPartNumber = Bom.AssemblyPartNumber;
                    quoteDetail.QuoteItemId = await _service.GetQuoteItem(quoters[i]);
                    quoteDetail.QuoteTime = Convert.ToDateTime(quote_Times[i]);
                    quoteDetails.Add(quoteDetail);
                }

                //讀取細部資料
                List<BomItem> bomItems = new List<BomItem>();
                for (int i = 10; i < sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    List<ICell> Cells = row.Cells;
                    bool read = false;
                    for (int j = 1; j <= 16; j++)
                    {
                        if (j == 13 | j == 14 | j == 15)
                            continue; //版次 圖號 版次 不看
                        else
                        {
                            if (Cells[j].ToString().Length > 0)
                            {
                                read = true;
                                break;
                            }
                        }
                    }

                    if (read)
                    {
                        string No = row.GetCell(0).ToString();
                        //partlevel 
                        int partlevel = -1; //假設為未填寫
                        for (int j = 1; j <= 9; j++)
                        {
                            if (IsHook(row.GetCell(j).ToString()))
                            {
                                partlevel = j - 1; //-1 是因為 階層從0開始，但j是表示欄號
                                break;
                            }
                        }
                        //新件 or 延用
                        string neworold = string.Empty;
                        if (IsHook(row.GetCell(28).ToString()))
                            neworold = "Old";
                        else if (IsHook(row.GetCell(29).ToString()))
                            neworold = "New";
                        //進口、支給、自製、外包
                        string source = string.Empty;
                        if (neworold == "New")
                        {
                            for (int j = 31; j <= 34; j++)
                            {
                                if (IsHook(row.GetCell(j).ToString()))
                                {
                                    if (j == 31)
                                        source = "進口件";
                                    else if (j == 32)
                                        source = "支給件";
                                    else if (j == 33)
                                        source = "自製件";
                                    else if (j == 34)
                                        source = "外包件";
                                }
                            }
                        }
                        else
                        {
                            source = "延用件";
                        }
                        //數量
                        decimal quantity = -1M;
                        if (row.GetCell(35).ToString() != string.Empty)
                            decimal.TryParse(row.GetCell(35).ToString(), out quantity);

                        var bomItem = new BomItem()
                        {
                            No = Bom.AssemblyPartNumber + "-" + No,
                            PartLevel = Convert.ToInt16(partlevel),
                            PartNumber = row.GetCell(10).ToString(),
                            PartName = row.GetCell(16).ToString(),
                            PartName_Eng = row.GetCell(17).ToString(),
                            Material = row.GetCell(18).ToString(),
                            ThicknessWire = row.GetCell(19).ToString(),
                            RoutingNo1 = row.GetCell(20).ToString(),
                            RoutingRule1 = row.GetCell(21).ToString(),
                            RoutingNo2 = row.GetCell(22).ToString(),
                            RoutingRule2 = row.GetCell(23).ToString(),
                            RoutingNo3 = row.GetCell(24).ToString(),
                            RoutingRule3 = row.GetCell(25).ToString(),
                            RoutingNo4 = row.GetCell(26).ToString(),
                            RoutingRule4 = row.GetCell(27).ToString(),
                            NeworOld = neworold,
                            Source = source,
                            Quantity = quantity
                        };
                        //檢查有無空值
                        BomItemCheckEmpty(Bom.AssemblyPartNumber, bomItem, ref ErrMsg);
                        bomItems.Add(bomItem);
                    }
                }

                if (ErrMsg.Length > 0)
                    throw new Exception(ErrMsg);
                Bom.BomItems = bomItems;
                //Copy資料去 MeasuringItem
                Bom.MeasuringItems = _service.CreateMeausringItems(bomItems).ToList();
                Bom.FixtureItems = _service.CreateFixtureItems(bomItems).ToList();
                Bom.QuoteDetails = quoteDetails;
                Bom.Status = 1; //Bom表匯入
                return Bom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsHook(string value)
        {
            value = value.Trim();
            if (value == "V" | value == "v")
                return true;
            else
                return false;
        }

        private static void BomItemCheckEmpty(string assemblyPartNumber, BomItem bomItem, ref string errMsg)
        {
            if (bomItem.No.Replace(assemblyPartNumber + "-", "") == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『No』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.PartLevel == -1)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『構成關係』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.PartNumber == string.Empty)
                errMsg += string.Format("總成件號：{0}，No：{1}，『件號』未填寫！", assemblyPartNumber, bomItem.No) + Environment.NewLine;
            if (bomItem.NeworOld == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『沿用』or『新件』未勾選！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            else if (bomItem.NeworOld == "Old" && bomItem.OldCarType == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『沿用車型』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.Source == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『進口』or『支給』or『自製』or『外包』未勾選！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.Quantity <= 0M)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『數量』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
        }
        #endregion
    }
}