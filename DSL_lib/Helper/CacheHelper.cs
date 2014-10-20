using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System;

namespace DSL_lib.Helper
{
    public static class CacheHelper
    {
        public static readonly Hashtable HashCache = new Hashtable();
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            var objCache = HashCache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static bool HasCache(string cacheKey)
        {
            var objCache = HashCache;
            return objCache[cacheKey] != null;
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            var objCache = HashCache;
            objCache.Add(cacheKey, objObject);
        }
    }

    public static class DslCacheHelper
    {
        public static readonly Dictionary<string, DslClassBase> HashCache = new Dictionary<string, DslClassBase>();
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static DslClassBase GetCache(string cacheKey)
        {
            return !HasCache(cacheKey) ? null : HashCache[cacheKey];
        }

        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static bool HasCache(string cacheKey)
        {
            return HashCache.ContainsKey(cacheKey);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, DslClassBase objObject)
        {
            HashCache.Add(cacheKey, objObject);
        }
    }
}
