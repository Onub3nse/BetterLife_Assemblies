
using RecipeID = Mafi.Core.Factory.Recipes.RecipeProto.ID;
using ProductID = Mafi.Core.Products.ProductProto.ID;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Serialization;
using Mafi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;
using Mafi.Unity;
using Mafi.Core.Entities;
using Mafi.Core;
using Mafi.Core.Entities.Static;

namespace BetterLife_Assemblies;
[GlobalDependency(RegistrationMode.AsEverything, false, false)]

public partial class BetterLIDs
{

    public partial class Recipes

    {
        public static readonly RecipeID easyRailParts = Ids.Recipes.CreateId("easyRailParts");
        public static readonly RecipeID EasyCement = Ids.Recipes.CreateId("EasyCement");
        public static readonly RecipeID EasyCement2 = Ids.Recipes.CreateId("EasyCement2");
        public static readonly RecipeID SulfurBurn = Ids.Recipes.CreateId("SulfurBurn");
        public static readonly RecipeID ConstructionParts3C = Ids.Recipes.CreateId("ConstructionParts3C");
        public static readonly RecipeID ConstructionParts2C = Ids.Recipes.CreateId("ConstructionParts2C");
        public static readonly RecipeID ConstructionParts1C = Ids.Recipes.CreateId("ConstructionParts1C");
        public static readonly RecipeID EasyFertilizer1 = Ids.Recipes.CreateId("EasyFertilizer1");
        public static readonly RecipeID EasyFertilizer2 = Ids.Recipes.CreateId("EasyFertilizer2");
        public static readonly RecipeID VehiclePartsT1C = Ids.Recipes.CreateId("VehicleParts1C");
        public static readonly RecipeID VehiclePartsT2C = Ids.Recipes.CreateId("VehicleParts2C");
        public static readonly RecipeID MechanicalParts1C = Ids.Recipes.CreateId("MechanicalParts1C");
        public static readonly RecipeID Electronics1C = Ids.Recipes.CreateId("Electronics1C");
        public static readonly RecipeID HighPressTurbine1 = Ids.Recipes.CreateId("HighPressTurbine1");
        public static readonly RecipeID ResearchLab1C = Ids.Recipes.CreateId("ResearchLab1C");
        public static readonly RecipeID ResearchLab2C = Ids.Recipes.CreateId("ResearchLab2C");
        public static readonly RecipeID FuelGas1 = Ids.Recipes.CreateId("FuelGas1");
        public static readonly RecipeID Amonia1 = Ids.Recipes.CreateId("Amonia1C");
        public static readonly RecipeID MyBricks = Ids.Recipes.CreateId("MyBricks");
        public static readonly RecipeID SulfuricAcid1 = Ids.Recipes.CreateId("SulfuricAcid1");
        public static readonly RecipeID Naphtha1 = Ids.Recipes.CreateId("Naphtha1");
        public static readonly RecipeID Dummy = Ids.Recipes.CreateId("Dummy");
        public static readonly RecipeID EasyFuel = Ids.Recipes.CreateId("EasyFuel");
        public static readonly RecipeID RLab1 = Ids.Recipes.CreateId("rLab1");
        public static readonly RecipeID ScrapToIron = Ids.Recipes.CreateId("scrapIron");
        public static readonly RecipeID SourWater1 = Ids.Recipes.CreateId("Sourwater1");
        public static readonly RecipeID Maintenance1 = Ids.Recipes.CreateId("Maintenance1");
        public static readonly RecipeID Energy600mw = Ids.Recipes.CreateId("Energy600");
        
        //public static readonly RecipeID pTar = Ids.Recipes.CreateId("pTar");
        public static readonly RecipeID EasyBricks = Ids.Recipes.CreateId("EasyBricks");
        public static readonly RecipeID EasyFertilizerT3C = Ids.Recipes.CreateId("EasyFertilizer3");
        public static readonly RecipeID EasySulfur = Ids.Recipes.CreateId("EasySulfur");
        public static readonly RecipeID ConstructionParts4C = Ids.Recipes.CreateId("ConstructionParts4C");
        public static readonly RecipeID ResearchLab3C = Ids.Recipes.CreateId("ResearchLab3C");
        public static readonly RecipeID VehiclePartsT3C = Ids.Recipes.CreateId("VehicleParts3C");
        // Nuevos
        public static readonly RecipeID ResearchLab4C = Ids.Recipes.CreateId("ResearchLab4C");
        public static readonly RecipeID Cement1 = Ids.Recipes.CreateId("Cement1");
        public static readonly RecipeID Ethanol1 = Ids.Recipes.CreateId("Ethanol1");
        public static readonly RecipeID Exhaust1 = Ids.Recipes.CreateId("Exaust1");
    }


}