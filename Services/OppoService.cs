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
        Task<Oppo> CreateOppo(Oppo oppo);
        Task<PlmMiddle> GetMiddleData(string oppoId, string assemblyPartNumber);
        Task<int> GetQuoteItem(string quoteItemId);
        Task<List<Oppo>> GetOppos();
        Task<Oppo> GetOppo(string oppoId);
    }

    public class OppoService : IOppoService
    {
        private readonly PriceQuationContext _context;
        public OppoService(PriceQuationContext context)
        {
            _context = context;
        }
        
        public async Task<Oppo> CreateOppo(Oppo oppo)
        {
            try
            {
                _context.Oppo.Add(oppo);
                await _context.SaveChangesAsync();
                return oppo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PlmMiddle> GetMiddleData(string oppoId, string assemblyPartNumber)
        {
            PlmMiddle plmMiddle = new PlmMiddle();
            try
            {
                await using (var oracleConnection = new OracleConnection())
                {
                    oracleConnection.ConnectionString = "Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.99.0.13)(PORT=1521))" +
                                                        "(CONNECT_DATA= (SERVER=dedicated)(SERVICE_NAME=HCMFDEV)));" +
                                                        "User Id = INTERFACE; Password = RFtgYHuj;";
                    oracleConnection.Open();
                    string sql = string.Format("Select * from PLM_QPP where OPPO='{0}' And HC_Product='{1}'", oppoId, assemblyPartNumber);
                    OracleCommand oracleCommand = new OracleCommand(sql, oracleConnection);
                    OracleDataReader reader = oracleCommand.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        //沒有資料
                        plmMiddle = null;
                    }
                    while (reader.Read())
                    {
                        plmMiddle.OPPO = reader.GetValue(reader.GetOrdinal("OPPO")).ToString();
                        plmMiddle.HC_PRODUCT = reader.GetValue(reader.GetOrdinal("HC_PRODUCT")).ToString();
                        plmMiddle.QUOTER = reader.GetValue(reader.GetOrdinal("QUOTER")).ToString();
                        plmMiddle.QUOTE_TIME = reader.GetValue(reader.GetOrdinal("QUOTE_TIME")).ToString();
                        plmMiddle.DESIGNER = reader.GetValue(reader.GetOrdinal("DESIGNER")).ToString();
                        plmMiddle.SALES_OWNER = reader.GetValue(reader.GetOrdinal("SALES_OWNER")).ToString();
                        plmMiddle.MODIFYDATE = reader.GetValue(reader.GetOrdinal("MODIFYDATE")).ToString();
                        plmMiddle.QUOTER_FINISHED = reader.GetValue(reader.GetOrdinal("QUOTER_FINISHED")).ToString();
                        plmMiddle.FINISHED_TIME = reader.GetValue(reader.GetOrdinal("FINISHED_TIME")).ToString();
                        plmMiddle.PQP_FINISHED = reader.GetValue(reader.GetOrdinal("PQP_FINISHED")).ToString();
                        plmMiddle.DESCRIPTION = reader.GetValue(reader.GetOrdinal("DESCRIPTION")).ToString();
                        plmMiddle.ME_OWNER = reader.GetValue(reader.GetOrdinal("ME_OWNER")).ToString();
                    }
                    oracleConnection.Close();
                }
                return plmMiddle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetQuoteItem(string quoteItemId)
        {
            try
            {
                int Id = Convert.ToInt16(quoteItemId);
                var quoteItem = await _context.QuoteItem.Where(q => q.QuoteItemId == Id).FirstOrDefaultAsync();
                // var quoteItem = quoteItems.Find(q => q.QuoteItemId == Id);

                if (quoteItem == null)
                    throw new Exception(string.Format("quoteItemId = {0} 不存在，請聯絡PLM管理者！", quoteItemId));
                return quoteItem.QuoteItemId;
            }
            catch (FormatException format_ex)
            {
                throw new Exception(string.Format(format_ex.Message + Environment.NewLine +
                 "系統無法識別中間區欄位，報價單位名稱為「{0}」，請聯絡PLM工程師", quoteItemId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Oppo>> GetOppos()
        {
            try
            {
                var oppos = await _context.Oppo.Include( o => o.Boms).ToListAsync();
                return oppos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Oppo> GetOppo(string oppoId)
        {
            try
            {
                var oppo = await _context.Oppo.FindAsync(oppoId);
                return oppo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}