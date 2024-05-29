using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Packet.Services.IGames
{
    public interface IInfrastructureTasksService
    {
        Task Start();
        Task Stop();
        Task Stop(string gameTypeStr);
    }
}
