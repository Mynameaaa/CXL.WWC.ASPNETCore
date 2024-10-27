using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.IService;

namespace WWC._240711.Extensions.ThirdPartyCache.Service
{
    public class RedisSetCacheService : ISetCacheService
    {
        private readonly CSRedisClient _redisClient;

        public RedisSetCacheService(CSRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        /// <summary>
        /// 添加元素到 SET（使用泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddToSetAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
                throw new ArgumentException("密钥和值不能为 null 或空。");

            return await _redisClient.SAddAsync(key, value.ToString()) > 0;
        }

        /// <summary>
        /// 添加元素到 SET（使用泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool AddToSet<T>(string key, T value)
        {
            return this.AddToSetAsync(key, value).Result;
        }

        /// <summary>
        /// 从 SET 中移除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> RemoveFromSetAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
                throw new ArgumentException("密钥和值不能为 null 或空。");

            return await _redisClient.SRemAsync(key, value.ToString()) > 0;
        }

        /// <summary>
        /// 从 SET 中移除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> RemoveFromSet<T>(string key, T value)
        {
            return await this.RemoveFromSetAsync(key, value);
        }

        /// <summary>
        /// 删除 SET 中的所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> ClearSetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("密钥不能为 null 或空。");

            return await _redisClient.DelAsync(key) > 0;
        }

        /// <summary>
        /// 删除 SET 中的所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool ClearSet(string key)
        {
            return this.ClearSetAsync(key).Result;
        }

        /// <summary>
        /// 检查元素是否在 SET 中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool IsMember<T>(string key, T value)
        {
            return this.IsMemberAsync(key, value).Result;
        }

        /// <summary>
        /// 检查元素是否在 SET 中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> IsMemberAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null)
                throw new ArgumentException("密钥和值不能为 null 或空。");

            return await _redisClient.SIsMemberAsync(key, value.ToString());
        }

        /// <summary>
        /// 获取 SET 中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetRandomMemberAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("密钥不能为 null 或空。");

            return await _redisClient.SRandMemberAsync(key);
        }

        /// <summary>
        /// 获取 SET 中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetRandomMember(string key)
        {
            return this.GetRandomMemberAsync(key).Result;
        }

        /// <summary>
        /// 获取 SET 中所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public HashSet<string> GetAllMembers(string key)
        {
            return this.GetAllMembersAsync(key).Result;
        }

        /// <summary>
        /// 获取 SET 中所有元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<HashSet<string>> GetAllMembersAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("密钥不能为 null 或空。");

            var members = await _redisClient.SMembersAsync(key);
            return new HashSet<string>(members);
        }

        /// <summary>
        /// 获取 SET 的元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long GetSetCount(string key)
        {
            return this.GetSetCountAsync(key).Result;
        }

        /// <summary>
        /// 获取 SET 的元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<long> GetSetCountAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("密钥不能为 null 或空。");

            return await _redisClient.SCardAsync(key);
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public HashSet<string> IntersectSets(params string[] keys)
        {
            return this.IntersectSetsAsync(keys).Result;
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<HashSet<string>> IntersectSetsAsync(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
                throw new ArgumentException("密钥不能为 null 或空。");

            var members = await _redisClient.SInterAsync(keys);
            return new HashSet<string>(members);
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public HashSet<string> UnionSets(params string[] keys)
        {
            return this.UnionSetsAsync(keys).Result;
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<HashSet<string>> UnionSetsAsync(params string[] keys)
        {
            if (keys == null || keys.Length == 0)
                throw new ArgumentException("密钥不能为 null 或空。");

            var members = await _redisClient.SUnionAsync(keys);
            return new HashSet<string>(members);
        }

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<HashSet<string>> DifferenceSetsAsync(string key1, string key2)
        {
            if (string.IsNullOrWhiteSpace(key1) || string.IsNullOrWhiteSpace(key2))
                throw new ArgumentException("密钥不能为 null 或空。");

            var members = await _redisClient.SDiffAsync(key1, key2);
            return new HashSet<string>(members);
        }

        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public HashSet<string> DifferenceSets(string key1, string key2)
        {
            return this.DifferenceSetsAsync(key1, key2).Result;
        }

    }
}
