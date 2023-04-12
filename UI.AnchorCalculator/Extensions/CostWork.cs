using Microsoft.AspNetCore.Hosting;
using Mysqlx.Crud;
using NLog;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using static System.Net.Mime.MediaTypeNames;

namespace UI.AnchorCalculator.Extensions
{
    public class CostWork
    {
        public double Cutting { get; set; }
        public double Bending { get; set; }
        public double CuttingThread { get; set; }
        public double Plashka { get; set; }
        public double Cutter { get; set; }
        public double BandSawBlade { get; set; }
        public double Margin { get; set; }
        public double CostFull { get { return Cutting + Bending + CuttingThread + Plashka + CuttingThread + BandSawBlade; } }

        public async Task AddCostWork(CostWork costWork, IWebHostEnvironment appEnvironment)
        {
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed", "costwork.json");
            string json = JsonSerializer.Serialize<CostWork>(costWork);
            using StreamWriter writer = new(path, false);
            await writer.WriteAsync(json);             
        }

        public async Task<CostWork> GetCostWork(IWebHostEnvironment appEnvironment)
        {
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed", "costwork.json");
            CostWork? costWork = new();                
            using (FileStream fs = new(path, FileMode.OpenOrCreate))
            {
                costWork = await JsonSerializer.DeserializeAsync<CostWork>(fs);
            }
            return costWork;
        }
    }
}
