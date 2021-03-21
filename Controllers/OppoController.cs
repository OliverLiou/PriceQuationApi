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
using Microsoft.AspNetCore.Authorization;

namespace PriceQuationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class OppoController :ControllerBase
    {
        private IOppoService _service;

        public OppoController(IOppoService service)
        {
             _service = service;
        }

        [HttpPost("CreateOppo/{oppoId}")]
        // [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> CreateOppo(string oppoId, [FromForm(Name = "file")] IFormFileCollection excelfiles)
        {
            try
            {
                if (oppoId == null)
                    throw new Exception("請輸入OPPO號碼。");
                //判斷輸入的OPPO是否已存在資料庫中(避免重複上傳)
                var item = await _service.GetOppo(oppoId);
                if (item != null)
                    throw new Exception(string.Format("立項號碼:【{0}】請勿重複上傳", oppoId));

                //判斷丟過來的Bom 是否有資料
                else if (excelfiles.Count <= 0)
                    throw new Exception("請確認是否有上傳檔案！");

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

                Oppo oppo = new Oppo()
                {
                    OppoId = oppoId,
                    Status = 0
                };
                //組成Bom
                PublicMethods publicMethod = new PublicMethods(_service);
                List<Bom> Boms = new List<Bom>();
                foreach (var sheet in sheetList)
                {
                    Boms.Add(await publicMethod.SetBomData(oppoId, sheet, evaluator));
                }
                oppo.Boms = Boms;
                await _service.CreateOppo(oppo);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetOppos")]
        public async Task<ActionResult<List<Oppo>>> GetOppos()
        {
            try
            {
                var items = await _service.GetOppos();
                return items;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

    }
}