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
        Task<int> GetQuoteItem(string quoteItemId);
        Task<int> SetDepartmentId(string category);
        Task<IEnumerable<Bom>> GetBomsAsync();

        IEnumerable<MeasuringItem> CreateMeausringItems(List<BomItem> bomItems);
        IEnumerable<FixtureItem> CreateFixtureItems(List<BomItem> bomItems);

        Task<Bom> GetBomDetailsAsync(string assemblyPartNumber);
    }

    public class BomService : IBomService
    {
        private readonly PriceQuationContext _context;

        public BomService(PriceQuationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bom>> CreateBoms(List<Bom> Boms)
        {
            try
            {
                foreach (var bom in Boms)
                {
                    _context.Bom.Add(bom);
                }
                await _context.SaveChangesAsync();
                return Boms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUser(int Id)
        {
            try
            {
                var user = await _context.User.FindAsync(Id);
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

        public async Task<int> SetDepartmentId(string category)
        {
            try
            {
                var quoteItem = await _context.QuoteItem.Where(q => q.ResponsibleItem.Contains(category)).AsNoTracking().FirstOrDefaultAsync();
                if (quoteItem == null)
                    throw new Exception("系統無法識別 " + category);

                return quoteItem.DepartemntId;
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
    
        public IEnumerable<MeasuringItem> CreateMeausringItems(List<BomItem> bomItems)
        {
            try
            {
                List<MeasuringItem> measuringItems = new List<MeasuringItem>();
                foreach(var item in bomItems)
                {
                    MeasuringItem measuringItem = new MeasuringItem()
                    {
                        No = item.No,
                        AssemblyPartNumber = item.AssemblyPartNumber,
                        PartNumber = item.PartNumber
                    };
                    measuringItems.Add(measuringItem);
                }
                return measuringItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public IEnumerable<FixtureItem> CreateFixtureItems(List<BomItem> bomItems)
        {
            try
            {
                List<FixtureItem> fixtureItems = new List<FixtureItem>();
                foreach(var item in bomItems)
                {
                    FixtureItem fixtureItem = new FixtureItem()
                    {
                        No = item.No,
                        AssemblyPartNumber = item.AssemblyPartNumber,
                        PartNumber = item.PartNumber
                    };
                    fixtureItems.Add(fixtureItem);
                }
                return fixtureItems;
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
    }
}