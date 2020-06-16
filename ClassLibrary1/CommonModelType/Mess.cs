using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Common
{
    /// <summary>
    /// 消息提示
    /// </summary>
    public class Mess
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 按钮组
        /// </summary>
        public List<string> Buttons { get; set; }
    }
}
