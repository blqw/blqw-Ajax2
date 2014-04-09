<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="web.demo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
用户id:<input type="text" id="user_id" value="1" />
id<0会抛出异常
<br />
<input type="button" name="name" value="获取用户名" onclick="getUser(user_id.value)" />
<br />
<input type="button" name="name" value="下一页" onclick="fanye(pager); alert('当前页:' + pager.pagenumber)" />
    </div>
</form>
<script>
    function getUser(id) {
        var user = GetUserInfo(id);
        alert(user.Name);
        fanye(pager);
    }
</script>
</body>
</html>
