using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Linq.Expressions;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace 测试
{
    public partial class 匿名函数 : Form
    {
        public 匿名函数( )
        {
            InitializeComponent();
        }

        private void button1_Click(object sender,EventArgs e)
        {
            string mid = ",middle part,";
            ///匿名写法
            Func<string,string> anonDel = delegate(string param)
            {
                param += mid;
                param += " And this was added to the string.";
                return param;
            };
            ///λ表达式写法
            Func<string,string> lambda = param =>
            {
                param += mid;
                param += " And this was added to the string.";
                return param;
            };
            ///λ表达式写法(整形)
            Func<int,int> lambdaint = paramint =>
            {
                paramint = 5;
                return paramint;
            };
            ///λ表达式带有两个参数的写法
            Func<int,int,int> twoParams = (x,y) =>
            {
                return x * y;
            };

            Func<int,bool> lambdaint1 = paramint => paramint > 5;
             

            label1.Text = "匿名方法：" + anonDel("Start of string");
            label2.Text = "λ表达式写法:" + lambda("Lambda expression");
            label3.Text = "λ表达式写法(整形):" + lambdaint(4).ToString();
            label4.Text = "λ表达式带有两个参数:" + twoParams(10,20).ToString();
            label5.Text = " Func<int,bool>" + lambdaint1(4);

        }

        private void label2_Click(object sender,EventArgs e)
        {

        }

        private void 匿名函数_Load(object sender,EventArgs e)
        {

        }

        private void button2_Click(object sender,EventArgs e)
        {

        }


     
    }
}
