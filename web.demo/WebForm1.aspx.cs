using blqw;
using System;
using System.Collections.Generic;
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
            if (Ajax2.IsAjaxing == false)
            {
                Ajax2.RegisterPager(new Pager("pager")
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
            return new
            {
                ID = id,
                Name = "\"blqw\"" + id
            };
        }
        
        [AjaxMethod("fanye")]
        public void Test(Pager pager)
        {
            pager.pageindex++;
            Ajax2.RegisterPager(pager);
        }
    }
}