using System;

namespace blqw
{

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AjaxMethodAttribute : Attribute
    {
        public AjaxMethodAttribute() { }
        public AjaxMethodAttribute(string functionName) { FunctionName = functionName; }
        public string FunctionName { get; set; }
    }
}