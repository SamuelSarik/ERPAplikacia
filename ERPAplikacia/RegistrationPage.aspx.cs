using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace ERPAplikacia
{
    public partial class RegistrationPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            if (userTableExists(sqlConnection) == false)
                createUserTable(sqlConnection);

            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;
            string dateOfBirth = BirthBox.Text;
            dateOfBirth = dateConversion(dateOfBirth);
            string street = StreetBox.Text;
            string city = CityBox.Text;
            string postCode = PostCodeBox.Text;
            string country = CountryBox.Text;
            string userName = UserBox.Text;
            string password = PasswordBox.Text;
            string confirmPassword = ConfirmPasswordBox.Text;

            if (checkEntries(dateOfBirth) == false)
            {
                WarningLabel.Text = "Nie všetky textové polia boli vyplnené alebo niektoré položky mali zlý formát";
                sqlConnection.Close();
            }               
            else if (password != confirmPassword)
            {
                WarningLabel.Text = "Heslá, ktoré ste zadali, sa nezhodujú!";
                sqlConnection.Close();
            }
            else if (userExists(userName, sqlConnection))
            {
                WarningLabel.Text = "Používateľ s daným používateľským menom už existuje. Prihláste sa alebo zadajte iné používateľské meno.";
                sqlConnection.Close();
            }
            else
            {
                string selectStatement = "SELECT MAX(Id) AS MaxId FROM Users";
                SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                int maxId;
                reader.Read();
                object obj = reader["MaxId"];
                if (obj.GetType() == typeof(DBNull))
                {
                    maxId = 0;
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(reader["MaxId"]);
                    maxId = int.Parse(builder.ToString());
                }
                reader.Close();
                maxId++;
                string insertStatement = "INSERT INTO Users " +
                    "VALUES (" + maxId + ",'" + firstName + "','" + lastName + "','" + dateOfBirth + "','" + street + "','" + postCode + "','" + city + "','" + country + "','" + userName + "','" + password + "','Very low')";
                SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);
                insertCommand.ExecuteNonQuery();
                sqlConnection.Close();
                Response.Redirect("MainPage.aspx");
            }
        }

        private bool checkEntries(string date)
        {
            if (FirstNameBox.Text == "")
                return false;
            if (LastNameBox.Text == "")
                return false;
            if (date == "")
                return false;
            if (StreetBox.Text == "")
                return false;
            if (CityBox.Text == "")
                return false;
            if (CountryBox.Text == "")
                return false;
            if (PostCodeBox.Text == "")
                return false;
            if (UserBox.Text == "")
                return false;
            if (PasswordBox.Text == "")
                return false;
            if (ConfirmPasswordBox.Text == "")
                return false;
            return true;
        }

        private string dateConversion(string date)
        {
            char splitter;
            if (date.Contains("-"))
                splitter = '-';
            else if (date.Contains("."))
                splitter = '.';
            else
                return "";

            string[] dateParts = date.Split(splitter);
            if (dateParts.Length != 3)
                return "";
            int year = 0, month = 0, day = 0;
            int part = 0;
            if (int.TryParse(dateParts[0], out part) == false)
                return "";
            if (part > 31)
                year = part;
            else if (part > 0)
                day = part;
            else
                return "";
            if (int.TryParse(dateParts[1], out part) == false)
                return "";
            if (part < 13 && part > 0)
                month = part;
            else
                return "";
            if (int.TryParse(dateParts[2], out part) == false)
                return "";
            if (part > 31)
                year = part;
            else if (part > 0)
                day = part;
            else
                return "";
            if (day == 0 || year == 0 || month == 0)
                return "";

            return year + "-" + month + "-" + day;
        }

        private bool userExists(string username, SqlConnection connection)
        {
            string selectStatement = "SELECT * FROM Users WHERE User_Name='" + username + "'";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            SqlDataReader reader = selectCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }

        }

        private bool userTableExists(SqlConnection connection)
        {
            string selectStatement = "SELECT * FROM Users";

            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            SqlDataReader reader = null;
            try
            {
                reader = selectCommand.ExecuteReader();
            }
            catch (SqlException)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        private void createUserTable(SqlConnection connection)
        {
            string createStatement = "CREATE TABLE Users(ID int,FirstName varchar(255),LastName varchar(255),DateOfBirth date,Occupation varchar(255),Payment int,UserName varchar(255),Password varchar(255))";

            SqlCommand createCommand = new SqlCommand(createStatement, connection);
            createCommand.ExecuteNonQuery();
        }

        protected void ReturnButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }
    }
}