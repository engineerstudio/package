using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Entities.RequestModel;
using Y.Infrastructure.Entities.ViewModel;
using Y.Packet.Entities.Members.ViewModels;

namespace Y.Infrastructure.IApplication
{
    public interface IAPICommonLogicService
    {
        Task<(bool, string)> SignUp(UserRegisterModel req);
        Task<(bool, string, UserLoginRt)> LogIn(UserSignInModel req);
        Task<(bool, string)> SignOut(int memeberid);
        Task<SiteMainPageDataBaseViewModel> MainPageDataLoadAsync();
        Task<SiteMainPageMessageViewModel> MainPageMessageDataLoadAsync();
        Task<(bool, object)> GetCsConfig();
        Task<(bool, object)> GetCsConfigForPcAsync();
        Task<(bool, string, UserFundsViewModel)> GetMemberBalanceAsync();
        Task<(bool, string, UserFundsViewModelV2)> GetMemberBalanceV2Async();
        Task<(bool, string, UserFundsViewModelV3)> GetMemberBalanceV3Async();
        #region Game
        Task<(bool, string)> GameLogIn(string type, string code);
        Task<WalletViewModel> GameUserBalanceAsync();
        Task<WalletViewModelV2> GameUserBalanceV2Async();
        Task<List<W27GameTypeWalletViewModel>> GetW27GamesWalletAsync();
        Task<(bool sucess, string msg, string transId)> Transfer(TransferReq req);
        #endregion

        Task<object> GetVips();

        #region W27 INDEX PAGE
        Task<(bool, string, W27UserInfo)> GetUserInfoForW27Async();
        Task<(bool, string, W27Vips)> GetVipsForW27Async();
        Task<(bool, string, W27FundsInfo)> GetFundsForW27Async();
        Task<(bool, string, List<W27WithdrawalsRange>)> GetWithdrawalsRangeForW27Async();

        #endregion
    }
}
