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
    public partial class EmployeePage : Page
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

                listEmployees();
            }
            else if (authorisation == "low")
            {
                MessageLabel.Visible = false;

                listEmployees();
            }
            else if (authorisation == "high")
            {
                MessageLabel.Visible = true;

                listEmployees();
            }
        }

        private void listEmployees()
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string selectStatement = "SELECT Id, First_Name, Last_Name, Date_Of_Birth, Occupation, Days_Worked, Salary, User_Name FROM Employees";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            SqlDataReader reader = selectCommand.ExecuteReader();
            List<string> headings = new List<string>();
            headings.Add("Id");
            headings.Add("First_Name");
            headings.Add("Last_Name");
            headings.Add("Date_Of_Birth");
            headings.Add("Occupation");
            headings.Add("Days_Worked");
            headings.Add("Salary");
            headings.Add("User_Name");
            initialiseHeadings(headings);

            while (reader.Read())
            {
                TableRow entry = new TableRow();
                TableCell Id = new TableCell();
                Id.Text = reader["Id"].ToString();
                TableCell First_Name = new TableCell();
                First_Name.Text = reader["First_Name"].ToString();
                TableCell Last_Name = new TableCell();
                Last_Name.Text = reader["Last_Name"].ToString();
                TableCell Date_Of_Birth = new TableCell();
                Date_Of_Birth.Text = reader["Date_Of_Birth"].ToString();
                TableCell Occupation = new TableCell();
                Occupation.Text = reader["Occupation"].ToString();
                TableCell Days_Worked = new TableCell();
                Days_Worked.Text = reader["Days_Worked"].ToString();
                TableCell Salary = new TableCell();
                Salary.Text = reader["Salary"].ToString();
                TableCell User_Name = new TableCell();
                User_Name.Text = reader["User_Name"].ToString();

                List<TableCell> cells = new List<TableCell>();
                cells.Add(Id);
                cells.Add(First_Name);
                cells.Add(Last_Name);
                cells.Add(Date_Of_Birth);
                cells.Add(Occupation);
                cells.Add(Days_Worked);
                cells.Add(Salary);
                cells.Add(User_Name);

                entry.Cells.AddRange(cells.ToArray());
                EmployeeTable.Rows.Add(entry);
            }
            reader.Close();
            EmployeeTable.GridLines = GridLines.Both;

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
            EmployeeTable.Rows.Add(header);
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenuPage.aspx?User_Id=" + userId + "&Authorisation=" + authorisation + "&User_Name=" + userName);
        }

    }
}