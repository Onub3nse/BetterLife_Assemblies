using BetterLife;
using Mafi;
using Mafi.Base;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Buildings.OreSorting;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Population;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace BetterLife_Assemblies
{
    public sealed class BetterLifeAssemblies : IDisposable, IMod, IModConfig
    {
        public void Dispose()
        {
        }
        public ModJsonConfig JsonConfig
        {
            get
            {
                return new ModJsonConfig(this);
            }
        }

        public BetterLifeAssemblies(ModManifest modManifest)
        {
            this.manifest = modManifest;
        }

        public void ChangeConfigs(Lyst<IConfig> configs)
        {
        }
        private ModManifest manifest;

        public ModManifest Manifest
        {
            get
            {
                return this.manifest;
            }
        }
        void IMod.Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            
        }
        public void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
        {
        }
        public Option<IConfig> ModConfig { get; }
        public static Version ModVersion
        {
            get
            {
                return typeof(BetterLifeAssemblies).Assembly.GetName().Version;
            }
        }
        public string Name
        {
            get
            {
                return typeof(BetterLifeAssemblies).Assembly.GetName().Name;
            }
        }
        public int Version
        {
            get
            {
                return typeof(BetterLifeAssemblies).Assembly.GetName().Version.Major * 100 + typeof(BetterLifeAssemblies).Assembly.GetName().Version.Minor * 10 + typeof(BetterLifeAssemblies).Assembly.GetName().Version.Build;
            }
        }

         
        public bool IsUiOnly => false;
        public bool GameWasLoaded;



        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            ProtosDb prototypesDb = registrator.PrototypesDb;
            registrator.RegisterAllProducts();
            registrator.RegisterData<AssembliesDef>();
            registrator.RegisterData<RecipesDef>();
            registrator.RegisterData<ResearchDt>();

            ProtosDb protosDb = registrator.PrototypesDb;


            OreSortingPlantProto myOreSorter =
                registrator.PrototypesDb.GetOrThrow<OreSortingPlantProto>(Ids.Buildings.OreSortingPlantT1);


            Quantity newOreCapacity = new Quantity(this.JsonConfig.GetInt("oresorter_capacity",300));



            ReflectionUtils.SetField(myOreSorter, "InputBufferCapacity", newOreCapacity);
            ReflectionUtils.SetField(myOreSorter, "OutputBuffersCapacity", newOreCapacity);

        }
        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        { 
        }


        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            GameWasLoaded = gameWasLoaded;

        }

        public void EarlyInit(DependencyResolver resolver)
        {

        }

    }
}



