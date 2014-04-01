<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="web.demo.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" name="name" value="按钮" onclick="getUser(1)" />
        <input type="button" name="name" value="异常" onclick="getUser(-1)" />
    </div>
    </form>
    <script>
        function getUser(id) {
            var user = GetUserInfo(id);
            alert(user.Name);
        }
    </script>
</body>
</html>
