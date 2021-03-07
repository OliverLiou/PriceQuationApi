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

        [HttpPut("UpdateBomDetail/{BomItemId}")]
        public async Task<ActionResult<BomItem>> UpdateBomDetail(string BomItemId)
        {
            try
            {
                return NoContent();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}