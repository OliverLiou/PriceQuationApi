using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using PriceQuationApi.Model;
using PriceQuationApi.Plm;
using Oracle.ManagedDataAccess.Client;

namespace PriceQuationApi.Services
{
    public interface IOppoService
    {
        Task<List<OPPO>> GetOppos();
    }

    public class OppoService : IOppoService
    {
        private readonly PriceQuationContext _context;
        public OppoService(PriceQuationContext context)
        {
            _context = context;
        }
    
        public async Task<List<OPPO>> GetOppos()
        {
            try
            {
                var oppos = await _context.OPPO.Include( o => o.Boms).ToListAsync();
                return oppos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}