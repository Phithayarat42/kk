using SSSWebFormASP.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSWebFormASP
{
    public partial class SummaryForecast : System.Web.UI.Page
    {
        Database func = new Database();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        double SaleAmount = 0;
        double MaterialCost = 0;
        double TotalAmount = 0;
        double TotalBalance = 0;

        #region Page load
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;


            if (!IsPostBack)
            {
                // Define the data source for the Repeater control
                string query = $@"SELECT [ID]
                                ,[Quotation_No]
                                ,[PO_Date]
                                ,[PO_No]
                                ,[Projection]
                                ,[Company_name]
                          	    ,[SMG]
                                ,[Sale_amount]
                                ,[Material_cost]
                                ,[Material_cost_rate]
                                ,[Total_amount]
                                ,[Total_Balance]
                                ,[Percentage]
                                ,[Status]
                            FROM [dbo].[DUMMY_Summary_forecast]";

                using (SqlConnection sqlCon = new SqlConnection(@"Data Source=PHITHAYARAT;Initial Catalog=TestASP;Integrated Security=True"))
                {
                    using (SqlCommand command = new SqlCommand(query, sqlCon))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }



                int[] FilterYear = dt.AsEnumerable()
                                   .Select(c => c.Field<DateTime>("PO_Date").Year)
                                   .Distinct()
                                   .ToArray();

                foreach (int year in FilterYear)
                {
                    DataRow[] Complete = dt.AsEnumerable()
                                     .Where(c => c.Field<string>("Status") == "Complete" &&
                                                 c.Field<DateTime>("PO_Date").Year == year)
                                     .ToArray();

                    if (Complete.Count() > 0)
                    {
                        DataTable Completes = Complete.CopyToDataTable();
                        Completes.TableName = $"Year {year} (Complete)";
                        ds.Tables.Add(Completes);
                    }

                    DataRow[] On_Process = dt.AsEnumerable()
                                     .Where(c => c.Field<string>("Status") == "On process" &&
                                                 c.Field<DateTime>("PO_Date").Year == year)
                                     .ToArray();

                    if (On_Process.Count() > 0)
                    {
                        DataTable OnProcess = On_Process.CopyToDataTable();
                        OnProcess.TableName = $"Year {year} (On Process)";
                        ds.Tables.Add(OnProcess);
                    }

                }

                rpt_show_all.DataSource = ds.Tables;
                rpt_show_all.DataBind();


                //var Total = dt.AsEnumerable().Sum(c => c.Field<double>("Sale_amount"));
                //gv_.Columns[1].FooterText = (from row in dt.AsEnumerable()
                //                                   select row.Field<int>("Sale_amount")).Sum().ToString();

            }

           
        }
        #endregion

        #region Show sum
        protected void gv_show_sum(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "Year";
                e.Row.Cells[1].Text = "Sale Amount";
                e.Row.Cells[2].Text = "Meterial Cost (Forecast)";
                e.Row.Cells[3].Text = "Meterial Cost (Actual)";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView Grid = (GridView)sender;
                DataTable dt = Grid.DataSource as DataTable;

                //double SaleAmount = dt.AsEnumerable()
                //                    .Select(c => c.Field<double>("Sale_amount"))
                //                    .Sum();

                e.Row.Cells[1].Text = double.Parse(e.Row.Cells[1].Text).ToString("N2");
                e.Row.Cells[2].Text = double.Parse(e.Row.Cells[2].Text).ToString("N2");
                e.Row.Cells[3].Text = double.Parse(e.Row.Cells[3].Text).ToString("N2");


                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;

                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }

        }
        #endregion

        #region  GV RowDataBound Show All

        protected void gv_show_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = "Quotation No.";
                e.Row.Cells[2].Text = "PO Date";
                e.Row.Cells[3].Text = "PO No.";
                e.Row.Cells[4].Text = "Projection";
                e.Row.Cells[5].Text = "Company name";
                e.Row.Cells[6].Text = "SMG Group";
                e.Row.Cells[7].Text = "Sale amount";
                e.Row.Cells[8].Text = "Material cost";
                e.Row.Cells[9].Text = "Material cost rate";
                e.Row.Cells[10].Text = "Total amount";
                e.Row.Cells[11].Text = "Total Balance";
                e.Row.Cells[12].Text = "Percentage";
                e.Row.Cells[13].Text = "Statuss";


                e.Row.Cells[1].Width = 150;
                e.Row.Cells[2].Width = 150;
                e.Row.Cells[3].Width = 200;
                e.Row.Cells[4].Width = 800;
                e.Row.Cells[5].Width = 500;
                e.Row.Cells[6].Width = 100;
                e.Row.Cells[7].Width = 150;
                e.Row.Cells[8].Width = 150;
                e.Row.Cells[9].Width = 70;
                e.Row.Cells[10].Width = 150;
                e.Row.Cells[11].Width = 150;
                e.Row.Cells[12].Width = 70;
                e.Row.Cells[13].Width = 100;

            }

            //Change bg color and text

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[13].Text;
                if (status == "Complete")
                {
                    e.Row.Cells[13].BackColor = Color.Green;
                    e.Row.Cells[13].ForeColor = Color.White;
                }
                else if (status == "On process")
                {
                    e.Row.Cells[13].BackColor = Color.SandyBrown;
                    e.Row.Cells[13].ForeColor = Color.White;
                }

                //Change text color
                string smg = e.Row.Cells[6].Text;
                if (smg == "1")
                {
                    e.Row.Cells[6].ForeColor = Color.Green;
                    e.Row.Cells[6].Text = "Yes";
                }
                else if (smg == "0")
                {
                    e.Row.Cells[6].ForeColor = Color.Red;
                    e.Row.Cells[6].Text = "No";
                }

                //Change bg 
                double check = double.Parse(e.Row.Cells[11].Text);
                if (check < 0)
                {
                    e.Row.Cells[10].BackColor = Color.LightCoral;
                    e.Row.Cells[11].BackColor = Color.LightCoral;
                    e.Row.Cells[12].BackColor = Color.LightCoral;
                }

                //Change double to string 
                e.Row.Cells[7].Text = double.Parse(e.Row.Cells[7].Text).ToString("N2");
                e.Row.Cells[8].Text = double.Parse(e.Row.Cells[8].Text).ToString("N2");
                e.Row.Cells[10].Text = double.Parse(e.Row.Cells[10].Text).ToString("N2");
                e.Row.Cells[11].Text = double.Parse(e.Row.Cells[11].Text).ToString("N2");

                //Split date times
                e.Row.Cells[2].Text = e.Row.Cells[2].Text.Split(' ')[0];

                //Set position
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;

                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Center;

                // Retrieve the value of the column you want to calculate the sum for
                //int value = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Total amount"));

                // Add the value to a variable that keeps track of the total
                //total += value;

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                GridView Grid = (GridView)sender;
                DataTable dt = Grid.DataSource as DataTable;


                double Sale_amount = dt.AsEnumerable()
                                    .Select(c => c.Field<double>("Sale_amount"))
                                    .Sum();
                double Material_cost = dt.AsEnumerable()
                                    .Select(c => c.Field<double>("Material_cost"))
                                    .Sum();
                double Total_amount = dt.AsEnumerable()
                                    .Select(c => c.Field<double>("Total_amount"))
                                    .Sum();
                double Total_Balance = dt.AsEnumerable()
                                    .Select(c => c.Field<double>("Total_Balance"))
                                    .Sum();

                e.Row.Font.Bold = true;
                e.Row.Cells[0].ColumnSpan = 7;
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Text = Sale_amount.ToString("N2");
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].Text = Material_cost.ToString("N2");
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Text = Total_amount.ToString("N2");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = Total_Balance.ToString("N2");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Attributes["style"] = "text-align: right; padding: 5px;";
                e.Row.Cells[2].Attributes["style"] = "text-align: right; padding: 5px;";
                e.Row.Cells[4].Attributes["style"] = "text-align: right; padding: 5px;";
                e.Row.Cells[5].Attributes["style"] = "text-align: right; padding: 5px;";

                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;

            }

        }

        #endregion

        #region Repeater show all
        protected void Repeater_show_all_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gv = e.Item.FindControl("gv_show_all") as GridView;
                gv.DataSource = ds.Tables[e.Item.ItemIndex];
                gv.DataBind();

                Label lal = e.Item.FindControl("lal_year") as Label;
                lal.Text = ds.Tables[e.Item.ItemIndex].TableName;

            }
        }
        #endregion
    }
}