using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ERPAplikacia
{
    public partial class ProductPage : Page
    {
        private string userId;
        private string userName;
        private string authorisation;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] == null)
                Response.Redirect("MainPage.aspx");
            userId = Request.QueryString["User_Id"];
            userName = Request.QueryString["User_Name"];
            authorisation = Request.QueryString["Authorisation"];
            MessageLabel.Text = "";
            LogLabel.Text = "Prihlásený požívateľ: " + userName + ".";

            if (authorisation == "User")
            {
                MessageLabel.Visible = false;

                listProducts();
            }
            else if (authorisation == "low")
            {
                MessageLabel.Visible = false;

                listProducts();
            }
            else if (authorisation == "high")
            {
                MessageLabel.Visible = true;

                listProducts();
            }
        }

        private void listProducts()
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string selectStatement = "SELECT Id, Product_Name, Unit_Price FROM Products";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            SqlDataReader reader = selectCommand.ExecuteReader();
            List<string> headings = new List<string>();
            headings.Add("Id");
            headings.Add("Product_Name");
            headings.Add("Unit_Price");
            initialiseHeadings(headings);

            while (reader.Read())
            {
                TableRow entry = new TableRow();
                TableCell Id = new TableCell();
                Id.Text = reader["Id"].ToString();
                TableCell Product_Name = new TableCell();
                Product_Name.Text = reader["Product_Name"].ToString();
                TableCell Unit_Price = new TableCell();
                Unit_Price.Text = reader["Unit_Price"].ToString();

                List<TableCell> cells = new List<TableCell>();
                cells.Add(Id);
                cells.Add(Product_Name);
                cells.Add(Unit_Price);

                entry.Cells.AddRange(cells.ToArray());
                ProductTable.Rows.Add(entry);
            }
            reader.Close();
            ProductTable.GridLines = GridLines.Both;

            sqlConnection.Close();
        }

        private void initialiseHeadings(List<string> args)
        {
            TableRow header = new TableRow();
            foreach (string argument in args)
            {
                TableCell cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = argument;
                header.Cells.Add(cell);
            }
            ProductTable.Rows.Add(header);
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenuPage.aspx?User_Id=" + userId + "&Authorisation=" + authorisation + "&User_Name=" + userName);
        }

    }
}