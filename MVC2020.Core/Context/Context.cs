using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2020.Core.Model;

//2020/03/12
//新数据上下文   
//DbContext不能直接取用需要继承初始化

namespace MVC2020.Core.Context
{

    //新上下文（初始字符串/创建数据库）
    public class Context : DbContext
    {
        /// <summary>
        /// 管理员模型
        /// </summary>
        public DbSet<Administrator> administrators { get; set; }

        /// <summary>
        /// 角色模型
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 用户模型
        /// </summary>
        public DbSet<User> User { get; set; }

 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public Context( )
            : base("DefaultConnection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());

        }
    }
}
