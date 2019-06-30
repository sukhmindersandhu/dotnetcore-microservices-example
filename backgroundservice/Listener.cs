using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace backgroundservice
{
    public class Listener
    {
        private readonly int id;
        public Listener(int id)
        {
            this.id = id;
            Debug.WriteLine($"Listener Constructor! : {id}");
        }

        public async Task Start(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Debug.WriteLine($"Listening ... Listener : {id}");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
