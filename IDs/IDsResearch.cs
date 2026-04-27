using Mafi.Base;
using Mafi.Core.Research;
using ResNodeID = Mafi.Core.Research.ResearchNodeProto.ID;

namespace BetterLife_Assemblies;


public partial class BetterLIDs
{
    public partial class Research
    {
//        [ResearchCosts(difficulty: 1)]
        public static readonly ResNodeID AlterRecipesT1a = Ids.Research.CreateId("AlterRecipesT1a");
        public static readonly ResNodeID AlterRecipesT1b = Ids.Research.CreateId("AlterRecipesT1b");
        public static readonly ResNodeID AlterRecipesT2 = Ids.Research.CreateId("AlterRecipesT2");
        public static readonly ResNodeID AlterRecipesT3 = Ids.Research.CreateId("AlterRecipesT3");
        public static readonly ResNodeID AlterRecipesT4 = Ids.Research.CreateId("AlterRecipesT4");
    }
}
