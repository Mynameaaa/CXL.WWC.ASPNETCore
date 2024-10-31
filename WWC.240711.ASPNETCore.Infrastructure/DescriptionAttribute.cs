using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWC._240711.ASPNETCore.Infrastructure
{
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

    }
}
