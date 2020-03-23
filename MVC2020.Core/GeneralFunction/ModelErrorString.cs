using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


///获取ModelState错误信息的方法（客户端提交未验证，服务器验证，然后客户端获取服务器结果）
///在项目中有些内容是通过AJAX方法提交，如果提交时客户端没有进行验证，在服务器端进行验证时会将错误信息保存在ModelState中，这里需要写一个方法来获取ModelState的错误信息，以便反馈给客户端。

namespace MVC2020.Core.GeneralFunction
{
    
    public class ModelErrorString
    {

        /// <summary>
        /// 获取模型错误
        /// </summary>
        /// <param name="modelState">模型状态</param>
        /// <returns></returns>
        public static string GetModelErrorString(ModelStateDictionary modelState)
        {
            StringBuilder _sb = new StringBuilder();
            var _ErrorModelState = modelState.Where(m => m.Value.Errors.Count() > 0);
            foreach(var item in _ErrorModelState)
            {
                foreach(var modelError in item.Value.Errors)
                {
                    _sb.AppendLine(modelError.ErrorMessage);
                }
            }
            return _sb.ToString();
        }

    }
}
