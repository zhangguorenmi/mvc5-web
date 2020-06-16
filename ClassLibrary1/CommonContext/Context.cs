using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

//2020/03/12
//新数据上下文   
//DbContext不能直接取用需要继承初始化

namespace MVC2020.Common
{

    //新上下文（初始字符串/创建数据库）
    public class Context : DbContext
    {
        

        ///// <summary>
        ///// 角色模型
        ///// </summary>
        //public DbSet<Role> Roles { get; set; }

    

 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public Context( )
            : base("DefaultConnection")
        {
            Database.SetInitializer<Context>(new CreateDatabaseIfNotExists<Context>());

        }
    }
}
