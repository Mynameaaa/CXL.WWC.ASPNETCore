using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Infrastructure
{
    /// <summary>
    /// 500+ 异常统一返回值
    /// </summary>
    public class ErrorMessageResult : MessageResult
    {
        /// <summary>   
        /// 异常消息
        /// </summary>
        public string ErrorMessage { get; set; }
    
        /// <summary>
        /// 异常详细信息
        /// </summary>
        public object ExceptionDetail { get; set; }

        /// <summary>
        /// 异常堆栈信息
        /// </summary>
        public string ErrorStackTrace { get; set; }

        /// <summary>
        /// 发生的异常类型
        /// </summary>
        public string ExceptionTypeName { get; set; }

    }
}
