using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    public interface IMerchantHyberService
    {
        Task<bool> CreateMerchantSiteAsync(string merchName, string pcTemplet, string h5Templet);


        /// <summary>
        /// 同步默认游戏图片
        /// </summary>
        /// <returns></returns>
        Task<bool> SynchronizeGameDefaultImage();

        /// <summary>
        /// 同步新增游戏菜单至所有站点
        /// </summary>
        /// <param name="templeteStr"></param>
        /// <returns></returns>
        Task<bool> SynchronizeNewGameToSiteMenu();

        /// <summary>
        /// 同步指定菜单至所有站点
        /// </summary>
        /// <param name="templeteDetailId"></param>
        /// <returns></returns>
        Task<bool> SynchronizeNewMenuToSiteMenu(int templeteDetailId);
    }
}
