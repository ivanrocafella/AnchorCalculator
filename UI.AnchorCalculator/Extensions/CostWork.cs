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
        public double ExchangeDollar { get; set; } // unity of measure = som / $
        public double PnrRollingThread { get; set; } // unity of measure = $
        public double PnrBendingAnchor { get; set; } // unity of measure = $
        public double PnrBandSaw { get; set; } // unity of measure = $
        public double LengthEffective { get; set; } // unity of measure = мм
        public double PriceBandSaw { get; set; } // unity of measure = $
        public double TimeSetThreadRolling { get; set; }  // unity of measure = ч
        public double TimeBend { get; set; } // unity of measure = ч
        public double TimeSetBend { get; set; } // unity of measure = ч
        public double Margin { get; set; } // unity of measure = %
        public double MarginFB { get; set; } // unity of measure = % margin for anchors from diameter 30mm and more
        public double AreaLockSmith { get; set; } // unity of measure = $
        public double PricePlashka { get; set; } // unity of measure = $ стоимость плашки
        public double PriceCutter { get; set; } // unity of measure = $ стоимость резца

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
