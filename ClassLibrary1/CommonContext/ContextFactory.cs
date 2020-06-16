using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

//ContextFactory是一个简单工厂类 2020/03/12

//CurrentContext()是一个静态函数,用来获取当前线程DbContext。

//CurrentContext()静态方法返回数据上下文Context。
//CallContext类在线程中存储Context。

namespace MVC2020.Common
{
    /// <summary>
    /// 数据上下文工厂类 
    /// </summary>
    public class ContextFactory
    {

        /// <summary>
        /// 获取当前线程的数据上下文
        /// </summary>
        /// <returns>数据上下文</returns>
        public static Context CurrentContext( )
        {
            Context _nContext = CallContext.GetData("Context") as Context;//CallContext.GetData获取Context
            if(_nContext == null)
            {
                _nContext = new Context();
                CallContext.SetData("Context",_nContext);//CallContext.SetData存储Context
            }
            return _nContext;
        }
    }


}
