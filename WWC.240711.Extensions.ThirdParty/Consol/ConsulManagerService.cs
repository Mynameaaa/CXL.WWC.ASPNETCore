using Consul;
using Consul.Filtering;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Ocelot.Values;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Infrastructure;
using WWC._240711.Extensions.ThirdParty.Consol;
using WWC._240711.Extensions.ThirdParty.Models;
using WWC._240711.Extensions.ThirdPartyCache.IService;
using WWC._240711.Extensions.ThirdPartyCache.Models;

namespace WWC._240711.Extensions.ThirdParty;

public class ConsulManagerService : IConsulManagerService
{
    private readonly List<ConsulClientOptions> consulOptions;
    private readonly IConsulCacheService _consulCacheService;

    private ConsulManagerService(IOptions<List<ConsulClientOptions>> _consulOptions
        , IConsulCacheService consulCacheService)
    {
        consulOptions = _consulOptions.Value;
        _consulCacheService = consulCacheService;
    }

    public List<string> _disposedServices = new List<string>();

    /// <summary>
    /// 注册 Consul 服务
    /// </summary>
    /// <param name="client"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> RegisterService(ConsulRegisterServiceModel model)
    {
        if (!HasConsulPoint())
            return false;

        // 健康检查配置
        var healthCheck = new AgentCheckRegistration
        {
            HTTP = Path.Combine(model.ServiceHost, model.HealthRoute), // 健康检查 URL
            Interval = TimeSpan.FromSeconds(10),
            Timeout = TimeSpan.FromSeconds(5),
        };

        // 服务注册信息
        var registration = new AgentServiceRegistration
        {
            ID = model.ServiceID,
            Name = model.ServiceName,
            Address = model.ServiceHost, // 或使用动态 IP
            Port = model.Port, // 服务端口
            Tags = model.Tags, // 标签
            Check = healthCheck,
        };

        var registerResult = await ResgisterService(registration);

        return registerResult;
    }

    /// <summary>
    /// 注销服务
    /// </summary>
    /// <param name="client"></param>
    /// <param name="serviceID"></param>
    /// <returns></returns>
    public async Task<bool> StopServiceAsync(string serviceName, string serviceID)
    {
        if (!HasConsulPoint())
            return false;

        var res = await StopServicesAsync(serviceName, [serviceID]);
        return res;
    }

    /// <summary>
    /// 清空服务列表
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<bool> StopServicesAsync(string serviceName)
    {
        if (!HasConsulPoint())
            return false;

        var res = await ClearServicesAsync(serviceName);
        if (res)
            await _consulCacheService.StopServices(serviceName);
        return true;
    }

    /// <summary>
    /// 是否存在 Consul 节点
    /// </summary>
    /// <returns></returns>
    public bool HasConsulPoint()
    {
        return consulOptions != null && consulOptions.Any();
    }

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="registration"></param>
    /// <returns></returns>
    private async Task<bool> ResgisterService(AgentServiceRegistration registration)
    {
        if (!HasConsulPoint())
            return false;

        var result = false;

        // 遍历所有的 Consul 地址，分别注册服务
        foreach (var model in consulOptions!)
        {
            var address = model.Host + ":" + model.Port;
            try
            {
                using (var consulClient = new ConsulClient(config => config.Address = new Uri(address)))
                {
                    await consulClient.Agent.ServiceRegister(registration);
                    Log.Logger.Information($"服务注册成功，Service：【{registration.Address + ":" + registration.Port}】，ServiceID：【{registration.ID}】，Consul：【{address}】");
                    result = true;
                    break;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"连接到 Consul 失败，Consul 连接信息：【{address}】");
            }
        }

        return result;
    }

    /// <summary>
    /// 注销服务
    /// </summary>
    /// <param name="serviceID"></param>
    /// <returns></returns>
    private async Task<bool> StopServicesAsync(string serviceName, List<string> serviceIDs)
    {
        if (!HasConsulPoint())
            return false;

        // 遍历所有的 Consul 地址，分别注册服务
        foreach (var model in consulOptions!)
        {
            var address = model.Host + ":" + model.Port;
            try
            {
                using (var consulClient = new ConsulClient(config => config.Address = new Uri(address)))
                {
                    foreach (var serviceID in serviceIDs)
                    {
                        await consulClient.Agent.ServiceDeregister(serviceID);
                        await _consulCacheService.StopServiceID(serviceName, serviceID);
                        Log.Logger.Information($"服务注销成功，ServiceID：【{serviceID}】");
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"连接到 Consul 失败，Consul 连接信息：【{address}】");
            }
        }

        return false;
    }

    /// <summary>
    /// 清空服务组
    /// </summary>
    /// <param name="serviceID"></param>
    /// <returns></returns>
    private async Task<bool> ClearServicesAsync(string serviceName)
    {
        if (!HasConsulPoint())
            return false;

        // 遍历所有的 Consul 地址，分别注册服务
        foreach (var model in consulOptions!)
        {
            var address = model.Host + ":" + model.Port;
            try
            {
                using (var consulClient = new ConsulClient(config => config.Address = new Uri(address)))
                {
                    var serviceInfos = await _consulCacheService.GetAllServicesByName(serviceName);
                    foreach (var serviceKeyV in serviceInfos)
                    {
                        var service = serviceKeyV.Value;

                        await consulClient.Agent.ServiceDeregister(service.ServiceID);
                        await _consulCacheService.StopServiceID(serviceName, service.ServiceID);
                        Log.Logger.Information($"服务注销成功，ServiceID：【{service.ServiceID}】");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"连接到 Consul 失败，Consul 连接信息：【{address}】");
            }
        }

        return false;
    }

    /// <summary>
    /// 根据名称获取服务组任意服务
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    public async Task<AgentServiceRegistrationCache> GetServiceByName(string serviceName)
    {
        var serviceInfo = await _consulCacheService.GetRandomService(serviceName);
        if (serviceInfo != null)
        {
            return serviceInfo;
        }

        var result = default(AgentServiceRegistrationCache);

        // 遍历所有的 Consul 地址，分别注册服务
        foreach (var model in consulOptions!)
        {
            var address = model.Host + ":" + model.Port;
            try
            {
                using (var consulClient = new ConsulClient(config => config.Address = new Uri(address)))
                {
                    var servicesResponse = await consulClient.Catalog.Service(serviceName);
                    var services = servicesResponse.Response;
                    if (services != null && services.Any())
                    {
                        var cacheModels = services.Select(sl => new AgentServiceRegistrationCache()
                        {
                            Host = sl.Address,
                            Port = sl.ServicePort,
                            ServiceID = sl.ServiceID,
                            ServiceName = sl.ServiceName,
                            ServiceTag = sl.ServiceTags,
                            Node = sl.Node,
                        });

                        result = cacheModels.FirstOrDefault();

                        var cache = await _consulCacheService.CacheServices(serviceName, cacheModels);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"连接到 Consul 失败，Consul 连接信息：【{address}】");
            }
        }
        return result;
    }

}
