using Mafi;
using Mafi.Base;
using Mafi.Core;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Base.Prototypes.Research;
using Mafi.Core.Entities.Static;
using Mafi.Core.Products;
using Mafi.Core.Research;
using Mafi.Core.UnlockingTree;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Core.Roads;

namespace BetterLife_Assemblies

{
    internal class ResearchDt : IResearchNodesData
    {
        public void RegisterData(ProtoRegistrator registrator)
        {



            ResearchNodeProto nodeProto = registrator.ResearchNodeProtoBuilder
                
                .Start("Alternative Recipes 1", BetterLIDs.Research.AlterRecipesT1a,4)
                .Description("Better Assembly with custom recipes...")
//                .AddIcon(Option<ResearchNodeProto>(BetterLIDs.Research.AlterRecipesT1a), "Assets/BetterLife/Icons/BL_Logo2.png")

                .AddMachineToUnlock(BetterLIDs.Machines.AssemblyBlt1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ConstructionParts1C)
                //.AddRecipeToUnlock(BetterLIDs.Recipes.Cement1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.MyBricks)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ScrapToIron)
                .AddRecipeToUnlock(BetterLIDs.Recipes.MechanicalParts1C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.Electronics1C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.RLab1)
                .AddRequirementForLifetimeProduction(Ids.Products.ConstructionParts, 10)

                .BuildAndAdd();

            //nodeProto.GridPosition = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CpPacking).GridPosition.AddX(4).AddY(8);   
            nodeProto.GridPosition = new Vector2i(0,-12);
            nodeProto.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.CpPacking));


            ResearchNodeProto nodeProto1b = registrator.ResearchNodeProtoBuilder
                .Start("Alternative Recipes 2", BetterLIDs.Research.AlterRecipesT1b, 6)
                .Description("New assembler with alternative recipes for a BetterLife!")

                .AddRecipeToUnlock(BetterLIDs.Recipes.EasyFertilizer1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.EasyFuel)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ConstructionParts2C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ConstructionParts3C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.VehiclePartsT1C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.VehiclePartsT2C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.Exhaust1)
                .AddRequiredProto(Ids.Research.ResearchLab2)
                .AddRecipeToUnlock(BetterLIDs.Recipes.easyRailParts)
                .AddRecipeToUnlock(BetterLIDs.Recipes.SlagToIronScrap)
                .AddRequirementForLifetimeProduction(Ids.Products.ConstructionParts2, 10)

                .BuildAndAdd();


            //nodeProto1b.GridPosition = registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(Ids.Research.Cp2Packing).GridPosition.AddX(4).AddY(-8);
            nodeProto1b.GridPosition = nodeProto.GridPosition.AddX(4);
            nodeProto1b.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(BetterLIDs.Research.AlterRecipesT1a));

            ResearchNodeProto nodeProto2 = registrator.ResearchNodeProtoBuilder
                .Start("Alternative Recipes 3", BetterLIDs.Research.AlterRecipesT2, 8)
                .Description("Alternative recipes for your pleasure II. Unlock with Research Lab")
                .AddRecipeToUnlock(BetterLIDs.Recipes.EasyFertilizer2)
                //.AddRecipeToUnlock(BetterLIDs.Recipes.ConstructionParts3C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ResearchLab2C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.Naphtha1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.SulfurBurn)
                .AddRequiredProto(Ids.Research.ResearchLab3)
                .AddMachineToUnlock(BetterLIDs.Machines.AssemblyBlt2)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ScrapToCopper)
                .AddRecipeToUnlock(BetterLIDs.Recipes.PCB1)
                .AddRequirementForLifetimeProduction(Ids.Products.ConstructionParts3, 10)
                 
                .BuildAndAdd();

            nodeProto2.GridPosition = nodeProto1b.GridPosition.AddX(4);
            nodeProto2.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(BetterLIDs.Research.AlterRecipesT1b));

            ResearchNodeProto nodeProto3 = registrator.ResearchNodeProtoBuilder
                .Start("Alternative Recipes 4", BetterLIDs.Research.AlterRecipesT3, 10)
                .Description("Alternative recipes for your pleasure part III... Construction III")
                .AddRecipeToUnlock(BetterLIDs.Recipes.FuelGas1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.Amonia1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.SulfuricAcid1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.SourWater1)
                .AddRecipeToUnlock(BetterLIDs.Recipes.EasyFertilizerT3C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.EasySulfur)
                .AddRecipeToUnlock(BetterLIDs.Recipes.HouseHoldGoods1)




                .AddRequiredProto(Ids.Research.ResearchLab4)
                .AddRequirementForLifetimeProduction(Ids.Products.ConstructionParts4, 10)
                .BuildAndAdd();
              
            nodeProto3.GridPosition = nodeProto2.GridPosition.AddX(4);
            nodeProto3.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(BetterLIDs.Research.AlterRecipesT2));

            ResearchNodeProto nodeProto4 = registrator.ResearchNodeProtoBuilder
                .Start("Alternative Recipes 5", BetterLIDs.Research.AlterRecipesT4, 12)
                .Description("The fun continues..")

                .AddRecipeToUnlock(BetterLIDs.Recipes.ResearchLab3C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ConstructionParts4C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.VehiclePartsT3C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.ResearchLab4C)
                .AddRecipeToUnlock(BetterLIDs.Recipes.Ethanol1)
                .AddRequiredProto(Ids.Research.ResearchLab5)
                .AddRequirementForLifetimeProduction(Ids.Products.ConstructionParts4, 25)
                .BuildAndAdd();

            nodeProto4.GridPosition = nodeProto3.GridPosition.AddX(4);
            nodeProto4.AddParent(registrator.PrototypesDb.GetOrThrow<ResearchNodeProto>(BetterLIDs.Research.AlterRecipesT3));

        }
    }
}
