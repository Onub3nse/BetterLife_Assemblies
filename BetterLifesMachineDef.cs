using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Buildings;
using Mafi.Base.Prototypes.Buildings.ThermalStorages;
using Mafi.Collections;
using Mafi.Collections.ImmutableCollections;
using Mafi.Collections.ReadonlyCollections;
using Mafi.Core;
using Mafi.Core.Economy;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Priorities;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory;
using Mafi.Core.Factory.ComputingPower;
using Mafi.Core.Factory.ElectricPower;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Factory.Zippers;
using Mafi.Core.Maintenance;
using Mafi.Core.Mods;
using Mafi.Core.Notifications;
using Mafi.Core.Population;
using Mafi.Core.Ports;
using Mafi.Core.Ports.Io;
using Mafi.Core.Products;
using Mafi.Core.PropertiesDb;
using Mafi.Core.Prototypes;
using Mafi.Core.Roads;
using Mafi.Core.Trains;
using Mafi.Core.Vehicles;
using Mafi.Localization;
using Mafi.Serialization;
using Mafi.Unity;
using Mafi.Unity.Entities;
using Mafi.Unity.Entities.Static;
using Mafi.Unity.InstancedRendering;
using Mafi.Unity.UiToolkit.Component;
using RTG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using static BetterLife_Assemblies.AssembliesDef;
using static Mafi.Base.Assets.Base.Terrain;
using static Mafi.Base.Assets.Base.Terrain.Surfaces.Decals;
using newEntityID = Mafi.Core.Entities.Static.StaticEntityProto.ID;
using Object = UnityEngine.Object;


namespace BetterLife_Assemblies
{
    internal class AssembliesDef : IModData
    {
        public static EntityCostsTpl.Builder Build => new EntityCostsTpl.Builder();
        public static EntityCosts RoadCosts => new EntityCosts();

        public void RegisterData(ProtoRegistrator registrator)
        {

            EntityCostsTpl costs = Build.CP(5).MaintenanceT1(0).Priority(8).Workers(0);

            string[] l_assemblyLayout = new string[6]
            {
                "   G~v      ^@E   ",
                "@H>[4][4][4][4]>Y@",
                "A#>[4][4][4][4]>W#",
                "B~>[4][4][4][4]>V~",
                "C#>[4][4][4][4]>X#",
                "@F>[4][4][4][4]>Z@"
            };



            MyMachineProto assemblyProto1 = CreateProto(registrator, BetterLIDs.Machines.AssemblyBlt1, "Better Assembly 1", "Custom machine with custom recipes", costs, 20.Kw(), l_assemblyLayout, "Assets/BetterLife/Buildings/BL2000.prefab", "Assets/BetterLife/AssemblyIcons/BL2000.png");
            MyMachineProto assemblyProto2 = CreateProto(registrator, BetterLIDs.Machines.AssemblyBlt2, "Better Assembly 2", "Custom machine with custom recipes", costs, 20.Kw(), l_assemblyLayout, "Assets/BetterLife/Buildings/BL2000T2.prefab", "Assets/BetterLife/AssemblyIcons/BL2000T2.png");

            assemblyProto1.AddParam(new DrawArrowWileBuildingProtoParam(1f, 0f));
            assemblyProto2.AddParam(new DrawArrowWileBuildingProtoParam(1f, 0f));


            registrator.PrototypesDb.Add(assemblyProto1);
            registrator.PrototypesDb.Add(assemblyProto2);

            Log.Info("BeTTerLife: ------------------------------ Adding BetterAssembler Tier 1 - Done");
            Log.Info($"SourceStringLayout of Assembler 1:  {assemblyProto1.Layout.SourceLayoutStr}");

        }

        public MyMachineProto CreateProto(ProtoRegistrator registrator, MachineProto.ID id, string name, string description, EntityCostsTpl costs, Electricity electricityConsumption, string[] layout, string prefabPath, string customIconPath)
        {
            Proto.Str assemblyName = Proto.CreateStr(id, name, description);
            Predicate<LayoutTile> predicate = null;
            EntityLayoutParams entityLayoutParams = new EntityLayoutParams(predicate, null, false, null, null, null, null, null, null, default, false, null, null);
            EntityLayout assemblyLayout = registrator.LayoutParser.ParseLayoutOrThrow(entityLayoutParams, layout);
            MyMachineProto.Gfx assemblyGfx = new MyMachineProto.Gfx
            (
                prefabPath: prefabPath,
                customIconPath: customIconPath,
                useInstancedRendering: false,
                useSemiInstancedRendering: false,
                categories: registrator.GetCategoriesProtos(Ids.ToolbarCategories.Production_General)
            );
            MyMachineProto cpAssembly = new MyMachineProto
                ( 
                    id,
                    assemblyName,
                    costs.MapToEntityCosts(registrator),
                    assemblyLayout,
                    assemblyGfx,
                    ImmutableArray.Create<AnimationParams>(AnimationParams.Loop(null, false, null))
                );
            return cpAssembly;
        }
  

    }
    [GenerateSerializer(false, null, 0)]
    public class MyMachineEntity : Machine, IEntity, ILayoutEntity
    {
        private MyMachineProto m_proto;

        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;

        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

        [DoNotSave(0, null)]
        public new MyMachineProto Prototype
        {
            get
            {
                return this.m_proto;
            }
            protected set
            {
                this.m_proto = value;
                base.Prototype = value;
            }
        }

        public void SimUpdate(GameTime time)
        {
            // Custom logic for simulation update
            
        }

        // Custom fields
        public float Progress { get; private set; }
        public bool IsRunning { get; private set; }

        public MyMachineEntity(EntityId id, MyMachineProto proto, TileTransform transform, EntityContext context, VirtualBuffersMap virtualBuffersMap, UnlockedProtosDb unlockedProtosDb, IVehicleBuffersRegistry vehicleBuffersRegistry, IEntityMaintenanceProvidersFactory maintenanceProvidersFactory, IAnimationStateFactory animationStateFactory)
            : base(id, proto, transform, context, virtualBuffersMap, unlockedProtosDb, vehicleBuffersRegistry, maintenanceProvidersFactory, animationStateFactory)
        {
            m_proto = proto;
        }
        static MyMachineEntity()
        {
            s_serializeDataDelayedAction = delegate (object obj, BlobWriter writer)
            {
                ((MyMachineEntity)obj).SerializeData(writer);
            };
            s_deserializeDataDelayedAction = delegate (object obj, BlobReader reader)
            {
                ((MyMachineEntity)obj).DeserializeData(reader);
            };
        }

        public static void Serialize(MyMachineEntity value, BlobWriter writer)
        {
            if (writer.TryStartClassSerialization(value))
            {
                writer.EnqueueDataSerialization(value, s_serializeDataDelayedAction);
            }
        }

        protected override void SerializeData(BlobWriter writer)
        {
            base.SerializeData(writer);
            writer.WriteGeneric<MyMachineProto>(m_proto);
            //writer.WriteInt(_pushCount);
        }

        public static new MyMachineEntity Deserialize(BlobReader reader)
        {
            if (reader.TryStartClassDeserialization(out MyMachineEntity obj, (Func<BlobReader, Type, MyMachineEntity>)null))
            {
                reader.EnqueueDataDeserialization(obj, s_deserializeDataDelayedAction);
            }
            return obj;
        }

        protected override void DeserializeData(BlobReader reader)
        {
            base.DeserializeData(reader);
            reader.SetField(this, "m_proto", reader.ReadGenericAs<MyMachineProto>());
            //reader.SetField(this, "_pushCount", reader.ReadInt());
            //reader.RegisterInitAfterLoad<McDonalds>(this, "initSelf", InitPriority.Normal);
        }


    }

    // Example: custom logic

    public class MyMachineProto : MachineProto
    {
        public MyMachineProto
        (
            MachineProto.ID id,
            Proto.Str nameLocKey,
            EntityCosts costs,
            EntityLayout layout,
            MyMachineProto.Gfx gfx,
            ImmutableArray<AnimationParams> animationParams
        // Add more parameters as needed
        )
            : base(id, nameLocKey, layout, costs, Electricity.FromKw(5), Computing.Zero, 1, false, ImmutableArray<AnimationParams>.Empty, gfx, null, false, false, null, null)
        {

        }
        public override Type EntityType
        {
            get
            {
                return typeof(MyMachineEntity);
            } 
        }

    }
    [GlobalDependency(RegistrationMode.AsAllInterfaces, false, false)]
    public class customMachineMbFactory : IEntityMbFactory<MyMachineEntity>, IFactory<MyMachineEntity,EntityMb>
    {

        private readonly AssetsDb m_assetsDb;
        private readonly ProtoModelFactory m_modelFactory;

        public customMachineMbFactory(AssetsDb assetsDb, ProtoModelFactory mFactory)
        {
            m_modelFactory = mFactory;
            m_assetsDb = assetsDb;
        }

        public EntityMb Create(MyMachineEntity entity)
        {
            Log.Info("BeTTerLife: customMachineMbFactory Create called for entity " + entity.Id);
            GameObject gameObject = this.m_modelFactory.CreateModelFor<MyMachineProto>(entity.Prototype);
            customMachineMb machineWithSignMb = gameObject.AddComponent<customMachineMb>();
            machineWithSignMb.Initialize(this.m_assetsDb, entity);
            return machineWithSignMb;
           //customMachineMb transpMb = m_modelFactory.CreateModelFor<MyMachineProto>(entity.Prototype).AddComponent<customMachineMb>();
            //transpMb.Initialize(entity);
            //McDonaldsColliderMB transpMb2 = modelFactory.CreateModelFor<McDonaldsPrototype>(transp.Prototype).AddComponent<McDonaldsColliderMB>();
            //transpMb2.Initialize(transp);
            //return (EntityMb)transpMb;
        } 
    }
    //public class customMachineMb : LayoutEntityWithSignMb, IEntityMbWithRenderUpdate, IEntityMb, IDestroyableEntityMb
    public class customMachineMb : StaticEntityMb, IEntityMbWithRenderUpdate
    {
        MyMachineEntity thisEntity;
        private Material mat_storageAlert;
        private Material mat_sign;
        private Transform m_Rotor1;
        private Transform m_Rotor2;
        private ParticleSystem m_smoke1;
        private ParticleSystem m_smoke2;
        private MyMachineEntity m_machine;
        private SignData m_signData;
        private static Option<Material> s_signBlueprintMaterial;
        private static readonly int ICON_SCALE_SHADER_ID;
        private static readonly int ICON_ALPHA_SHADER_ID;
        private static readonly int ICON_TEX_SHADER_ID;
        private Color c_orange = new Color(1f, 0.5f, 0f);
        private Color c_currentState = Color.grey;
        private int m_frameLimiter = 0;
        protected AssetsDb AssetsDb;
        private const float COLOR_LERP_SPEED = 8f;   // Higher = faster transition
        private Color _currentEmissive = Color.grey;
        private const float EMISSIVE_MULTIPLIER = 6f;
        private const float EMISSIVE_LERP_SPEED = 8f;
        private bool _isBlt1;
        private bool _isBlt2;
        private Color _targetBaseColor = Color.grey;
        private Color _targetEmissiveColor = Color.grey;
        public Option<ProductProto> DisplayedSignProduct
        {
            get
            {
                return this.m_signData.DisplayedProduct;
            }
        }
        private struct SignData
        {
            // Token: 0x06006923 RID: 26915 RVA: 0x0022BB5C File Offset: 0x00229D5C
            public SignData(MeshRenderer renderer, Option<ProductProto> displayedProduct, Option<Material> originalSignMaterial)
            {
                this.Renderer = renderer;
                this.DisplayedProduct = displayedProduct;
                this.OriginalSignMaterial = originalSignMaterial;
            }

            // Token: 0x04005058 RID: 20568
            public readonly MeshRenderer Renderer;

            // Token: 0x04005059 RID: 20569
            public Option<ProductProto> DisplayedProduct;

            // Token: 0x0400505A RID: 20570
            public Option<Material> OriginalSignMaterial;
        }

        private float m_speed;
        private void InitializeMachineType()
        {
            var id = thisEntity.Prototype.Id;
            _isBlt1 = id == BetterLIDs.Machines.AssemblyBlt1;
            _isBlt2 = id == BetterLIDs.Machines.AssemblyBlt2;
        }
        public void RenderUpdate(GameTime time)
        {
            if (this.thisEntity.IsDestroyed)
            {
                return;
            }

            if (m_frameLimiter < 10)
            {
                m_frameLimiter++;
                return;
            }
            m_frameLimiter = 0;

            if ((this.m_machine.LastRecipeInProgress.IsNone || this.m_machine.LastRecipeInProgress.Value.OutputsAtEnd.IsEmpty) && this.DisplayedSignProduct.HasValue)
            {
                UpdateSignProduct(Option<ProductProto>.None);
//                return;
            }
            if (this.m_machine.LastRecipeInProgress.HasValue && this.m_machine.LastRecipeInProgress.Value.OutputsAtEnd.IsNotEmpty && this.m_machine.LastRecipeInProgress.Value.OutputsAtEnd.First.Product != this.DisplayedSignProduct)
            {
                UpdateSignProduct(this.m_machine.LastRecipeInProgress.Value.OutputsAtEnd.First.Product);
            }

            if (this.m_machine.CurrentState == Machine.State.None)
            {
                c_currentState = Color.grey;

                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
                {
                    m_speed = 0f;
                }
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
                {
                    m_smoke1.Stop();
                    m_smoke2.Stop();
                }
            }
            if (this.m_machine.CurrentState == Machine.State.OutputFull)
            {
                c_currentState = c_orange;
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
                {
                    if (m_speed > 0f)
                    {
                        m_speed -= 0.5f;
                    }
                    m_Rotor1.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                    m_Rotor2.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                }
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
                {
                    m_smoke1.Stop();
                    m_smoke2.Stop();
                }
            }
            if (this.m_machine.CurrentState == Machine.State.Working)
            {
                c_currentState = Color.green;
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
                {
                    if (m_speed < 5f)
                    {
                        m_speed += 0.5f;
                    }
                    m_Rotor1.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                    m_Rotor2.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                }
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
                {
                    if (m_smoke1.isStopped)
                    {
                        m_smoke1.Play();
                        m_smoke2.Play();
                    }
                }
            }
            if (this.m_machine.CurrentState == Machine.State.NotEnoughComputing || this.m_machine.CurrentState == Machine.State.NotEnoughPower)
            {
                c_currentState = Color.red;
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
                {
                    if (m_speed > 0f)
                    {
                        m_speed -= 0.5f;
                    }
                    m_Rotor1.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                    m_Rotor2.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                }
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
                {
                    m_smoke1.Stop();
                    m_smoke2.Stop();
                }
            }
            if (this.m_machine.CurrentState == Machine.State.NotEnoughInput)
            {
                c_currentState = Color.cyan;
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
                {
                    if (m_speed > 0f)
                    {
                        m_speed -= 0.5f;
                    }
                    m_Rotor1.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                    m_Rotor2.Rotate(Vector3.up, m_speed * 1f * time.DeltaT);
                }
                if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
                {
                    m_smoke1.Stop();
                    m_smoke2.Stop();
                }
            }
            mat_storageAlert.SetColor("_Color", Color.Lerp(c_currentState * 6, c_currentState , time.DeltaT));
            mat_storageAlert.SetColor("_EmissiveColor", Color.Lerp(c_currentState * 6, c_currentState , time.DeltaT));


        }

        // Separate method for clarity

        public void Initialize(AssetsDb assetsDb,MyMachineEntity entity)
        {
            base.Initialize(entity);
            thisEntity = entity;
            this.m_machine = entity;
            this.AssetsDb = assetsDb;
            var id = thisEntity.Prototype.Id;
            _isBlt1 = id == BetterLIDs.Machines.AssemblyBlt1;
            _isBlt2 = id == BetterLIDs.Machines.AssemblyBlt2;
            if (s_signBlueprintMaterial.IsNone)
            {
                s_signBlueprintMaterial = assetsDb.GetSharedMaterial("Assets/Core/Materials/BuildingSignBlueprint.mat");
            }
            string text = "sign";
            float num = 0.7692308f;
            RecipeProto valueOrNull = this.m_machine.LastRecipeInProgress.ValueOrNull;
            ProductProto productProto;
            if (valueOrNull == null)
            {
                productProto = null;
            }
            else
            {
                RecipeOutput recipeOutput = valueOrNull.OutputsAtEnd.FirstOrDefault();
                productProto = ((recipeOutput != null) ? recipeOutput.Product : null);
            }
            InitializeSign(text, num, productProto, null);

            //            Log.Info("Custom Machine MB initialized for entity: " + cusMachine.Id);
            if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt1)
            {
                m_Rotor1 = base.gameObject.transform.Find("Rotor1");
                m_Rotor2 = base.gameObject.transform.Find("Rotor2");

                mat_storageAlert = gameObject.FindMaterialByName("mat_StorageAlert", false);
                if (mat_storageAlert == null)
                { 
                    Log.Error("Custom Machine MB: Could not find 'mat_StorageAlert' material.");
                    return; 
                }
                mat_sign = gameObject.FindMaterialByName("sign", false);
                if (mat_sign == null)
                { 
                    Log.Error("Custom Machine MB: Could not find 'sign' material.");
                    return; 
                }
            }
            if (thisEntity.Prototype.Id == BetterLIDs.Machines.AssemblyBlt2)
            {
                m_smoke1 = base.gameObject.transform.Find("Smoke1").GetComponent<ParticleSystem>();
                m_smoke2 = base.gameObject.transform.Find("Smoke2").GetComponent<ParticleSystem>();
                var main1 = m_smoke1.main;
                var main2 = m_smoke2.main;
                main1.simulationSpeed = 1.5f; // Adjust the speed as needed
                main2.simulationSpeed = 1.5f; // Adjust the speed as needed
                mat_storageAlert = gameObject.FindMaterialByName("mat_StorageAlert", false);
                if (mat_storageAlert == null)
                {
                    Log.Error("Custom Machine MB: Could not find 'mat_StorageAlert' material.");
                    return;
                }
            }
        }
        static customMachineMb()
        {
            customMachineMb.ICON_SCALE_SHADER_ID = Shader.PropertyToID("_IconScale");
            customMachineMb.ICON_ALPHA_SHADER_ID = Shader.PropertyToID("_IconAlpha");
            customMachineMb.ICON_TEX_SHADER_ID = Shader.PropertyToID("_MainTex");

        }
        protected void InitializeSign(string signGoName, float iconScale, Option<ProductProto> productToRender, float? iconAlpha = null)
        {
            Transform transform;
            if (!base.transform.TryFindChild(signGoName, out transform))
            {
                Log.WarningOnce(string.Format("Failed to get sign child called '{0}' for entity {1}.", signGoName, this.thisEntity.Prototype));
                return;
            }
            GameObject gameObject = transform.gameObject;
            MeshRenderer meshRenderer;
            if (!gameObject.TryGetComponent<MeshRenderer>(out meshRenderer))
            {
                Log.WarningOnce(string.Format("Game object sign for '{0}' is missing MeshRenderer.", this.thisEntity.Prototype));
                return;
            }
            if (!meshRenderer)
            {
                Log.WarningOnce(string.Format("Invalid sign renderer for '{0}'.", this.thisEntity.Prototype));
                return;
            }
            if (!meshRenderer.sharedMaterial)
            {
                Log.WarningOnce(string.Format("Material of sign for '{0}' is missing.", this.thisEntity.Prototype));
                return;
            }
            if (meshRenderer.sharedMaterial.HasFloat(customMachineMb.ICON_TEX_SHADER_ID))
            {
                Log.WarningOnce(string.Format("Material of sign for '{0}' is missing '{1}' param, wrong shader?", this.thisEntity.Prototype, customMachineMb.ICON_TEX_SHADER_ID));
                return;
            }
            if (this.thisEntity.Transform.IsReflected && !this.thisEntity.Prototype.Graphics.DoNotFlipModel)
            {
                iconScale = -iconScale;
            }
            meshRenderer.sharedMaterial = Object.Instantiate<Material>(meshRenderer.sharedMaterial);
            meshRenderer.sharedMaterial.SetFloat(customMachineMb.ICON_SCALE_SHADER_ID, iconScale);
            if (iconAlpha != null)
            {
                meshRenderer.sharedMaterial.SetFloat(customMachineMb.ICON_ALPHA_SHADER_ID, iconAlpha.Value);
            }
            this.m_signData = new customMachineMb.SignData(meshRenderer, Option<ProductProto>.None, Option<Material>.None);
            this.UpdateSignProduct(productToRender);
        }
        protected void UpdateSignProduct(Option<ProductProto> productToRender)
        {
            this.m_signData.DisplayedProduct = productToRender;
            if (!this.m_signData.Renderer)
            {
                Log.Info("No sign to update.");
                return;
            }
            Log.Info("Updating sign product to: " + (productToRender.HasValue ? productToRender.Value.Id.ToString() : "None"));
            Texture2D texture2D = (productToRender.HasValue ? this.AssetsDb.GetSharedTexture(productToRender.Value.IconPath) : null);
            if (texture2D == null)
                Log.Info("No texture found for product: " + (productToRender.HasValue ? productToRender.Value.Id.ToString() : "None"));
            this.m_signData.Renderer.sharedMaterial.SetTexture(customMachineMb.ICON_TEX_SHADER_ID, texture2D);
        }
        protected void SetSignSharedMaterial(Material material)
        {
            if (!this.m_signData.Renderer)
            { 
                Log.Info("No sign to update.");
                return;
            }
            Log.Info("Setting sign shared material.");
            material.SetFloat(customMachineMb.ICON_SCALE_SHADER_ID, this.m_signData.Renderer.sharedMaterial.GetFloat(customMachineMb.ICON_SCALE_SHADER_ID));
            material.SetTexture(customMachineMb.ICON_TEX_SHADER_ID, this.m_signData.Renderer.sharedMaterial.GetTexture(customMachineMb.ICON_TEX_SHADER_ID));
            if (this.m_signData.OriginalSignMaterial.HasValue)
            {
                this.m_signData.OriginalSignMaterial = material;
                InstancingUtils.CopyTextures(this.m_signData.Renderer.sharedMaterial, material, true);
                return;
            }
            this.m_signData.Renderer.sharedMaterial = material;
        }
        public override void EnsureBlueprintMaterial(Material material, ColorRgba color)
        {
            if (this.m_signData.Renderer && this.m_signData.OriginalSignMaterial.IsNone)
            {
                this.m_signData.OriginalSignMaterial = this.m_signData.Renderer.sharedMaterial;
            }
            base.EnsureBlueprintMaterial(material, color);
            MeshRenderer renderer = this.m_signData.Renderer;
            if (!renderer)
            {
                return;
            }
            renderer.sharedMaterial = InstancingUtils.InstantiateMaterialAndCopyTextures(customMachineMb.s_signBlueprintMaterial.Value, this.m_signData.OriginalSignMaterial.Value, true);
            renderer.sharedMaterial.SetColor(StaticEntityMb.TINT_COLOR_SHADER_ID, color.ToColor());
            renderer.sharedMaterial.SetFloat(customMachineMb.ICON_SCALE_SHADER_ID, this.m_signData.OriginalSignMaterial.Value.GetFloat(customMachineMb.ICON_SCALE_SHADER_ID));
            this.UpdateSignProduct(this.DisplayedSignProduct);
        }
        public override void EnsureDefaultMaterial()
        {
            base.EnsureDefaultMaterial();
            if (this.m_signData.OriginalSignMaterial.HasValue)
            {
                this.m_signData.Renderer.sharedMaterial = this.m_signData.OriginalSignMaterial.Value;
                this.m_signData.OriginalSignMaterial = Option<Material>.None;
            }
            this.UpdateSignProduct(this.DisplayedSignProduct);
        }


    }
    public static class insideGOUtility
    {
        /// <summary>
        /// Finds the first material with the specified name from MeshRenderer components in a GameObject and its children.
        /// </summary>
        /// <param name="parent">The root GameObject to start the search from.</param>
        /// <param name="materialName">The name of the material to find.</param>
        /// <param name="includeInactive">Whether to include MeshRenderers on inactive GameObjects.</param>
        /// <returns>The first matching Material, or null if not found.</returns>

        public static Material FindMaterialByName(this GameObject parent, string materialName, bool includeInactive = false)
        {

            MeshRenderer[] renderers = parent.GetComponentsInChildren<MeshRenderer>(includeInactive);
            //Log.Info($"FindMaterialByName: Found {renderers.Length} MeshRenderers in {parent.name}.");



            string searchNameLower = materialName.Trim().ToLower();
            foreach (MeshRenderer renderer in renderers)
            {
                if (renderer != null && renderer.materials != null)
                {
                    //      Log.Info($"FindMaterialByName: Processing MeshRenderer on {renderer.gameObject.name} with {renderer.sharedMaterials.Length} materials.");

                    foreach (Material material in renderer.materials)
                    {
                        if (material != null && material.name != null)
                        {
                            // Handle Unity's potential "(Instance)" suffix and trim whitespace
                            string materialNameLower = material.name.Replace("(Instance)", "").Trim().ToLower();
                            //            Log.Info($"FindMaterialByName: Checking material '{material.name}' (normalized: '{materialNameLower}') against search '{searchNameLower}'.");

                            if (materialNameLower.Contains(searchNameLower))
                            {
                                //              Log.Info($"FindMaterialByName: Matched material '{material.name}' on GameObject '{renderer.gameObject.name}'.");
                                return material;
                            }
                        }
                        else
                        {
                            //        Log.Info($"FindMaterialByName: Skipping null material or null material name in {renderer.gameObject.name}.");
                        }
                    }
                }
                else
                {
                    //    Log.Info($"FindMaterialByName: Skipping null MeshRenderer or sharedMaterials in {renderer?.gameObject.name ?? "null"}.");
                }
            }
            // Log.Info($"FindMaterialByName: Found {materials.Count} matching materials for '{materialName}'.");
            return null;
        }
        /// <summary>
        /// Finds the first component on a GameObject with the specified name in a GameObject and its children.
        /// </summary>
        /// <param name="parent">The root GameObject to start the search from.</param>
        /// <param name="gameObjectName">The name of the GameObject containing the component to find.</param>
        /// <param name="includeInactive">Whether to include components on inactive GameObjects.</param>
        /// <returns>The first matching Component, or null if not found.</returns>
        public static UnityEngine.Component FindComponentByName(this GameObject parent, string gameObjectName, bool includeInactive = false)
        {
            // Validate input
            if (parent == null || string.IsNullOrEmpty(gameObjectName))
            {
                //Log.Info("FindComponentByName: Invalid input - null parent or empty GameObject name.");
                return null;
            }

            // Get all components of any type in the parent and its children
            UnityEngine.Component[] components = parent.GetComponentsInChildren<UnityEngine.Component>(includeInactive);
            //Log.Info($"FindComponentByName: Found {components.Length} components in {parent.name}.");

            string searchNameLower = gameObjectName.Trim().ToLower();
            foreach (UnityEngine.Component component in components)
            {
                if (component != null)
                {
                    // Get the GameObject's name and normalize it
                    string goNameLower = component.gameObject.name.Trim().ToLower();
                    //Log.Info($"FindComponentByName: Checking GameObject '{component.gameObject.name}' (normalized: '{goNameLower}') against search '{searchNameLower}'.");

                    if (goNameLower.Contains(searchNameLower))
                    {
                        //Log.Info($"FindComponentByName: Matched component '{component.GetType().Name}' on GameObject '{component.gameObject.name}'.");
                        return component;
                    }
                }
                else
                {
                    //Log.Info("FindComponentByName: Skipping null component.");
                }
            }

            //Log.Info($"FindComponentByName: No matching component found for GameObject name '{gameObjectName}'.");
            return null;
        }
    }
}

