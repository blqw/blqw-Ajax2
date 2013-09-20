using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace blqw
{
    public static class Ajax2
    {
        static class Cache
        {
            static Dictionary<string, object> _Items = new Dictionary<string, object>(255);

            public static object Get(string key, Converter<string, object> get)
            {
                object obj;
                if (_Items.TryGetValue(key, out obj) == false)
                {
                    if (get == null)
                    {
                        return null;
                    }
                    _Items[key] = obj = get(key);
                }
                return obj;
            }

            private static void Set(string key, object cache)
            {
                _Items[key] = cache;
            }

            public static void Remove(string key)
            {
                _Items.Remove(key);
            }

        }

        #region javascript min
#if !DEBUG
        const string JAVASCRIPT = @"window.blqw=window.blqw||{};blqw.Ajax=blqw.Ajax||{};blqw.Ajax.GetRequest=function(){if(window.ActiveXObject){try{return new ActiveXObject(""Msxml2.XMLHTTP"")}catch(A){return new ActiveXObject(""Microsoft.XMLHTTP"")}}else{if(window.XMLHttpRequest){return new XMLHttpRequest()}}};blqw.Ajax.Throw=function(D){function A(F,G,E){this.name=""AjaxError"";this.type=E;this.message=F;this.stack=G;this.innerError=null;this.toString=function(){return""ajaxerr:""+this.message}}var B=new A(D.message,D.stack,D.type);var C=B;while(D.innerError){D=D.innerError;C.innerError=new A(D.message,D.stack,D.type);C=C.innerError}return B};blqw.Ajax.Exec=function(method,args){var getStr=function(obj){if(obj==null){return""""}var type=typeof(obj);switch(type){case""number"":case""boolean"":return obj.toString();case""string"":return encodeURIComponent(obj.replace(""\0"",""\0\0""));case""undefined"":return""undefined"";case""function"":try{return arguments.callee(obj())}catch(e){return""null""}case""object"":type=Object.prototype.toString.apply(obj);switch(type){case""[object Date]"":return encodeURIComponent(obj.getFullYear()+""-""+(obj.getMonth()+1)+""-""+obj.getDate()+"" ""+obj.getHours()+"":""+obj.getMinutes()+"":""+obj.getSeconds()+"".""+obj.getMilliseconds());case""[object RegExp]"":return encodeURIComponent(obj.toString().replace(""\0"",""\0\0""));case""[object Array]"":var arr=[];for(var i in obj){arr.push(arguments.callee(obj[i]))}return arr.join("","");case""[object Object]"":return""[object Object]""}break}};var arr=[];for(var i=0;i<args.length;i++){arr.push(getStr(args[i]))}url=window.location.href;var req=blqw.Ajax.GetRequest();req.open(""POST"",url,false);req.setRequestHeader(""Content-Type"",""application/x-www-form-urlencoded; charset=utf-8"");var ret=req.send(""blqw.ajaxdata=""+arr.join(""\0"")+""&blqw.ajaxmethod=""+method);if(req.status==200){var html=req.responseText;var data=eval(""(""+html+"")"");if(""v"" in data){eval(data.v)}if(""e"" in data){throw blqw.Ajax.Throw(data.e)}else{return data.d}}else{alert(""出现错误"")}};";
#endif
        #endregion

        #region javascript full
#if DEBUG
        const string JAVASCRIPT = @"
	window.blqw = window.blqw || {};
    blqw.Ajax = blqw.Ajax || {};
    blqw.Ajax.GetRequest = function () {
        if (window.ActiveXObject) {
            try {
                return new ActiveXObject('Msxml2.XMLHTTP');
            } catch (e) {
                return new ActiveXObject('Microsoft.XMLHTTP');
            }
        }
        else if (window.XMLHttpRequest) {
            return new XMLHttpRequest();
        }
    }

    blqw.Ajax.Throw = function(e){{
            function AjaxError(message,stack,type){{
                this.name = 'AjaxError';
                this.type = type;
                this.message = message;
                this.stack = stack;
                this.innerError = null;
                this.toString = function () {{
                                    return 'ajaxerr:' + this.message;
                                }};
            }};
            var err = new AjaxError(e.message,e.stack,e.type);
            var e1 = err;
            while(e.innerError){{
                e = e.innerError;
                e1.innerError = new AjaxError(e.message,e.stack,e.type);
                e1 = e1.innerError;
            }}
            return err;
        }}

    blqw.Ajax.Exec = function (method, args) {

            var getStr = function (obj) {
                if (obj == null) return '';
                var type = typeof (obj);
                switch (type) {
                    case 'number':
                    case 'boolean':
                        return obj.toString();
                    case 'string':
                        return encodeURIComponent(obj.replace('\0', '\0\0'));
                    case 'undefined':
                        return 'undefined';
                    case 'function':
                        try {
                            return arguments.callee(obj());
                        } catch (e) {
                            return 'null';
                        }
                    case 'object':
                        type = Object.prototype.toString.apply(obj);
                        switch (type) {
                            case '[object Date]':
                                return encodeURIComponent(obj.getFullYear() + '-' +
                            (obj.getMonth() + 1) + '-' +
                            obj.getDate() + ' ' +
                            obj.getHours() + ':' +
                            obj.getMinutes() + ':' +
                            obj.getSeconds() + '.' +
                            obj.getMilliseconds());
                            case '[object RegExp]':
                                return encodeURIComponent(obj.toString().replace('\0', '\0\0'));
                            case '[object Array]':
                                var arr = [];
                                for (var i in obj)
                                    arr.push(arguments.callee(obj[i]));
                                return arr.join(',');
                            case '[object Object]':
                                return '[object Object]';
                        }
                        break;
                }
            }
            var arr = [];
            for (var i = 0; i < args.length; i++) {
                arr.push(getStr(args[i]));
            }

            url = window.location.href;
            var req = blqw.Ajax.GetRequest();
            req.open('POST', url, false);
            req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=utf-8');
            var ret = req.send('blqw.ajaxdata=' + arr.join('\0') + '&blqw.ajaxmethod=' + method);
            if (req.status == 200) {
                var html = req.responseText;
                var data = eval('(' + html+ ')');
                if ('v' in data) {
                    eval(data.v);
                } 
                if ('e' in data) {
                    throw blqw.Ajax.Throw(data.e);
                } else {
                    return data.d;
                }
            } else {
                alert('出现错误');
            }
        } 
";

        private static void Encode()
        {
            //var aaaa = new Yahoo.Yui.Compressor.JavaScriptCompressor(JAVASCRIPT, false, Encoding.UTF8, System.Globalization.CultureInfo.CurrentCulture).Compress().Replace("\"", "\"\"");
        }

#endif
        #endregion

        public static bool IsAjaxing
        {
            get
            {
                return HttpContext.Current.Request != null && HttpContext.Current.Request.Form["blqw.ajaxmethod"] != null;
            }
        }

        private static string ConvertVarName(string name)
        {
            if (name.Contains("."))
            {
                var arr = name.Split('.');
                string n = arr[0];
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("window.{0}=window.{0}||{{}};", n);
                int i = 1;
                for (; i < arr.Length - 1; i++)
                {
                    n += "." + arr[i];
                    sb.AppendFormat("{0}={0}||{{}};", n);
                }
                sb.Append(name);
                return sb.ToString();
            }
            else
            {
                return "window." + name;
            }
        }

        public static void RegisterVar(string name, object value)
        {
            var js = ConvertVarName(name) + "=" + Json.ToJsonString(value) + ";";
            RegisterScript(js);
        }

        public static void Alert(string message)
        {
            var js = "window.alert({0});";
            js = string.Format(js, Json.ToJsonString(message));
            RegisterScript(js);
        }

        public static void RegisterScript(string javascript)
        {
            var page = (Page)HttpContext.Current.Handler;
            if (page == null)
            {
                return;
            }
            if (IsAjaxing)
            {
                if (page.Items.Contains("blqw.Ajax2.Js"))
                {
                    page.Items["blqw.Ajax2.Js"] += javascript;
                }
            }
            else
            {
                page.ClientScript.RegisterStartupScript(typeof(void), Guid.NewGuid().ToString("N"), javascript, true);
            }
        }

        /// <summary> 
        /// </summary>
        /// <param name="page"></param>
        public static void Register(Page page)
        {
            if (page == null)
            {
                return;
            }
            if (page.Items.Contains("blqw.Ajax2"))
            {
                return;
            }
            page.Items.Add("blqw.Ajax2", true);
            page.Items.Add("blqw.Ajax2.Js", "");
            if (page.Request.Form["blqw.ajaxmethod"] != null)
            {
                string str;
                try
                {
                    var name = page.Request.Form["blqw.ajaxmethod"];
                    var method = page.GetType().GetMethod(name, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
                    var met = Literacy.CreateCaller(method);
                    var data = page.Request.Form["blqw.ajaxdata"];
                    var p = method.GetParameters();
                    object[] args = null;
                    if (data != null)
                    {
                        var d = data.Split('\0');
                        args = new object[p.Length];

                        if (args.Length != d.Length)
                        {
                            throw new ArgumentException("该方法需要提供 " + p.Length + " 个参数!");
                        }
                        for (int i = 0; i < p.Length; i++)
                        {
                            var val = d[i].Replace("\0\0", "\0");
                            var pit = p[i].ParameterType;
                            if (val.Length == 0)
                            {
                                if (pit == typeof(string))
                                {
                                    args[i] = "";
                                }
                                else if (pit.IsValueType && Nullable.GetUnderlyingType(pit) == null)
                                {
                                    throw new InvalidCastException("<参数 " + (i + 1) + ">转换失败!无法将空值转换为 " + p[i].ParameterType.Name + " 类型");
                                }
                                else
                                {
                                    args[i] = null;
                                }
                            }
                            else
                            {
                                try
                                {
                                    var nulltype = Nullable.GetUnderlyingType(pit);
                                    if (nulltype != null)
                                    {
                                        args[i] = Convert.ChangeType(val, nulltype);
                                    }
                                    else
                                    {
                                        args[i] = Convert.ChangeType(val, pit);
                                    }
                                }
                                catch (Exception mex)
                                {
                                    throw new ArgumentException("<参数 " + (i + 1) + ">转换失败!无法将 {" + val + "} 转换为 " + p[i].ParameterType.Name + " 类型", mex);
                                }
                            }
                        }

                    }
                    else if (p.Length > 0)
                    {
                        throw new ArgumentException("该方法需要提供 " + p.Length + " 个参数!");
                    }

                    var obj = met(page, args);
                    if (method.ReturnType.Namespace == "System")
                    {
                        if (method.ReturnType == typeof(void))
                        {
                            str = "";
                        }
                        else if (method.ReturnType == typeof(string))
                        {
                            str = "d:" + Json.ToJsonString(obj.ToString());
                        }
                        else if (method.ReturnType.IsValueType)
                        {
                            str = "d:" + Json.ToJsonString(obj.ToString());
                        }
                        else
                        {
                            str = "d:" + Json.ToJsonString(obj);
                        }
                    }
                    else
                    {
                        str = "d:" + Json.ToJsonString(obj);
                    }
                    var ext = page.Items["blqw.Ajax2.Js"].ToString();
                    if (ext.Length > 0)
                    {
                        ext = "v:" + Json.ToJsonString(ext);
                        if (str.Length > 0)
                        {
                            str = ext + "," + str;
                        }
                    }
                }
                catch (Exception ex)
                {
                    str = "e:" + new AjaxError(ex).ToString();
                }
                page.Response.Write("{" + str + "}");
                page.Response.End();
            }
            else
            {

                string js = (string)Cache.Get(page.GetType().FullName + "->js", key =>
                {
                    var t = page.GetType();
                    StringBuilder sb = new StringBuilder();
                    foreach (var m in t.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public))
                    {
                        var attr = (AjaxMethodAttribute)Attribute.GetCustomAttribute(m, typeof(AjaxMethodAttribute));
                        if (attr != null)
                        {
                            var ps = m.GetParameters();
                            sb.Append(ConvertVarName(attr.FunctionName ?? m.Name));
                            sb.Append("=function(){return blqw.Ajax.Exec('");
                            sb.Append(m.Name);
                            sb.Append("',arguments);}");
                            sb.AppendLine();
                        }
                    }
                    return sb.ToString();
                });

                if (page.Form == null)
                {
                    page.Response.Write(
                        string.Concat(@"<script type=""text/javascript"">
//<![CDATA[
", JAVASCRIPT, js, @"
//]]>
</script>"));
                }
                else
                {
                    page.ClientScript.RegisterClientScriptBlock(typeof(void), Guid.NewGuid().ToString("N"), JAVASCRIPT + js, true);
                }
            }
        }

        class AjaxError
        {
            public AjaxError(Exception ex)
            {
                if (ex is TargetInvocationException)
                {
                    ex = ex.InnerException;
                }
                this.message = ex.Message;
                this.type = ex.GetType().Name;

                this.stack = ex.StackTrace;

                if (ex.InnerException != null)
                {
                    this.innerError = new AjaxError(ex.InnerException);
                }
            }
            public string message { get; set; }
            public string stack { get; set; }
            public string type { get; set; }
            public AjaxError innerError { get; set; }
            public override string ToString()
            {
                return Json.ToJsonString(this);
            }
        }
    }
}