﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    public interface IGameLogsCallBackProcessService
    {
        Task Process(int orderId = 0);
    }
}
