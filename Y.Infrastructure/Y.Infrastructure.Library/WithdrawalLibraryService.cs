using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Library
{
    public interface IWithdrawalLibraryService
    {
        WithdrawalsSubmitResult CreateRequestData(string className, Dictionary<string, string> config);
        Dictionary<string, object> ProcessRequest();
        WithdrawalsSubmitResult ProcessCallback(string className, Dictionary<string, string> setting, Dictionary<string, string> callbackData);
    }

    public class WithdrawalLibraryService : IWithdrawalLibraryService
    {
        public WithdrawalsSubmitResult CreateRequestData(string className, Dictionary<string, string> config)
        {
            return new WithdrawalsSubmitResult();
        }

        public WithdrawalsSubmitResult ProcessCallback(string className, Dictionary<string, string> setting, Dictionary<string, string> callbackData)
        {
            return new WithdrawalsSubmitResult();
        }

        public Dictionary<string, object> ProcessRequest()
        {
            throw new NotImplementedException();
        }
    }
}
