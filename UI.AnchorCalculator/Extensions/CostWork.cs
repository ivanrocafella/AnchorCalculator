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
        public double CostFull { get { return Cutting + Bending + CuttingThread + Plashka + CuttingThread + BandSawBlade; } }

        public CostWork()
        {
            Cutting = 10;
            Bending = 2;
            CuttingThread = 94;
            Plashka = 20;
            Cutter = 27;
            BandSawBlade = 20;
        }
    }
}
