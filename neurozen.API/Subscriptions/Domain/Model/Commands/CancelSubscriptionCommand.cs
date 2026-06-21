using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neurozen.API.Subscriptions.Domain.Model.Commands
{
    public record CancelSubscriptionCommand(Guid UserId);
    
}