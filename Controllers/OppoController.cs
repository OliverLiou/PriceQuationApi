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
    public class OppoController :ControllerBase
    {
        private IOppoService _service;

        public OppoController(IOppoService service)
        {
             _service = service;
        }

        [HttpGet("GetOppos")]
        public async Task<ActionResult<List<OPPO>>> GetOppos()
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