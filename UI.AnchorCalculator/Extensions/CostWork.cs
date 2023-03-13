using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

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

        public CostWork()
        {

          //  GetCostWork();

          // Cutting = 10;
          // Bending = 2;
          // CuttingThread = 94;
          // Plashka = 20;
          // Cutter = 27;
          // BandSawBlade = 20;
          // Margin = 0.5;
        }

      //  private void GetCostWork()
      //  { 
      //      CostWork costWork = new CostWork();
      //  }
        public async void AddCostWork(CostWork costWork, IWebHostEnvironment appEnvironment)
        {
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed\\costwork.json");
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                await JsonSerializer.SerializeAsync<CostWork>(fs, costWork);
        }

        public async Task<CostWork> GetCostWork(IWebHostEnvironment appEnvironment)
        {
            CostWork? costWork = new CostWork();
            string path = Path.Combine(appEnvironment.WebRootPath, "jsonsDataSeed\\costwork.json");
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                costWork = await JsonSerializer.DeserializeAsync<CostWork>(fs);
            }     
            return costWork;
        }
    }
}
