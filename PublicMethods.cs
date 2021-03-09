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

namespace PriceQuationApi
{
    public class PublicMethods
    {
        IOppoService _oppoService;
        public PublicMethods(IOppoService oppoService)
        {
            _oppoService = oppoService;
        }

        public async Task<Bom> SetBomData(string oppoId, ISheet sheet, IFormulaEvaluator evaluator)
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
                PlmMiddle plmMiddle = await _oppoService.GetMiddleData(oppoId, Bom.AssemblyPartNumber);
                if (plmMiddle == null)
                    throw new Exception(string.Format("PLM中間區【立項單號：{0}】無【總成件號：{1}】的資料！請確認，上傳Excel是否正確！", oppoId, Bom.AssemblyPartNumber));
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
                    quoteDetail.QuoteItemId = await _oppoService.GetQuoteItem(quoters[i]);
                    quoteDetail.QuoteTime = Convert.ToDateTime(quote_Times[i]);
                    quoteDetails.Add(quoteDetail);
                }

                //讀取細部資料
                List<BomItem> bomItems = new List<BomItem>();
                int rowCount = 0;
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
                        rowCount ++;
                        string No = rowCount.ToString().PadLeft(3, '0'); //系統計算，不要拿Bom表上的 No.，要做ID使用
                        //partlevel 
                        int partlevel = -1; //假設為未填寫
                        for (int j = 1; j <= 9; j++)
                        {
                            if (IsHook(row.GetCell(j)))
                            {
                                partlevel = j - 1; //-1 是因為 階層從0開始，但j是表示欄號
                                break;
                            }
                        }
                        //新件 or 延用
                        string neworold = string.Empty;
                        if (IsHook(row.GetCell(28)))
                            neworold = "Old";
                        else if (IsHook(row.GetCell(29)))
                            neworold = "New";
                        //進口、支給、自製、外包
                        string source = string.Empty;
                        if (neworold == "New")
                        {
                            for (int j = 31; j <= 34; j++)
                            {
                                if (IsHook(row.GetCell(j)))
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
                        else if (neworold == "Old")
                        {
                            source = "延用件";
                        }
                        //數量
                        decimal quantity = -1M;
                        if (row.GetCell(35).ToString() != string.Empty)
                            decimal.TryParse(row.GetCell(35).ToString(), out quantity);

                        //製造類別
                        string manufactureCategory = string.Empty;
                        for (int k = 36; k <= 39; k++)
                        {
                            if (IsHook(row.GetCell(k)))
                            {
                                if (k == 36)
                                    manufactureCategory = "沖壓件";
                                else if (k == 37)
                                    manufactureCategory = "塑膠件";
                                else if (k == 38)
                                    manufactureCategory = "鍛造件";
                                else if (k == 39)
                                    manufactureCategory = "其它";
                            }
                        }
                        //模具類別
                        string modelCategory = string.Empty;
                        for (int m = 40; m <= 41; m++)
                        {
                            if (IsHook(row.GetCell(m)))
                                modelCategory = "模具自製";
                            else if (IsHook(row.GetCell(m)))
                                modelCategory = "模具外包";
                        }

                        var bomItem = new BomItem()
                        {
                            BomItemId = Bom.AssemblyPartNumber + "-" + No,
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
                            Quantity = quantity,
                            Category = manufactureCategory,
                            ModelCategory = modelCategory
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
                List<MeasuringItem> measuringItems = new List<MeasuringItem>();
                List<FixtureItem> fixtureItems = new List<FixtureItem>();
                foreach (var item in bomItems)
                {
                    MeasuringItem measuringItem = new MeasuringItem()
                    {
                        MeasuringItemId = item.BomItemId,
                        AssemblyPartNumber = item.AssemblyPartNumber,
                        PartNumber = item.PartNumber
                    };
                    measuringItems.Add(measuringItem);
                }
                foreach (var item in bomItems)
                {
                    FixtureItem fixtureItem = new FixtureItem()
                    {
                        FixtureItemId = item.BomItemId,
                        AssemblyPartNumber = item.AssemblyPartNumber,
                        PartNumber = item.PartNumber
                    };
                    fixtureItems.Add(fixtureItem);
                }
                Bom.MeasuringItems = measuringItems;
                Bom.FixtureItems = fixtureItems;
                Bom.QuoteDetails = quoteDetails;
                return Bom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // public static bool IsHook(string value)
        // {
        //     value = value.Trim();
        //     if (value == "V" | value == "v")
        //         return true;
        //     else
        //         return false;
        // }
        public static bool IsHook(ICell cell)
        {
            if (cell == null) 
                return false;

            string value = cell.ToString();
            value = value.Trim();
            if (value == "V" | value == "v")
                return true;
            else
                return false;
        }

        public static void BomItemCheckEmpty(string assemblyPartNumber, BomItem bomItem, ref string errMsg)
        {
            if (bomItem.BomItemId.Replace(assemblyPartNumber + "-", "") == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『No』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.PartLevel == -1)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『構成關係』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.PartNumber == string.Empty)
                errMsg += string.Format("總成件號：{0}，No：{1}，『件號』未填寫！", assemblyPartNumber, bomItem.BomItemId) + Environment.NewLine;
            if (bomItem.NeworOld == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『沿用』or『新件』未勾選！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            else if (bomItem.NeworOld == "Old" && bomItem.OldCarType == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『沿用車型』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.Source == string.Empty)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『進口』or『支給』or『自製』or『外包』未勾選！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
            if (bomItem.Quantity <= 0M)
                errMsg += string.Format("總成件號：{0}，件號：{1}，『數量』未填寫！", assemblyPartNumber, bomItem.PartNumber) + Environment.NewLine;
        }

        //取得欄位值
        public static object getCellValue(ICell cell)
        {
            object value = null;

            if (cell != null && cell.CellType != CellType.Blank)
            {
                switch (cell.CellType)
                {
                    case CellType.Numeric:
                        if (DateUtil.IsCellInternalDateFormatted(cell) |
                            DateUtil.IsCellDateFormatted(cell))
                        {
                            // value = cell.DateCellValue.ToString("yyyyMMdd"); 二次執行會出錯 誤使用
                            value = DateTime.FromOADate(cell.NumericCellValue).ToString("yyyy/MM/dd");
                            //參考網址：https://www.dotblogs.com.tw/BerryNote/2020/09/14/214805
                        }
                        else
                        {
                            value = cell.NumericCellValue;
                        }
                        break;
                    case CellType.Boolean:
                        value = cell.BooleanCellValue;
                        break;
                    case CellType.Formula:
                        value = cell.CellFormula;
                        break;
                    default:
                        value = cell.StringCellValue;
                        break;
                }
            }
            return value;
        }
    }
}