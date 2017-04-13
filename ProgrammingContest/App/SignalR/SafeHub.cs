using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ProgrammingContest.App.SignalR
{
    [HubName("SafeCode")]
    public class SafeHub : Hub
    {        
        public void CheckCode()
        {
            
            Clients.Others.hello("Hello");

        }
    }
}