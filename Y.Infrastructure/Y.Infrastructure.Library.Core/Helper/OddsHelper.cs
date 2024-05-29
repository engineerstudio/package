namespace Y.Infrastructure.Library.Core.Helper
{
    /// <summary>
    /// 核心思想是把赔率转换为胜率然后进行相互转换
    /// </summary>
    public class OddsHelper
    {
        /// <summary>
        /// 美赔换算欧赔
        /// </summary>
        /// <param name="AMOdds"></param>
        /// <returns></returns>
        public static decimal AMToDE(decimal aMOdds)
        {
            if (aMOdds > 0) return 1 + (aMOdds / 100);
            else return 1 - (100 / aMOdds);
        }


        /// <summary>
        /// 香港赔率转欧赔
        /// </summary>
        /// <param name="hkOdds"></param>
        /// <returns></returns>
        public static decimal HKToDE(decimal hkOdds)
        {
            return 1 + hkOdds;
        }


        /// <summary>
        /// 马来盘赔率转欧赔
        /// </summary>
        /// <param name="myOdds"></param>
        /// <returns></returns>
        public static decimal MYToDE(decimal myOdds)
        {
            if (myOdds > 0) return myOdds + 1;
            else return 1 - (1 / myOdds);
        }

        /// <summary>
        /// 印尼盘赔率转欧赔
        /// </summary>
        /// <param name="idOdds"></param>
        /// <returns></returns>
        public static decimal IDToDE(decimal idOdds)
        {
            if (idOdds > 1) return idOdds + 1;
            else return (-1 / idOdds) + 1;
        }

        /// <summary>
        /// 缅甸盘赔率转欧赔 , 未实现
        /// </summary>
        /// <param name="mrOdds"></param>
        /// <returns></returns>
        public static decimal MRToDE(decimal mrOdds)
        {
            return default(decimal);
        }
    }
}