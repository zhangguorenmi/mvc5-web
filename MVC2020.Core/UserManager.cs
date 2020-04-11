using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Core.GeneralTypes;
using MVC2020.Core.Model;

namespace MVC2020.Core
{

    /// <summary>
    /// 用户管理类
    /// </summary>
    public class UserManager : BaseManager<User>
    {
        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="accounts">用户名[不区分大小写]</param>
        /// <returns></returns>
        public bool HasUsername(string username)
        {
            return base.Repository.IsContains(u => u.Username.ToUpper() == username.ToUpper());
        }

        /// <summary>
        /// Email是否存在
        /// </summary>
        /// <param name="email">Email[不区分大小写]</param>
        /// <returns></returns>
        public bool HasEmail(string email)
        {
            return base.Repository.IsContains(u => u.Email.ToUpper() == email.ToUpper());
        }

        /// <summary>
        /// 添加【返回值Response.Code:0-失败，1-成功，2-账号已存在，3-Email已存在】
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public override Response Add(User user)
        {
            Response _resp = new Response();
            //账号是否存在
            if(!string.IsNullOrEmpty(user.Username) && HasUsername(user.Username))
            {
                _resp.Code = 2;
                _resp.Message = "用户名已存在";
            }
            //Email是否存在
            if(!string.IsNullOrEmpty(user.Email) && HasUsername(user.Email))
            {
                _resp.Code = 3;
                _resp.Message = "Email已存在";
            }
            if(_resp.Code == 0) _resp = base.Add(user);
            return _resp;
        }




    }
}
