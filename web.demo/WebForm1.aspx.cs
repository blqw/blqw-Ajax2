using blqw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxDemo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax2.ThrowStack = false;//全局标识 : 是否抛出详细异常堆栈 ,默认false, 可以在global中设定
            Ajax2.Register(this);
            Ajax2.RegisterVar("datetime.now", DateTime.Now);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Ajax2.Alert("成功!");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Ajax2.RegisterVar("datetime.now", DateTime.Now);
        }

        // Ajax1,服务期延迟2秒返回
        [AjaxMethod]
        public string PostAjax1()
        {
            Thread.Sleep(2000);
            return "成功!!!";
        }

        //PostAjax2,注入Alert
        [AjaxMethod]
        public void PostAjax2()
        {
            Ajax2.Alert("Ajax注入Alert成功!!!!");
        }

        //PostAjax3,改变变量ccc
        [AjaxMethod]
        public void PostAjax3()
        {
            Ajax2.RegisterVar("datetime.now", DateTime.Now);
        }

        //PostAjax4,返回服务器错误,具体请按F12查看
        [AjaxMethod]
        public void PostAjax4()
        {
            var i = 0;
            i = 7 / i;  //触发除0异常
        }

        //PostAjax5,服务器方法叫PostAjax5,特性将调用名改为ReNameAjax5()
        [AjaxMethod("ReNameAjax5")]
        public void PostAjax5()
        {
            Ajax2.Alert("PostAjax5调用成功!!!!");
        }

        //PostAjax6,接受一般参数,参数1 数字 567,参数2 字符串 "asd", 参数3 时间 new Date()
        [AjaxMethod()]
        public void PostAjax6(int i, string s, DateTime d)
        {
            Ajax2.Alert("int:" + i);
            Ajax2.Alert("string:" + s);
            Ajax2.Alert("DateTime:" + d);
        }

        //PostAjax7,接受Json参数, { Name: "冰麟轻武" , Age : 28,Birthday:"1986-10-29" }
        [AjaxMethod()]
        public void PostAjax7(User user)
        {
            Ajax2.Alert(string.Join(Environment.NewLine, "姓名:"+ user.Name, "年龄:"+ user.Age, "生日:"+ user.Birthday));
        }

        //PostAjax8,返回Json对象
        [AjaxMethod()]
        public User PostAjax8()
        {
            return new User() { Name = "小明", Age = 1, Birthday = new DateTime(2013, 4, 5) };
        }

        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}