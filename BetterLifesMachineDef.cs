using Mafi;
using Mafi.Base;
using Mafi.Base.Prototypes.Buildings;
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
using Mafi.Unity.Entities;
using Mafi.Unity.UiToolkit.Component;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using static Mafi.Base.Assets.Base.Terrain;
using newEntityID = Mafi.Core.Entities.Static.StaticEntityProto.ID;


namespace BetterLife_Assemblies
{
    internal class AssembliesDef : IModData
    {
        public static EntityCostsTpl.Builder Build => new EntityCostsTpl.Builder();
        public static EntityCosts RoadCosts => new EntityCosts();



        public void RegisterData(ProtoRegistrator registrator)
        {



            MachineProto cpAssembly = registrator.PrototypesDb.GetOrThrow<MachineProto>(Ids.Machines.AssemblyManual);

            ProtosDb protosDb = registrator.PrototypesDb;

            EntityCostsTpl costs = Build.CP(5).MaintenanceT1(0).Priority(8).Workers(0);

            EntityCostsTpl costsFuelStation = Build.CP(5).MaintenanceT1(5).Priority(8).Workers(4);




            Log.Info("BeTTerLife: ------------------------------ Adding BetterAssembler Tier 2");

            registrator.MachineProtoBuilder
            .Start("Better Assembly II", BetterLIDs.Machines.AssemblyBlt2)
            .Description("Better Assembly II")
            .SetCost(costs)
            .SetElectricityConsumption(20.Kw())
            .SetLayout
            (
                "   G~v      ^@E   ",
                "@H>[4][4][4][4]>Y@",
                "A#>[4][4][4][4]>W#",
                "B~>[4][4][4][4]>V~",
                "C#>[4][4][4][4]>X#",
                "@F>[4][4][4][4]>Z@"
            )
            //.AddParticleParams(ParticlesParams.Loop("SmokeWhite", false, new Func<RecipeProto, bool>(SmokeStackData < registrator > Ids.Recipes.SmokeStackExhaust | 0_7), null))
            .SetCategories(Ids.ToolbarCategories.Production_General)
            .ShowTerrainDesignatorsOnCreation()
            .SetPrefabPath("Assets/BetterLife/Buildings/BL2000T2.prefab")
            .SetCustomIconPath("Assets/BetterLife/AssemblyIcons/BL2000T2.png")
            .SetAnimationParams(
                animParams: AnimationParams.Loop(100.Percent(), false, null))
            //.EnableSemiInstancedRendering(new string[1] { "sign" }.ToImmutableArray())
            //.AddSign()

            .BuildAndAdd()
            .AddParam(new DrawArrowWileBuildingProtoParam(4f));



            Log.Info("BeTTerLife: ------------------------------ Adding BetterAssembler Tier 1");

            registrator.MachineProtoBuilder
                .Start("Better Assembly I", BetterLIDs.Machines.AssemblyBlt1)
                .Description("Better Assembly I")
                .SetCost(costs)
                .SetElectricityConsumption(20.Kw())
                .SetLayout(
                    "   G~v      ^@E   ",
                    "@H>[4][4][4][4]>Y@",
                    "A#>[4][4][4][4]>W#",
                    "B~>[4][4][4][4]>V~",
                    "C#>[4][4][4][4]>X#",
                    "@F>[4][4][4][4]>Z@"
                ) 
                .SetCategories(Ids.ToolbarCategories.Production_General)
                .ShowTerrainDesignatorsOnCreation()
                .SetPrefabPath("Assets/BetterLife/Buildings/BL2000.prefab")
                .SetCustomIconPath("Assets/BetterLife/AssemblyIcons/BL2000.png")

                //.SetAnimationParams(
                //    animParams: AnimationParams.Loop(100.Percent(), false, null))
                //.EnableSemiInstancedRendering(new string[1] { "sign" }.ToImmutableArray())
                //.AddSign()
                .BuildAndAdd()
                .AddParam(new DrawArrowWileBuildingProtoParam(4f));




            Log.Info($"BETTERLIFES: Custom assemblers added...");

            string[] layoutstr = new string[6]
            {
                "   G~v      ^@E   ",
                "@H>[4][4][4][4]>Y@",
                "A#>[4][4][4][4]>W#",
                "B~>[4][4][4][4]>V~",
                "C#>[4][4][4][4]>X#",
                "@F>[4][4][4][4]>Z@"
            };

        //    AnimationParams animParams = AnimationParams.Loop(100.Percent(), false, null);

             

        //    Predicate<LayoutTile> predicate = null;
        //    EntityLayoutParams entityLayoutParams = new EntityLayoutParams(predicate, null , false, null, null, null, null, null, null, default);
        //    EntityLayout eLayout = registrator.LayoutParser.ParseLayoutOrThrow(entityLayoutParams, layoutstr);

        //    Proto.Str cusMachine = Proto.CreateStr(BetterLIDs.Machines.AssemblyBlt3, "Custom Machine");

        //    customMachineProto myMachine = new customMachineProto(BetterLIDs.Machines.AssemblyBlt3,cusMachine, eLayout, costs.MapToEntityCosts(registrator), Electricity.OneKw, Computing.Zero, 1, true,null , 
        //        new customMachineProto.Gfx("Assets/BetterLife/Buildings/BL2000.prefab", registrator.GetCategoriesProtos(Ids.ToolbarCategories.Buildings), default,
        //        "Assets/BetterLife/AssemblyIcons/BL2000.png",default, default, default, false,false, default,null, false,null,default,null)
        //        , null, false,false,null,null);

        //    registrator.PrototypesDb.Add(myMachine, false);


        //    ImmutableArray<RecipeInput> recipe1inputs =
        //        ImmutableArray.Create(new RecipeInput(resolvePortSelector(myMachine,"*",IoPortType.Input, ProductType.ANY), registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.IronScrap), new Quantity(10)));
        //    ImmutableArray<RecipeOutput> recipe1outputs =
        //        ImmutableArray.Create(new RecipeOutput(resolvePortSelector(myMachine, "WX", IoPortType.Output, ProductType.ANY),registrator.PrototypesDb.GetOrThrow<ProductProto>(Ids.Products.Iron), new Quantity(20)));


        //    Proto.Str recipeStr1 = Proto.CreateStr(BetterLIDs.Recipes.ScrapToIron, "Scrap to Iron");
        //    RecipeProto recipe1 = new RecipeProto(BetterLIDs.Recipes.ScrapToIron, recipeStr1, 5.Seconds(),
        //        recipe1inputs, recipe1outputs,
        //        10.Percent(), DestroyReason.General, false, null);
                

        //    myMachine.AddRecipe(recipe1);

        //}

        //private ImmutableArray<IoPortTemplate> resolvePortSelector(customMachineProto machine, string portSelector, IoPortType type, ProductType productType)
        //{
        //    if (portSelector == "VIRTUAL" || (portSelector == "*" && productType == VirtualProductProto.ProductType))
        //    {
        //        return ImmutableArray<IoPortTemplate>.Empty;
        //    }
        //    ImmutableArray<IoPortTemplate> immutableArray;
        //    try
        //    {
        //        immutableArray = machine.Layout.ResolvePortSelectorOrThrow(portSelector, type, productType).ToImmutableArray();
        //    }
        //    catch (ProtoBuilderException ex)
        //    {
        //        throw new ProtoBuilderException(string.Format("Failed to resolve build recipe '{0}', invalid port(s) of machine '{1}'.", machine.Id, machine.Id), ex);
        //    }
        //    return immutableArray;
        //}


    }



    }





    //public class customMachine :
    //  LayoutEntity,
    //  IMaintainedEntity,
    //  IEntityWithGeneralPriority,
    //  IEntity,
    //  IObjectWithTitle,
    //  IIsSafeAsHashKey,
    //  IAnimatedEntity,
    //  IUnityConsumingEntity,
    //  IEntityWithLogisticsControl,
    //  IElectricityConsumingEntity,
    //  IComputingConsumingEntity,
    //  IEntityWithSimUpdate,
    //  IEntityWithPorts,
    //  IStaticEntity,
    //  IEntityWithPosition,
    //  IAreaSelectableEntity,
    //  IRenderedEntity,
    //  IEntityWithBoost,
    //  IEntityWithSound,
    //  IEntityWithCloneableConfig,
    //  IEntityWithEmission,
    //  IUpgradableEntity,
    //  IEntityWithReplaceVerification,
    //  IRecipeExecutorForUi,
    //  IEntityWithWorkers,
    //  IEntityWithAssignedRecipes,
    //  IEntityWithProductivityCounter
    //{
    //    private static readonly ThreadLocal<customMachine.CacheContext> s_cache;
    //    public static readonly ProductivityCounterLabels PROD_COUNTERS_LABELS;
    //    private customMachineProto m_proto;
    //    private Option<IElectricityConsumer> m_electricityConsumer;
    //    private Option<IComputingConsumer> m_computingConsumer;
    //    /// <summary>Utilization from the last tick where we worked.</summary>
    //    private Percent m_lastWorkUtilization;
    //    private readonly Lyst<RecipeProto> m_recipesAssigned;
    //    [DoNotSave(0, null)]
    //    private int m_assignedRecipesCount;
    //    /// <summary>
    //    /// Provides great perf benefit. Instead of iterating over regular recipes we
    //    /// use this list of structs that also point directly to the corresponding
    //    /// buffers. Thanks to this we skip lots of dictionary calls and hops on RecipeProto.
    //    /// </summary>
    //    [DoNotSaveCreateNewOnLoad(null, 0)]
    //    protected LystStruct<RecipeWrapper> m_recipesFast;
    //    /// <summary>
    //    /// Recipe result that is being worked on. No other recipe can be started until this one is empty.
    //    /// </summary>
    //    internal customMachine.RecipeResult m_recipeResult;
    //    private readonly IVehicleBuffersRegistry m_vehicleBuffersRegistry;
    //    private readonly IProductsManager m_productsManager;
    //    public readonly VirtualBuffersMap m_virtualBuffersMap;
    //    private EntityNotificator m_noRecipeSelectedNotif;
    //    private EntityNotificator m_entityBoostedNotif;
    //    private EntityNotificatorWithProtoParam m_needsTransportNotif;
    //    [NewInSaveVersion(140, null, null, null, null)]
    //    private Duration m_lowPowerChargeLeft;
    //    [NewInSaveVersion(140, null, null, null, null)]
    //    private Duration m_lowComputingChargeLeft;
    //    [NewInSaveVersion(140, null, "Percent.Hundred", null, null)]
    //    private Percent m_speedFactorBase;
    //    [NewInSaveVersion(140, null, "Percent.Hundred", null, null)]
    //    private Percent m_speedFactorSecondary;
    //    [DoNotSave(0, null)]
    //    private Percent m_speedOnLowPower;
    //    [DoNotSave(0, null)]
    //    private Percent m_speedOnLowComputing;
    //    private static readonly Percent SPEED_WHEN_BROKEN;
    //    private static readonly Duration MaxDurationWithNoPower;
    //    private static readonly Duration MaxDurationWithNoComputing;
    //    [DoNotSave(0, null)]
    //    private int m_inputPortsCount;
    //    [DoNotSave(0, null)]
    //    private int m_outputPortsCount;
    //    /// <summary>
    //    /// If we fail to start work on new recipe due to lack of input / output we set this to true.
    //    /// Thanks to this, in the next sim we can skip the expensive checking for recipes. Every-time a buffer
    //    /// receives more quantity (an input one) or sends away some quantity (an output one) it sets this
    //    /// back to false which means we try recipes again. This is also invalidated on recipes change.
    //    /// We do not need to save this and in fact we would have to invalidate it anyway because it can
    //    /// happen that some update lowers a recipe input requirements.
    //    /// </summary>
    //    [DoNotSave(0, null)]
    //    private bool m_hasNoNeedToSearchForRecipe;
    //    /// <summary>
    //    /// The last status we had when we set m_hasNoNeedToSearchForRecipe to true. This is either full output
    //    /// or not enough input.
    //    /// </summary>
    //    [DoNotSave(0, null)]
    //    private customMachine.State m_statusWhenNoNeedForRecipe;
    //    /// <summary>
    //    /// This gets set to true if all output buffers were empty during last send attempt. This saves some perf
    //    /// on having to hop on each buffer and checking that there is nothing to save anyway. This gets invalidated
    //    /// anytime we store more products into output buffers or when a new port gets connected. We also don't
    //    /// save this as there is no advantage in doing so (we just try to send products once after we load).
    //    /// </summary>
    //    [DoNotSave(0, null)]
    //    private bool m_allConnectedOutputsBuffersEmpty;
    //    private readonly Option<IInputBufferPriorityProvider> m_customStrategy;
    //    private Option<ProductProto> m_productInNeedOfTransport;
    //    [DoNotSaveCreateNewOnLoad(null, 0)]
    //    private LystStruct<RecipeProto> m_recipesWithFullOutput;
    //    private LystStruct<customMachineInputBuffer> m_inputBuffers;
    //    private LystStruct<customMachineOutputBuffer> m_outputBuffers;
    //    [NewInSaveVersion(263, null, "new ProductivityCounter(Id.Value)", null, null)]
    //    private ProductivityCounter m_productivityCounter;
    //    private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;
    //    private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

    //    private static customMachine.CacheContext Cache => customMachine.s_cache.Value;

    //    public IProtoWithUpgrade UpgradableProto => (IProtoWithUpgrade)this.Prototype;

    //    [DoNotSave(0, null)]
    //    new public customMachineProto Prototype
    //    {
    //        get => this.m_proto;
    //        protected set
    //        {
    //            this.m_proto = value;
    //            this.Prototype = value;
    //        }
    //    }

    //    public override bool CanBePaused => true;

    //    public Mafi.Core.Entities.SoundParams? SoundParams
    //    {
    //        get
    //        {
    //            return !this.Prototype.Graphics.MachineSoundPrefabPath.HasValue ? new Mafi.Core.Entities.SoundParams?() : new Mafi.Core.Entities.SoundParams?(new Mafi.Core.Entities.SoundParams(this.Prototype.Graphics.MachineSoundPrefabPath.Value, SoundSignificance.Normal));
    //        }
    //    }

    //    bool IEntityWithSound.IsSoundOn => this.WorkedThisTick;

    //    public float? EmissionIntensity
    //    {
    //        get
    //        {
    //            return !this.Prototype.EmissionWhenRunning.HasValue ? new float?() : new float?(this.WorkedThisTick ? (float)this.Prototype.EmissionWhenRunning.Value : 0.0f);
    //        }
    //    }

    //    public Upoints MaxMonthlyUnityConsumed => this.MonthlyUnityConsumed;

    //    public Upoints MonthlyUnityConsumed
    //    {
    //        get
    //        {
    //            return !this.IsBoostRequested || !this.BoostCost.HasValue ? Upoints.Zero : this.BoostCost.Value;
    //        }
    //    }

    //    public Proto.ID UpointsCategoryId => IdsCore.UpointsCategories.Boost;

    //    public override bool IsCargoAffectedByGeneralPriority => true;

    //    public customMachine.State CurrentState { get; private set; }

    //    public bool CanDisableLogisticsInput => this.m_inputPortsCount > 0;

    //    public bool CanDisableLogisticsOutput => this.m_outputPortsCount > 0;

    //    public EntityLogisticsMode LogisticsInputMode { get; private set; }

    //    public EntityLogisticsMode LogisticsOutputMode { get; private set; }

    //    int IEntityWithWorkers.WorkersNeeded => this.Prototype.Costs.Workers;

    //    [DoNotSave(0, null)]
    //    bool IEntityWithWorkers.HasWorkersCached { get; set; }

    //    public Electricity PowerRequired
    //    {
    //        get
    //        {
    //            return !this.m_recipeResult.Recipe.HasValue ? this.Prototype.ConsumedPowerPerTick : this.Prototype.ConsumedPowerPerTick.ScaledBy(this.m_recipeResult.Recipe.Value.PowerMultiplier);
    //        }
    //    }

    //    public Option<IElectricityConsumerReadonly> ElectricityConsumer
    //    {
    //        get => this.m_electricityConsumer.As<IElectricityConsumerReadonly>();
    //    }

    //    Computing IComputingConsumingEntity.ComputingRequired => this.Prototype.ComputingConsumed;

    //    public Option<IComputingConsumerReadonly> ComputingConsumer
    //    {
    //        get => this.m_computingConsumer.As<IComputingConsumerReadonly>();
    //    }

    //    MaintenanceCosts IMaintainedEntity.MaintenanceCosts => this.Prototype.Costs.Maintenance;

    //    bool IMaintainedEntity.IsIdleForMaintenance => this.CurrentState != customMachine.State.Working;

    //    public IEntityMaintenanceProvider Maintenance { get; private set; }

    //    protected override bool IsEnabledNow => base.IsEnabledNow && this.Maintenance.CanWork();

    //    public ImmutableArray<Mafi.Core.Entities.Animations.AnimationParams> AnimationParams
    //    {
    //        get => this.Prototype.AnimationParams;
    //    }

    //    public AnimationStatesProvider AnimationStatesProvider { get; private set; }

    //    /// <summary>
    //    /// Whether the player request boost. That doesn't mean that the boost is active if there isn't enough unity.
    //    /// </summary>
    //    public bool IsBoostRequested { get; private set; }

    //    public bool IsBoosted { get; private set; }

    //    public Upoints? BoostCost => this.Prototype.BoostCost;

    //    [RenamedInVersion(140, "BoostedUnityConsumer")]
    //    public Option<Mafi.Core.Population.UnityConsumer> UnityConsumer { get; private set; }

    //    /// <summary>
    //    /// Last recipe this entity has worked on (this might be old value if the entity didn't work for a longer time).
    //    /// </summary>
    //    public Option<RecipeProto> LastRecipeInProgress { get; private set; }

    //    /// <summary>
    //    /// This allows to return progress for a super-fast recipes that take 1 tick. The reason is that fast recipes
    //    /// have m_recipeResult always None. Which would when calculating progress return 0. It is also correct because
    //    /// the entity really worked that tick (so even for slow entity it is correct. Another thing is that even machine
    //    /// has progress it doesn't mean that it actually runs. For instance if output ports are full, machine stops but
    //    /// progress is still 100%. For animations this is how to check that machine actually stopped to work.
    //    /// </summary>
    //    public bool WorkedThisTick => this.CurrentState == customMachine.State.Working;

    //    /// <summary>Progress on the current recipe.</summary>
    //    /// <remarks>
    //    /// If recipe takes 1 tick and is repeated all the time, you will get 1.0f from this method every time you ask.
    //    /// </remarks>
    //    public Percent ProgressPerc
    //    {
    //        get
    //        {
    //            if (this.m_recipeResult.HasResult)
    //                return Percent.FromRatio(this.m_recipeResult.DurationDone.Ticks, this.m_recipeResult.Duration.Ticks);
    //            return !this.WorkedThisTick ? Percent.Zero : Percent.Hundred;
    //        }
    //    }

    //    /// <summary>Number of performed ticks on current recipe so far.</summary>
    //    public Duration RecipeProductionTicks
    //    {
    //        get => !this.m_recipeResult.HasResult ? Duration.Zero : this.m_recipeResult.DurationDone;
    //    }

    //    public Percent Utilization => !this.WorkedThisTick ? Percent.Zero : this.m_lastWorkUtilization;

    //    public IIndexable<RecipeProto> RecipesAssigned
    //    {
    //        get => (IIndexable<RecipeProto>)this.m_recipesAssigned;
    //    }

    //    /// <summary>
    //    /// Note: speed factor does not take boost into the account.
    //    /// </summary>
    //    [NewInSaveVersion(140, null, "Percent.Hundred", null, null)]
    //    public Percent SpeedFactor { get; private set; }

    //    public Percent DurationMultiplier => Percent.Hundred / this.SpeedFactor;

    //    [NewInSaveVersion(180, null, "Percent.Hundred", null, null)]
    //    public Percent VirtualOutputMultiplier { get; protected set; }

    //    /// <summary>
    //    /// Introduced for maintenance depot which outputs its products into a global limited virtual buffer.
    //    /// The machine optimization is not able to handle that case and once it would reach full output buffer
    //    /// it would never try to send products again.
    //    /// </summary>
    //    protected virtual bool UsesVirtualLimitedBuffers => false;

    //    public ProductivityCounterHistory.Data OngoingMonthlyData
    //    {
    //        get => this.m_productivityCounter.OngoingMonthlyData;
    //    }

    //    public ProductivityCounterHistory ProductivityCounterHistory
    //    {
    //        get => this.m_productivityCounter.HistoricData;
    //    }

    //    public ProductivityCounterLabels ProductivityCounterLabels => Machine.PROD_COUNTERS_LABELS;

    //    public customMachine(
    //      EntityId id,
    //      customMachineProto proto,
    //      TileTransform transform,
    //      EntityContext context,
    //      VirtualBuffersMap virtualBuffersMap,
    //      UnlockedProtosDb unlockedProtosDb,
    //      IVehicleBuffersRegistry vehicleBuffersRegistry,
    //      IEntityMaintenanceProvidersFactory maintenanceProvidersFactory,
    //      IAnimationStateFactory animationStateFactory)
    //        : base(id, (LayoutEntityProto)proto, transform, context)
    //    {
    //        //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //        this.m_recipesAssigned = new Lyst<RecipeProto>();
    //        this.m_recipeResult = new customMachine.RecipeResult();
    //        // ISSUE: reference to a compiler-generated field
    //        //            this.\u003CSpeedFactor\u003Ek__BackingField = Percent.Hundred;
    //        this.m_speedFactorBase = Percent.Hundred;
    //        this.m_speedFactorSecondary = Percent.Hundred;
    //        // ISSUE: reference to a compiler-generated field
    //        //            this.\u003CVirtualOutputMultiplier\u003Ek__BackingField = Percent.Hundred;
    //        // ISSUE: explicit constructor call
    //        //            base.\u002Ector(id, (LayoutEntityProto)proto, transform, context);
    //        this.Prototype = proto.CheckNotNull<customMachineProto>();
    //        this.m_vehicleBuffersRegistry = vehicleBuffersRegistry;
    //        this.m_virtualBuffersMap = virtualBuffersMap;
    //        this.m_productsManager = this.Context.ProductsManager;
    //        this.updateProperties();
    //        this.m_electricityConsumer = this.Context.ElectricityConsumerFactory.CreateConsumerIfNeeded((IElectricityConsumingEntity)this, this.m_electricityConsumer);
    //        this.m_computingConsumer = this.Context.ComputingConsumerFactory.CreateConsumerIfNeeded((IComputingConsumingEntity)this, this.m_computingConsumer);
    //        this.m_customStrategy = (proto.IsWasteDisposal ? WasteInputPortPriorityProvider.Instance : null);
    //        this.m_noRecipeSelectedNotif = context.NotificationsManager.CreateNotificatorFor(IdsCore.Notifications.NoRecipeSelected);
    //        this.m_entityBoostedNotif = context.NotificationsManager.CreateNotificatorFor(IdsCore.Notifications.EntityIsBoosted);
    //        this.m_needsTransportNotif = context.NotificationsManager.CreateNotificatorFor(IdsCore.Notifications.NeedsTransportConnected);
    //        this.m_productivityCounter = new ProductivityCounter(id.Value);
    //        this.AnimationStatesProvider = animationStateFactory.CreateProviderFor((IAnimatedEntity)this);
    //        this.UnityConsumer = Option<Mafi.Core.Population.UnityConsumer>.None;
    //        this.Maintenance = maintenanceProvidersFactory.CreateFor((IMaintainedEntity)this);
    //        if (proto.DisableLogisticsByDefault)
    //        {
    //            this.LogisticsInputMode = EntityLogisticsMode.Off;
    //            this.LogisticsOutputMode = EntityLogisticsMode.Off;
    //        }
    //        this.updatePortsCount();
    //        Lyst<RecipeProto> recipes = customMachine.Cache.Recipes.ClearAndReturn();
    //        foreach (RecipeProto recipe in this.Prototype.Recipes)
    //        {
    //            if (unlockedProtosDb.IsUnlocked(recipe))
    //                recipes.Add(recipe);
    //        }
    //        if (this.Prototype.UseAllRecipesAtStartOrAfterUnlock)
    //            this.AssignRecipes((IEnumerable<RecipeProto>)recipes);
    //        else if (recipes.Count == 1)
    //            this.AssignRecipe(recipes.First);
    //        recipes.Clear();
    //    }

    //    private void updateProperties()
    //    {
    //        this.m_speedOnLowPower = this.Context.PropertiesDb.GetValueAndRegisterForUpdates<Percent>((IEntity)this, IdsCore.PropertyIds.MachineSpeedOnLowPower);
    //        this.m_speedOnLowComputing = this.Context.PropertiesDb.GetValueAndRegisterForUpdates<Percent>((IEntity)this, IdsCore.PropertyIds.MachineSpeedOnLowComputing);
    //    }

    //    [InitAfterLoad(InitPriority.Normal)]
    //    private void initSelf()
    //    {
    //        if (this.IsDestroyed)
    //            return;
    //        this.updateProperties();
    //        Lyst<RecipeProto> lyst = customMachine.Cache.Recipes.ClearAndReturn();
    //        foreach (RecipeProto recipeProto in this.m_recipesAssigned)
    //        {
    //            if (recipeProto.IsNotAvailable)
    //                lyst.Add(recipeProto);
    //        }
    //        foreach (RecipeProto recipeProto in lyst)
    //            this.m_recipesAssigned.Remove(recipeProto);
    //        this.rebuildRecipes(true);
    //        this.updatePortsCount();
    //        this.m_electricityConsumer = this.Context.ElectricityConsumerFactory.CreateConsumerIfNeeded((IElectricityConsumingEntity)this, this.m_electricityConsumer);
    //        this.m_computingConsumer = this.Context.ComputingConsumerFactory.CreateConsumerIfNeeded((IComputingConsumingEntity)this, this.m_computingConsumer);
    //        if (!this.IsBoostRequested)
    //            return;
    //        Mafi.Core.Population.UnityConsumer valueOrNull = this.UnityConsumer.ValueOrNull;
    //        if ((valueOrNull != null ? (valueOrNull.MonthlyUnity.IsNotPositive ? 1 : 0) : 0) == 0)
    //            return;
    //        this.UnityConsumer.Value?.RefreshUnityConsumed();
    //    }

    //    protected override void OnPropertiesChanged()
    //    {
    //        this.updateProperties();
    //        base.OnPropertiesChanged();
    //    }

    //    public void GetAllMissingInputs(Set<ProductProto> result)
    //    {
    //        result.Clear();
    //        foreach (RecipeWrapper recipeWrapper in this.m_recipesFast)
    //        {
    //            foreach (RecipeProductQuantity allInput in recipeWrapper.AllInputs)
    //            {
    //                if (allInput.Buffer.Quantity < allInput.Quantity)
    //                    result.Add(allInput.Buffer.Product);
    //            }
    //        }
    //    }

    //    private void updatePortsCount()
    //    {
    //        ImmutableArray<IoPort> ports = this.Ports;
    //        this.m_inputPortsCount = ports.Count((Func<IoPort, bool>)(x => x.Type == IoPortType.Input));
    //        ports = this.Ports;
    //        this.m_outputPortsCount = ports.Count((Func<IoPort, bool>)(x => x.Type == IoPortType.Output));
    //    }

    //    protected override void OnPortsLoadOrChange()
    //    {
    //        base.OnPortsLoadOrChange();
    //        this.updatePortsCount();
    //    }

    //    protected override void OnPortConnectionChanged(IoPort ourPort)
    //    {
    //        base.OnPortConnectionChanged(ourPort);
    //        if (ourPort.Type == IoPortType.Input)
    //        {
    //            this.onInputPortConnectionChanged(ourPort);
    //        }
    //        else
    //        {
    //            if (ourPort.Type != IoPortType.Output)
    //                return;
    //            this.onOutputPortConnectionChanged(ourPort);
    //        }
    //    }

    //    void IEntityWithSimUpdate.SimUpdate() => this.SimUpdateInternal();

    //    protected virtual void SimUpdateInternal()
    //    {
    //        if (this.IsNotEnabled)
    //        {
    //            stepDisabled(this.Maintenance.Status.IsBroken ? customMachine.State.Broken : customMachine.State.Paused);
    //            this.m_noRecipeSelectedNotif.Deactivate((IEntity)this);
    //            if (!this.IsConstructed || !this.IsNotPaused)
    //                return;
    //            this.m_productivityCounter.ReportCategoryD();
    //        }
    //        else
    //        {
    //            this.m_noRecipeSelectedNotif.NotifyIff(this.m_assignedRecipesCount == 0, (IEntity)this);
    //            if (Entity.IsMissingWorkers((IEntityWithWorkers)this))
    //            {
    //                stepDisabled(customMachine.State.NotEnoughWorkers);
    //                this.m_productivityCounter.ReportCategoryD();
    //            }
    //            else
    //            {
    //                Percent speedFactor = Percent.Hundred;
    //                if (this.m_computingConsumer.HasValue && !this.m_computingConsumer.CanConsume())
    //                {
    //                    if (this.m_speedOnLowComputing.IsPositive && this.m_lowComputingChargeLeft.IsPositive)
    //                    {
    //                        speedFactor = speedFactor.Min(this.m_speedOnLowComputing);
    //                        this.m_lowComputingChargeLeft -= Duration.OneTick;
    //                    }
    //                    else
    //                    {
    //                        stepDisabled(customMachine.State.NotEnoughComputing);
    //                        this.m_productivityCounter.ReportCategoryD();
    //                        return;
    //                    }
    //                }
    //                int num;
    //                if (this.IsBoostRequested)
    //                {
    //                    Mafi.Core.Population.UnityConsumer valueOrNull = this.UnityConsumer.ValueOrNull;
    //                    num = valueOrNull != null ? (valueOrNull.CanWork() ? 1 : 0) : 0;
    //                }
    //                else
    //                    num = 0;
    //                this.IsBoosted = num != 0;
    //                if (this.m_electricityConsumer.HasValue && !this.m_electricityConsumer.Value.CanConsume() && !this.IsBoosted)
    //                {
    //                    if (this.m_speedOnLowPower.IsPositive && this.m_lowPowerChargeLeft.IsPositive)
    //                    {
    //                        speedFactor = speedFactor.Min(this.m_speedOnLowPower);
    //                        this.m_lowPowerChargeLeft -= Duration.OneTick;
    //                    }
    //                    else
    //                    {
    //                        stepDisabled(customMachine.State.NotEnoughPower);
    //                        this.m_productivityCounter.ReportCategoryD();
    //                        return;
    //                    }
    //                }
    //                this.m_needsTransportNotif.NotifyIff((Proto)this.m_productInNeedOfTransport.ValueOrNull, this.m_productInNeedOfTransport.HasValue, (IEntity)this);
    //                if (this.Maintenance.ShouldSlowDown())
    //                    speedFactor = speedFactor.Min(customMachine.SPEED_WHEN_BROKEN);
    //                this.setSecondarySpeedFactor(speedFactor);
    //                bool startedNewWorkThisTick;
    //                customMachine.State state = this.updateWorkOnRecipes(out startedNewWorkThisTick);
    //                if (state != customMachine.State.Working)
    //                {
    //                    if (this.CurrentState == customMachine.State.Working)
    //                        this.AnimationStatesProvider.Pause();
    //                    this.CurrentState = state;
    //                    this.sendOutputs();
    //                    if (state == customMachine.State.NotEnoughInput)
    //                        this.m_productivityCounter.ReportCategoryB();
    //                    else if (state == customMachine.State.OutputFull)
    //                        this.m_productivityCounter.ReportCategoryC();
    //                    else
    //                        this.m_productivityCounter.ReportCategoryD();
    //                }
    //                else
    //                {
    //                    this.CurrentState = state;
    //                    if (startedNewWorkThisTick)
    //                    {
    //                        this.AnimationStatesProvider.Start(this.m_recipeResult.Duration);
    //                    }
    //                    else
    //                    {
    //                        Percent percent1 = this.Utilization;
    //                        Percent percent2 = percent1.ScaleBy(this.SpeedFactor);
    //                        AnimationStatesProvider animationStatesProvider = this.AnimationStatesProvider;
    //                        Percent speed = this.IsBoosted ? percent2.ScaleBy(150.Percent()) : percent2;
    //                        percent1 = this.ProgressPerc;
    //                        Percent progress = percent1.IsNearHundred ? Percent.Zero : this.ProgressPerc;
    //                        animationStatesProvider.Step(speed, progress);
    //                    }
    //                    if (this.m_electricityConsumer.TryConsume() && this.m_speedOnLowPower.IsPositive && this.m_lowPowerChargeLeft < customMachine.MaxDurationWithNoPower)
    //                        this.m_lowPowerChargeLeft += 2.Ticks();
    //                    if (this.m_computingConsumer.TryConsume() && this.m_speedOnLowComputing.IsPositive && this.m_lowComputingChargeLeft < customMachine.MaxDurationWithNoComputing)
    //                        this.m_lowComputingChargeLeft += 2.Ticks();
    //                    this.sendOutputs();
    //                    this.m_productivityCounter.ReportCategoryA();
    //                }
    //            }
    //        }

    //        void stepDisabled(customMachine.State state)
    //        {
    //            if (this.CurrentState != customMachine.State.Working)
    //                this.AnimationStatesProvider.Pause();
    //            this.CurrentState = state;
    //            this.m_needsTransportNotif.Deactivate((IEntity)this);
    //        }
    //    }

    //    public void SetBoosted(bool isBoosted)
    //    {
    //        if (isBoosted && !this.BoostCost.HasValue)
    //            Assert.Fail(string.Format("Cannot boost '{0}', not allowed!", (object)this.Prototype.Id));
    //        this.m_entityBoostedNotif.NotifyIff(isBoosted, (IEntity)this);
    //        this.IsBoostRequested = isBoosted;
    //        if (this.IsBoostRequested && this.UnityConsumer.IsNone)
    //            this.UnityConsumer = (Option<Mafi.Core.Population.UnityConsumer>)this.Context.UnityConsumerFactory.CreateConsumer((IUnityConsumingEntity)this);
    //        else if (!this.IsBoostRequested && this.UnityConsumer.HasValue)
    //        {
    //            this.UnityConsumer.Value.Destroy();
    //            this.UnityConsumer = (Option<Mafi.Core.Population.UnityConsumer>)Option.None;
    //        }
    //        if (!this.UnityConsumer.HasValue || !this.UnityConsumer.Value.MonthlyUnity.IsNotPositive)
    //            return;
    //        this.UnityConsumer.Value.RefreshUnityConsumed();
    //        Assert.That<Upoints>(this.UnityConsumer.Value.MonthlyUnity).IsPositive();
    //    }

    //    private customMachine.State updateWorkOnRecipes(out bool startedNewWorkThisTick)
    //    {
    //        startedNewWorkThisTick = false;
    //        if (this.m_recipeResult.IsEmpty)
    //        {
    //            Percent machineUtilization;
    //            customMachine.State newWork = this.TryGetNewWork(out machineUtilization);
    //            if (!this.m_recipeResult.HasResult)
    //                return newWork;
    //            startedNewWorkThisTick = true;
    //            this.LastRecipeInProgress = this.m_recipeResult.Recipe;
    //            this.m_lastWorkUtilization = machineUtilization;
    //        }
    //        this.m_recipeResult.DurationDone = (this.m_recipeResult.DurationDone + (this.IsBoosted ? 2.Ticks() : Duration.OneTick)).Min(this.m_recipeResult.Duration);
    //        return this.RecipeProductionTicks < this.m_recipeResult.Duration ? customMachine.State.Working : this.tryPushFinishedRecipeToBuffers();
    //    }

    //    private customMachine.State tryPushFinishedRecipeToBuffers()
    //    {
    //        bool flag = false;
    //        for (int index = 0; index < this.m_recipeResult.ProducedOutputs.Count; ++index)
    //        {
    //            ProductQuantity producedOutput = this.m_recipeResult.ProducedOutputs[index];
    //            if (producedOutput.IsNotEmpty)
    //            {
    //                Quantity newQuantity = this.m_recipeResult.Buffers[index].StoreAsMuchAs(producedOutput.Quantity);
    //                this.m_recipeResult.ProducedOutputs[index] = producedOutput.WithNewQuantity(newQuantity);
    //                flag |= newQuantity.IsPositive;
    //            }
    //        }
    //        if (flag)
    //            return customMachine.State.OutputFull;
    //        this.m_recipeResult.Clear();
    //        return customMachine.State.Working;
    //    }

    //    private customMachine.State canStartRecipe(
    //      RecipeWrapper recipe,
    //      out Percent utilization,
    //      out int multiplier)
    //    {
    //        utilization = Percent.Zero;
    //        multiplier = 0;
    //        if (recipe.MinUtilization == Percent.Hundred)
    //        {
    //            if (!recipe.CanRemoveFromInputs())
    //                return customMachine.State.NotEnoughInput;
    //            if (!recipe.CanStoreToOutputs())
    //                return customMachine.State.OutputFull;
    //            utilization = Percent.Hundred;
    //            multiplier = 1;
    //            return customMachine.State.Working;
    //        }
    //        int num1 = int.MaxValue;
    //        foreach (RecipeProductQuantity allInput in recipe.AllInputs)
    //        {
    //            int num2 = allInput.Buffer.Quantity.Min(allInput.Quantity).Value / allInput.BaseFraction.Value;
    //            if (num2 <= 0)
    //                return customMachine.State.NotEnoughInput;
    //            num1 = num1.Min(num2);
    //        }
    //        if (Percent.FromRatio(num1, recipe.QuantitiesGcd) < recipe.MinUtilization)
    //            return customMachine.State.NotEnoughInput;
    //        foreach (RecipeProductQuantity recipeProductQuantity in recipe.OutputsAtStart)
    //        {
    //            int num3 = recipeProductQuantity.Buffer.UsableCapacity.Min(recipeProductQuantity.Quantity).Value / recipeProductQuantity.BaseFraction.Value;
    //            num1 = num1.Min(num3);
    //        }
    //        foreach (RecipeProductQuantity recipeProductQuantity in recipe.OutputsAtEnd)
    //        {
    //            int num4 = recipeProductQuantity.Buffer.UsableCapacity.Min(recipeProductQuantity.Quantity).Value / recipeProductQuantity.BaseFraction.Value;
    //            num1 = num1.Min(num4);
    //        }
    //        if (num1 <= 0)
    //            return customMachine.State.OutputFull;
    //        Assert.That<int>(num1).IsLessOrEqual(recipe.QuantitiesGcd);
    //        utilization = Percent.FromRatio(num1, recipe.QuantitiesGcd);
    //        if (utilization < recipe.MinUtilization)
    //            return customMachine.State.OutputFull;
    //        multiplier = num1;
    //        return customMachine.State.Working;
    //    }

    //    protected virtual customMachine.State TryGetNewWork(out Percent machineUtilization)
    //    {
    //        machineUtilization = Percent.Zero;
    //        if (this.m_recipesFast.IsEmpty)
    //            return customMachine.State.NoRecipes;
    //        if (this.m_hasNoNeedToSearchForRecipe)
    //            return this.m_statusWhenNoNeedForRecipe;
    //        this.m_recipesWithFullOutput.Clear();
    //        RecipeWrapper? nullable = new RecipeWrapper?();
    //        int num = 1;
    //        customMachine.State newWork = customMachine.State.Working;
    //        foreach (RecipeWrapper recipe in this.m_recipesFast)
    //        {
    //            Percent utilization;
    //            int multiplier;
    //            customMachine.State state = this.canStartRecipe(recipe, out utilization, out multiplier);
    //            if (newWork != customMachine.State.OutputFull || state != customMachine.State.NotEnoughInput)
    //                newWork = state;
    //            if (state == customMachine.State.OutputFull && recipe.RecipesWithSameOutputs.IsNotEmpty)
    //                this.m_recipesWithFullOutput.Add(recipe.Recipe);
    //            else if (state == customMachine.State.Working && !(utilization <= machineUtilization))
    //            {
    //                if (this.m_recipesWithFullOutput.IsNotEmpty && recipe.RecipesWithSameOutputs.IsNotEmpty)
    //                {
    //                    bool flag = false;
    //                    foreach (RecipeProto recipeProto in this.m_recipesWithFullOutput)
    //                    {
    //                        if (recipe.RecipesWithSameOutputs.Contains(recipeProto))
    //                        {
    //                            flag = true;
    //                            break;
    //                        }
    //                    }
    //                    if (flag)
    //                        continue;
    //                }
    //                machineUtilization = utilization;
    //                nullable = new RecipeWrapper?(recipe);
    //                num = multiplier;
    //                if (utilization == Percent.Hundred)
    //                    break;
    //            }
    //        }
    //        if (!nullable.HasValue)
    //        {
    //            this.m_hasNoNeedToSearchForRecipe = !this.UsesVirtualLimitedBuffers;
    //            this.m_statusWhenNoNeedForRecipe = newWork;
    //            return newWork;
    //        }
    //        RecipeWrapper recipeWrapper = nullable.Value;
    //        Lyst<ProductQuantity> inputs = customMachine.Cache.InputsUsed.ClearAndReturn();
    //        Lyst<ProductQuantity> outputs = customMachine.Cache.OutputsCreated.ClearAndReturn();
    //        foreach (RecipeProductQuantity allInput in recipeWrapper.AllInputs)
    //        {
    //            Quantity quantity1 = num * allInput.BaseFraction;
    //            Quantity quantity2 = allInput.Buffer.RemoveAsMuchAs(quantity1);
    //            inputs.Add(allInput.Buffer.Product.WithQuantity(quantity2));
    //            Assert.That<Quantity>(quantity2).IsEqualTo(quantity1);
    //        }
    //        foreach (RecipeProductQuantity recipeProductQuantity in recipeWrapper.OutputsAtStart)
    //        {
    //            Quantity quantity3 = num * recipeProductQuantity.BaseFraction;
    //            Quantity quantity4 = recipeProductQuantity.Buffer.StoreAsMuchAsReturnStored(quantity3);
    //            outputs.Add(recipeProductQuantity.Buffer.Product.WithQuantity(quantity4));
    //            Assert.That<Quantity>(quantity4).IsEqualTo(quantity3);
    //        }
    //        if (this.m_recipeResult.HasResult)
    //        {
    //            Log.Error("Previous result not cleared?");
    //            this.m_recipeResult.Clear();
    //        }
    //        this.m_recipeResult.SetRecipe(recipeWrapper.Recipe, this.DurationMultiplier);
    //        for (int index = 0; index < recipeWrapper.OutputsAtEnd.Length; ++index)
    //        {
    //            IProductBuffer buffer = recipeWrapper.OutputsAtEnd[index].Buffer;
    //            Quantity quantity = num * recipeWrapper.OutputsAtEnd[index].BaseFraction;
    //            if (buffer.Product.Type == VirtualProductProto.ProductType)
    //                quantity = quantity.ScaledBy(this.VirtualOutputMultiplier);
    //            ProductQuantity productQuantity = buffer.Product.WithQuantity(quantity);
    //            outputs.Add(productQuantity);
    //            this.m_recipeResult.ProducedOutputs.Add(productQuantity);
    //            this.m_recipeResult.Buffers.Add(buffer);
    //        }
    //        this.m_productsManager.ReportProductsTransformation((IIndexable<ProductQuantity>)inputs, (IIndexable<ProductQuantity>)outputs, recipeWrapper.Recipe.ProductsDestroyReason, CreateReason.Produced, recipeWrapper.Recipe.DisableSourceProductsConversionLoss);
    //        if (this.m_electricityConsumer.HasValue && this.m_electricityConsumer.Value.PowerRequired != this.PowerRequired)
    //            this.m_electricityConsumer.Value.OnPowerRequiredChanged();
    //        return customMachine.State.Working;
    //    }

    //    public void AssignRecipes(IEnumerable<RecipeProto> recipes)
    //    {
    //        foreach (RecipeProto recipe in recipes)
    //        {
    //            if (!this.m_recipesAssigned.AddIfNotPresent(recipe))
    //            {
    //                Log.Error("Recipe already added!");
    //                return;
    //            }
    //        }
    //        this.rebuildRecipes(false);
    //    }

    //    public void AssignRecipe(RecipeProto recipe)
    //    {
    //        if (recipe.IsNotAvailable)
    //            Log.Error("Recipe not available!");
    //        else if (!this.m_recipesAssigned.AddIfNotPresent(recipe))
    //            Log.Error("Recipe already added!");
    //        else
    //            this.rebuildRecipes(false);
    //    }

    //    public void RemoveAssignedRecipe(RecipeProto recipe)
    //    {
    //        if (!this.m_recipesAssigned.Remove(recipe))
    //            Log.Error("Recipe not found!");
    //        else
    //            this.rebuildRecipes(false);
    //    }

    //    public void ClearAssignedRecipes()
    //    {
    //        this.m_recipesAssigned.Clear();
    //        this.rebuildRecipes(false);
    //    }

    //    /// <summary>
    //    /// Set fullRebuild = true, if Proto changed or game was loaded.
    //    /// </summary>
    //    private void rebuildRecipes(bool fullRebuild)
    //    {
    //        this.m_hasNoNeedToSearchForRecipe = false;
    //        this.rebuildOutputBuffers(fullRebuild);
    //        this.rebuildInputBuffers(fullRebuild);
    //        this.m_assignedRecipesCount = this.m_recipesAssigned.Count;
    //        this.m_recipesFast.Clear();
    //        Lyst<RecipeProto> lyst = customMachine.Cache.Recipes.ClearAndReturn();
    //        foreach (RecipeProto recipe in this.m_recipesAssigned)
    //        {
    //            foreach (RecipeProto recipeProto in this.m_recipesAssigned)
    //            {
    //                RecipeProto otherRecipe = recipeProto;
    //                if (!((Proto)recipe == (Proto)otherRecipe) && otherRecipe.AllOutputs.Length == recipe.AllOutputs.Length && recipe.AllOutputs.All((Func<RecipeOutput, bool>)(output => otherRecipe.AllOutputs.Any((Func<RecipeOutput, bool>)(x => (Proto)x.Product == (Proto)output.Product)))))
    //                    lyst.Add(otherRecipe);
    //            }
    //            this.m_recipesFast.Add(new RecipeWrapper(recipe, this, lyst.ToImmutableArrayAndClear()));
    //        }
    //        lyst.Clear();
    //    }

    //    public void SetLogisticsInputMode(EntityLogisticsMode mode)
    //    {
    //        if (this.LogisticsInputMode == mode)
    //            return;
    //        this.LogisticsInputMode = mode;
    //        if (this.LogisticsInputMode == EntityLogisticsMode.Off)
    //        {
    //            foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //                inputBuffer.UnregisterFromLogistics(true);
    //        }
    //        else
    //        {
    //            foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //            {
    //                if (!inputBuffer.IsNotUsedByCurrentRecipes)
    //                    inputBuffer.RegisterBufferToLogistics(true);
    //            }
    //        }
    //    }

    //    private void onInputPortConnectionChanged(IoPort port)
    //    {
    //        if (port.IsConnected || this.LogisticsInputMode != EntityLogisticsMode.Auto)
    //            return;
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //        {
    //            if (!(inputBuffer.Product.Type != port.ShapePrototype.AllowedProductType) && !inputBuffer.IsNotUsedByCurrentRecipes)
    //                inputBuffer.RegisterBufferToLogistics(true);
    //        }
    //    }

    //    private void rebuildInputBuffers(bool isUpgrade)
    //    {
    //        if (isUpgrade)
    //        {
    //            Set<ProductProto> set = customMachine.Cache.ProductsSet.ClearAndReturn();
    //            foreach (RecipeProto recipe in this.Prototype.Recipes)
    //                set.AddRange(recipe.AllInputs.Select<ProductProto>((Func<RecipeInput, ProductProto>)(x => x.Product)));
    //            Lyst<customMachineInputBuffer> lyst = customMachine.Cache.InputBuffers.ClearAndReturn();
    //            foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //            {
    //                if (!set.Contains(inputBuffer.Product))
    //                    lyst.Add(inputBuffer);
    //            }
    //            foreach (customMachineInputBuffer buffer in lyst)
    //            {
    //                this.Context.AssetTransactionManager.ClearAndDestroyBuffer((IProductBuffer)buffer);
    //                this.m_inputBuffers.TryRemoveReplaceLast(buffer);
    //            }
    //        }
    //        Set<ProductProto> set1 = customMachine.Cache.ProductsSet.ClearAndReturn();
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //        {
    //            if (!inputBuffer.IsNotUsedByCurrentRecipes)
    //                set1.Add(inputBuffer.Product);
    //        }
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //            inputBuffer.MinCapacity = Quantity.Zero;
    //        foreach (RecipeProto recipeProto in this.m_recipesAssigned)
    //        {
    //            foreach (RecipeInput allInput in recipeProto.AllInputs)
    //            {
    //                if (!(allInput.Product.Type == VirtualProductProto.ProductType))
    //                {
    //                    foreach (IoPortTemplate port in allInput.Ports)
    //                        Assert.That<IoPortType>(port.Type).IsEqualTo<IoPortType>(IoPortType.Input);
    //                    customMachineInputBuffer result;
    //                    if (!this.tryGetInputBuffer(allInput.Product, out result))
    //                    {
    //                        result = new customMachineInputBuffer(Quantity.One, allInput.Product, this);
    //                        this.m_inputBuffers.Add(result);
    //                    }
    //                    result.MinCapacity = result.MinCapacity.Max(allInput.Quantity);
    //                }
    //            }
    //        }
    //        Lyst<customMachineInputBuffer> lyst1 = customMachine.Cache.InputBuffers.ClearAndReturn();
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //        {
    //            if (inputBuffer.IsNotUsedByCurrentRecipes)
    //            {
    //                if (inputBuffer.IsEmpty)
    //                    lyst1.Add(inputBuffer);
    //                else
    //                    inputBuffer.UnregisterFromLogistics(false);
    //            }
    //            else if (!set1.Contains(inputBuffer.Product) && this.LogisticsInputMode != EntityLogisticsMode.Off)
    //                inputBuffer.RegisterBufferToLogistics(false);
    //        }
    //        foreach (customMachineInputBuffer machineInputBuffer in lyst1)
    //        {
    //            machineInputBuffer.Destroy();
    //            this.m_inputBuffers.TryRemoveReplaceLast(machineInputBuffer);
    //        }
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //            inputBuffer.UpdateCapacity();
    //    }

    //    public bool tryGetInputBuffer(ProductProto product, out customMachineInputBuffer result)
    //    {
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //        {
    //            if ((Proto)inputBuffer.Product == (Proto)product)
    //            {
    //                result = inputBuffer;
    //                return true;
    //            }
    //        }
    //        result = (customMachineInputBuffer)null;
    //        return false;
    //    }

    //    /// <summary>Called by ports to store products.</summary>
    //    Quantity IEntityWithPorts.ReceiveAsMuchAsFromPort(ProductQuantity pq, IoPortToken sourcePort)
    //    {
    //        customMachineInputBuffer result;
    //        if (pq.IsEmpty || !this.IsEnabled || !this.tryGetInputBuffer(pq.Product, out result) || result.IsNotUsedByCurrentRecipes)
    //            return pq.Quantity;
    //        if (this.LogisticsInputMode == EntityLogisticsMode.Auto)
    //            result.UnregisterFromLogistics(true);
    //        return result.StoreAsMuchAs(pq.Quantity);
    //    }

    //    public void SetLogisticsOutputMode(EntityLogisticsMode mode)
    //    {
    //        if (this.LogisticsOutputMode == mode)
    //            return;
    //        this.LogisticsOutputMode = mode;
    //        if (this.LogisticsOutputMode == EntityLogisticsMode.Off)
    //        {
    //            foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //                outputBuffer.UnregisterFromLogistics(true);
    //        }
    //        else
    //        {
    //            foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //                outputBuffer.RegisterBufferToLogistics(true);
    //        }
    //        this.m_hasNoNeedToSearchForRecipe = false;
    //    }

    //    /// <summary>
    //    /// Sends products in buffers through output ports, if possible.
    //    /// </summary>
    //    private void sendOutputs()
    //    {
    //        if (this.m_allConnectedOutputsBuffersEmpty && this.m_productInNeedOfTransport.IsNone)
    //            return;
    //        bool flag = true;
    //        Option<ProductProto> option = Option<ProductProto>.None;
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //        {
    //            if (!outputBuffer.IsEmpty)
    //            {
    //                if (!outputBuffer.IsAnyPortConnected)
    //                {
    //                    if (!outputBuffer.IsRegisteredToLogistics && !outputBuffer.IsNotUsedByCurrentRecipes)
    //                        option = (Option<ProductProto>)outputBuffer.Product;
    //                }
    //                else
    //                {
    //                    outputBuffer.Send();
    //                    flag &= outputBuffer.IsEmpty;
    //                }
    //            }
    //        }
    //        this.m_allConnectedOutputsBuffersEmpty = flag;
    //        this.m_productInNeedOfTransport = option;
    //    }

    //    public bool tryGetOutputBuffer(ProductProto product, out customMachineOutputBuffer result)
    //    {
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //        {
    //            if ((Proto)outputBuffer.Product == (Proto)product)
    //            {
    //                result = outputBuffer;
    //                return true;
    //            }
    //        }
    //        result = (customMachineOutputBuffer)null;
    //        return false;
    //    }

    //    private void rebuildOutputBuffers(bool isUpgrade)
    //    {
    //        if (isUpgrade)
    //        {
    //            Set<ProductProto> set = customMachine.Cache.ProductsSet.ClearAndReturn();
    //            foreach (RecipeProto recipe in this.Prototype.Recipes)
    //                set.AddRange(recipe.AllOutputs.Select<ProductProto>((Func<RecipeOutput, ProductProto>)(x => x.Product)));
    //            Lyst<customMachineOutputBuffer> lyst = customMachine.Cache.OutputBuffers.ClearAndReturn();
    //            foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //            {
    //                if (!set.Contains(outputBuffer.Product))
    //                    lyst.Add(outputBuffer);
    //            }
    //            foreach (customMachineOutputBuffer buffer in lyst)
    //                this.clearAndRemoveOutputBuffer(buffer);
    //            lyst.Clear();
    //        }
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //        {
    //            outputBuffer.MinCapacity = Quantity.Zero;
    //            outputBuffer.ClearAllPorts();
    //        }
    //        foreach (RecipeProto recipeProto in this.m_recipesAssigned)
    //        {
    //            foreach (RecipeOutput allOutput in recipeProto.AllOutputs)
    //            {
    //                if (!(allOutput.Product.Type == VirtualProductProto.ProductType))
    //                {
    //                    customMachineOutputBuffer result;
    //                    if (!this.tryGetOutputBuffer(allOutput.Product, out result))
    //                    {
    //                        result = new customMachineOutputBuffer(Quantity.One, allOutput.Product, this.m_productsManager, this);
    //                        this.m_outputBuffers.Add(result);
    //                    }
    //                    result.MinCapacity = result.MinCapacity.Max(allOutput.Quantity);
    //                    foreach (IoPortTemplate port in allOutput.Ports)
    //                    {
    //                        IoPortTemplate portProto = port;
    //                        Assert.That<IoPortType>(portProto.Type).IsEqualTo<IoPortType>(IoPortType.Output);
    //                        IoPort orDefault = this.Ports.FindOrDefault((Predicate<IoPort>)(x => (int)x.Name == (int)portProto.Name));
    //                        if (orDefault != null)
    //                            result.AddPort(orDefault);
    //                    }
    //                }
    //            }
    //        }
    //        Lyst<customMachineOutputBuffer> lyst1 = customMachine.Cache.OutputBuffers.ClearAndReturn();
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //        {
    //            if (outputBuffer.IsNotUsedByCurrentRecipes && outputBuffer.Quantity.IsZero)
    //            {
    //                lyst1.Add(outputBuffer);
    //            }
    //            else
    //            {
    //                if (this.LogisticsOutputMode != EntityLogisticsMode.Off && !outputBuffer.IsAnyPortConnected)
    //                    outputBuffer.RegisterBufferToLogistics(false);
    //                outputBuffer.UpdateCapacity();
    //            }
    //        }
    //        foreach (customMachineOutputBuffer buffer in lyst1)
    //            this.clearAndRemoveOutputBuffer(buffer);
    //        lyst1.Clear();
    //    }

    //    private void clearAndRemoveOutputBuffer(customMachineOutputBuffer buffer)
    //    {
    //        this.Context.AssetTransactionManager.ClearAndDestroyBuffer((IProductBuffer)buffer);
    //        this.m_outputBuffers.TryRemoveReplaceLast(buffer);
    //        if (!this.m_recipeResult.HasResult)
    //            return;
    //        foreach (IProductBuffer buffer1 in this.m_recipeResult.Buffers)
    //        {
    //            if (buffer == buffer1)
    //            {
    //                this.destroyRecipeResult();
    //                break;
    //            }
    //        }
    //    }

    //    private void onOutputPortConnectionChanged(IoPort port)
    //    {
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //            outputBuffer.OnSomePortConnectionChanged(port);
    //    }

    //    bool IUpgradableEntity.TryReplaceSelf(
    //      IProtoWithUpgrade newProto,
    //      bool dryRun,
    //      out LocStrFormatted errorMessage)
    //    {
    //        errorMessage = LocStrFormatted.Empty;
    //        if (!(newProto is customMachineProto machineProto) || this.Prototype.GetType() != newProto.GetType())
    //            return false;
    //        if (!dryRun)
    //            this.Prototype = machineProto;
    //        return true;
    //    }

    //    protected override void OnUpgradeDone(IEntityProto oldProto, IProtoWithUpgrade newProto)
    //    {
    //        base.OnUpgradeDone(oldProto, newProto);
    //        Set<RecipeProto> items = new Set<RecipeProto>();
    //        foreach (RecipeProto source in this.m_recipesAssigned)
    //        {
    //            Option<RecipeProto> bestRecipeMatch = MachineUtils.GetBestRecipeMatch(source, this.Prototype.Recipes);
    //            if (bestRecipeMatch.HasValue)
    //                items.Add(bestRecipeMatch.Value);
    //        }
    //        this.destroyRecipeResult();
    //        this.m_recipesAssigned.Clear();
    //        this.m_recipesAssigned.AddRange((IEnumerable<RecipeProto>)items);
    //        this.rebuildRecipes(true);
    //        if (this.IsBoostRequested && !this.Prototype.BoostCost.HasValue)
    //            this.SetBoosted(false);
    //        this.m_electricityConsumer = this.Context.ElectricityConsumerFactory.CreateConsumerIfNeeded((IElectricityConsumingEntity)this, this.m_electricityConsumer);
    //        this.m_computingConsumer = this.Context.ComputingConsumerFactory.CreateConsumerIfNeeded((IComputingConsumingEntity)this, this.m_computingConsumer);
    //    }

    //    private void destroyRecipeResult()
    //    {
    //        if (!this.m_recipeResult.HasResult)
    //            return;
    //        foreach (ProductQuantity producedOutput in this.m_recipeResult.ProducedOutputs)
    //            this.m_productsManager.ClearProductNoChecks(producedOutput);
    //        this.m_recipeResult.Clear();
    //    }

    //    protected override void OnDestroy()
    //    {
    //        this.destroyRecipeResult();
    //        this.m_recipesAssigned.Clear();
    //        foreach (IProductBuffer inputBuffer in this.m_inputBuffers)
    //            this.Context.AssetTransactionManager.ClearAndDestroyBuffer(inputBuffer);
    //        this.m_inputBuffers.Clear();
    //        foreach (IProductBuffer outputBuffer in this.m_outputBuffers)
    //            this.Context.AssetTransactionManager.ClearAndDestroyBuffer(outputBuffer);
    //        this.m_outputBuffers.Clear();
    //        base.OnDestroy();
    //    }

    //    public void ReorderRecipe(int oldIndex, int newIndexAfterRemove)
    //    {
    //        if (oldIndex < 0 || oldIndex >= this.RecipesAssigned.Count)
    //            Log.Error(string.Format("Recipe index '{0}' out of range!", (object)oldIndex));
    //        else if (newIndexAfterRemove < 0 || newIndexAfterRemove >= this.RecipesAssigned.Count)
    //        {
    //            Log.Error(string.Format("Recipe index '{0}' out of range!", (object)newIndexAfterRemove));
    //        }
    //        else
    //        {
    //            RecipeProto recipeProto = this.m_recipesAssigned[oldIndex];
    //            this.m_recipesAssigned.RemoveAt(oldIndex);
    //            this.m_recipesAssigned.Insert(newIndexAfterRemove, recipeProto);
    //            RecipeWrapper recipeWrapper = this.m_recipesFast[oldIndex];
    //            this.m_recipesFast.RemoveAt(oldIndex);
    //            this.m_recipesFast.Insert(newIndexAfterRemove, recipeWrapper);
    //        }
    //    }

    //    public void GetUnusedBuffersToClear(Lyst<ProductQuantity> result)
    //    {
    //        foreach (customMachineInputBuffer inputBuffer in this.m_inputBuffers)
    //        {
    //            if (inputBuffer.IsNotUsedByCurrentRecipes && inputBuffer.Product.Type != VirtualProductProto.ProductType && inputBuffer.IsNotEmpty)
    //                result.Add(inputBuffer.ProductQuantity);
    //        }
    //        foreach (customMachineOutputBuffer outputBuffer in this.m_outputBuffers)
    //        {
    //            if (outputBuffer.IsNotUsedByCurrentRecipes && outputBuffer.Product.Type != VirtualProductProto.ProductType && outputBuffer.IsNotEmpty)
    //                result.Add(outputBuffer.ProductQuantity);
    //        }
    //    }

    //    public bool ClearUnusedProducts()
    //    {
    //        foreach (customMachineInputBuffer buffer in this.m_inputBuffers.ToArray())
    //        {
    //            if (buffer.IsNotUsedByCurrentRecipes && buffer.Product.Type != VirtualProductProto.ProductType && buffer.IsNotEmpty)
    //            {
    //                this.Context.AssetTransactionManager.ClearAndDestroyBuffer((IProductBuffer)buffer);
    //                this.m_inputBuffers.TryRemoveReplaceLast(buffer);
    //            }
    //        }
    //        foreach (customMachineOutputBuffer buffer in this.m_outputBuffers.ToArray())
    //        {
    //            if (buffer.IsNotUsedByCurrentRecipes && buffer.Product.Type != VirtualProductProto.ProductType && buffer.IsNotEmpty)
    //                this.clearAndRemoveOutputBuffer(buffer);
    //        }
    //        return false;
    //    }

    //    public virtual void AddToConfig(EntityConfigData data)
    //    {
    //        if (!this.RecipesAssigned.IsNotEmpty<RecipeProto>())
    //            return;
    //        data.Recipes = new ImmutableArray<RecipeProto>?(this.RecipesAssigned.ToImmutableArray<RecipeProto>());
    //    }

    //    public virtual void ApplyConfig(EntityConfigData data)
    //    {
    //        if (data.Prototype.IsNone || !UpgradeHelper.AreProtosInSameUpgradeChain((IProto)this.Prototype, (IProto)data.Prototype.Value))
    //            return;
    //        ImmutableArray<RecipeProto> immutableArray = data.Recipes ?? ImmutableArray<RecipeProto>.Empty;
    //        this.m_recipesAssigned.Clear();
    //        foreach (RecipeProto source in immutableArray)
    //        {
    //            Option<RecipeProto> bestRecipeMatch = MachineUtils.GetBestRecipeMatch(source, this.Prototype.Recipes);
    //            if (bestRecipeMatch.HasValue)
    //                this.m_recipesAssigned.Add(bestRecipeMatch.Value);
    //        }
    //        this.rebuildRecipes(false);
    //    }

    //    protected override void OnEnabledChanged()
    //    {
    //        base.OnEnabledChanged();
    //        if (this.IsEnabled)
    //            return;
    //        this.m_vehicleBuffersRegistry.ClearAndCancelAllJobs((IStaticEntity)this);
    //    }

    //    public Quantity GetInputQuantityFor(ProductProto product)
    //    {
    //        customMachineInputBuffer result;
    //        return !this.tryGetInputBuffer(product, out result) ? Quantity.Zero : result.Quantity;
    //    }

    //    public Quantity GetInputCapacityFor(ProductProto product)
    //    {
    //        customMachineInputBuffer result;
    //        return !this.tryGetInputBuffer(product, out result) ? Quantity.Zero : result.Capacity;
    //    }

    //    public Quantity GetOutputQuantityFor(ProductProto product)
    //    {
    //        customMachineOutputBuffer result;
    //        return !this.tryGetOutputBuffer(product, out result) ? Quantity.Zero : result.Quantity;
    //    }

    //    public Quantity GetOutputCapacityFor(ProductProto product)
    //    {
    //        customMachineOutputBuffer result;
    //        return !this.tryGetOutputBuffer(product, out result) ? Quantity.Zero : result.Capacity;
    //    }

    //    public Percent ProgressOnRecipe(IRecipeForUi recipe)
    //    {
    //        if (this.LastRecipeInProgress.ValueOrNull != recipe)
    //        {
    //            return Percent.Zero;
    //        }
    //        return this.ProgressPerc;
    //    }
    //    public Duration GetTargetDurationFor(IRecipeForUi recipe)
    //    {
    //        Duration duration = recipe.Duration.ScaledBy(this.DurationMultiplier);
    //        return !this.IsBoosted ? duration : duration / 2;
    //    }

    //    protected void SetBaseSpeedFactor(Percent speedFactorBase)
    //    {
    //        if (this.m_speedFactorBase == speedFactorBase)
    //            return;
    //        if (speedFactorBase.IsNotPositive)
    //        {
    //            Log.Error(string.Format("Non positive base speed factor {0} is not allowed!", (object)speedFactorBase));
    //        }
    //        else
    //        {
    //            this.m_speedFactorBase = speedFactorBase;
    //            this.updateSpeedFactor();
    //        }
    //    }

    //    private void setSecondarySpeedFactor(Percent speedFactor)
    //    {
    //        if (this.m_speedFactorSecondary == speedFactor)
    //            return;
    //        if (speedFactor.IsNotPositive)
    //        {
    //            Log.Error(string.Format("Non positive speed factor {0} is not allowed!", (object)speedFactor));
    //        }
    //        else
    //        {
    //            this.m_speedFactorSecondary = speedFactor;
    //            this.updateSpeedFactor();
    //        }
    //    }

    //    private void updateSpeedFactor()
    //    {
    //        Percent percent = this.m_speedFactorBase.ScaleBy(this.m_speedFactorSecondary);
    //        if (!this.m_recipeResult.HasResult)
    //        {
    //            this.SpeedFactor = percent;
    //        }
    //        else
    //        {
    //            if (!this.SpeedFactor.IsNearHundred)
    //                this.m_recipeResult.DurationDone = this.m_recipeResult.DurationDone.ScaledBy(this.SpeedFactor);
    //            this.SpeedFactor = percent;
    //            this.m_recipeResult.Duration = this.m_recipeResult.Recipe.Value.Duration.ScaledBy(this.DurationMultiplier);
    //            this.m_recipeResult.DurationDone = this.m_recipeResult.DurationDone.ScaledBy(this.DurationMultiplier);
    //        }
    //    }

    //    public virtual LocStrFormatted GetSlowDownMessageForUi()
    //    {
    //        if (this.SpeedFactor >= Percent.Hundred)
    //            return LocStrFormatted.Empty;
    //        string str = Tr.SpeedReduced__Machine.Format(this.SpeedFactor.ToStringRounded()).Value;
    //        IElectricityConsumerReadonly valueOrNull1 = this.ElectricityConsumer.ValueOrNull;
    //        if ((valueOrNull1 != null ? (valueOrNull1.NotEnoughPower ? 1 : 0) : 0) != 0)
    //            str += string.Format("\n- {0}", (object)Tr.EntityStatus__LowPower);
    //        IComputingConsumerReadonly valueOrNull2 = this.ComputingConsumer.ValueOrNull;
    //        if ((valueOrNull2 != null ? (valueOrNull2.NotEnoughComputing ? 1 : 0) : 0) != 0)
    //            str += string.Format("\n- {0}", (object)Tr.EntityStatus__NoComputing);
    //        if (this.Maintenance.Status.IsBroken)
    //            str += string.Format("\n- {0}", (object)Tr.EntityStatus__Broken);
    //        return new LocStrFormatted(str);
    //    }

    //    internal Option<IProductBufferReadOnly> GetInternalOutputBufferFor(ProductProto product)
    //    {
    //        customMachineOutputBuffer result;
    //        return this.tryGetOutputBuffer(product, out result) ? (Option<IProductBufferReadOnly>)result : Option<IProductBufferReadOnly>.None;
    //    }

    //    public static void Serialize(customMachine value, BlobWriter writer)
    //    {
    //        if (!writer.TryStartClassSerialization<customMachine>(value))
    //            return;
    //        writer.EnqueueDataSerialization((object)value, customMachine.s_serializeDataDelayedAction);
    //    }

    //    protected override void SerializeData(BlobWriter writer)
    //    {
    //        base.SerializeData(writer);
    //        AnimationStatesProvider.Serialize(this.AnimationStatesProvider, writer);
    //        Option<Mafi.Core.Population.UnityConsumer>.Serialize(this.UnityConsumer, writer);
    //        writer.WriteInt((int)this.CurrentState);
    //        writer.WriteBool(this.IsBoosted);
    //        writer.WriteBool(this.IsBoostRequested);
    //        Option<RecipeProto>.Serialize(this.LastRecipeInProgress, writer);
    //        writer.WriteInt((int)this.LogisticsInputMode);
    //        writer.WriteInt((int)this.LogisticsOutputMode);
    //        Option<IComputingConsumer>.Serialize(this.m_computingConsumer, writer);
    //        Option<IInputBufferPriorityProvider>.Serialize(this.m_customStrategy, writer);
    //        Option<IElectricityConsumer>.Serialize(this.m_electricityConsumer, writer);
    //        EntityNotificator.Serialize(this.m_entityBoostedNotif, writer);
    //        LystStruct<customMachineInputBuffer>.Serialize(this.m_inputBuffers, writer);
    //        Percent.Serialize(this.m_lastWorkUtilization, writer);
    //        Duration.Serialize(this.m_lowComputingChargeLeft, writer);
    //        Duration.Serialize(this.m_lowPowerChargeLeft, writer);
    //        EntityNotificatorWithProtoParam.Serialize(this.m_needsTransportNotif, writer);
    //        EntityNotificator.Serialize(this.m_noRecipeSelectedNotif, writer);
    //        LystStruct<customMachineOutputBuffer>.Serialize(this.m_outputBuffers, writer);
    //        Option<ProductProto>.Serialize(this.m_productInNeedOfTransport, writer);
    //        ProductivityCounter.Serialize(this.m_productivityCounter, writer);
    //        writer.WriteGeneric<IProductsManager>(this.m_productsManager);
    //        writer.WriteGeneric<customMachineProto>(this.m_proto);
    //        customMachine.RecipeResult.Serialize(this.m_recipeResult, writer);
    //        Lyst<RecipeProto>.Serialize(this.m_recipesAssigned, writer);
    //        Percent.Serialize(this.m_speedFactorBase, writer);
    //        Percent.Serialize(this.m_speedFactorSecondary, writer);
    //        writer.WriteGeneric<IVehicleBuffersRegistry>(this.m_vehicleBuffersRegistry);
    //        VirtualBuffersMap.Serialize(this.m_virtualBuffersMap, writer);
    //        writer.WriteGeneric<IEntityMaintenanceProvider>(this.Maintenance);
    //        Percent.Serialize(this.SpeedFactor, writer);
    //        Percent.Serialize(this.VirtualOutputMultiplier, writer);
    //    }

    //    public static customMachine Deserialize(BlobReader reader)
    //    {
    //        customMachine machine;
    //        if (reader.TryStartClassDeserialization<customMachine>(out machine))
    //            reader.EnqueueDataDeserialization((object)machine, customMachine.s_deserializeDataDelayedAction);
    //        return machine;
    //    }

    //    protected override void DeserializeData(BlobReader reader)
    //    {
    //        base.DeserializeData(reader);
    //        this.AnimationStatesProvider = AnimationStatesProvider.Deserialize(reader);
    //        this.UnityConsumer = Option<Mafi.Core.Population.UnityConsumer>.Deserialize(reader);
    //        this.CurrentState = (customMachine.State)reader.ReadInt();
    //        this.IsBoosted = reader.ReadBool();
    //        this.IsBoostRequested = reader.ReadBool();
    //        this.LastRecipeInProgress = Option<RecipeProto>.Deserialize(reader);
    //        this.LogisticsInputMode = (EntityLogisticsMode)reader.ReadInt();
    //        this.LogisticsOutputMode = (EntityLogisticsMode)reader.ReadInt();
    //        this.m_computingConsumer = Option<IComputingConsumer>.Deserialize(reader);
    //        reader.SetField<customMachine>(this, "m_customStrategy", (object)Option<IInputBufferPriorityProvider>.Deserialize(reader));
    //        this.m_electricityConsumer = Option<IElectricityConsumer>.Deserialize(reader);
    //        this.m_entityBoostedNotif = EntityNotificator.Deserialize(reader);
    //        if (reader.LoadedSaveVersion < 194)
    //            reader.ReadGenericAs<IProperty<bool>>();
    //        this.m_inputBuffers = LystStruct<customMachineInputBuffer>.Deserialize(reader);
    //        this.m_lastWorkUtilization = Percent.Deserialize(reader);
    //        this.m_lowComputingChargeLeft = reader.LoadedSaveVersion >= 140 ? Duration.Deserialize(reader) : new Duration();
    //        this.m_lowPowerChargeLeft = reader.LoadedSaveVersion >= 140 ? Duration.Deserialize(reader) : new Duration();
    //        this.m_needsTransportNotif = EntityNotificatorWithProtoParam.Deserialize(reader);
    //        this.m_noRecipeSelectedNotif = EntityNotificator.Deserialize(reader);
    //        this.m_outputBuffers = LystStruct<customMachineOutputBuffer>.Deserialize(reader);
    //        this.m_productInNeedOfTransport = Option<ProductProto>.Deserialize(reader);
    //        this.m_productivityCounter = reader.LoadedSaveVersion >= 263 ? ProductivityCounter.Deserialize(reader) : new ProductivityCounter(this.Id.Value);
    //        reader.SetField<customMachine>(this, "m_productsManager", (object)reader.ReadGenericAs<IProductsManager>());
    //        this.m_proto = reader.ReadGenericAs<customMachineProto>();
    //        this.m_recipeResult = customMachine.RecipeResult.Deserialize(reader);
    //        reader.SetField<customMachine>(this, "m_recipesAssigned", (object)Lyst<RecipeProto>.Deserialize(reader));
    //        this.m_recipesFast = new LystStruct<RecipeWrapper>();
    //        this.m_recipesWithFullOutput = new LystStruct<RecipeProto>();
    //        this.m_speedFactorBase = reader.LoadedSaveVersion >= 140 ? Percent.Deserialize(reader) : Percent.Hundred;
    //        this.m_speedFactorSecondary = reader.LoadedSaveVersion >= 140 ? Percent.Deserialize(reader) : Percent.Hundred;
    //        reader.SetField<customMachine>(this, "m_vehicleBuffersRegistry", (object)reader.ReadGenericAs<IVehicleBuffersRegistry>());
    //        reader.SetField<customMachine>(this, "m_virtualBuffersMap", (object)VirtualBuffersMap.Deserialize(reader));
    //        this.Maintenance = reader.ReadGenericAs<IEntityMaintenanceProvider>();
    //        this.SpeedFactor = reader.LoadedSaveVersion >= 140 ? Percent.Deserialize(reader) : Percent.Hundred;
    //        if (reader.LoadedSaveVersion < 241)
    //            reader.ReadGenericAs<IUpgrader>();
    //        this.VirtualOutputMultiplier = reader.LoadedSaveVersion >= 180 ? Percent.Deserialize(reader) : Percent.Hundred;
    //        reader.RegisterInitAfterLoad<customMachine>(this, "initSelf", InitPriority.Normal);
    //    }

    //    static customMachine()
    //    {
    //        //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //        customMachine.s_cache = new ThreadLocal<customMachine.CacheContext>((Func<customMachine.CacheContext>)(() => new customMachine.CacheContext()));
    //        customMachine.PROD_COUNTERS_LABELS = new ProductivityCounterLabels(new ProductivityLabelCategory(Tr.EntityStatus__Working, ColorRgba.DarkGreen, true), new ProductivityLabelCategory(Tr.EntityStatus__MissingInput, ColorRgba.Orange), new ProductivityLabelCategory(Tr.EntityStatus__FullOutput, ColorRgba.CornflowerBlue), new ProductivityLabelCategory(Tr.EntityStatus__CannotWork, ColorRgba.DarkRed));
    //        customMachine.SPEED_WHEN_BROKEN = 50.Percent();
    //        customMachine.MaxDurationWithNoPower = 8.Months();
    //        customMachine.MaxDurationWithNoComputing = 8.Months();
    //        customMachine.s_serializeDataDelayedAction = (Action<object, BlobWriter>)((obj, writer) => ((customMachine)obj).SerializeData(writer));
    //        customMachine.s_deserializeDataDelayedAction = (Action<object, BlobReader>)((obj, reader) => ((customMachine)obj).DeserializeData(reader));
    //    }

    //    public enum State
    //    {
    //        None,
    //        Broken,
    //        Paused,
    //        NotEnoughWorkers,
    //        NotEnoughPower,
    //        NotEnoughComputing,
    //        NotEnoughInput,
    //        InvalidPlacement,
    //        OutputFull,
    //        NoRecipes,
    //        Working,
    //    }

    //    [GenerateSerializer(false, null, 0, null)]
    //    internal struct RecipeResult
    //    {
    //        public LystStruct<ProductQuantity> ProducedOutputs;
    //        public LystStruct<IProductBuffer> Buffers;
    //        public Duration Duration;
    //        public Duration DurationDone;

    //        public bool HasResult => this.Recipe.HasValue;

    //        public bool IsEmpty => this.Recipe.IsNone;

    //        public Option<RecipeProto> Recipe { get; private set; }

    //        public RecipeResult()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            this.ProducedOutputs = new LystStruct<ProductQuantity>();
    //            this.Buffers = new LystStruct<IProductBuffer>();
    //            this.Duration = Duration.Zero;
    //            this.DurationDone = Duration.Zero;
    //            this.Recipe = Option<RecipeProto>.None;
    //        }

    //        public void Clear()
    //        {
    //            this.ProducedOutputs.Clear();
    //            this.Buffers.Clear();
    //            this.Recipe = Option<RecipeProto>.None;
    //            this.Duration = Duration.Zero;
    //            this.DurationDone = Duration.Zero;
    //        }

    //        public void SetRecipe(RecipeProto recipe, Percent durationMultiplier)
    //        {
    //            Assert.That<bool>(this.IsEmpty).IsTrue();
    //            this.Recipe = (Option<RecipeProto>)recipe;
    //            this.Duration = recipe.Duration.ScaledBy(durationMultiplier);
    //        }

    //        public static void Serialize(customMachine.RecipeResult value, BlobWriter writer)
    //        {
    //            LystStruct<IProductBuffer>.Serialize(value.Buffers, writer);
    //            Duration.Serialize(value.Duration, writer);
    //            Duration.Serialize(value.DurationDone, writer);
    //            LystStruct<ProductQuantity>.Serialize(value.ProducedOutputs, writer);
    //            Option<RecipeProto>.Serialize(value.Recipe, writer);
    //        }

    //        public static customMachine.RecipeResult Deserialize(BlobReader reader)
    //        {
    //            return new customMachine.RecipeResult()
    //            {
    //                Buffers = LystStruct<IProductBuffer>.Deserialize(reader),
    //                Duration = Duration.Deserialize(reader),
    //                DurationDone = Duration.Deserialize(reader),
    //                ProducedOutputs = LystStruct<ProductQuantity>.Deserialize(reader),
    //                Recipe = Option<RecipeProto>.Deserialize(reader)
    //            };
    //        }
    //    }

    //    /// <summary>
    //    /// We are using normal priority for waste to not bother logistics. IF anything needs to get
    //    /// rid of waste it will have high prio set on its own.
    //    /// </summary>
    //    [GenerateSerializer(false, "Instance", 0, null)]
    //    public class WasteInputPortPriorityProvider : IInputBufferPriorityProvider
    //    {
    //        public static readonly customMachine.WasteInputPortPriorityProvider Instance;

    //        public BufferStrategy GetInputPriority(IProductBuffer buffer, Quantity pendingQuantity)
    //        {
    //            return new BufferStrategy(15, new Quantity?(buffer.Capacity / 2));
    //        }

    //        public static void Serialize(customMachine.WasteInputPortPriorityProvider value, BlobWriter writer)
    //        {
    //            writer.WriteBool(value != null);
    //        }

    //        public static customMachine.WasteInputPortPriorityProvider Deserialize(BlobReader reader)
    //        {
    //            return !reader.ReadBool() ? (customMachine.WasteInputPortPriorityProvider)null : customMachine.WasteInputPortPriorityProvider.Instance;
    //        }

    //        public WasteInputPortPriorityProvider()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            // ISSUE: explicit constructor call
    //            //base.\u002Ector();
    //        }

    //        static WasteInputPortPriorityProvider()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            customMachine.WasteInputPortPriorityProvider.Instance = new customMachine.WasteInputPortPriorityProvider();
    //        }
    //    }

    //    /// <summary>
    //    /// Provides caches for one-time or init-on-load operations.
    //    /// Instead of having these in memory for each storage we share them.
    //    /// WARNING: Clear them before using them!
    //    /// </summary>
    //    public class CacheContext
    //    {
    //        public readonly Lyst<ProductQuantity> InputsUsed;
    //        public readonly Lyst<ProductQuantity> OutputsCreated;
    //        public readonly Lyst<RecipeProto> Recipes;
    //        public readonly Lyst<customMachineOutputBuffer> OutputBuffers;
    //        public readonly Lyst<customMachineInputBuffer> InputBuffers;
    //        public readonly Set<ProductProto> ProductsSet;

    //        public CacheContext()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            this.InputsUsed = new Lyst<ProductQuantity>(8);
    //            this.OutputsCreated = new Lyst<ProductQuantity>(8);
    //            this.Recipes = new Lyst<RecipeProto>(32);
    //            this.OutputBuffers = new Lyst<customMachineOutputBuffer>(32);
    //            this.InputBuffers = new Lyst<customMachineInputBuffer>(32);
    //            this.ProductsSet = new Set<ProductProto>(32);
    //            // ISSUE: explicit constructor call
    //            //base.\u002Ector();
    //        }
    //    }

    //    [GenerateSerializer(false, null, 0, null)]
    //    public sealed class customMachineInputBuffer : ProductBuffer, IInputBufferPriorityProvider
    //    {
    //        [DoNotSave(0, null)]
    //        public Quantity MinCapacity;
    //        private readonly customMachine m_entity;
    //        private bool m_isRegisteredToLogistics;
    //        public static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;
    //        public static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

    //        public bool IsNotUsedByCurrentRecipes => this.MinCapacity.IsZero;

    //        public customMachineInputBuffer(Quantity capacity, ProductProto product, customMachine entity)
    //            : base(capacity, product)
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            // ISSUE: explicit constructor call
    //            //base.\u002Ector(capacity, product);
    //            this.m_entity = entity;
    //        }

    //        protected override void OnQuantityChanged(Quantity diff)
    //        {
    //            base.OnQuantityChanged(diff);
    //            if (!diff.IsPositive)
    //                return;
    //            this.m_entity.m_hasNoNeedToSearchForRecipe = false;
    //        }

    //        public void RegisterBufferToLogistics(bool updateCapacity)
    //        {
    //            if (this.m_isRegisteredToLogistics)
    //                return;
    //            this.m_entity.m_vehicleBuffersRegistry.TryRegisterInputBuffer((IStaticEntity)this.m_entity, (IProductBuffer)this, this.m_entity.m_customStrategy.ValueOrNull ?? (IInputBufferPriorityProvider)this);
    //            this.m_isRegisteredToLogistics = true;
    //            if (!updateCapacity)
    //                return;
    //            this.UpdateCapacity();
    //        }

    //        public void UnregisterFromLogistics(bool updateCapacity)
    //        {
    //            if (!this.m_isRegisteredToLogistics)
    //                return;
    //            this.m_entity.m_vehicleBuffersRegistry.TryUnregisterInputBuffer((IProductBuffer)this);
    //            this.m_isRegisteredToLogistics = false;
    //            if (!updateCapacity)
    //                return;
    //            this.UpdateCapacity();
    //        }

    //        public void UpdateCapacity()
    //        {
    //            if (this.MinCapacity.IsZero)
    //                return;
    //            this.ForceNewCapacityTo(!this.m_entity.Prototype.BuffersMultiplier.HasValue ? (!this.m_isRegisteredToLogistics ? this.MinCapacity : (4 * this.MinCapacity).Max(2 * TruckCaps.SmallTruckCapacity)) : this.MinCapacity * this.m_entity.Prototype.BuffersMultiplier.Value);
    //        }

    //        BufferStrategy IInputBufferPriorityProvider.GetInputPriority(
    //          IProductBuffer buffer,
    //          Quantity pendingQuantity)
    //        {
    //            return new BufferStrategy(this.m_entity.GeneralPriority, new Quantity?(buffer.Capacity / 2));
    //        }

    //        public override void Destroy()
    //        {
    //            this.UnregisterFromLogistics(false);
    //            base.Destroy();
    //        }

    //        public static void Serialize(customMachine.customMachineInputBuffer value, BlobWriter writer)
    //        {
    //            if (!writer.TryStartClassSerialization<customMachine.customMachineInputBuffer>(value))
    //                return;
    //            writer.EnqueueDataSerialization((object)value, customMachine.customMachineInputBuffer.s_serializeDataDelayedAction);
    //        }

    //        protected override void SerializeData(BlobWriter writer)
    //        {
    //            base.SerializeData(writer);
    //            customMachine.Serialize(this.m_entity, writer);
    //            writer.WriteBool(this.m_isRegisteredToLogistics);
    //        }

    //        public new static customMachine.customMachineInputBuffer Deserialize(BlobReader reader)
    //        {
    //            customMachine.customMachineInputBuffer machineInputBuffer;
    //            if (reader.TryStartClassDeserialization<customMachine.customMachineInputBuffer>(out machineInputBuffer))
    //                reader.EnqueueDataDeserialization((object)machineInputBuffer, customMachine.customMachineInputBuffer.s_deserializeDataDelayedAction);
    //            return machineInputBuffer;
    //        }

    //        protected override void DeserializeData(BlobReader reader)
    //        {
    //            base.DeserializeData(reader);
    //            reader.SetField<customMachine.customMachineInputBuffer>(this, "m_entity", (object)customMachine.Deserialize(reader));
    //            this.m_isRegisteredToLogistics = reader.ReadBool();
    //        }

    //        static customMachineInputBuffer()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            customMachineInputBuffer.s_serializeDataDelayedAction = (Action<object, BlobWriter>)((obj, writer) => ((customMachineInputBuffer)obj).SerializeData(writer));
    //            customMachineInputBuffer.s_deserializeDataDelayedAction = (Action<object, BlobReader>)((obj, reader) => ((customMachineInputBuffer)obj).DeserializeData(reader));
    //        }
    //    }

    //    [GenerateSerializer(false, null, 0, null)]
    //    public sealed class customMachineOutputBuffer : GlobalOutputBuffer, IOutputBufferPriorityProvider
    //    {
    //        /// <summary>
    //        /// Index of port used as last output port, -1 at start. This is done as previous port index instead of next
    //        /// port index, so that after load the index is automatically checked before usage without perf impact.
    //        /// </summary>
    //        private int m_prevUsedPort;
    //        [DoNotSave(0, null)]
    //        public Quantity MinCapacity;
    //        [DoNotSaveCreateNewOnLoad(null, 0)]
    //        private readonly Lyst<IoPort> m_ports;
    //        /// <summary>
    //        /// Vast majority of recipes have a single output port per product. There is few exceptions
    //        /// such as cooling towers. So having single port as first class citizen improves
    //        /// performance (~5-10% of sim update for machine will full outputs).
    //        /// </summary>
    //        [DoNotSave(0, null)]
    //        private IoPortData m_portFast;
    //        [DoNotSave(0, null)]
    //        private LystStruct<IoPortData> m_portsFast;
    //        private bool m_madeRegistrationToLogistics;
    //        private readonly customMachine m_entity;
    //        private static readonly Action<object, BlobWriter> s_serializeDataDelayedAction;
    //        private static readonly Action<object, BlobReader> s_deserializeDataDelayedAction;

    //        [DoNotSave(0, null)]
    //        public bool IsAnyPortConnected { get; private set; }

    //        public bool IsNotUsedByCurrentRecipes => this.MinCapacity.IsZero;

    //        /// <summary>
    //        /// IsRegisteredToLogistics can be false when m_madeRegistrationToLogistics is true.
    //        /// Reason can be products that can't be transported. But we store that we made the
    //        /// request to make sure we don't apply for it every single time.
    //        /// </summary>
    //        public bool IsRegisteredToLogistics { get; private set; }

    //        public customMachineOutputBuffer(
    //          Quantity capacity,
    //          ProductProto product,
    //          IProductsManager productsManager,
    //          customMachine entity)
    //            : base(capacity, product, productsManager, 15, (IEntity)entity)
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            this.m_prevUsedPort = -1;
    //            this.m_ports = new Lyst<IoPort>();
    //            // ISSUE: explicit constructor call
    //            //base.\u002Ector(capacity, product, productsManager, 15, (IEntity)entity);
    //            this.m_entity = entity;
    //        }

    //        private void recreatePorts()
    //        {
    //            if (this.m_ports.Count == 1)
    //            {
    //                this.m_portFast = new IoPortData(this.m_ports.First);
    //                this.m_portsFast.Clear();
    //            }
    //            else if (this.m_ports.Count > 1)
    //            {
    //                this.m_portFast = IoPortData.Invalid;
    //                this.m_portsFast.Clear();
    //                foreach (IoPort port in this.m_ports)
    //                    this.m_portsFast.Add(new IoPortData(port));
    //            }
    //            else
    //            {
    //                this.m_portFast = IoPortData.Invalid;
    //                this.m_portsFast.Clear();
    //            }
    //        }

    //        public void AddPort(IoPort port)
    //        {
    //            if (this.m_ports.Contains(port))
    //                return;
    //            this.m_ports.Add(port);
    //            this.IsAnyPortConnected |= port.IsConnected;
    //            this.recreatePorts();
    //            this.m_entity.m_allConnectedOutputsBuffersEmpty = false;
    //        }

    //        public void ClearAllPorts()
    //        {
    //            this.m_ports.Clear();
    //            this.m_portFast = new IoPortData();
    //            this.m_portsFast.Clear();
    //            this.IsAnyPortConnected = false;
    //        }

    //        public void Send()
    //        {
    //            if (this.m_portFast.IsValid)
    //            {
    //                sendToPort(this.m_portFast);
    //            }
    //            else
    //            {
    //                if (!this.m_portsFast.IsNotEmpty)
    //                    return;
    //                for (int index = 0; index < this.m_portsFast.Count && this.Quantity.IsPositive; ++index)
    //                {
    //                    this.m_prevUsedPort = (this.m_prevUsedPort + 1) % this.m_portsFast.Count;
    //                    sendToPort(this.m_portsFast[this.m_prevUsedPort]);
    //                }
    //            }

    //            [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //            void sendToPort(IoPortData port)
    //            {
    //                if (port.ConnectedTo.ValueOrNull is IEntityWithPortsEarlyExit valueOrNull && !valueOrNull.CouldReceiveFromPortEarlyExit(port.ConnectedPortToken))
    //                    return;
    //                Quantity quantity = this.Quantity - port.SendAsMuchAs(new ProductQuantity(this.Product, this.Quantity));
    //                if (!quantity.IsPositive)
    //                    return;
    //                this.RemoveExactly(quantity);
    //                if (this.m_entity.LogisticsOutputMode != EntityLogisticsMode.Auto || !this.IsRegisteredToLogistics)
    //                    return;
    //                this.UnregisterFromLogistics(true);
    //            }
    //        }

    //        public void RegisterBufferToLogistics(bool updateCapacity)
    //        {
    //            if (this.IsRegisteredToLogistics)
    //                return;
    //            this.IsRegisteredToLogistics = this.m_entity.m_vehicleBuffersRegistry.TryRegisterOutputBuffer((IStaticEntity)this.m_entity, (IProductBuffer)this, (IOutputBufferPriorityProvider)this, true);
    //            this.m_madeRegistrationToLogistics = true;
    //            if (!updateCapacity)
    //                return;
    //            this.UpdateCapacity();
    //        }

    //        public void UnregisterFromLogistics(bool updateCapacity)
    //        {
    //            if (!this.IsRegisteredToLogistics)
    //                return;
    //            this.m_entity.m_vehicleBuffersRegistry.TryUnregisterOutputBuffer((IProductBuffer)this);
    //            this.IsRegisteredToLogistics = false;
    //            this.m_madeRegistrationToLogistics = false;
    //            if (!updateCapacity)
    //                return;
    //            this.UpdateCapacity();
    //        }

    //        public void UpdateCapacity()
    //        {
    //            if (this.MinCapacity.IsZero)
    //                return;
    //            this.ForceNewCapacityTo(!this.m_entity.Prototype.BuffersMultiplier.HasValue ? (!this.IsRegisteredToLogistics ? this.MinCapacity * 2 : (8 * this.MinCapacity).Max(2 * TruckCaps.SmallTruckCapacity)) : this.MinCapacity * this.m_entity.Prototype.BuffersMultiplier.Value);
    //        }

    //        public void OnSomePortConnectionChanged(IoPort port)
    //        {
    //            if (!this.m_ports.Contains(port))
    //                return;
    //            this.recreatePorts();
    //            bool flag = this.m_ports.Any<IoPort>((Predicate<IoPort>)(x => x.IsConnected));
    //            if (flag == this.IsAnyPortConnected)
    //                return;
    //            this.IsAnyPortConnected = flag;
    //            if (this.IsAnyPortConnected)
    //            {
    //                if (!this.Quantity.IsPositive)
    //                    return;
    //                this.m_entity.m_allConnectedOutputsBuffersEmpty = false;
    //            }
    //            else
    //            {
    //                if (this.m_entity.LogisticsOutputMode != EntityLogisticsMode.Auto || this.m_madeRegistrationToLogistics)
    //                    return;
    //                this.RegisterBufferToLogistics(true);
    //            }
    //        }

    //        protected override void OnQuantityChanged(Quantity diff)
    //        {
    //            if (diff.IsPositive)
    //                this.m_entity.m_allConnectedOutputsBuffersEmpty = false;
    //            else
    //                this.m_entity.m_hasNoNeedToSearchForRecipe = false;
    //            base.OnQuantityChanged(diff);
    //        }

    //        BufferStrategy IOutputBufferPriorityProvider.GetOutputPriority(OutputPriorityRequest request)
    //        {
    //            return new BufferStrategy(this.m_entity.GeneralPriority, new Quantity?(request.Buffer.Capacity / 2));
    //        }

    //        public override void Destroy()
    //        {
    //            this.m_ports.Clear();
    //            this.UnregisterFromLogistics(false);
    //            base.Destroy();
    //        }

    //        public static void Serialize(customMachine.customMachineOutputBuffer value, BlobWriter writer)
    //        {
    //            if (!writer.TryStartClassSerialization<customMachine.customMachineOutputBuffer>(value))
    //                return;
    //            writer.EnqueueDataSerialization((object)value, customMachine.customMachineOutputBuffer.s_serializeDataDelayedAction);
    //        }

    //        protected override void SerializeData(BlobWriter writer)
    //        {
    //            base.SerializeData(writer);
    //            writer.WriteBool(this.IsRegisteredToLogistics);
    //            customMachine.Serialize(this.m_entity, writer);
    //            writer.WriteBool(this.m_madeRegistrationToLogistics);
    //            writer.WriteInt(this.m_prevUsedPort);
    //        }

    //        public new static customMachine.customMachineOutputBuffer Deserialize(BlobReader reader)
    //        {
    //            customMachine.customMachineOutputBuffer machineOutputBuffer;
    //            if (reader.TryStartClassDeserialization<customMachine.customMachineOutputBuffer>(out machineOutputBuffer))
    //                reader.EnqueueDataDeserialization((object)machineOutputBuffer, customMachine.customMachineOutputBuffer.s_deserializeDataDelayedAction);
    //            return machineOutputBuffer;
    //        }

    //        protected override void DeserializeData(BlobReader reader)
    //        {
    //            base.DeserializeData(reader);
    //            this.IsRegisteredToLogistics = reader.ReadBool();
    //            reader.SetField<customMachine.customMachineOutputBuffer>(this, "m_entity", (object)customMachine.Deserialize(reader));
    //            this.m_madeRegistrationToLogistics = reader.ReadBool();
    //            reader.SetField<customMachine.customMachineOutputBuffer>(this, "m_ports", (object)new Lyst<IoPort>());
    //            this.m_prevUsedPort = reader.ReadInt();
    //        }

    //        static customMachineOutputBuffer()
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            customMachine.customMachineOutputBuffer.s_serializeDataDelayedAction = (Action<object, BlobWriter>)((obj, writer) => ((customMachineOutputBuffer)obj).SerializeData(writer));
    //            customMachine.customMachineOutputBuffer.s_deserializeDataDelayedAction = (Action<object, BlobReader>)((obj, reader) => ((customMachineOutputBuffer)obj).DeserializeData(reader));
    //        }
    //    }

    //    public readonly struct RecipeWrapper
    //    {
    //        public readonly RecipeProto Recipe;
    //        public readonly Percent MinUtilization;
    //        public readonly int QuantitiesGcd;
    //        public readonly ImmutableArray<RecipeProductQuantity> AllInputs;
    //        public readonly ImmutableArray<RecipeProductQuantity> OutputsAtStart;
    //        public readonly ImmutableArray<RecipeProductQuantity> OutputsAtEnd;
    //        public readonly ImmutableArray<RecipeProto> RecipesWithSameOutputs;

    //        public RecipeWrapper(
    //          RecipeProto recipe,
    //          customMachine entity,
    //          ImmutableArray<RecipeProto> recipesWithSameOutputs)
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            this.Recipe = recipe;
    //            this.MinUtilization = recipe.MinUtilization;
    //            this.QuantitiesGcd = recipe.QuantitiesGcd;
    //            this.AllInputs = recipe.AllInputs.Map<RecipeProductQuantity>((Func<RecipeInput, RecipeProductQuantity>)(x => new RecipeProductQuantity(recipe, x.Quantity, resolveInputBuffer(x.Product))));
    //            this.OutputsAtStart = recipe.OutputsAtStart.Map<RecipeProductQuantity>((Func<RecipeOutput, RecipeProductQuantity>)(x => new RecipeProductQuantity(recipe, x.Quantity, resolveOutputBuffer(x.Product))));
    //            this.OutputsAtEnd = recipe.OutputsAtEnd.Map<RecipeProductQuantity>((Func<RecipeOutput, RecipeProductQuantity>)(x => new RecipeProductQuantity(recipe, x.Quantity, resolveOutputBuffer(x.Product))));
    //            this.RecipesWithSameOutputs = recipesWithSameOutputs;

    //            IProductBuffer resolveInputBuffer(ProductProto product)
    //            {
    //                if (product.Type == VirtualProductProto.ProductType)
    //                    return entity.m_virtualBuffersMap.GetBuffer(product, (IEntity)entity).Value;
    //                customMachineInputBuffer result;
    //                entity.tryGetInputBuffer(product, out result);
    //                Assert.That<customMachineInputBuffer>(result).IsNotNull<customMachineInputBuffer>();
    //                return (IProductBuffer)result;
    //            }

    //            IProductBuffer resolveOutputBuffer(ProductProto product)
    //            {
    //                if (product.Type == VirtualProductProto.ProductType)
    //                {
    //                    Option<IProductBuffer> buffer = entity.m_virtualBuffersMap.GetBuffer(product, (IEntity)entity);
    //                    if (buffer.IsNone)
    //                        Log.Error(string.Format("Virtual output buffer not found for product '{0}' ", (object)product.Id) + string.Format("on entity '{0}'.", (object)entity.Prototype.Id));
    //                    return buffer.Value;
    //                }
    //                customMachine.customMachineOutputBuffer result;
    //                entity.tryGetOutputBuffer(product, out result);
    //                Assert.That<customMachine.customMachineOutputBuffer>(result).IsNotNull<customMachine.customMachineOutputBuffer>();
    //                return (IProductBuffer)result;
    //            }
    //        }

    //        [Pure]
    //        public bool CanRemoveFromInputs()
    //        {
    //            foreach (RecipeProductQuantity allInput in this.AllInputs)
    //            {
    //                if (!allInput.Buffer.CanRemove(allInput.Quantity))
    //                    return false;
    //            }
    //            return true;
    //        }

    //        [Pure]
    //        public bool CanStoreToOutputs()
    //        {
    //            foreach (RecipeProductQuantity recipeProductQuantity in this.OutputsAtStart)
    //            {
    //                if (!recipeProductQuantity.Buffer.CanStore(recipeProductQuantity.Quantity))
    //                    return false;
    //            }
    //            foreach (RecipeProductQuantity recipeProductQuantity in this.OutputsAtEnd)
    //            {
    //                if (!recipeProductQuantity.Buffer.CanStore(recipeProductQuantity.Quantity))
    //                    return false;
    //            }
    //            return true;
    //        }
    //    }

    //    public readonly struct RecipeProductQuantity
    //    {
    //        public readonly Quantity Quantity;
    //        public readonly Quantity BaseFraction;
    //        public readonly IProductBuffer Buffer;

    //        public RecipeProductQuantity(RecipeProto recipe, Quantity quantity, IProductBuffer buffer)
    //        {
    //            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
    //            this.Quantity = quantity;
    //            this.Buffer = buffer;
    //            this.BaseFraction = this.Quantity / recipe.QuantitiesGcd;
    //        }
    //    }
    //}
    //internal static class MachineUtils
    //{
    //    /// <summary>
    //    /// Finds the best matching recipe from <paramref name="candidates" /> for a given <paramref name="source" /> recipe.
    //    /// Returns immediately on exact reference match. Otherwise, scores candidates by similarity
    //    /// (matching products, duration, power multiplier, quantities) and returns the best one.
    //    /// </summary>
    //    internal static Option<RecipeProto> GetBestRecipeMatch(
    //      RecipeProto source,
    //      IIndexable<RecipeProto> candidates)
    //    {
    //        if (candidates.Contains<RecipeProto>(source))
    //            return (Option<RecipeProto>)source;
    //        RecipeProto recipeProto = (RecipeProto)null;
    //        int num1 = -1;
    //        foreach (RecipeProto candidate in candidates)
    //        {
    //            if (MachineUtils.areProductsSame(source, candidate))
    //            {
    //                int num2 = 0;
    //                if (source.Duration == candidate.Duration)
    //                    ++num2;
    //                if (source.PowerMultiplier == candidate.PowerMultiplier)
    //                    ++num2;
    //                if (MachineUtils.areQuantitiesSame(source, candidate))
    //                    ++num2;
    //                if (num2 > num1)
    //                {
    //                    num1 = num2;
    //                    recipeProto = candidate;
    //                }
    //            }
    //        }
    //        return recipeProto.CreateOption<RecipeProto>();
    //    }

    //    private static bool areProductsSame(RecipeProto first, RecipeProto second)
    //    {
    //        foreach (RecipeInput allInput in first.AllInputs)
    //        {
    //            RecipeInput input = allInput;
    //            if (!second.AllInputs.Any((Func<RecipeInput, bool>)(x => (Proto)x.Product == (Proto)input.Product)))
    //                return false;
    //        }
    //        foreach (RecipeInput allInput in second.AllInputs)
    //        {
    //            RecipeInput input = allInput;
    //            if (!first.AllInputs.Any((Func<RecipeInput, bool>)(x => (Proto)x.Product == (Proto)input.Product)))
    //                return false;
    //        }
    //        foreach (RecipeOutput allOutput in first.AllOutputs)
    //        {
    //            RecipeOutput output = allOutput;
    //            if (!second.AllOutputs.Any((Func<RecipeOutput, bool>)(x => (Proto)x.Product == (Proto)output.Product)))
    //                return false;
    //        }
    //        foreach (RecipeOutput allOutput in second.AllOutputs)
    //        {
    //            RecipeOutput output = allOutput;
    //            if (!first.AllOutputs.Any((Func<RecipeOutput, bool>)(x => (Proto)x.Product == (Proto)output.Product)))
    //                return false;
    //        }
    //        return true;
    //    }

    //    private static bool areQuantitiesSame(RecipeProto first, RecipeProto second)
    //    {
    //        foreach (RecipeInput allInput in first.AllInputs)
    //        {
    //            RecipeInput input = allInput;
    //            RecipeInput recipeInput = second.AllInputs.FirstOrDefault((Func<RecipeInput, bool>)(x => (Proto)x.Product == (Proto)input.Product));
    //            if ((Proto)recipeInput?.Product != (Proto)null && recipeInput.Quantity != input.Quantity)
    //                return false;
    //        }
    //        foreach (RecipeOutput allOutput in first.AllOutputs)
    //        {
    //            RecipeOutput output = allOutput;
    //            RecipeOutput recipeOutput = second.AllOutputs.FirstOrDefault((Func<RecipeOutput, bool>)(x => (Proto)x.Product == (Proto)output.Product));
    //            if ((Proto)recipeOutput?.Product != (Proto)null && recipeOutput.Quantity != output.Quantity)
    //                return false;
    //        }
    //        return true;
    //    }
    //}

}