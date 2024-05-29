using System;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.Mapper
{
    public class ModelToEntity
    {
        /// <summary>
        /// R代表目标实体   T代表数据源实体
        /// </summary>
        /// <typeparam name="R">目标实体</typeparam>
        /// <typeparam name="T">数据源实体</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static R Mapping<R, T>(T model)
        {
            R result = Activator.CreateInstance<R>();
            foreach (PropertyInfo info in typeof(R).GetProperties())
            {
                PropertyInfo pro = typeof(T).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }

            return result;
        }


        // TODO 增加两个MOdel的映射，一个Model映射到另一个model, 存在相同属性则写入，不存在则忽略
    }


    public static class Mapper
    {
        /// <summary>
        /// 针对属性相同的进行映射
        /// </summary>
        /// <typeparam name="Resource"></typeparam>
        /// <typeparam name="Target"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static Target Mapping<Resource, Target>(this Resource model)
        {
            Target result = Activator.CreateInstance<Target>();
            foreach (PropertyInfo info in typeof(Target).GetProperties())
            {
                PropertyInfo pro = typeof(Resource).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }

            return result;
        }
    }
}