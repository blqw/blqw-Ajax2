超级方便的Asp.Net  Ajax解决方案
http://www.cnblogs.com/blqw/p/Ajax.html

主要代码如下
服务器端

protected void Page_Load(object sender, EventArgs e)

{

    Ajax2.Register(this);//注册当前页面

}

[AjaxMethod]//声明Ajax方法

public object GetUserInfo(int id)

{

    if (id < 0)

    {

        throw new ArgumentOutOfRangeException("id", "id不能小于0");

    }

    return new { ID = id, Name = "blqw" + id };

}

web页面 直接调用

function getUser(id) {
    var user = GetUserInfo(id);
    alert(user.Name);
}
