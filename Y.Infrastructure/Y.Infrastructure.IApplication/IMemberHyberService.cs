using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    public interface IMemberHyberService
    {
        Task CreateUsersDailyStatisticAsync(int merchantId, int userId, DateTime date);
    }
}
