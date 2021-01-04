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
    public interface IBomService
    {
        Task<IEnumerable<Bom>> CreateBoms(List<Bom> Boms);
        Task<User> GetUser(int Id);

        Task<PlmMiddle> GetMiddleData(string assembylyPartNumber);
        Task<int> GetQuoteItem(int quoteItemId);
        Task<int> SetDepartmentId(string category);
    }

    public class BomService: IBomService
    {
        private readonly PriceQuationContext _context;

        public BomService (PriceQuationContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Bom>> CreateBoms (List<Bom> Boms)
        {
            try
            {
                foreach(var bom in Boms)
                {
                    _context.Boms.Add(bom);
                }
                await _context.SaveChangesAsync();
                return Boms;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    
        public async Task<User> GetUser (int Id)
        {
            try
            {
                var user = await _context.Users.FindAsync(Id);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public async Task<PlmMiddle> GetMiddleData(string assemblyPartNumber)
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
                    string sql = string.Format("Select * from PLM_QPP where HC_Product='{0}'", assemblyPartNumber);
                    OracleCommand oracleCommand = new OracleCommand(sql, oracleConnection);
                    OracleDataReader reader = oracleCommand.ExecuteReader();
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
   
        public async Task<int> GetQuoteItem(int quoteItemId)
        {
            try
            {
                var quoteItem = await _context.QuoteItems.Where(q => q.QuoteItemId == quoteItemId).FirstOrDefaultAsync();
                if(quoteItem == null)
                    throw new Exception(string.Format("quoteItemId = {0} 不存在，請聯絡PLM管理者！", quoteItemId));
                return quoteItem.QuoteItemId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SetDepartmentId(string category)
        {
            try
            {
                var quoteItem = await _context.QuoteItems.Where(q => q.ResponsibleItem.Contains(category)).AsNoTracking().FirstOrDefaultAsync();
                if(quoteItem == null)
                    throw new Exception("系統無法識別 "+ category);

                 return quoteItem.DepartemntId;   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}