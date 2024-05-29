using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Entities.ViewModel;

namespace Y.Infrastructure.IApplication
{
    public interface IReportsHybridService
    {

        Task<(bool, string, List<AgentsReportsViewModel>)> LoadAgentsReportsAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt, string agentName);


    }
}
