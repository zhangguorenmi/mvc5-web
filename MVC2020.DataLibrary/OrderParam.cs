using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.DataLibrary
{
    /// <summary>
    /// 排序参数
    /// </summary>
    public class OrderParam
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderMethod Method { get; set; }

    }

    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderMethod
    {
        /// <summary>
        /// 正序
        /// </summary>
        ASC = 0,
        /// <summary>
        /// 倒序
        /// </summary>
        DESC = 1
    }
}
