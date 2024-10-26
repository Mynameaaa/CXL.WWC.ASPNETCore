using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.IService
{
    public interface IStringCacheService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<bool> SetAsync(string key, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Set(string key, string value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<bool> SetSerializeAsync<T>(string key, T value);

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetSerializeAsync<T>(string key);

    }
}
