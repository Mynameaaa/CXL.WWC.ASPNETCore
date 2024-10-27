using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.IService
{
    public interface ISetCacheService
    {
        /// <summary>
        /// 添加元素到 SET（使用泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> AddToSetAsync<T>(string key, T value);

        /// <summary>
        /// 添加元素到 SET（使用泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        bool AddToSet<T>(string key, T value);

        /// <summary>
        /// 从 SET 中移除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> RemoveFromSetAsync<T>(string key, T value);

        /// <summary>
        /// 从 SET 中移除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> RemoveFromSet<T>(string key, T value);

        /// <summary>
        /// 删除 SET 中的所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> ClearSetAsync(string key);

        /// <summary>
        /// 删除 SET 中的所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        bool ClearSet(string key);

        /// <summary>
        /// 检查元素是否在 SET 中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        bool IsMember<T>(string key, T value);

        /// <summary>
        /// 检查元素是否在 SET 中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> IsMemberAsync<T>(string key, T value);

        /// <summary>
        /// 获取 SET 中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> GetRandomMemberAsync(string key);

        /// <summary>
        /// 获取 SET 中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        string GetRandomMember(string key);

        /// <summary>
        /// 获取 SET 中所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        HashSet<string> GetAllMembers(string key);

        /// <summary>
        /// 获取 SET 中所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<HashSet<string>> GetAllMembersAsync(string key);

        /// <summary>
        /// 获取 SET 的元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        long GetSetCount(string key);

        /// <summary>
        /// 获取 SET 的元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<long> GetSetCountAsync(string key);

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        HashSet<string> IntersectSets(params string[] keys);

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<HashSet<string>> IntersectSetsAsync(params string[] keys);

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        HashSet<string> UnionSets(params string[] keys);

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<HashSet<string>> UnionSetsAsync(params string[] keys);

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<HashSet<string>> DifferenceSetsAsync(string key1, string key2);

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        HashSet<string> DifferenceSets(string key1, string key2);
    }
}
