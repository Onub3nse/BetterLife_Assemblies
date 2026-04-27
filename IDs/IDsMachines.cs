using Mafi.Base;
using Mafi.Core.Entities.Static.Layout;
using MachineID = Mafi.Core.Factory.Machines.MachineProto.ID;
namespace BetterLife_Assemblies
{
    public partial class BetterLIDs
    {
        public partial class Machines
        {
            public static readonly MachineID AssemblyBlt1 = Ids.Machines.CreateId("AssemblyBlt1");
            public static readonly MachineID AssemblyBlt2 = Ids.Machines.CreateId("AssemblyBlt2");
            public static readonly MachineID AssemblyBlt3 = Ids.Machines.CreateId("AssemblyBlt3");
            public static readonly MachineID DummyMachine = Ids.Machines.CreateId("DummyMachine");
            public static readonly MachineID ComPowerPlant = Ids.Machines.CreateId("comPowerPlant");
            public static readonly MachineID windpowertest = Ids.Machines.CreateId("windpower");
            public static readonly ToolbarCategoryProto.ID toolbar_customMachine = Ids.Machines.CreateId("customMachines");
        }
    }
}