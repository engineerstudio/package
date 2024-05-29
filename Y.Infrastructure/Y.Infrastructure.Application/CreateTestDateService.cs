using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.Application
{
    /// <summary>
    /// TODO 创建测试数据
    /// </summary>
    public class CreateTestDateService : ICreateTestDateService
    {


        public CreateTestDateService()
        {

        }


        /// <summary>
        /// 创建代理和代理下用户数据
        /// 1. 判断代理是否创建
        /// 2. 创建该代理下今日的用户
        /// </summary>
        public void CreateTestAgentAndUsers()
        {
            // 1. 默认站点1000
            // 2. 默认代理是default_agent_1000
            // 3. 按照日期创建用户default_user_20200601






        }

        /// <summary>
        /// 创建站点的游戏测试数据
        /// 1. 获取站点开启的游戏数据
        /// 2. 创建代理的和代理下面的用户数据
        /// </summary>
        public void CreateTestAgentAndUsersData()
        {
            throw new NotImplementedException();
        }
    }
}
