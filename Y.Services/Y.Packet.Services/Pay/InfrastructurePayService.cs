using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class InfrastructurePayService : IInfrastructurePayService
    {

        private readonly IPayCategoryService _payCategoryService;

        public InfrastructurePayService(IPayCategoryService payCategoryService)
        {
            _payCategoryService = payCategoryService;
        }



        /// <summary>
        /// 同步数据接口到数据库
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string)> AsyncPayMethodAsync()
        {
            string currentDirectory = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            // 1. 获取到程序集的所有数据
            Assembly assem = Assembly.LoadFrom($"{currentDirectory}\\Y.Infrastructure.Pay.dll");
            if (assem == null) throw new Exception("未找到程序集");
            Dictionary<string, string> data = new Dictionary<string, string>();
            foreach (Type classType in assem.GetTypes().Where(t => t.Namespace == "Y.Infrastructure.Pay.Merchants"))
            {

                PropertyInfo[] members = classType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                Dictionary<string, string> config_data = new Dictionary<string, string>();
                foreach (var item in members)
                {
                    config_data.Add(item.Name, item.GetCustomAttribute<DescriptionAttribute>().Description);
                }
                if (classType.GetCustomAttribute<DescriptionAttribute>() != null)
                    data.Add($"{classType.Name}#{classType.GetCustomAttribute<DescriptionAttribute>().Description}", config_data.ToJson());
            }

            // 2. 检测数据是否存在数据库了
            //Dictionary<string, string> notExistIndb = new Dictionary<string, string>();
            List<string> strList = new List<string>();
            var keys = await _payCategoryService.GetAllPayTypeAsync();
            foreach (var d in data)
            {
                var key = d.Key.Split("#")[0];
                if (!keys.Contains(key))
                {
                    //notExistIndb.Append(d);
                    // 3. 插入不存在于数据库中的数据
                    var rt = await _payCategoryService.InsertAsync(d);
                    strList.Add($"{d.Key.Split("#")[1]}:{rt.Item2}");
                }
            }

            return (true, String.Join(",", strList));
        }





        /// <summary>
        /// 从反射接口获取到商户信息
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetWithdrawalsMerchantFromInterfaceAsync()
        {
            string currentDirectory = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            // 1. 获取到程序集的所有数据
            Assembly assem = Assembly.LoadFrom($"{currentDirectory}\\Y.Infrastructure.Withdrawals.dll");
            if (assem == null) throw new Exception("未找到程序集");
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (Type classType in assem.GetTypes().Where(t => t.Namespace == "Y.Infrastructure.Withdrawals.Merchants"))
            {

                PropertyInfo[] members = classType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                Dictionary<string, string> config_data = new Dictionary<string, string>();
                foreach (var member in members)
                {
                    //config_data.Add(item.Name, item.GetCustomAttribute<DescriptionAttribute>().Description);
                    var attr = member.GetCustomAttribute<PayDbAttribute>();
                    if (attr == null) continue;
                    if (attr.IsConfig == PayConfig.True)
                        config_data.Add(member.Name, attr.Name);
                }
                //data.Add($"{classType.Name}#{classType.GetCustomAttribute<DescriptionAttribute>().Description}", config_data.ToJson());
                data.Add(classType.Name, classType.GetCustomAttribute<DescriptionAttribute>().Description);
            }
            return data;
        }


        /// <summary>
        /// 获取指定代付需要配置的字符串
        /// </summary>
        /// <param name="merchantClassName"></param>
        /// <param name="isPayment">true:支付，false:代付</param>
        /// <returns></returns>
        public async Task<string> GetMerchantConfigMembersFromInterfaceAsync(string merchantClassName, bool isPayment)
        {
            string dllName = isPayment ? "Y.Infrastructure.Pay.dll" : "Y.Infrastructure.Withdrawals.dll";
            string nameSpace = isPayment ? "Y.Infrastructure.Pay.Merchants" : "Y.Infrastructure.Withdrawals.Merchants";

            string currentDirectory = System.IO.Path.GetDirectoryName(GetType().Assembly.Location);
            Assembly assem = Assembly.LoadFrom($"{currentDirectory}\\{dllName}");
            if (assem == null) throw new Exception("未找到程序集");
            Dictionary<string, string> data = new Dictionary<string, string>();

            var classType = assem.GetTypes().Where(t => t.Namespace == nameSpace).Where(t => t.Name == merchantClassName).SingleOrDefault();
            if (classType == null) throw new Exception($"未找到类{merchantClassName}");

            PropertyInfo[] members = classType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var member in members)
            {
                var attr = member.GetCustomAttribute<PayDbAttribute>();
                if (attr == null) continue;
                if (attr.IsConfig == PayConfig.True)
                    data.Add(member.Name, attr.Name);
            }

            return data.ToJson();
        }

        /// <summary>
        /// 获取代付所有的字符串与属性信息
        /// </summary>
        /// <param name="merchantClassName"></param>
        /// <param name="isPayment">是否是支付，true是支付</param>
        /// <returns></returns>
        public Dictionary<string, PayDbAttribute> GetMerchantMembersAndAttrFromInterface(string merchantClassName, bool isPayment)
        {
            string dllName = isPayment ? "Y.Infrastructure.Pay.dll" : "Y.Infrastructure.Withdrawals.dll";
            string nameSpace = isPayment ? "Y.Infrastructure.Pay.Merchants" : "Y.Infrastructure.Withdrawals.Merchants";

            // 路径需要考虑linux下环境
            string currentDirectory = Y.Infrastructure.Library.Core.Helper.ServerHelper.GetAssemblyPath();
            string assemPath = Path.Combine(currentDirectory, dllName);
            Assembly assem = Assembly.LoadFrom(assemPath);
            if (assem == null) throw new Exception($"未找到程序集{assemPath}");
            Dictionary<string, PayDbAttribute> data = new Dictionary<string, PayDbAttribute>();

            var classType = assem.GetTypes().Where(t => t.Namespace == nameSpace).Where(t => t.Name == merchantClassName).SingleOrDefault();
            if (classType == null) throw new Exception($"未找到类{nameSpace}.{merchantClassName}");

            PropertyInfo[] members = classType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            foreach (var member in members)
            {
                var attr = member.GetCustomAttribute<PayDbAttribute>();
                if (attr == null) continue;
                data.Add(member.Name, attr);
            }

            return data;
        }


        public Dictionary<string, string> ToWithdrawalsConfig(string merchantConfig, Dictionary<string, PayDbAttribute> attrs, Dictionary<string, string> dicValus)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            JObject jo = JObject.Parse(merchantConfig);
            foreach (var attr in attrs)
            {
                string configdValue = string.Empty;
                switch (attr.Value.DbType)
                {
                    case PaySQLDbType.GateWay:
                    case PaySQLDbType.Key:
                    case PaySQLDbType.PrivateKey:
                    case PaySQLDbType.PublicKey:
                    case PaySQLDbType.String:
                        break;
                    case PaySQLDbType.Url:
                        configdValue = dicValus["CallbackUrl"];
                        break;
                    case PaySQLDbType.OrderId:
                        configdValue = dicValus["OrderId"];
                        break;
                    case PaySQLDbType.MoneyInteger:
                        configdValue = dicValus["Amount"];
                        break;
                    case PaySQLDbType.Money2Decimal:
                        configdValue = decimal.Parse(dicValus["Amount"]).ToString("f2");
                        break;
                    case PaySQLDbType.IP:
                        configdValue = dicValus["IP"];
                        break;
                    case PaySQLDbType.Random:
                        configdValue = RandomHelper.GetNumber().ToString();
                        break;
                    case PaySQLDbType.DateTime:
                        configdValue = DateTime.UtcNow.AddHours(8).ToString();
                        break;
                    case PaySQLDbType.Second:
                        configdValue = DateTime.UtcNow.AddHours(8).GetTimeStampToSeconds().ToString();
                        break;
                    case PaySQLDbType.Millisecond:
                        configdValue = DateTime.UtcNow.AddHours(8).GetTimeStampTicks().ToString();
                        break;
                    case PaySQLDbType.BankName:
                        configdValue = dicValus["BankName"];
                        break;
                    case PaySQLDbType.BankAccount:
                        configdValue = dicValus["BankAccount"];
                        break;
                    case PaySQLDbType.BankAccountName:
                        configdValue = dicValus["BankAccountName"];
                        break;
                    default:
                        configdValue = null;
                        break;
                }

                if (attr.Value.DefaultStr != null)
                    configdValue = attr.Value.DefaultStr.ToString();

                if (jo[attr.Key] != null && jo[attr.Key].Value<string>() != "")
                    configdValue = jo[attr.Key].Value<string>();

                dic.Add(attr.Key, configdValue);
            }

            return dic;
        }






    }
}
