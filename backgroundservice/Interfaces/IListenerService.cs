using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backgroundservice
{
    public interface IListenerService : IHostedService
    {
        Task CreateNewListerner();
    }
}
