using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Infrastructure
{
    /// <summary>
    /// 消息返回值
    /// </summary>
    public class MessageResult
    {

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

    }

    public class MessageResult<T> : MessageResult
    {
        public T Value { get; set; }
    }

}
