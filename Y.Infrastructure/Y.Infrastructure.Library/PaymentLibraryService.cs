using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Library
{
    public interface IPaymentLibraryService
    {
        DepositedSubmitResult CreateRequestData(string payCategoryType,Dictionary<string,string> configs);
        Dictionary<string, object> ProcessRequest();
        DepositedSubmitResult ProcessCallback(string payCategoryType, Dictionary<string, string> setting, Dictionary<string, string> callbackData);
    }

    public class PaymentLibraryService : IPaymentLibraryService
    {
        public DepositedSubmitResult CreateRequestData(string payCategoryType, Dictionary<string, string> configs)
        {
            return new DepositedSubmitResult() ;
        }

        public DepositedSubmitResult ProcessCallback(string payCategoryType, Dictionary<string, string> setting, Dictionary<string, string> callbackData)
        {
            return new DepositedSubmitResult();
        }

        public Dictionary<string, object> ProcessRequest()
        {
            return null;
        }
    }
}
