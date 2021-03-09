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
        private IBomService _bomService;
        private IOppoService _oppoService;

        public BomController(IBomService bomservice, IOppoService oppoService)
        {
            _bomService = bomservice;
            _oppoService = oppoService;
        }

        [HttpGet("GetBoms")]
        public async Task<ActionResult<IEnumerable<Bom>>> GetBoms()
        {
            try
            {
                var Boms = await _bomService.GetBomsAsync();
                return Boms.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("GetBomItems/{assemblyPartNumber}")]
        public async Task<ActionResult<Bom>> GetBomItems(string assemblyPartNumber)
        {
            try
            {
                Bom bom = new Bom();
                bom = await _bomService.GetBomDetailsAsync(assemblyPartNumber);
                if (bom == null)
                    return NotFound();

                return bom;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CreateBom/{oppoId}")]
        public async Task<ActionResult> CreateBom(string oppoId, [FromForm(Name = "file")] IFormFile formFile)
        {
            try
            {
                //確認oppOId 是否存在
                var oppo = _oppoService.GetOppo(oppoId);
                if (oppo == null)
                    throw new Exception(string.Format("上傳失敗！【立項單號: {0} 不存在】。", oppoId));

                //讀取單個 Excel  創立Bom
                PublicMethods publicMethods = new PublicMethods(_oppoService);

                IWorkbook workBook = null;
                ISheet sheet = null;
                IFormulaEvaluator evaluator = null;
                await using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    Stream stream = new MemoryStream(ms.ToArray());
                    if (formFile.FileName.Contains(".xlsx"))
                    {
                        workBook = new XSSFWorkbook(stream);
                        evaluator = new XSSFFormulaEvaluator(workBook);
                    }
                    else
                    {
                        workBook = new HSSFWorkbook(stream);
                        evaluator = new HSSFFormulaEvaluator(workBook);
                    }
                    //讀取工作表
                    sheet = (ISheet)workBook.GetSheet("BOM續頁");
                }

                Bom bom = await publicMethods.SetBomData(oppoId, sheet, evaluator);
                bom.OppoId = oppoId;

                //檢查總成件號是否重複
                Bom temp_Bom = await _bomService.GetBom(bom.AssemblyPartNumber);
                if (temp_Bom != null)
                    throw new Exception(string.Format("總成件號：{0} 已存在，請勿重複上傳。", bom.AssemblyPartNumber));

                await _bomService.CreateBom(bom);

                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateBomItem/{BomItemId}")]
        public async Task<ActionResult<BomItem>> UpdateBomItem(string bomItemId, BomItem bomItem)
        {
            try
            {
                if (bomItemId != bomItem.BomItemId)
                    return BadRequest(ModelState);

                BomItem bItem = await _bomService.GetBomItem(bomItemId);
                MeasuringItem mItem = await _bomService.GetMeasuringItem(bomItemId);
                FixtureItem fItme = await _bomService.GetFixtureItem(bomItemId);
                if (bomItem == null | mItem == null | fItme == null)
                    NotFound();
                
                mItem.PartNumber = bomItem.PartNumber;
                fItme.PartNumber = bomItem.PartNumber;

                await _bomService.UpdateBomItem(bomItemId, bomItem , mItem, fItme);

                return NoContent();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}