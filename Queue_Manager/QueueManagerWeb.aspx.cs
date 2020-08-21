using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;

namespace Queue_Manager
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        //string connectionString = @"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;Persist Security Info=True;User ID=test;Password=test";
        //string sqlString = "select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_name"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!Page.IsPostBack)
            {
                //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True"; //@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test";
                //string sqlString = "select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER";
                //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
                string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
                refreshdata(connectionString);
            }
        }

        //public void refreshdata(string sqlString, string connectionString)
        public void refreshdata(string connectionString)
        {
            ////var nameOrConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Capstone_dbConnectionString2"].ConnectionString;
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;Persist Security Info=True;User ID=test;Password=test");
            //SqlCommand cmd = new SqlCommand("select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER", con);
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            ////GridView1.DataSource = dt;
            //GridView1.DataBind();

            //using (SqlConnection connection = new SqlConnection(
            //    nameOrConnectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(sqlString, connection))
            //    {
            //        //command.Connection.Open();
            //        //command.ExecuteNonQuery();

            //        SqlDataAdapter sda = new SqlDataAdapter(command);
            //        DataTable dt = new DataTable();
            //        sda.Fill(dt);
            //        GridView1.DataSource = dt;
            //        GridView1.DataBind();
            //    }
            //}

            //try
            //{
            //    SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=HaDataMart;Integrated Security= True");
            //    cn.Open();
            //}
            //catch (SqlException ex)
            //{
            //    Console.WriteLine("Error {0} ErrorCode {1} LineNumber {2}  Number {3}", ex.Message, ex.ErrorCode, ex.LineNumber, ex.Number);
            //    foreach (SqlError err in ex.Errors)
            //    {
            //        Console.WriteLine("** Error : {0} LineNumber {1} Number {2},err.Message,err.LineNumber,err.Number");
            //    }
            //    if (ex.InnerException != null)
            //    {
            //        Console.WriteLine("Inner1 : {0}", ex.InnerException.Message);
            //    }
            //}
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER ORDER BY CHECK_IN ASC", sqlCon); 
                sqlDa.Fill(dtbl);
            }
            if (dtbl.Rows.Count > 0)
            {
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
            else
            {
                dtbl.Rows.Add(dtbl.NewRow());
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
                GridView1.Rows[0].Cells[0].Text = "No Data Found...!";
                GridView1.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;


            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblFailureMsg.Text = null;
            lblSuccessMsg.Text = null;
            //sqlconnection con = new sqlconnection(@"data source=desktop-u3m0as7;initial catalog=capstone_db;user id=test;password=test");
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True"; //@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";                                                                                                  //string sqlString = "select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    try
                    { 
                {
                    int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("delete from QUEUE_MANAGER where id =@id", sqlCon);
                    cmd.Parameters.AddWithValue("id", id);
                    int i = cmd.ExecuteNonQuery();
                    sqlCon.Close();
                        lblSuccessMsg.Text = "Record Deleted";
                }
            }
            catch (Exception ex)
            {
                    lblFailureMsg.Text = "Record cannot be deleted: " + ex.Message;
            }
            refreshdata(connectionString);
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //string sqlString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True"; //@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test";
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            GridView1.EditIndex = e.NewEditIndex;
            refreshdata(connectionString);
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True"; //@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            //string sqlString = "select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER";
            GridView1.EditIndex = -1;
            refreshdata(connectionString);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblFailureMsg.Text = null;
            lblSuccessMsg.Text = null;
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test");
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True"; //@"Data Source=DESKTOP-U3M0AS7;Initial Catalog=Capstone_db;User ID=test;Password=test";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            //string sqlString = "select FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                try
                { 

                TextBox txtfirstName = GridView1.Rows[e.RowIndex].FindControl("TextBox1") as TextBox;
                TextBox txtlastName = GridView1.Rows[e.RowIndex].FindControl("TextBox2") as TextBox;
                TextBox txtphoneNumber = GridView1.Rows[e.RowIndex].FindControl("TextBox3") as TextBox;
                int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Update Queue_Manager set first_name=@first_name,last_name=@last_name,phone_number=@phone_number,Check_In='12/31/9999 12:00:00 PM' where id=@id", sqlCon);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("first_name", txtfirstName.Text);
                cmd.Parameters.AddWithValue("last_name", txtlastName.Text);
                cmd.Parameters.AddWithValue("phone_number", txtphoneNumber.Text);
                cmd.Parameters.AddWithValue("id", id);

                int i = cmd.ExecuteNonQuery();
                sqlCon.Close();
                    lblSuccessMsg.Text = "Record Updated";
            }
            catch (Exception ex)
            {
                    lblFailureMsg.Text = "Record cannot be updated: " + ex.Message;
            }
            }
            GridView1.EditIndex = -1;
            refreshdata(connectionString);


        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblFailureMsg.Text = null;
            lblSuccessMsg.Text = null;
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            if (e.CommandName.Equals("AddNew"))
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlCon.Open();
                        string query = "Insert into Queue_Manager (First_Name, Last_Name, Phone_Number, Check_In) Values (@First_Name, @Last_Name, @Phone_Number, '12/31/9999 12:00:00 PM')";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@First_Name", (GridView1.FooterRow.FindControl("txtFirstNameFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@Last_Name", (GridView1.FooterRow.FindControl("txtLastNameFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@Phone_Number", (GridView1.FooterRow.FindControl("txtPhoneNumberFooter") as TextBox).Text.Trim());
                        int i = cmd.ExecuteNonQuery();
                        refreshdata(connectionString);
                        lblSuccessMsg.Text = "Record Inserted";
                    }
                    catch(Exception ex)
                    {
                        lblFailureMsg.Text = "Record cannot be inserted " + ex.Message;
                    }
                }
            }

            if (e.CommandName.Equals("CheckIn"))
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    //foreach (GridViewRow gvr in GridView1.Rows)
                    //{
                    //    string PrimaryKey = GridView1.DataKeys[gvr.RowIndex].Values[0].ToString();
                    //    string id = GridView1.DataKeys[gvr.RowIndex].Values["id"].ToString();
                    //}
                    var id = e.CommandArgument;
                    //string id = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                    //TextBox txtStatus = GridView1.Rows[e.RowIndex].FindControl("TextBox5") as TextBox;
                    //int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                    sqlCon.Open();
                    string query = "Update Queue_Manager set check_in=@check_in,status=@status where id=@id";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    DateTime check_in = DateTime.Now;
                    cmd.Parameters.AddWithValue("@check_in", check_in.ToString());
                    cmd.Parameters.AddWithValue("@status", "waiting");
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    int i = cmd.ExecuteNonQuery();
                    refreshdata(connectionString);
                }
            }

                if (e.CommandName.Equals("CheckOut"))
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        //foreach (GridViewRow gvr in GridView1.Rows)
                        //{
                        //    string PrimaryKey = GridView1.DataKeys[gvr.RowIndex].Values[0].ToString();
                        //    string id = GridView1.DataKeys[gvr.RowIndex].Values["id"].ToString();
                        //}
                        var id = e.CommandArgument;
                        //string id = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                        //TextBox txtStatus = GridView1.Rows[e.RowIndex].FindControl("TextBox5") as TextBox;
                        //int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                        sqlCon.Open();
                        string query = "Update Queue_Manager set check_in='12/31/9999 12:00:00 PM',status=null where id=@id";
                        SqlCommand cmd = new SqlCommand(query, sqlCon);
                        cmd.Parameters.AddWithValue("@id", id.ToString());
                        int i = cmd.ExecuteNonQuery();
                        refreshdata(connectionString);
                    }
                }

            if (e.CommandName.Equals("InProgress"))
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    //foreach (GridViewRow gvr in GridView1.Rows)
                    //{
                    //    string PrimaryKey = GridView1.DataKeys[gvr.RowIndex].Values[0].ToString();
                    //    string id = GridView1.DataKeys[gvr.RowIndex].Values["id"].ToString();
                    //}
                    var id = e.CommandArgument;
                    //string id = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                    //TextBox txtStatus = GridView1.Rows[e.RowIndex].FindControl("TextBox5") as TextBox;
                    //int id = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
                    sqlCon.Open();
                    string query = "Update Queue_Manager set status=@status where id=@id";
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.Parameters.AddWithValue("@status", "In Progress");
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    int i = cmd.ExecuteNonQuery();
                    refreshdata(connectionString);
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    cell.BackColor = Color.Green;
            //}
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    GridViewRow grv = e.Row;
            //    if (grv.Cells[5].Text.Equals("Fail"))
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //    }
            //}
            //if (e.Row.Cells[2].Text == "Shoaib")
            //{
            //    e.Row.BackColor = Color.Green;
            //}
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    String header = GridView1.Columns[i].HeaderText;
            //    String cellText = e.Row.Cells[i].Text;
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var ddlCompany = (TextBox)e.Row.FindControl("FIRST_NAME");
                //string status = (e.Row.Cells[0].Text);
                //var status = "status";
                //string txtfirstName = e.Row.FindControl("FIRST_NAME").ToString();
                //if (txtfirstName.Text == "Awais")
                //{
                foreach (TableCell cell in e.Row.Cells)
                {
                    string status = cell.Text;
                    if(status == "waiting")
                    {
                        e.Row.BackColor = Color.Green;
                    }
                    if (status == "In Progress")
                    {
                        e.Row.BackColor = Color.Yellow;
                    }
                }
                    //e.Row.BackColor = Color.Green;
                //}
                //if (e.Row.Cells[4].Text == "waiting")
                //{
                //    e.Row.BackColor = Color.Green;
                //}
            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.Cells[1].Text == "Shoaib")
            //    {
            //        e.Row.BackColor = Color.Green;
            //    }
            //}
        }

        protected void btnFirstName_Click(object sender, EventArgs e)
        {
            DataTable dtbl = new DataTable();
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER Where FIRST_NAME in ('" + txtboxFirstName.Text + "')", sqlCon);
                //cmd.Parameters.AddWithValue("first_name", txtboxFirstName.Text);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
        }

        protected void btnLastName_Click(object sender, EventArgs e)
        {
            DataTable dtbl = new DataTable();
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER Where LAST_NAME in ('" + txtboxLastName.Text + "')", sqlCon);
                //cmd.Parameters.AddWithValue("first_name", txtboxFirstName.Text);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
        }

        protected void btnPhoneNumber_Click(object sender, EventArgs e)
        {
            DataTable dtbl = new DataTable();
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select ID, FIRST_NAME, LAST_NAME, PHONE_NUMBER, CHECK_IN, STATUS from QUEUE_MANAGER Where PHONE_NUMBER in ('" + txtboxPhoneNumber.Text + "')", sqlCon);
                //cmd.Parameters.AddWithValue("first_name", txtboxFirstName.Text);
                sqlDa.Fill(dtbl);
                GridView1.DataSource = dtbl;
                GridView1.DataBind();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //string connectionString = @"Server=DESKTOP-U3M0AS7;Database=Capstone_db;Trusted_Connection=True";
            string connectionString = @"Server=nerddb.cnbu9ywwbsrd.us-west-2.rds.amazonaws.com,1433;Database=QueueManagerdb;user=sa;password=testtest";
            refreshdata(connectionString);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["user_name"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        //protected void Insert(object sender, EventArgs e)
        //{
        //    //string name = txtName.Text;
        //    //string country = txtCountry.Text;
        //    //txtName.Text = "";
        //    //txtCountry.Text = "";
        //    //string query = "INSERT INTO Customers VALUES(@Name, @Country)";
        //    //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    //using (SqlConnection con = new SqlConnection(constr))
        //    //{
        //    //    using (SqlCommand cmd = new SqlCommand(query))
        //    //    {
        //    //        cmd.Parameters.AddWithValue("@Name", name);
        //    //        cmd.Parameters.AddWithValue("@Country", country);
        //    //        cmd.Connection = con;
        //    //        con.Open();
        //    //        cmd.ExecuteNonQuery();
        //    //        con.Close();
        //    //    }
        //    //}
        //    //this.BindGrid();
        //}
    }
    }