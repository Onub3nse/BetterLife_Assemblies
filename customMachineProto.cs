using Mafi;
using Mafi.Collections;
using Mafi.Collections.ImmutableCollections;
using Mafi.Collections.ReadonlyCollections;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Entities.Animations;
using Mafi.Core.Entities.Static;
using Mafi.Core.Entities.Static.Layout;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Products;
using Mafi.Core.Prototypes;
using Mafi.Serialization;
using Mafi.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Mafi.Core.Entities.Static.Layout.LayoutEntityProto;

namespace BetterLife_Assemblies
{
    public class customMachineProto :
      LayoutEntityProto,
      IProtoWithUpgrade,
      IProtoWithTiers,
      IProtoWithIcon,
      IProto,
      IProtoWithPowerConsumption,
      IProtoWithComputingConsumption,
      IProtoWithRecipes,
      IProtoWithAnimation
    {
        public static readonly Upoints BOOST_COST;
        /// <summary>
        /// Electricity consumed every tick to allow the machine to work.
        /// </summary>
        public readonly Electricity ConsumedPowerPerTick;
        public readonly int? BuffersMultiplier;
        private readonly Lyst<RecipeProto> m_recipes;
        /// <summary>
        /// Graphics-only properties that does not affect game simulation and are not needed or accessed by the game
        /// simulation.
        /// </summary>
        public new readonly customMachineProto.Gfx Graphics;
        public readonly int? EmissionWhenRunning;
        public readonly bool DisableLogisticsByDefault;

        public override Type EntityType => typeof(MyMachineEntity);

        public Electricity ElectricityConsumed => this.ConsumedPowerPerTick;

        public Computing ComputingConsumed { get; }

        /// <summary>Set of recipes usable in this machine.</summary>
        public IIndexable<RecipeProto> Recipes => (IIndexable<RecipeProto>)this.m_recipes;

        public UpgradeData Upgrade { get; }

        public ITierData TierData => (ITierData)this.Upgrade.TierData;

        /// <summary>
        /// We need to know if this machine is waste disposal so we don't give its input buffers high priorities for logistics. Otherwise
        /// we would empty storages of products that might be useful (e.g. acid).
        /// </summary>
        public bool IsWasteDisposal { get; }

        /// <summary>
        /// Whether to add all recipes when the entity is constructed or new recipes are unlocked.
        /// This is useful for entities like smoke stack or flare.
        /// </summary>
        public bool UseAllRecipesAtStartOrAfterUnlock { get; }

        /// <summary>Parameters for animation.</summary>
        public ImmutableArray<Mafi.Core.Entities.Animations.AnimationParams> AnimationParams { get; }

        public customMachineProto(
          StaticEntityProto.ID id,
          Proto.Str strings,
          EntityLayout layout,
          EntityCosts costs,
          Electricity consumedPowerPerTick,
          Computing computingConsumed,
          int? buffersMultiplier,
          bool useAllRecipesAtStartOrAfterUnlock,
          ImmutableArray<Mafi.Core.Entities.Animations.AnimationParams> animationParams,
          customMachineProto.Gfx graphics,
          int? emissionWhenRunning = null,
          bool isWasteDisposal = false,
          bool disableLogisticsByDefault = false,
          Upoints? boostCost = null,
          IEnumerable<Tag> tags = null)
            : base(id, strings, layout, costs, graphics, constructionDurationPerProduct: new Duration?(), boostCost: boostCost ?? customMachineProto.BOOST_COST, collapseRubbleScale: new Percent?(), customBuriedTolerance: new ThicknessTilesF?(), tags: tags)
        {
            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
            this.m_recipes = new Lyst<RecipeProto>();
            StaticEntityProto.ID id1 = (StaticEntityProto.ID)id;
            Proto.Str strings1 = strings;
            EntityLayout layout1 = layout;
            EntityCosts costs1 = costs;
            Upoints? nullable = boostCost;
            customMachineProto.Gfx graphics1 = graphics;
            IEnumerable<Tag> tags1 = tags;
            //Duration? constructionDurationPerProduct = new Duration?();
            Upoints? boostCost1 = nullable;
            //Percent? collapseRubbleScale = new Percent?();
            //ThicknessTilesF? customBuriedTolerance = new ThicknessTilesF?();
            IEnumerable<Tag> tags2 = tags1;
            // ISSUE: explicit constructor call
            //base.\u002Ector(id1, strings1, layout1, costs1, (LayoutEntityProto.Gfx)graphics1, constructionDurationPerProduct, boostCost1, collapseRubbleScale: collapseRubbleScale, customBuriedTolerance: customBuriedTolerance, tags: tags2);
            this.ConsumedPowerPerTick = consumedPowerPerTick;
            this.ComputingConsumed = computingConsumed;
            this.BuffersMultiplier = buffersMultiplier;
            this.IsWasteDisposal = isWasteDisposal;
            this.DisableLogisticsByDefault = disableLogisticsByDefault;
            this.Graphics = graphics;
            this.EmissionWhenRunning = emissionWhenRunning;
            this.AnimationParams = animationParams;
            this.UseAllRecipesAtStartOrAfterUnlock = useAllRecipesAtStartOrAfterUnlock;
            this.Upgrade = new UpgradeData((IProtoWithUpgrade)this);
            if (this.AnimationParams.Length > 1 && (graphics.UseInstancedRendering || graphics.UseSemiInstancedRendering))
                throw new ProtoBuilderException("Instanced rendering not compatible with multiple animations.");
        }

        /// <summary>Adds recipe prototype to the machine.</summary>
        public void AddRecipe(RecipeProto recipe)
        {
            Assert.That<bool>(this.IsInitialized).IsFalse();
            foreach (RecipeProduct allInput in recipe.AllInputs)
                Assert.That<Quantity>(allInput.Quantity).IsPositive();
            foreach (RecipeProduct allOutput in recipe.AllOutputs)
                Assert.That<Quantity>(allOutput.Quantity).IsPositive();
            ProductProto[] recipeInputProducts = recipe.AllInputs.MapArray<ProductProto>((Func<RecipeInput, ProductProto>)(rp => rp.Product));
            ProductProto[] recipeOutputProducts = recipe.AllOutputs.MapArray<ProductProto>((Func<RecipeOutput, ProductProto>)(rp => rp.Product));
            RecipeProto recipeProto = this.m_recipes.FirstOrDefault<RecipeProto>((Predicate<RecipeProto>)(r => r.AllInputs.Length == recipe.AllInputs.Length && r.AllOutputs.Length == recipe.AllOutputs.Length && r.AllInputs.Select<ProductProto>((Func<RecipeInput, ProductProto>)(i => i.Product)).All<ProductProto>((Func<ProductProto, bool>)(p => ((IEnumerable<ProductProto>)recipeInputProducts).Contains<ProductProto>(p))) && r.AllOutputs.Select<ProductProto>((Func<RecipeOutput, ProductProto>)(i => i.Product)).All<ProductProto>((Func<ProductProto, bool>)(p => ((IEnumerable<ProductProto>)recipeOutputProducts).Contains<ProductProto>(p))) && r.PowerMultiplier == recipe.PowerMultiplier));
            if ((Proto)recipeProto != (Proto)null)
                throw new ProtoBuilderException(string.Format("Cannot add recipe {0} to machine {1}, recipe with same inputs & outputs already added: {2}.", (object)recipe, (object)this, (object)recipeProto));
            this.m_recipes.Add(recipe);
        }

        public override void GetAssetsPaths(Lyst<string> assetsPaths)
        {
            base.GetAssetsPaths(assetsPaths);
            if (!this.Graphics.MachineSoundPrefabPath.HasValue)
                return;
            assetsPaths.Add(this.Graphics.MachineSoundPrefabPath.Value);
        }

        public new customMachineProto.ID Id => new customMachineProto.ID(base.Id.Value);

        static customMachineProto()
        {
            //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
            customMachineProto.BOOST_COST = 0.25.Upoints();
        }

        [DebuggerDisplay("{Value,nq}")]
        [ManuallyWrittenSerialization]
        [InlineValue("Value")]
        [DebuggerStepThrough]
        public new readonly struct ID : IEquatable<customMachineProto.ID>, IComparable<customMachineProto.ID>
        {
            /// <summary>Underlying string value of this Id.</summary>
            public readonly string Value;

            public ID(string value)
            {
                //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
                this.Value = value;
            }

            /// <summary>
            /// Implicit conversion to parent <see cref="T:Mafi.Core.Entities.Static.StaticEntityProto.ID" />.
            /// </summary>
            public static implicit operator StaticEntityProto.ID(customMachineProto.ID id)
            {
                return new StaticEntityProto.ID(id.Value);
            }

            public static bool operator ==(StaticEntityProto.ID lhs, customMachineProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator ==(customMachineProto.ID lhs, StaticEntityProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(StaticEntityProto.ID lhs, customMachineProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(customMachineProto.ID lhs, StaticEntityProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public bool Equals(StaticEntityProto.ID other)
            {
                return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
            }

            /// <summary>
            /// Implicit conversion to parent <see cref="T:Mafi.Core.Entities.EntityProto.ID" />.
            /// </summary>
            public static implicit operator EntityProto.ID(customMachineProto.ID id)
            {
                return new EntityProto.ID(id.Value);
            }

            public static bool operator ==(EntityProto.ID lhs, customMachineProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator ==(customMachineProto.ID lhs, EntityProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(EntityProto.ID lhs, customMachineProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(customMachineProto.ID lhs, EntityProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public bool Equals(EntityProto.ID other)
            {
                return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
            }

            /// <summary>
            /// Implicit conversion to parent <see cref="T:Mafi.Core.Prototypes.Proto.ID" />.
            /// </summary>
            public static implicit operator Proto.ID(customMachineProto.ID id) => new Proto.ID(id.Value);

            public static bool operator ==(Proto.ID lhs, customMachineProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator ==(customMachineProto.ID lhs, Proto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(Proto.ID lhs, customMachineProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(customMachineProto.ID lhs, Proto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public bool Equals(Proto.ID other)
            {
                return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
            }

            public static bool operator ==(customMachineProto.ID lhs, customMachineProto.ID rhs)
            {
                return string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public static bool operator !=(customMachineProto.ID lhs, customMachineProto.ID rhs)
            {
                return !string.Equals(lhs.Value, rhs.Value, StringComparison.Ordinal);
            }

            public override bool Equals(object other)
            {
                return other is customMachineProto.ID other1 && this.Equals(other1);
            }

            public bool Equals(customMachineProto.ID other)
            {
                return string.Equals(this.Value, other.Value, StringComparison.Ordinal);
            }

            public int CompareTo(customMachineProto.ID other) => string.CompareOrdinal(this.Value, other.Value);

            public override string ToString() => this.Value ?? string.Empty;

            public override int GetHashCode()
            {
                string str = this.Value;
                return str == null ? 0 : str.GetHashCode();
            }

            public static void Serialize(customMachineProto.ID value, BlobWriter writer)
            {
                writer.WriteString(value.Value);
            }

            public static customMachineProto.ID Deserialize(BlobReader reader)
            {
                return new customMachineProto.ID(reader.ReadString());
            }
        }

        public new class Gfx : LayoutEntityProto.Gfx
        {
            public new static readonly customMachineProto.Gfx Empty;
            /// <summary>Parameters for particles.</summary>
            public readonly ImmutableArray<ParticlesParams> ParticlesParams;
            public readonly ImmutableArray<EmissionParams> EmissionsParams;
            /// <summary>The sound the machine make while it is operating.</summary>
            public readonly Option<string> MachineSoundPrefabPath;
            /// <summary>
            /// Whether this machine has a sign showing the last product it made.
            /// </summary>
            public readonly bool HasSign;

            public Gfx(
              string prefabPath,
              ImmutableArray<ToolbarEntryData> categories,
              RelTile3f prefabOffset = default(RelTile3f),
              Option<string> customIconPath = default(Option<string>),
              ImmutableArray<ParticlesParams> particlesParams = default(ImmutableArray<ParticlesParams>),
              ImmutableArray<EmissionParams> emissionsParams = default(ImmutableArray<EmissionParams>),
              Option<string> machineSoundPrefabPath = default(Option<string>),
              bool useInstancedRendering = false,
              bool useSemiInstancedRendering = false,
              ImmutableArray<string> instancedRenderingExcludedObjects = default(ImmutableArray<string>),
              string instancedRenderingAnimationProtoSwap = null,
              bool hasSign = false,
              IReadOnlyDictionary<string, string> instancedRenderingAnimationMaterialSwap = null,
              ColorRgba color = default(ColorRgba),
              LayoutEntityProto.VisualizedLayers? visualizedLayers = null)
                : base(prefabPath, prefabOffset, customIconPath, color, visualizedLayers: visualizedLayers ?? LayoutEntityProto.VisualizedLayers.Empty, categories: categories, useInstancedRendering: useInstancedRendering, useSemiInstancedRendering: useSemiInstancedRendering, instancedRenderingAnimationProtoSwap: instancedRenderingAnimationProtoSwap, instancedRenderingAnimationMaterialSwap: instancedRenderingAnimationMaterialSwap, instancedRenderingExcludedObjects: instancedRenderingExcludedObjects)
            {
                //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
                string prefabPath1 = prefabPath;
                RelTile3f prefabOrigin = prefabOffset;
                Option<string> customIconPath1 = customIconPath;
                ColorRgba color1 = color;
                bool flag1 = useInstancedRendering;
                bool flag2 = useSemiInstancedRendering;
                ImmutableArray<string> immutableArray = instancedRenderingExcludedObjects;
                string str = instancedRenderingAnimationProtoSwap;
                IReadOnlyDictionary<string, string> readOnlyDictionary = instancedRenderingAnimationMaterialSwap;
                LayoutEntityProto.VisualizedLayers? visualizedLayers1 = new LayoutEntityProto.VisualizedLayers?(visualizedLayers ?? LayoutEntityProto.VisualizedLayers.Empty);
                ImmutableArray<ToolbarEntryData>? categories1 = new ImmutableArray<ToolbarEntryData>?(categories);
                int num1 = flag1 ? 1 : 0;
                int num2 = flag2 ? 1 : 0;
                string instancedRenderingAnimationProtoSwap1 = str;
                IReadOnlyDictionary<string, string> instancedRenderingAnimationMaterialSwap1 = readOnlyDictionary;
                ImmutableArray<string> instancedRenderingExcludedObjects1 = immutableArray;
                AngleDegrees1f? yawForGeneratedIcon = new AngleDegrees1f?();
                // ISSUE: explicit constructor call
                //base.\u002Ector(prefabPath1, prefabOrigin, customIconPath1, color1, visualizedLayers: visualizedLayers1, categories: categories1, useInstancedRendering: num1 != 0, useSemiInstancedRendering: num2 != 0, instancedRenderingAnimationProtoSwap: instancedRenderingAnimationProtoSwap1, instancedRenderingAnimationMaterialSwap: instancedRenderingAnimationMaterialSwap1, instancedRenderingExcludedObjects: instancedRenderingExcludedObjects1, yawForGeneratedIcon: yawForGeneratedIcon);
                if (emissionsParams.IsNotEmpty & useInstancedRendering)
                    throw new ProtoBuilderException("Instanced rendering not compatible with custom emission.");
                if (particlesParams.IsNotEmpty & useInstancedRendering)
                    throw new ProtoBuilderException("Pure instanced rendering not compatible with particles.");
                this.ParticlesParams = particlesParams.IsValid ? particlesParams : ImmutableArray<ParticlesParams>.Empty;
                this.EmissionsParams = emissionsParams.IsValid ? emissionsParams : ImmutableArray<EmissionParams>.Empty;
                this.MachineSoundPrefabPath = machineSoundPrefabPath;
                this.HasSign = hasSign;
            }

            static Gfx()
            {
                //vhv2wawtCFKx7m5R0w.ySWtb2WnH();
                RelTile3f zero = RelTile3f.Zero;
                Option<string> customIconPath = (Option<string>)"EMPTY";
                ImmutableArray<ParticlesParams> empty1 = ImmutableArray<ParticlesParams>.Empty;
                ImmutableArray<EmissionParams> empty2 = ImmutableArray<EmissionParams>.Empty;
                Option<string> none = (Option<string>)Option.None;
                ColorRgba empty3 = ColorRgba.Empty;
                LayoutEntityProto.VisualizedLayers? visualizedLayers = new LayoutEntityProto.VisualizedLayers?(LayoutEntityProto.VisualizedLayers.Empty);
                customMachineProto.Gfx.Empty = new customMachineProto.Gfx("EMPTY", ImmutableArray<ToolbarEntryData>.Empty, zero, customIconPath, empty1, empty2, none, color: empty3, visualizedLayers: visualizedLayers);
            }
        }
    }
}