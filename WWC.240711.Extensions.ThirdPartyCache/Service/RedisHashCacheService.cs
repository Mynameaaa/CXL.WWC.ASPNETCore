using CSRedis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdPartyCache.IService;

namespace WWC._240711.Extensions.ThirdPartyCache.Service
{
    public class RedisHashCacheService : IHashCacheService
    {
        private readonly CSRedisClient _redisClient;

        public RedisHashCacheService(CSRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        /// <summary>
        /// 添加元素到 Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddToHashAsync<T>(string key, string field, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(field) || value == null)
                throw new ArgumentException("Key 字段名称或者值不能为空或 null");

            return await _redisClient.HSetAsync(key, field, value.ToString());
        }

        /// <summary>
        /// 添加元素到 Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool AddToHash<T>(string key, string field, T value)
        {
            return this.AddToHashAsync(key, field, value).Result;
        }

        /// <summary>
        /// 从 Hash 中获取字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetFromHashAsync(string key, string field)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(field))
                throw new ArgumentException("Key 或字段名称不能为空或 null");

            return await _redisClient.HGetAsync(key, field);
        }

        /// <summary>
        /// 从 Hash 中获取字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetFromHash(string key, string field)
        {
            return this.GetFromHashAsync(key, field).Result;
        }

        /// <summary>
        /// 从 Hash 中删除字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> RemoveFieldAsync(string key, string field)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(field))
                throw new ArgumentException("Key 字段名称或者值不能为空或 null");

            return await _redisClient.HDelAsync(key, field) > 0;
        }

        /// <summary>
        /// 从 Hash 中删除字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool RemoveField(string key, string field)
        {
            return this.RemoveFieldAsync(key, field).Result;
        }

        /// <summary>
        /// 清除整个 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> ClearHashAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key 不能为空或 null");

            // 使用 DEL 命令删除整个 Hash
            return await _redisClient.DelAsync(key) > 0;
        }

        /// <summary>
        /// 清除整个 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool ClearHash(string key)
        {
            return  ClearHashAsync(key).Result;
        }

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Dictionary<string, string>> GetAllFieldsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key 不能为空或 null");

            var fields = await _redisClient.HGetAllAsync(key);
            return fields;
        }

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Dictionary<string, T>> GetAllFieldsAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key 不能为空或 null");

            var fields = await _redisClient.HGetAllAsync(key);
            var json = JsonConvert.SerializeObject(fields);
            return JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
        }

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, string> GetAllFields(string key)
        {
            return this.GetAllFieldsAsync(key).Result;
        }

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, T> GetAllFields<T>(string key)
        {
            return this.GetAllFieldsAsync<T>(key).Result;
        }

        /// <summary>
        /// 获取 Hash 的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<long> GetFieldCountAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key 不能为空或 null");

            return await _redisClient.HLenAsync(key);
        }

        /// <summary>
        /// 获取 Hash 的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long GetFieldCount(string key)
        {
            return this.GetFieldCountAsync(key).Result;
        }

        /// <summary>
        /// 获取 Hash 中的一个随机字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<string> GetRandomFieldAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key 不能为空或 null");

            var allFields = await GetAllFieldsAsync(key);
            if (allFields.Count == 0)
                return null;

            var randomField = allFields.Keys.ToList()[new Random().Next(allFields.Count)];
            return randomField;
        }

        /// <summary>
        /// 获取 Hash 中的一个随机字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetRandomField(string key)
        {
            return this.GetRandomFieldAsync(key).Result;
        }

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetRandomFieldValueAsync(string key)
        {
            var field = GetRandomField(key);
            return field != null ? await this.GetFromHashAsync(key, field) : null;
        }

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetRandomFieldValueAsync<T>(string key)
        {
            var field = GetRandomField(key);
            return field != null ? JsonConvert.DeserializeObject<T>(await this.GetFromHashAsync(key, field)) : default(T);
        }

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRandomFieldValue(string key)
        {
            return this.GetRandomFieldValueAsync(key).Result;
        }

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetRandomFieldValue<T>(string key)
        {
            return this.GetRandomFieldValueAsync<T>(key).Result;
        }

        /// <summary>
        /// 批量添加字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> AddMultipleToHashAsync<T>(string key, Dictionary<string, T> fields)
        {
            if (string.IsNullOrWhiteSpace(key) || fields == null || fields.Count == 0)
                throw new ArgumentException("Key 不能为空或 null，并且字段数量不能为 0");

            // 准备 HMSET 命令的参数
            var entries = new List<string>();
            foreach (var field in fields)
            {
                entries.Add(field.Key);
                entries.Add(field.Value?.ToString() ?? throw new Exception("出现了空值")); // 将值转换为字符串
            }

            // 使用 HMSET 一次性添加多个字段
            return await _redisClient.HMSetAsync(key, entries.ToArray());
        }


        /// <summary>
        /// 批量添加字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool AddMultipleToHash<T>(string key, Dictionary<string, T> fields)
        {
            return this.AddMultipleToHashAsync(key, fields).Result;
        }

        /// <summary>
        /// 批量获取字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Dictionary<string, string>> GetMultipleFromHashAsync(string key, IEnumerable<string> fields)
        {
            if (string.IsNullOrWhiteSpace(key) || fields == null || !fields.Any())
                throw new ArgumentException("Key 不能为空或 null，并且字段数量不能为 0");

            // 使用 HMGET 一次性获取多个字段的值
            var values = _redisClient.HMGet(key, fields.ToArray());

            var result = new Dictionary<string, string>();

            // 将字段和值配对到结果字典中
            for (int i = 0; i < fields.Count(); i++)
            {
                if (i < values.Length && values[i] != null)
                {
                    result[fields.ElementAt(i)] = values[i];
                }
            }

            return result;
        }


        /// <summary>
        /// 批量获取字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Dictionary<string, string> GetMultipleFromHash(string key, IEnumerable<string> fields)
        {
            return this.GetMultipleFromHashAsync(key, fields).Result;
        }

        /// <summary>
        /// 更新 Hash 中的字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateFieldAsync<T>(string key, string field, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(field) || value == null)
                throw new ArgumentException("Key 字段名称或者值不能为空或 null");

            return await AddToHashAsync(key, field, value); // 直接使用 AddToHash 方法更新
        }

        /// <summary>
        /// 更新 Hash 中的字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool UpdateField<T>(string key, string field, T value)
        {
            return this.UpdateFieldAsync(key, field, value).Result;
        }

    }
}
