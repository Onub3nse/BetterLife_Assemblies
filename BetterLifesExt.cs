using Mafi;
using Mafi.Base;
using Mafi.Core.Buildings.OreSorting;
using Mafi.Core.Buildings.Storages;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Factory;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.Vehicles;
using Mafi.Core.Vehicles.Excavators;
using Mafi.Core.Terrain;
using Mafi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Mafi.Base.Prototypes;
using Mafi.Base.Terrain.Maps;
using Mafi.Core.Buildings.Beacons;
using Mafi.Core.Buildings.VehicleDepots;
using Mafi.Core.Economy;
using Mafi.Core.Entities.Dynamic;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Factory.Transports;
using Mafi.Core.Research;
using Mafi.Core.Vehicles.Trucks;
using MachineID = Mafi.Core.Factory.Machines.MachineProto.ID;
using RecipeID = Mafi.Core.Factory.Recipes.RecipeProto.ID;
using ResNodeID = Mafi.Core.Research.ResearchNodeProto.ID;
using Mafi.Collections.ImmutableCollections;
using Mafi.Core.Products;
using Mafi.Core.Terrain.Surfaces;
using Mafi.Core.Buildings.Farms;
using Mafi.Core;

namespace BetterLife_Assemblies
{
    internal class BetterLifesExt : IModData
    {


        public void RegisterData(ProtoRegistrator registrator)
        {


            ProtosDb protosDb = registrator.PrototypesDb;


            OreSortingPlantProto myOreSorter =
                registrator.PrototypesDb.GetOrThrow<OreSortingPlantProto>(Ids.Buildings.OreSortingPlantT1);


            Quantity newOreCapacity = 25000.Quantity();



            ReflectionUtils.SetField(myOreSorter, "InputBufferCapacity", newOreCapacity);
            ReflectionUtils.SetField(myOreSorter, "OutputBuffersCapacity", newOreCapacity);


        }
    }
}