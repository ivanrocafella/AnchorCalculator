using Microsoft.AspNetCore.Hosting;
using Mysqlx.Crud;
using NLog;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace UI.AnchorCalculator.Extensions
{
    public class CostWork
    {
        private readonly LoggerManager _logger;
        public double Cutting { get; set; }
        public double Bending { get; set; }
        public double CuttingThread { get; set; }
        public double Plashka { get; set; }
        public double Cutter { get; set; }
        public double BandSawBlade { get; set; }
        public double Margin { get; set; }
        public double CostFull { get { return Cutting + Bending + CuttingThread + Plashka + CuttingThread + BandSawBlade; } }

        public CostWork(LoggerManager logger)
        {
            _logger = logger;
        }
        public async Task AddCostWork(CostWork costWork, IWebHostEnvironment appEnvironment)
        {
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed", "costwork.json");
            using FileStream fs = new(path, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync<CostWork>(fs, costWork);
        }

        public async Task<CostWork> GetCostWork(IWebHostEnvironment appEnvironment)
        {
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed", "costwork.json");
            try
            {
                CostWork? costWork = new(_logger);                
                using (FileStream fs = new(path, FileMode.OpenOrCreate))
                {
                    costWork = await JsonSerializer.DeserializeAsync<CostWork>(fs);
                }
                return costWork;
            }
            catch (Exception ex)
            {
                string exception = $"error:{ex.Message}, wrongPath: {path}";
                _logger.LogDebug(exception);
                throw;
            }
        }
    }
}
