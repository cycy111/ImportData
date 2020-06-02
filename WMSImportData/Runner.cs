using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMSImportData.BLL;
using WMSImportData.Model;

namespace WMSImportData
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;
        private IImportData _importData;
        private IConfiguration _configuration;
        public Runner(ILogger<Runner> logger,
           IImportData ImportData, IConfiguration configuration
            )
        {
            _logger = logger;
            _importData = ImportData;
            _configuration = configuration;
        }
        public async Task DoAction(string name)
        {
            try
            {
                if (name == "ImportData")
                {
                    Console.WriteLine("开始读取文件...");
                    string path = "";
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    if (!string.IsNullOrEmpty(_configuration["ApiConfig:OneColumnFile"]))
                    {
                        path= _configuration["ApiConfig:OneColumnFile"];
                        await _importData.RunImport(path, false);

                    }
                    if (!string.IsNullOrEmpty(_configuration["ApiConfig:FileName"]))
                    {
                        path = _configuration["ApiConfig:FileName"];
                        await _importData.RunImport(path, true);

                    }
                    //_logger.LogInformation($"同步成功 {_syncAccount.successCount} 条记录； 同步失败 {_syncAccount.failCount} 条记录");
                    Thread.Sleep(5000);
                    //if (s == "success")
                    //{
                    //    Console.WriteLine("同步完成");
                    //    return 0;
                    //}
                    //else
                    //{
                    //    _logger.LogError("同步中出现无法修复的错误，请稍后重试!");
                    //    return 1;
                    //}
                }
                else
                {
                    return ;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);
                return ;
            }

        }

        
    }
}
