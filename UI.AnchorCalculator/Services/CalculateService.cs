using Core.AnchorCalculator.Entities;
using Core.AnchorCalculator.Entities.Enums;
using DAL.AnchorCalculator;
using UI.AnchorCalculator.Extensions;

namespace UI.AnchorCalculator.Services
{
    public class CalculateService
    {
        private readonly LoggerManager _logger;
        const double dencitySteel = 7850; // dencity of steel
        private readonly MaterialService MService;
        private readonly IWebHostEnvironment appEnvironment;

        public CalculateService(MaterialService mService, IWebHostEnvironment appEnvironment, LoggerManager logger)
        {
            MService = mService;
            this.appEnvironment = appEnvironment;
            _logger = logger;
        }

        public async Task Calculate(Anchor anchor)
        {
            CostWork costWork = new();
            try
            {
                costWork = await costWork.GetCostWork(appEnvironment);
            }
            catch (Exception ex)
            {
                string exception = $"Error:{ex.Message}";
                _logger.LogDebug(exception);
                throw;
            }

            anchor.BilletLength = Math.Round(GetLengthBillet(anchor), 2);
            double BilletWeight = ((anchor.BilletLength * (Math.PI * Math.Pow(anchor.Diameter, 2) / 4)) / Math.Pow(10, 9)) * dencitySteel; // weight of anchor's billet

            anchor.Weight = Math.Round(BilletWeight - ((((Math.PI * Math.Pow(anchor.Diameter, 2) / 4) - (Math.PI * Math.Pow(anchor.ThreadDiameter, 2) / 4)) * anchor.ThreadLength) / Math.Pow(10, 9)) * dencitySteel, 2); // weight of anchor
            if (anchor.Kind == Kind.BendDouble)
                anchor.Weight = Math.Round(BilletWeight - 2 * ((((Math.PI * Math.Pow(anchor.Diameter, 2) / 4) - (Math.PI * Math.Pow(anchor.ThreadDiameter, 2) / 4)) * anchor.ThreadLength) / Math.Pow(10, 9)) * dencitySteel, 2);

            anchor.BatchWeight =  Math.Round(anchor.Weight * anchor.Quantity, 2); // weight of anchor's batch  

            int quantityBend = anchor.Kind switch
            {
                Kind.Bend => 1,
                Kind.BendDouble => 2,
                _ => 0,
            };

            double priceBend = costWork.TimeBend * costWork.AreaWelding * quantityBend; // price of bending in $
            double priceThreadRolling;
            if (anchor.Kind == Kind.BendDouble)
                priceThreadRolling = anchor.Material.TimeTheradRolling * (2 * anchor.ThreadLength / costWork.LengthEffective) * costWork.AreaWelding; // price of threadrolling in $
            else
                priceThreadRolling = anchor.Material.TimeTheradRolling * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective) * costWork.AreaWelding; // price of threadrolling in $ 
            
            double priceBandSaw = anchor.Material.TimeBandSaw * costWork.AreaWelding + anchor.Material.LengthBladeBandSaw * costWork.PriceBandSaw; // price of band saw in $
            double priceMaterialAnchor = ((anchor.BilletLength / 1000) * anchor.Material.PricePerMetr) / costWork.ExchangeDollar; // price of anchor's material in $

            double setBend = 0;
            if (anchor.Kind != Kind.Straight)
                setBend = costWork.TimeSetBend * costWork.AreaWelding;

            double costWorkInterm = ((priceBend + priceThreadRolling + priceBandSaw) * anchor.Quantity
                + costWork.TimeSetTheradRolling * costWork.AreaWelding + setBend);
            anchor.BatchSebes = (costWorkInterm + priceMaterialAnchor * anchor.Quantity) * costWork.ExchangeDollar; // sebes of anchor in som
            anchor.Sebes = anchor.BatchSebes / anchor.Quantity;
            anchor.Amount = (costWorkInterm * (1 + costWork.Margin) + priceMaterialAnchor * anchor.Quantity) * costWork.ExchangeDollar;
            anchor.Price = anchor.Amount / anchor.Quantity;
            anchor.PriceMaterial = priceMaterialAnchor * costWork.ExchangeDollar; // price of anchor's material in som
            anchor.BatchPriceMaterial = anchor.PriceMaterial * anchor.Quantity; // price of anchor's material batch in som
        }

        static double GetLengthBillet(Anchor anchor)
        {
            double lengthBillet;
            double kFactor = 1 / (Math.Log(1 + (double)anchor.Diameter / anchor.BendRadius)) - anchor.BendRadius / anchor.Diameter; // K-factor
            anchor.LengthPathRoller = Math.PI * anchor.BendRadius * 1 / 2;      
            
            if (anchor.Kind != Kind.Straight)
                anchor.LengthBeforeEndPathRoller = anchor.Length - anchor.BendRadius - anchor.Diameter
                                                 + (Math.PI * (anchor.BendRadius + kFactor * anchor.Diameter) * 1 / 2);
            else
                anchor.LengthBeforeEndPathRoller = 0;

            if (anchor.Kind == Kind.Bend)
            {               
                lengthBillet = anchor.Length - anchor.BendRadius - anchor.Diameter
                    + ((Math.PI * (anchor.BendRadius + kFactor * anchor.Diameter) * 1 / 2)
                    + (anchor.BendLength - (anchor.Diameter + anchor.BendRadius)));
            }
            else if (anchor.Kind == Kind.BendDouble)
            {
                lengthBillet = 2 * (anchor.Length - anchor.BendRadius - anchor.Diameter
                    + (Math.PI * (anchor.BendRadius + kFactor * anchor.Diameter) * 1 / 2))
                    + anchor.BendLength - 2 * (anchor.Diameter + anchor.BendRadius);
            }
            else
                lengthBillet = anchor.Length;    
            return lengthBillet;
        }
    }
}
