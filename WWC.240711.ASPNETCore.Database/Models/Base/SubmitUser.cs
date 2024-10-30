using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public interface SubmitUser
{
    /// <summary>
    /// 提交人编号
    /// </summary>
    long SubmitID { get; set; }

    /// <summary>
    /// 提交人名称
    /// </summary>
    string SubmitName { get; set; }

    /// <summary>
    /// 提交时间
    /// </summary>
    DateTime SubmitTime { get; set; }

}
