using Mafi;
using Mafi.Core;
using Mafi.Core.Entities;
using Mafi.Core.Factory.Machines;
using Mafi.Core.Factory.Recipes;
using Mafi.Core.Factory.WellPumps;
using Mafi.Core.Input;
using Mafi.Core.Population;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterLife_Assemblies
{
    [GlobalDependency(RegistrationMode.AsAllInterfaces, false, false)]
    public class customMachineCommandsProcessor : ICommandProcessor<MachineSetRecipeActiveCmd>, IAction<MachineSetRecipeActiveCmd>, ICommandProcessor<MachineBoostToggleCmd>, IAction<MachineBoostToggleCmd>, ICommandProcessor<MachineToggleRecipeActiveCmd>, IAction<MachineToggleRecipeActiveCmd>, ICommandProcessor<ReorderRecipeCmd>, IAction<ReorderRecipeCmd>, ICommandProcessor<WellPumpAlertSetEnabledCmd>, IAction<WellPumpAlertSetEnabledCmd>, ICommandProcessor<ClearUnusedMachineBuffersCmd>, IAction<ClearUnusedMachineBuffersCmd>
    {
        // Token: 0x04003F1B RID: 16155
        public static readonly Upoints COST_TO_DISCARD_PRODUCTS;

        // Token: 0x04003F1C RID: 16156
        private readonly EntitiesManager m_entitiesManager;

        // Token: 0x04003F1D RID: 16157
        private readonly ProtosDb m_protosDb;

        // Token: 0x04003F1E RID: 16158
        private readonly IUpointsManager m_upointsManager;

        // Token: 0x060075EC RID: 30188 RVA: 0x001FB474 File Offset: 0x001F9674
        public customMachineCommandsProcessor(EntitiesManager entitiesManager, ProtosDb protosDb, IUpointsManager upointsManager)
            : base()
        {
            //global::iKyIOCd8XMSWfDOjTs.vhv2wawtCFKx7m5R0w.ySWtb2WnH();
            this.m_entitiesManager = entitiesManager;
            this.m_protosDb = protosDb;
            this.m_upointsManager = upointsManager;
        }

        // Token: 0x060075ED RID: 30189 RVA: 0x001FB498 File Offset: 0x001F9698
        public void Invoke(MachineSetRecipeActiveCmd cmd)
        {
            RecipeProto recipeProto;
            customMachine machine;
            string text;
            if (!this.tryGetMachineRecipe(cmd.MachineId, cmd.RecipeId, out recipeProto, out machine, out text))
            {
                cmd.SetResultError(text);
                return;
            }
            if (cmd.EnableRecipe)
            {
                machine.AssignRecipe(recipeProto);
            }
            else
            {
                machine.RemoveAssignedRecipe(recipeProto);
            }
            cmd.SetResultSuccess();
        } 

        // Token: 0x060075EE RID: 30190 RVA: 0x001FB4F4 File Offset: 0x001F96F4
        public void Invoke(MachineToggleRecipeActiveCmd cmd)
        {
            RecipeProto recipeProto;
            customMachine machine;
            string text;
            if (!this.tryGetMachineRecipe(cmd.MachineId, cmd.RecipeId, out recipeProto, out machine, out text))
            {
                cmd.SetResultError(text);
                return;
            }
            if (machine.RecipesAssigned.Contains(recipeProto))
            {
                machine.RemoveAssignedRecipe(recipeProto);
            }
            else
            {
                machine.AssignRecipe(recipeProto);
            }
            cmd.SetResultSuccess();
        }

        // Token: 0x060075EF RID: 30191 RVA: 0x001FB558 File Offset: 0x001F9758
        public void Invoke(MachineBoostToggleCmd cmd)
        {
            customMachine machine;
            if (!this.m_entitiesManager.TryGetEntity<customMachine>(cmd.MachineId, out machine))
            {
                cmd.SetResultError(string.Format("Unknown machine '{0}'.", cmd.MachineId));
                return;
            }
            machine.SetBoosted(!machine.IsBoostRequested);
            cmd.SetResultSuccess();
        }

        // Token: 0x060075F0 RID: 30192 RVA: 0x001FB5B0 File Offset: 0x001F97B0
        public void Invoke(WellPumpAlertSetEnabledCmd cmd)
        {
            Machine machine;
            if (!this.m_entitiesManager.TryGetEntity<Machine>(cmd.WellPumpId, out machine))
            {
                cmd.SetResultError(string.Format("Unknown machine '{0}'.", cmd.WellPumpId));
                return;
            }
            WellPump wellPump = machine as WellPump;
            if (wellPump == null)
            {
                cmd.SetResultError("Machine '" + machine.GetTitle() + "' is not a pump.");
                return;
            }
            wellPump.NotifyOnLowReserve = cmd.IsEnabled;
            cmd.SetResultSuccess();
        }

        // Token: 0x060075F1 RID: 30193 RVA: 0x001FB62C File Offset: 0x001F982C
        public void Invoke(ReorderRecipeCmd cmd)
        {
            customMachine machine;
            if (!this.m_entitiesManager.TryGetEntity<customMachine>(cmd.MachineId, out machine))
            {
                cmd.SetResultError(string.Format("Unknown machine '{0}'.", cmd.MachineId));
                return;
            }
            machine.ReorderRecipe(cmd.OldIndex, cmd.NewIndexAfterRemove);
            cmd.SetResultSuccess();
        }

        // Token: 0x060075F2 RID: 30194 RVA: 0x001FB688 File Offset: 0x001F9888
        private bool tryGetMachineRecipe(EntityId machineId, Proto.ID recipeId, out RecipeProto recipe, out customMachine machine, out string error)
        {
            if (!this.m_protosDb.TryGetProto<RecipeProto>(recipeId, out recipe))
            {
                error = string.Format("Unknown recipe proto '{0}'.", recipeId);
                machine = null;
                return false;
            }
            if (!this.m_entitiesManager.TryGetEntity<customMachine>(machineId, out machine))
            {
                error = string.Format("Unknown machine '{0}'.", machineId);
                recipe = null;
                return false;
            }
            if (!machine.Prototype.Recipes.Contains(recipe))
            {
                error = string.Format("Recipe '{0}' is invalid for machine '{1}'.", recipeId, machineId);
                return false;
            }
            error = null;
            return true;
        }

        // Token: 0x060075F3 RID: 30195 RVA: 0x001FB724 File Offset: 0x001F9924
        public void Invoke(ClearUnusedMachineBuffersCmd cmd)
        {
            customMachine machine;
            if (!this.m_entitiesManager.TryGetEntity<customMachine>(cmd.MachineId, out machine))
            {
                cmd.SetResultError(string.Format("Unknown machine '{0}'.", cmd.MachineId));
                return;
            }
            if (!this.m_upointsManager.TryConsume(IdsCore.UpointsCategories.QuickRemove, MachineCommandsProcessor.COST_TO_DISCARD_PRODUCTS, default(Option<IEntity>), null))
            {
                cmd.SetResultError("Not enough unity");
                return;
            }
            machine.ClearUnusedProducts();
            cmd.SetResultSuccess();
        }

        // Token: 0x060075F4 RID: 30196 RVA: 0x001FB7AC File Offset: 0x001F99AC
        // Note: this type is marked as 'beforefieldinit'.
        static customMachineCommandsProcessor()
        {
//            global::iKyIOCd8XMSWfDOjTs.vhv2wawtCFKx7m5R0w.ySWtb2WnH();
            customMachineCommandsProcessor.COST_TO_DISCARD_PRODUCTS = 0.1.Upoints();
        }
    }
}
