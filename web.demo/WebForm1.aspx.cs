using blqw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.demo
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax2.Register(this);
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
    }
}