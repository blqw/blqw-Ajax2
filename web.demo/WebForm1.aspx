<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AjaxDemo.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            使用ASP控件,在服务器click事件中注入Alert<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <pre>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Ajax2.Alert("成功!");
        }
            </pre>
            <hr />

            使用ASP控件,在服务器click事件中注入变量datetime.now
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
            当前datetime.now的值:<label id="lable1"></label>
            <pre>
        protected void Button2_Click(object sender, EventArgs e)
        {
            Ajax2.RegisterVar("datetime.now", DateTime.Now);
        }
            </pre>
            <hr />

            触发PostAjax1,服务期延迟2秒返回:<input type="button" name="name" value="Ajax" onclick="ajax1()" /><input id="text1" value="" />
            <pre>
        //后台
        [AjaxMethod]
        public string PostAjax1()
        {
            Thread.Sleep(2000);
            return "成功!!!";
        }
        //前台
        onclick="ajax1()"
        function ajax1() {
            text1.value = "正在获取数据...";
            setTimeout(function () {
                var a = PostAjax1();
                text1.value = "服务器返回:" + a;
            }, 0);
        }
            </pre>
            <hr />

            触发PostAjax2,注入Alert:<input type="button" name="name" value="Ajax" onclick="PostAjax2();" />
            <pre>
        //后台
        [AjaxMethod]
        public void PostAjax2()
        {
            Ajax2.Alert("Ajax注入Alert成功!!!!");
        }
        //前台
        onclick="PostAjax2();"
            </pre>
            <hr />

            触发PostAjax3,改变变量datetime.now:<input type="button" name="name" value="Ajax" onclick="PostAjax3(); alert('当前datetime.now的值:' + datetime.now)" />
            <pre>
        //后台
        [AjaxMethod]
        public void PostAjax3()
        {
            Ajax2.RegisterVar("datetime.now", DateTime.Now);
        }
        //前台
        onclick="PostAjax3(); alert('当前datetime.now的值:' + datetime.now)"
            </pre>
            <hr />

            触发PostAjax4,返回服务器错误,具体请按F12查看:<input type="button" name="name" value="Ajax" onclick="PostAjax4()" />
            <pre>
        //后台
        [AjaxMethod]
        public void PostAjax4()
        {
            var i = 0;
            i = 7 / i;  //触发除0异常
        }
        //前台
        onclick="PostAjax4()"
            </pre>
            <hr />

            触发PostAjax5,服务器方法叫PostAjax5,特性将调用名改为ReNameAjax5()<input type="button" name="name" value="Ajax" onclick="ReNameAjax5()" />
            <pre>
        //后台
        [AjaxMethod("ReNameAjax5")]
        public void PostAjax5()
        {
            Ajax2.Alert("PostAjax5调用成功!!!!");
        }
        //前台
        onclick="ReNameAjax5()"
            </pre>
            <hr />

            触发PostAjax6,接受一般参数:<input type="button" name="name" value="Ajax" onclick="PostAjax6(123, 'blqw', new Date())" />
            <pre>
        //后台
        [AjaxMethod()]
        public void PostAjax6(int i, string s, DateTime d)
        {
            Ajax2.Alert("int:" + i);
            Ajax2.Alert("string:" + s);
            Ajax2.Alert("DateTime:" + d);
        }
        //前台
        onclick="PostAjax6(123, 'blqw', new Date())"
            </pre>
            <hr />

            触发PostAjax7,接受Json参数:<input type="button" name="name" value="Ajax" onclick="PostAjax7({ Name: '冰麟轻武', Age: 28, Birthday: '1986-10-29' })" />
            <pre>
        //后台
        [AjaxMethod()]
        public void PostAjax7(User user)
        {
            Ajax2.Alert(string.Join(Environment.NewLine, "姓名:"+ user.Name, "年龄:"+ user.Age, "生日:"+ user.Birthday));
        }
        //前台
        onclick="PostAjax7({ Name: '冰麟轻武', Age: 28, Birthday: '1986-10-29' })"
            </pre>
            <hr />

            触发PostAjax8,返回Json对象:<input type="button" name="name" value="Ajax" onclick="ajax8()" />
            <pre>
        //后台
        [AjaxMethod()]
        public User PostAjax8()
        {
            return new User() { Name = "小明", Age = 1, Birthday = new DateTime(2013, 4, 5) };
        }
        //前台
        onclick="ajax8()"
        function ajax8() {
            var user = PostAjax8();
            var message = ["姓名:"+ user.Name, "年龄:"+ user.Age, "生日:"+ user.Birthday].join("\n\r");
            alert(message);
        }
            </pre>
            <hr />


        </div>
    </form>
    <script>

        window.onload = function () {
            lable1.innerText = datetime.now;
        }

        function ajax1() {
text1.value = "正在获取数据...";
setTimeout(function () {
    var a = PostAjax1();
    text1.value = "服务器返回:" + a;
}, 0);
        }

        function myfunction() {

        }

function ajax8() {
    try {
        var user = PostAjax8();
        var message = ["姓名:" + user.Name, "年龄:" + user.Age, "生日:" + user.Birthday].join("\n\r");
        alert(message);
    } catch (e) {
        alert("出现异常:" + e.message);
    }
}

    </script>
</body>
</html>
