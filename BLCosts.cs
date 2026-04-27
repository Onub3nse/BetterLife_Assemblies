using Mafi.Base;
using Mafi.Core.Prototypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BetterLifeAssemblies
{
    public static class BLCosts
    {
        public static EntityCostsTpl.Builder Build
        {
            get
            {
                return new EntityCostsTpl.Builder();
            }
        }

        public static class Machines
        {
            public static EntityCostsTpl BetterAssemblerT1
            {
                get
                {
                    return BLCosts.Build.CP(10).Product(5, Ids.Products.Iron);
                }
            }
            public static EntityCostsTpl BetterAssemblerT2
            {
                get
                {
                    return BLCosts.Build.CP(20).Product(10, Ids.Products.Iron);
                }
            }
        }

        public static class Buildings
        {
            public static EntityCostsTpl McDonalds
            {
                get
                {
                    return BLCosts.Build.CP2(40).Product(10, Ids.Products.Potato);
                }
            }
        }
        public static class Roads
        {
            public static EntityCostsTpl Industrial
            {
                get
                {
                    return BLCosts.Build.MaintenanceT1(0).Priority(8).Workers(0).CP(10);
                }
            }
        }

        //            EntityCostsTpl roadCosts2 = Build.Priority(8).CP3(20).Product(10, Ids.Products.Iron);
        //            EntityCostsTpl roadCosts3 = Build.Priority(8).CP3(40).Product(20, Ids.Products.Iron);
        //            EntityCostsTpl balancerCosts = Build.Priority(8).CP(5).Product(5, Ids.Products.Iron).Product(5, Ids.Products.MechanicalParts);

        public static class transPORTs
        {
            public static class transBarCosts
            {
                public static EntityCostsTpl Loader1
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(50).Product(75, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl bar2m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(20).Product(40, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl bar4m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(40).Product(80, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl bar10m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(00).Product(120, Ids.Products.Iron);
                    }
                }

            }

            public static class balancerCosts
            {
                public static EntityCostsTpl balancer0
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(20).Product(25, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl balancer1
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(30).Product(45, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl balancer2
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(50).Product(75, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl balancer3
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(75).Product(85, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl balancer4
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(85).Product(105, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl balancer5
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(100).Product(125, Ids.Products.Iron);
                    }
                }
            }
            public static class transPortCosts
            {
                public static EntityCostsTpl transp10m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(10).Product(25, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl transp20m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(20).Product(50, Ids.Products.Iron);
                    }
                }
                public static EntityCostsTpl transp100m
                {
                    get
                    {
                        return BLCosts.Build.MaintenanceT1(0).Priority(8).CP(100).Product(150, Ids.Products.Iron);
                    }
                }

            }
        }



    }
}
