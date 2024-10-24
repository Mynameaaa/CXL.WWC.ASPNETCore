using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using WWC._240711.Extensions.ThirdParty.Consol;

namespace WWC._240711.ASPNETCore.Extensions;

public class ConsulUnRegisterHostApplicationLifetime : IHostedService
{
	private readonly IConsulRegisterService _consulRegisterService;
	private readonly IHostApplicationLifetime _hostApplicationLifetime;

	public ConsulUnRegisterHostApplicationLifetime(IConsulRegisterService consulRegisterService,
												   IHostApplicationLifetime hostApplicationLifetime)
	{
		_consulRegisterService = consulRegisterService;
		_hostApplicationLifetime = hostApplicationLifetime;

		// ע�� ApplicationStarted �¼�
		_hostApplicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
		// ע�� ApplicationStopping �¼�
		_hostApplicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
	}

	// ����Ӧ������ʱ���߼�
	private void OnApplicationStarted()
	{
		Log.Information("Ӧ�������ɹ���Consul ע�����");
	}

	// ����Ӧ��ֹͣʱ���߼�
	private void OnApplicationStopping()
	{
		Log.Information("Ӧ��ֹͣ�У�׼��ע�� Consul ����");
        _consulRegisterService.StopServicesAsync().Wait();
	}

	// ��Щ�� IHostedService �ӿڵı�Ҫʵ�֣�����������������������ֹͣ��
	public Task StartAsync(CancellationToken cancellationToken)
	{
		// Ӧ������ʱ������
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		// Ӧ�ùر�ʱ������
		return Task.CompletedTask;
	}
}
