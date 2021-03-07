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
        // Task<IEnumerable<Bom>> CreateBoms(List<Bom> Boms);
        // Task<int> SetDepartmentId(string category);
        Task<User> GetUser(int userId);
        Task<IEnumerable<Bom>> GetBomsAsync();
        Task<Bom> GetBomDetailsAsync(string assemblyPartNumber);
        Task<List<string>> GetPlmAssemblyPNs(string oppoId);

    }

    public class BomService : IBomService
    {
        private readonly PriceQuationContext _context;

        public BomService(PriceQuationContext context)
        {
            _context = context;
        }
        
        public async Task<User> GetUser(int userId)
        {
            try
            {
                var user = await _context.User.FindAsync(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<IEnumerable<Bom>> GetBomsAsync()
        {
            try
            {
                var Boms = await _context.Bom.ToListAsync();
                return Boms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Bom> GetBomDetailsAsync(string assemblyPartNumber)
        {
            try
            {
                var bom = await _context.Bom.Where(b => b.AssemblyPartNumber == assemblyPartNumber)
                                            .Include(b => b.BomItems)
                                            .Include(b => b.MeasuringItems)
                                            .Include(b => b.FixtureItems)
                                            .OrderBy(b => b.AssemblyPartNumber).FirstOrDefaultAsync();
                return bom;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetPlmAssemblyPNs(string OPPOId)
        {
            try
            {
                List<string> plmAssemblyPNs = new List<string>();
                await using (var oracleConnection = new OracleConnection())
                {
                    oracleConnection.ConnectionString = "Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.99.0.13)(PORT=1521))" +
                                                        "(CONNECT_DATA= (SERVER=dedicated)(SERVICE_NAME=HCMFDEV)));" +
                                                        "User Id = INTERFACE; Password = RFtgYHuj;";
                    oracleConnection.Open();
                    string sql = string.Format("Select * from PLM_QPP where OPPO='{0}'", OPPOId);
                    OracleCommand oracleCommand = new OracleCommand(sql, oracleConnection);
                    OracleDataReader reader = oracleCommand.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        //沒有資料
                        throw new Exception(string.Format("中間區查無總成件號:{0}，請聯絡PLM管理員。", OPPOId));
                    }
                    while (reader.Read())
                    {
                        string plmAssemblyPN = reader.GetValue(reader.GetOrdinal("HC_PRODUCT")).ToString();
                        plmAssemblyPNs.Add(plmAssemblyPN);
                    }
                    oracleConnection.Close();
                }
                return plmAssemblyPNs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // public async Task<IEnumerable<Bom>> CreateBoms(List<Bom> Boms)
        // {
        //     try
        //     {
        //         foreach (var bom in Boms)
        //         {
        //             _context.Bom.Add(bom);
        //         }
        //         await _context.SaveChangesAsync();
        //         return Boms;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

        // public async Task<int> SetDepartmentId(string category)
        // {
        //     try
        //     {
        //         var quoteItem = await _context.QuoteItem.Where(q => q.ResponsibleItem.Contains(category)).AsNoTracking().FirstOrDefaultAsync();
        //         if (quoteItem == null)
        //             throw new Exception("系統無法識別 " + category);

        //         return quoteItem.DepartemntId;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }

    }
}