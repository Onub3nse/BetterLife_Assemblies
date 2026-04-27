using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Mafi.Core.Notifications;
using NotifyID = Mafi.Core.Notifications.EntityNotificationProto.ID;
using Mafi.Base;
namespace BetterLife;

public partial class BetterLIDs
{ 
    public partial class Notify
    {
        public static readonly NotifyID noPortConnected = new NotifyID("noPortConnected");
    }
}
