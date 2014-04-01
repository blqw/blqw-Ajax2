超级方便的Asp.Net  Ajax解决方案
http://www.cnblogs.com/blqw/p/Ajax.html

主要代码如下 <br />
服务器端 <br /> 
protected void Page_Load(object sender, EventArgs e) <br /> 
{ <br /> 
    Ajax2.Register(this);//注册当前页面 <br /> 
} <br /> 

[AjaxMethod]//声明Ajax方法 <br /> 
public object GetUserInfo(int id) <br /> 
{ <br /> 
    if (id < 0) <br /> 
    { <br /> 
        throw new ArgumentOutOfRangeException("id", "id不能小于0"); <br /> 
    } <br /> 
    return new { ID = id, Name = "blqw" + id }; <br /> 
} <br /> 
web页面 直接调用 <br /> 
&lt;script&gt; <br /> 
    function getUser(id) { <br /> 
        var user = GetUserInfo(id); <br /> 
        alert(user.Name); <br /> 
    }
&lt;/script&gt;