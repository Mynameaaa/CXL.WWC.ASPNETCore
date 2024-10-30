using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Database.Models;

public interface ApproveUser
{

    /// <summary>
    /// 审核人编号
    /// </summary>
    long ApproveID { get; set; }

    /// <summary>
    /// 审核人名称
    /// </summary>
    string ApproveName { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    DateTime ApproveTime { get; set; }

}
