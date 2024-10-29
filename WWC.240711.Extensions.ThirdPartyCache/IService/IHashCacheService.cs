using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.Extensions.ThirdPartyCache.IService
{
    public interface IHashCacheService
    {

        /// <summary>
        /// 添加元素到 Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> AddToHashAsync<T>(string key, string field, T value);

        /// <summary>
        /// 添加元素到 Hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        bool AddToHash<T>(string key, string field, T value);

        /// <summary>
        /// 从 Hash 中获取字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> GetFromHashAsync(string key, string field);

        /// <summary>
        /// 从 Hash 中获取字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        string GetFromHash(string key, string field);

        /// <summary>
        /// 从 Hash 中删除字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> RemoveFieldAsync(string key, string field);

        /// <summary>
        /// 从 Hash 中删除字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        bool RemoveField(string key, string field);



        /// <summary>
        /// 清除整个 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> ClearHashAsync(string key);

        /// <summary>
        /// 清除整个 Hash
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentException"></exception>
        bool ClearHash(string key);

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<Dictionary<string, string>> GetAllFieldsAsync(string key);

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<Dictionary<string, T>> GetAllFieldsAsync<T>(string key);

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Dictionary<string, string> GetAllFields(string key);

        /// <summary>
        /// 获取 Hash 中所有字段及其值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Dictionary<string, T> GetAllFields<T>(string key);

        /// <summary>
        /// 获取 Hash 的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<long> GetFieldCountAsync(string key);

        /// <summary>
        /// 获取 Hash 的字段数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        long GetFieldCount(string key);

        /// <summary>
        /// 获取 Hash 中的一个随机字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<string> GetRandomFieldAsync(string key);

        /// <summary>
        /// 获取 Hash 中的一个随机字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        string GetRandomField(string key);

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetRandomFieldValueAsync(string key);

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetRandomFieldValueAsync<T>(string key);

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetRandomFieldValue(string key);

        /// <summary>
        /// 获取随机字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetRandomFieldValue<T>(string key);

        /// <summary>
        /// 批量添加字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> AddMultipleToHashAsync<T>(string key, Dictionary<string, T> fields);

        /// <summary>
        /// 批量添加字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <exception cref="ArgumentException"></exception>
        bool AddMultipleToHash<T>(string key, Dictionary<string, T> fields);

        /// <summary>
        /// 批量获取字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Task<Dictionary<string, string>> GetMultipleFromHashAsync(string key, IEnumerable<string> fields);

        /// <summary>
        /// 批量获取字段值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        Dictionary<string, string> GetMultipleFromHash(string key, IEnumerable<string> fields);

        /// <summary>
        /// 更新 Hash 中的字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        Task<bool> UpdateFieldAsync<T>(string key, string field, T value);

        /// <summary>
        /// 更新 Hash 中的字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        bool UpdateField<T>(string key, string field, T value);

    }
}
