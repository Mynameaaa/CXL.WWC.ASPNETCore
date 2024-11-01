using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWC._240711.ASPNETCore.Database.Models;
using WWC._240711.ASPNETCore.Infrastructure;

namespace WWC._240711.ASPNETCore.Database.Extensions
{
    public static class ModelExtensions
    {

        /// <summary>
        /// 设置创建信息
        /// </summary>
        /// <param name="create"></param>
        /// <param name="userInfo"></param>
        public static void SetCreateUser(this CreateUser create, AuthUserInfo userInfo)
        {
            create.CreateTime = DateTime.Now;
            create.CreateID = userInfo.UserID;
            create.CreateName = userInfo.UserName;
        }

        /// <summary>
        /// 设置修改信息
        /// </summary>
        /// <param name="create"></param>
        /// <param name="userInfo"></param>
        public static void SetUpdateUser(this UpdateUser update, AuthUserInfo userInfo)
        {
            update.UpdateTime = DateTime.Now;
            update.UpdateID = userInfo.UserID;
            update.UpdateName = userInfo.UserName;
        }

        /// <summary>
        /// 设置保存信息
        /// </summary>
        /// <param name="create"></param>
        /// <param name="userInfo"></param>
        public static void SetSaveUser(this SaveUser save, AuthUserInfo userInfo)
        {
            var datetime = DateTime.Now;
            save.CreateTime = datetime;
            save.CreateID = userInfo.UserID;
            save.CreateName = userInfo.UserName;
            save.UpdateTime = datetime;
            save.UpdateID = userInfo.UserID;
            save.UpdateName = userInfo.UserName;
        }

    }
}
