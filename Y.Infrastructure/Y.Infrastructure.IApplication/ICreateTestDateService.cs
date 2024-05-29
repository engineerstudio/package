using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.IApplication
{
    public interface ICreateTestDateService
    {

        /// <summary>
        /// 创建代理和代理下用户数据
        /// 1. 判断代理是否创建
        /// 2. 创建该代理下今日的用户
        /// </summary>
        void CreateTestAgentAndUsers();


        /// <summary>
        /// 创建站点的游戏测试数据
        /// 1. 获取站点开启的游戏数据
        /// 2. 创建代理的和代理下面的用户数据
        /// </summary>
        void CreateTestAgentAndUsersData();


    }
}
