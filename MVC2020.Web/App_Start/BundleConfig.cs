using System.Web;
using System.Web.Optimization;

namespace MVC2020.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(             //js
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jqueryValidate").Include(    //客户端表单验证
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Scripts/modernizr").Include(         //使用Modernizr进行特征检测实现跨浏览器兼容性
                        "~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/Scripts/bootstrap66").Include(        //bootstrap
                  "~/Scripts/bootstrap/bootstrap.min.js",
                  "~/Scripts/bootstrap/respond.js")); //实现响应式网页设计- 靠前放置放置闪烁

            bundles.Add(new StyleBundle("~/Css/Bootstrap11").Include(
                    "~/Content/bootstrap/bootstrap.min.css",
                    "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Css/Bootstrap22").Include(
            //        "~/Content/bootstrap/bootstrap.min.css",
            //        "~/Content/StyleContent.css"));

            /////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            //bootstrapp 插件js
            bundles.Add(new ScriptBundle("~/Scripts/bootstrapCJ").Include(
                     "~/Scripts/bootstrap/moment-with-locales.js",  //日期插件
                      "~/Scripts/modernizr-2.6.2.js",
                     "~/Scripts/bootstrap/bootstrap-datetimepicker.js",
                     "~/Scripts/bootstrap/bootstrap-dialog.js",
                     "~/Scripts/bootstrap/bootstrap-select.js",
                     "~/Scripts/bootstrap/bootstrap-select-zh_CN.js",
                     "~/Scripts/bootstrap/bootstrap-table.js",
                     "~/Scripts/bootstrap/bootstrap-table-zh-CN.js",
                     "~/Scripts/bootstrap/bootstrap-treeview.js",
                     "~/Scripts/jquery.twbsPagination.js")); //分页插件
            //bootstrapp 插件 css
            bundles.Add(new StyleBundle("~/Css/bootstrapCJ").Include(
                    "~/Content/bootstrap/bootstrap-datetimepicker.css",
                    "~/Content/bootstrap/bootstrap-dialog.css",
                    "~/Content/bootstrap/bootstrap-select.css",
                    "~/Content/bootstrap/bootstrap-table.css",
                    "~/Content/bootstrap/bootstrap-treeview.css"));



            BundleTable.EnableOptimizations = true;  //是否打包压缩


        }
    }
}
