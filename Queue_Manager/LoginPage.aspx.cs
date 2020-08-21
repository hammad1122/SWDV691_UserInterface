using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Queue_Manager
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if ((txtboxUser.Text.ToUpper() == "ADMIN") && (txtboxPass.Text == "test"))
            {
                Session["user_name"] = txtboxUser.Text;
                Response.Redirect("QueueManagerWeb.aspx");
            }
            else
            {
                lblMsg.Text = "wrong login";
            }
        }
    }
}