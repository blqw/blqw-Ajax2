超级方便的Asp.Net  Ajax解决方案  
  http://www.cnblogs.com/blqw/p/Ajax.html
  http://www.cnblogs.com/blqw/p/3699880.html

主要代码如下  
服务器端
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    Ajax2.Register(this);			//注册当前页面到Ajax
    if (Ajax2.IsAjaxing == false)	//判断当前请求是否是正在Ajax
    {
		//注册Pager对象
        Ajax2.RegisterPager(new Pager("pager") //设置Pager对象的js名称,默认:pager
        {
            itemcount = 93,
            pageindex = 0,
            pagesize = 20,
        });
    }
}

[AjaxMethod]
public object GetUserInfo(int id)
{
    if (id < 0)
    {
        throw new ArgumentOutOfRangeException("id", "id不能小于0");
    }
    return new { ID = id, Name = "blqw" + id };
}

[AjaxMethod("fanye")]   //注册Ajax方法,调用方法名为fanye()
public void Test(Pager pager)
{
    pager.pageindex++;			//增加页数
    Ajax2.RegisterPager(pager);	//注册Pager对象
}
```
web页面 直接调用  
```html
用户id:<input type="text" id="user_id" value="1" />
id<0会抛出异常
<br />
<input type="button" name="name" value="获取用户名" onclick="getUser(user_id.value)" />
<br />
<input type="button" name="name" value="下一页" onclick="fanye(pager); alert('当前页:' + pager.pagenumber)" />
<script>
function getUser(id) {
	var user = GetUserInfo(id);
	alert(user.Name);
}
</script>
```