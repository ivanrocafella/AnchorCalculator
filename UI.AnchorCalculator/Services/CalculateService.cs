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
        private readonly IWebHostEnvironment appEnvironment;

        public CalculateService(IWebHostEnvironment appEnvironment, LoggerManager logger)
        {
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

            anchor.BilletLength = GetLengthBillet(anchor);
            anchor.LengthFull = anchor.BilletLength * anchor.Quantity / 1000; // length of material of anchor's batch in metres
            double BilletWeight = anchor.BilletLength * (Math.PI * Math.Pow(anchor.Diameter, 2) / 4) / Math.Pow(10, 9) * dencitySteel; // weight of anchor's billet
            anchor.Weight = Math.Round(BilletWeight - ((Math.PI * Math.Pow(anchor.Diameter, 2) / 4) - (Math.PI * Math.Pow(anchor.ThreadDiameter, 2) / 4)) * anchor.ThreadLength / Math.Pow(10, 9) * dencitySteel, 2); // weight of anchor
            if (anchor.Kind == Kind.BendDouble)
                anchor.Weight = Math.Round(BilletWeight - 2 * ((((Math.PI * Math.Pow(anchor.Diameter, 2) / 4) - (Math.PI * Math.Pow(anchor.ThreadDiameter, 2) / 4)) * anchor.ThreadLength) / Math.Pow(10, 9)) * dencitySteel, 2);

            anchor.BatchWeight =  Math.Round(anchor.Weight * anchor.Quantity, 2); // weight of anchor's batch  

            int quantityBend = anchor.Kind switch
            {
                Kind.Bend => 1,
                Kind.BendDouble => 2,
                _ => 0,
            };

            double priceBend = costWork.TimeBend * costWork.PnrBendingAnchor * quantityBend; // price of bending in $
            double priceThreadRolling = 0;
            double priceThreadCutting = 0;
            double timeProductionThread = 0;
            double timeProductionBend = 0;
            double timeProductionBandSaw = 0;
            double timeProduction = 0;

            timeProduction += costWork.TimeBend * quantityBend;
            timeProductionBend += costWork.TimeBend * quantityBend;

            if (anchor.ThreadLength > 0)
            {
                if (anchor.Kind == Kind.BendDouble)
                {
                    priceThreadRolling = anchor.Material.TimeThreadRolling * (2 * anchor.ThreadLength / costWork.LengthEffective) * costWork.PnrRollingThread; // price of threadrolling in $
                    priceThreadCutting = anchor.Material.TimeThreadCutting * (2 * anchor.ThreadLength / costWork.LengthEffective) * costWork.AreaLockSmith
                             + anchor.Material.Cutter * costWork.PriceCutter + anchor.Material.Plashka * costWork.PricePlashka; // price of threadcutting in $ 
                    if (anchor.Production == 0)
                    {
                        timeProduction += anchor.Material.TimeThreadRolling * (2 * anchor.ThreadLength / costWork.LengthEffective);
                        timeProduction += costWork.TimeSetThreadRolling / anchor.Quantity;
                        timeProductionThread += anchor.Material.TimeThreadRolling * (2 * anchor.ThreadLength / costWork.LengthEffective);
                        timeProductionThread += costWork.TimeSetThreadRolling / anchor.Quantity;
                    }
                    else
                    { 
                        timeProduction += anchor.Material.TimeThreadCutting * (2 * anchor.ThreadLength / costWork.LengthEffective);
                        timeProductionThread += anchor.Material.TimeThreadCutting * (2 * anchor.ThreadLength / costWork.LengthEffective);
                    }
                }
                else
                {
                    priceThreadRolling = anchor.Material.TimeThreadRolling * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective) * costWork.PnrRollingThread; // price of threadrolling in $ 
                    priceThreadCutting = anchor.Material.TimeThreadCutting * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective) * costWork.AreaLockSmith
                        + anchor.Material.Cutter * costWork.PriceCutter + anchor.Material.Plashka * costWork.PricePlashka; // price of threadcutting in $ 
                    if (anchor.Production == 0)
                    {
                        timeProduction += anchor.Material.TimeThreadRolling * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective);
                        timeProduction += costWork.TimeSetThreadRolling / anchor.Quantity;
                        timeProductionThread += anchor.Material.TimeThreadRolling * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective);
                        timeProductionThread += costWork.TimeSetThreadRolling / anchor.Quantity;
                    }
                    else
                    { 
                        timeProduction += anchor.Material.TimeThreadCutting * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective);
                        timeProductionThread += anchor.Material.TimeThreadCutting * ((anchor.ThreadLength + anchor.ThreadLengthSecond) / costWork.LengthEffective);
                    }
                }                
            }

            double priceBandSaw = anchor.Material.TimeBandSaw * costWork.PnrBandSaw + anchor.Material.LengthBladeBandSaw * costWork.PriceBandSaw; // price of band saw in $
            double priceMaterialAnchor = ((anchor.BilletLength / 1000) * anchor.Material.PricePerMetr) / costWork.ExchangeDollar; // price of anchor's material in $

            timeProduction += anchor.Material.TimeBandSaw;
            timeProductionBandSaw += anchor.Material.TimeBandSaw;

            double setBend = 0;
            if (anchor.Kind != Kind.Straight)
            { 
                setBend = costWork.TimeSetBend * costWork.PnrBendingAnchor;
                timeProduction += costWork.TimeSetBend;
                timeProductionBend += costWork.TimeSetBend;
            }

            double costWorkInterm;
            if (anchor.Production == 0)
                costWorkInterm = (priceBend + priceThreadRolling + priceBandSaw) * anchor.Quantity
                     + costWork.TimeSetThreadRolling * costWork.PnrRollingThread + setBend;
            else 
                costWorkInterm = (priceBend + priceThreadCutting + priceBandSaw) * anchor.Quantity + setBend;

            anchor.BatchSebes = (costWorkInterm + priceMaterialAnchor * anchor.Quantity) * costWork.ExchangeDollar; // sebes of anchor in som
            anchor.Sebes = anchor.BatchSebes / anchor.Quantity;            
            anchor.Amount = (costWorkInterm * (1 + costWork.Margin) + priceMaterialAnchor * anchor.Quantity) * costWork.ExchangeDollar;
            if (anchor.Quantity < 50)
                anchor.Amount *= 2;
            anchor.Price = anchor.Amount / anchor.Quantity;
            anchor.PriceMaterial = priceMaterialAnchor * costWork.ExchangeDollar; // price of anchor's material in som
            anchor.BatchPriceMaterial = anchor.PriceMaterial * anchor.Quantity; // price of anchor's material batch in som
            anchor.TimeProductionThread = timeProductionThread * anchor.Quantity; // time of anchor's thread production in hours
            anchor.TimeProductionBend = timeProductionBend * anchor.Quantity; // time of anchor's bend production in hours
            anchor.TimeProductionBandSaw = timeProductionBandSaw * anchor.Quantity; // time of anchor's bandSaw production in hours
        }

        static double GetLengthBillet(Anchor anchor)
        {
            double lengthBillet;
            double kFactor = 1 / (Math.Log(1 + (double)anchor.Diameter / anchor.BendRadius)) - anchor.BendRadius / anchor.Diameter; // K-factor
            anchor.LengthPathRoller = Math.PI * anchor.BendRadius * 1 / 2;      
            
            if (anchor.Kind != Kind.Straight)
                anchor.LengthBeforeEndPathRoller = anchor.Length - anchor.BendRadius + anchor.LengthPathRoller;
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
