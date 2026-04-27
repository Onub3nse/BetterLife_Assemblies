using Mafi;
using Mafi.Core.Mods;
using Mafi.Base;


namespace BetterLife_Assemblies
{
    internal class RecipesDef : IModData
    {

        
        public void RegisterData(ProtoRegistrator registrator)

        {

            //registrator.RecipeProtoBuilder
            //    .Start("Easy Bricks", BetterLIDs.Recipes.EasyBricks, BetterLIDs.Machines.AssemblyBlt1)
            //    .AddInput(5, Ids.Products.Dirt, "*")
            //    .AddInput(5, Ids.Products.Water, "HF")
            //    .SetDuration(5.Seconds())
            //   .AddOutput(10, Ids.Products.Bricks, "WX")
            //   .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Easy Rail Parts", BetterLIDs.Recipes.easyRailParts, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.ConcreteSlab, "*")
                .AddInput(5, Ids.Products.Iron, "*")
                .SetDuration(10.Seconds())
                .AddOutput(10, Ids.Products.RailParts, "WX")
                .BuildAndAdd();


            registrator.RecipeProtoBuilder
                .Start("Scrap to Iron", BetterLIDs.Recipes.ScrapToIron, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(50, Ids.Products.IronScrap, "*")
                .SetDuration(5.Seconds())
                .AddOutput(15, Ids.Products.Iron, "WX")
                .BuildAndAdd();



            registrator.RecipeProtoBuilder
                .Start("Research Lab 1", BetterLIDs.Recipes.RLab1, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Electronics, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.LabEquipment, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("SulfurBurn", BetterLIDs.Recipes.SulfurBurn, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(50, Ids.Products.Sulfur, "*")
                .SetDuration(5.Seconds())
                .AddOutput(100, Ids.Products.SteamHi, "YZ")
                .AddOutput(10, Ids.Products.Exhaust, "E")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Easy Fuel", BetterLIDs.Recipes.EasyFuel, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.CrudeOil, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Diesel, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("My Bricks", BetterLIDs.Recipes.MyBricks, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Dirt, "*")
                .AddInput(5, Ids.Products.Cement, "*")
                .AddInput(5, Ids.Products.Water, "HF")
                .SetDuration(5.Seconds())
               .AddOutput(10, Ids.Products.ConcreteSlab, "WX")
               .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Fuel Gas 1", BetterLIDs.Recipes.FuelGas1, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Dirt, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.FuelGas, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Electronics T1", BetterLIDs.Recipes.Electronics1C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Iron, "*")
                .AddInput(5, Ids.Products.Copper, "*")
                .SetDuration(Duration.FromSec(10))
                .AddOutput(10, Ids.Products.Electronics, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Mechanical Parts 1", BetterLIDs.Recipes.MechanicalParts1C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Iron, "*")
                .SetDurationSeconds(5)
                .AddOutput(10, Ids.Products.MechanicalParts, "WX")
                .BuildAndAdd();


            registrator.RecipeProtoBuilder.Start("Vehicle Parts T1C", BetterLIDs.Recipes.VehiclePartsT1C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Iron, "*")
                .AddInput(5, Ids.Products.MechanicalParts, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.VehicleParts, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Construction Parts 3C", BetterLIDs.Recipes.ConstructionParts3C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.ConstructionParts2, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.ConstructionParts3, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Construction Part 2C", BetterLIDs.Recipes.ConstructionParts2C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.ConstructionParts, "*")
                .AddInput(5, Ids.Products.MechanicalParts, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.ConstructionParts2, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Construction Parts 1C", BetterLIDs.Recipes.ConstructionParts1C, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Iron, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.ConstructionParts, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Easy Fertilizer T2", BetterLIDs.Recipes.EasyFertilizer2, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Dirt, "*")
                .AddInput(5, Ids.Products.FertilizerOrganic, "HF")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.FertilizerChemical, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Easy Fertilizer", BetterLIDs.Recipes.EasyFertilizer1, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Dirt, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.FertilizerOrganic, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Naphtha1", BetterLIDs.Recipes.Naphtha1, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.LightOil, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Naphtha, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Sulfuric Acid 1", BetterLIDs.Recipes.SulfuricAcid1, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.Sulfur, "*")
                .AddInput(5, Ids.Products.Water, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Acid, "YZ")
                .BuildAndAdd();


            registrator.RecipeProtoBuilder.Start("Amonia 1", BetterLIDs.Recipes.Amonia1, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.Water, "*")
                .AddInput(5, Ids.Products.Sulfur, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Ammonia, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Research Lab 2C", BetterLIDs.Recipes.ResearchLab2C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.LabEquipment, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.LabEquipment2, "WX")
                .BuildAndAdd();


            registrator.RecipeProtoBuilder.Start("Vehicle Parts T2C", BetterLIDs.Recipes.VehiclePartsT2C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.VehicleParts, "*")
                .AddInput(5, Ids.Products.Steel, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.VehicleParts2, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Vehicle Parts T3C", BetterLIDs.Recipes.VehiclePartsT3C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.VehicleParts2, "*")
                .AddInput(5, Ids.Products.Steel, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.VehicleParts3, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder.Start("Construction Parts 4C", BetterLIDs.Recipes.ConstructionParts4C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.ConstructionParts3, "*")
                .SetDuration(Duration.FromSec(5))
                .AddOutput(10, Ids.Products.ConstructionParts4, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Research Lab 3", BetterLIDs.Recipes.ResearchLab3C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.LabEquipment2, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.LabEquipment3, "WX")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Easy Sulfur", BetterLIDs.Recipes.EasySulfur, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(50, Ids.Products.Coal)
                .AddInput(50, Ids.Products.Brine)
                .SetDurationSeconds(5)
                .AddOutput(20, Ids.Products.Sulfur, "*")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Easy Fertilizer 3", BetterLIDs.Recipes.EasyFertilizerT3C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.FertilizerOrganic, "*")
                .AddInput(5, Ids.Products.FertilizerChemical, "*")
                .SetDuration(5.Seconds())
               .AddOutput(10, Ids.Products.FertilizerChemical2, "YZ")
               .BuildAndAdd();

            //registrator.RecipeProtoBuilder
            //    .Start("TAR", BetterLIDs.Recipes.pTar, BetterLIDs.Machines.AssemblyBlt2)
            //    .AddInput(8, Ids.Products.CrudeOil, "*")
            //    .AddInput(50, Ids.Products.Coal, "*")
            //    .SetDurationSeconds(5)
            //    .AddOutput(10, BetterLIDs.Products.pTar)
            //    .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Useful Sour Water", BetterLIDs.Recipes.SourWater1, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.SourWater, "*")
                .SetDuration(5.Seconds())
                .AddOutput(20, Ids.Products.Water, "YZ")
                .BuildAndAdd();

            registrator.RecipeProtoBuilder
                .Start("Research Lab 4", BetterLIDs.Recipes.ResearchLab4C, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.LabEquipment3, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.LabEquipment4, "WX")
                .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Easy Cement", BetterLIDs.Recipes.Cement1, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Limestone, "*")
                .AddInput(5, Ids.Products.Rock, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Cement, "WX")
                .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Easy Cement 2", BetterLIDs.Recipes.EasyCement2, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(5, Ids.Products.Limestone, "*")
                .AddInput(5, Ids.Products.Slag, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Cement, "WX")
                .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Better Ethanol", BetterLIDs.Recipes.Ethanol1, BetterLIDs.Machines.AssemblyBlt2)
                .AddInput(5, Ids.Products.CarbonDioxide, "*")
                .AddInput(5, Ids.Products.CornMash, "*")
                .SetDuration(5.Seconds())
                .AddOutput(10, Ids.Products.Ethanol, "YZ")
                .BuildAndAdd();
            registrator.RecipeProtoBuilder
                .Start("Exaust Recycling 1", BetterLIDs.Recipes.Exhaust1, BetterLIDs.Machines.AssemblyBlt1)
                .AddInput(50, Ids.Products.Exhaust, "*")
                .SetDuration(2.Seconds())
                .AddOutput(10, Ids.Products.Coal, "*")
                .BuildAndAdd();


        }

    }
}
