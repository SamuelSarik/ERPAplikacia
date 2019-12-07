using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;

namespace ERPAplikacia
{
    public partial class MainPage : Page
    {
        private string authorisation;
        protected void Page_Load(object sender, EventArgs e)
        {
            LogLabel.Text = "Neprihlásený žiadny používateľ";
            authorisation = "";
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            if (userTableExists(sqlConnection) == false)
                createUserTable(sqlConnection);

            string username = LoginBox.Text;
            string password = PasswordBox.Text;

            string selectStatement = "SELECT First_Name,Last_Name,Id FROM Users WHERE User_Name='" + username + "' AND Password='" + password + "'";

            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            SqlDataReader reader = selectCommand.ExecuteReader();

            StringBuilder builder = new StringBuilder();
            StringBuilder occupation = new StringBuilder();
            StringBuilder userId = new StringBuilder();
            while (reader.Read())
            {
                builder.Append(reader["First_Name"]);
                builder.Append(reader["Last_Name"]);
                userId.Append(reader["Id"]);
            }
            reader.Close();

            if (userId.ToString() == "")
            {
                selectStatement = "SELECT First_Name,Last_Name,Occupation,Id FROM Employees WHERE User_Name='" + username + "' AND Password='" + password + "'";
                selectCommand = new SqlCommand(selectStatement, sqlConnection);
                reader = selectCommand.ExecuteReader();

                builder.Clear();
                occupation.Clear();
                userId.Clear();

                while (reader.Read())
                {
                    builder.Append(reader["First_Name"]);
                    builder.Append(reader["Last_Name"]);
                    occupation.Append(reader["Occupation"]);
                    userId.Append(reader["Id"]);
                }
                reader.Close();

                selectStatement = "SELECT Authorisation FROM Permissions WHERE Occupation='" + occupation.ToString() + "'";
                selectCommand = new SqlCommand(selectStatement, sqlConnection);
                reader = selectCommand.ExecuteReader();

                StringBuilder authorisation = new StringBuilder();
                while (reader.Read())
                {
                    authorisation.Append(reader["Authorisation"]);
                }
                reader.Close();

                this.authorisation = authorisation.ToString();
            }

            if (authorisation == "")
                authorisation = "User";
            string id = userId.ToString();

            sqlConnection.Close();

            if (id != "")
            {
                Session.Add("New", 1);
                Response.Redirect("MainMenuPage.aspx?Authorisation=" + this.authorisation + "&User_Name=" + LoginBox.Text + "&User_Id=" + id, true);
            }
            else
                LogLabel.Text = "User login failed.";
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

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationPage.aspx");
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string dropStatement = "DROP TABLE Output_Warehouse;" +
                                   "DROP TABLE Input_Warehouse;" +
                                   "DROP TABLE Authorisation;" +
                                   "DROP TABLE Payment;" +
                                   "DROP TABLE Invoice;" +
                                   "DROP TABLE Materials;" +
                                   "DROP TABLE Products;" +
                                   "DROP TABLE Users;" +
                                   "DROP TABLE Employees;" +
                                   "DROP TABLE Permissions";

            SqlCommand dropCommand = new SqlCommand(dropStatement, sqlConnection);
            try
            {
                dropCommand.ExecuteNonQuery();
            }
            catch (SqlException) { }


            string createStatement = "CREATE TABLE Permissions" +
                                     "(" +
                                        "Occupation VARCHAR(50) PRIMARY KEY," +
                                        "Authorisation VARCHAR(20)," +
                                     ");" +

                                     "CREATE TABLE Users" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "First_Name VARCHAR(50)," +
                                        "Last_Name VARCHAR(50)," +
                                        "Date_Of_Birth DATE," +
                                        "Street VARCHAR(20)," +
                                        "Post_Code VARCHAR(20)," +
                                        "City VARCHAR(20)," +
                                        "Country VARCHAR(20)," +
                                        "User_Name VARCHAR(20)," +
                                        "Password VARCHAR(20)," +
                                        "Importance VARCHAR(20)" +
                                     ");" +

                                     "CREATE TABLE Employees" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "First_Name VARCHAR(50)," +
                                        "Last_Name VARCHAR(50)," +
                                        "Date_Of_Birth DATE," +
                                        "Occupation VARCHAR(50) REFERENCES Permissions(Occupation)," +
                                        "Days_Worked INT," +
                                        "Salary REAL," +
                                        "User_Name VARCHAR(20)," +
                                        "Password VARCHAR(20)" +
                                     ");" +

                                     "CREATE TABLE Products" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "Product_Name VARCHAR(100)," +
                                        "Unit_Price REAL" +
                                     ")" +

                                     "CREATE TABLE Invoice" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "User_Id INT REFERENCES Users(Id)," +
                                        "Product_Id INT REFERENCES Products(Id)," +
                                        "Color VARCHAR(20)," +
                                        "Created DATE," +
                                        "Amount INT," +
                                        "Price REAL," +
                                        "Priority INT," +
                                        "Status VARCHAR(20)," +
                                        "Authorised VARCHAR(3)" +
                                     ");" +

                                     "CREATE TABLE Payment" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "Invoice_Id INT REFERENCES Invoice(Id)" +
                                     ");" +

                                     "CREATE TABLE Input_Warehouse" +
                                     "(" +
                                        "Material_Name VARCHAR(20) PRIMARY KEY," +
                                        "Amount INT," +
                                        "Quality INT" +
                                     ");" +

                                     "CREATE TABLE Authorisation" +
                                     "(" +
                                        "Id INT PRIMARY KEY," +
                                        "Invoice_Id INT REFERENCES Invoice(Id)," +
                                        "Occupation VARCHAR(50) REFERENCES Permissions(Occupation)" +
                                     ");" +

                                     "CREATE TABLE Output_Warehouse" +
                                     "(" +
                                        "Invoice_Id INT PRIMARY KEY REFERENCES Invoice(Id)," +
                                        "Product_Id INT REFERENCES Products(Id)," +
                                        "Amount REAL" +
                                     ")";
            SqlCommand createCommand = new SqlCommand(createStatement, sqlConnection);
            createCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        protected void FillButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string insertString = "INSERT INTO Permissions VALUES ('CEO','high');" +
                                  "INSERT INTO Permissions VALUES('COO','high');" +
                                  "INSERT INTO Permissions VALUES('Logistic manager','high');" +
                                  "INSERT INTO Permissions VALUES('Quality manager','high');" +
                                  "INSERT INTO Permissions VALUES('Customer services','low');" +
                                  "INSERT INTO Permissions VALUES('Team leader','low');" +
                                  "INSERT INTO Permissions VALUES('Operator','low');" +
                                  "INSERT INTO Permissions VALUES('Troubelshooter','low');" +
                                  "INSERT INTO Permissions VALUES('Maintenance','low');" +
                                  "INSERT INTO Permissions VALUES('Recruitment','low');" +
                                  "INSERT INTO Permissions VALUES('Payroll','low');" +
                                  "INSERT INTO Permissions VALUES('Accountant','low');" +
                                  "INSERT INTO Employees VALUES('1','Vladimir','Weiss','1994-08-16','CEO','20','15','weiss','weiss');" +
                                  "INSERT INTO Employees VALUES('2','Martin','Skrtel','1994-09-11','COO','19','14','skrtel','skrtel');" +
                                  "INSERT INTO Employees VALUES('3','Juraj','Kucka','1991-01-01','Team leader','21','10','kucka','kucka');" +
                                  "INSERT INTO Employees VALUES('4','Martina','Moravcova','1989-12-12','Operator','18','9','moravcova','moravcova');" +
                                  "INSERT INTO Employees VALUES('5','Gabriela','Ferková','1991-07-02','Logistic manager','22','16','gferkova','gferkova');" +
                                  "INSERT INTO Employees VALUES('6','Miroslav','Sivák','1976-11-10','Customer services','23','17','msivak','msivak');" +
                                  "INSERT INTO Employees VALUES('7','Franstišek','Petro','1978-01-23','Quality manager','20','15','fpetro','fpetro');" +
                                  "INSERT INTO Employees VALUES('8','Peter','Ihnát','1981-10-08','Troubelshooter','20','15','pihnat','pihnat');" +
                                  "INSERT INTO Employees VALUES('9','Martin','Melník','1994-03-15','Maintenance','20','15','mmelnik','mmelnik');" +
                                  "INSERT INTO Employees VALUES('10','Jaroslav','Bajnák','1974-02-27','Operator','20','15','jbajnak','jbajnak');" +
                                  "INSERT INTO Employees VALUES('11','Zuzana','Novotná','1989-06-13','Recruitment','20','15','znovotna','znovotna');" +
                                  "INSERT INTO Employees VALUES('12','Mária','Balážová','1994-09-26','Payroll','20','15','mbalazova','mbalazova');" +
                                  "INSERT INTO Employees VALUES('13','Kristína','Svarinská','1986-04-12','Accountant','20','15','ksvarinska','ksvarinska');" +
                                  "INSERT INTO Employees VALUES('14','Ján','Horváth','1979-09-01','Operator','20','15','jhorvath','jhorvath');" +
                                  "INSERT INTO Employees VALUES('15','Dušan','Hnáth','1972-05-17','Accountant','20','15','dhnath','dhnath');" +
                                  "INSERT INTO Employees VALUES('16','Henrieta','Bilerová','1992-07-22','Operator','20','15','hbilerova','hbilerova');" +
                                  "INSERT INTO Employees VALUES('17','Darina','Frenková','1993-12-05','Accountant','20','15','dfrenkova','dfrenkova');" +
                                  "INSERT INTO Products VALUES('1','Farebný papier','5000.00');" +
                                  "INSERT INTO Products VALUES('2','Klasický papier','2500.00');" +
                                  "INSERT INTO Products VALUES('3','Kartón','1250.00'); " +
                                  "INSERT INTO Products VALUES('4','Guličkové pero','800.00'); " +
                                  "INSERT INTO Products VALUES('5','Plniace pero','1250.00'); " +
                                  "INSERT INTO Products VALUES('6','Sada zvýrazňovačov','1500.00'); " +
                                  "INSERT INTO Products VALUES('7','Ceruzka HB1','500.00'); " +
                                  "INSERT INTO Products VALUES('8','Ceruzka HB2','500.00'); " +
                                  "INSERT INTO Products VALUES('9','Ceruzka HB3','500.00'); " +
                                  "INSERT INTO Products VALUES('10','Kalkulačka CASIO500','1800.00'); " +
                                  "INSERT INTO Products VALUES('11','Euroobaly 100ks','450.00'); " +
                                  "INSERT INTO Products VALUES('12','Šanón A4','1350.00'); " +
                                  "INSERT INTO Products VALUES('13','Zošívačka','2250.00'); " +
                                  "INSERT INTO Products VALUES('14','Nožnice','1650.00'); " +
                                  "INSERT INTO Products VALUES('15','Stojan','900.00'); " +
                                  "INSERT INTO Products VALUES('16','Zošit 540','300.00'); " +
                                  "INSERT INTO Products VALUES('17','Zošit 520','300.00'); " +
                                  "INSERT INTO Products VALUES('18','Obaly zošit','1000.00'); " +
                                  "INSERT INTO Products VALUES('19','Guma','500.00'); " +
                                  "INSERT INTO Products VALUES('20','Poznámkový blokA4','800.00'); " +
                                  "INSERT INTO Products VALUES('21','Poznámkový blokA5','600.00'); " +
                                  "INSERT INTO Products VALUES('22','Diár 2020','1650.00'); " +
                                  "INSERT INTO Products VALUES('23','Stolový kalendár','1150.00'); " +
                                  "INSERT INTO Products VALUES('24','Fixa flipchart','900.00'); ";

            SqlCommand insertCommand = new SqlCommand(insertString, sqlConnection);
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        protected void SimulationButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=tcp:apis19db.database.windows.net,1433;Initial Catalog=apis19db;Persist Security Info=False;User ID=adminadmin;Password=VojciceTrebisov1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string insertString = "INSERT INTO Users VALUES ('1','Cristiano','Ronaldo','1944-06-06','Pasteurova 9','040 12','Košice','Slovensko','ronaldo','ronaldo','Very high');" +
                                  "INSERT INTO Users VALUES('2','Lionel','Messi','1975-08-24','Rozvojová 3','040 22','Košice','Slovensko','messi','messi','High');" +
                                  "INSERT INTO Users VALUES('3','Gonzalo','Higuain','1995-12-24','Diamantová 5','040 23','Košice','Slovensko','higuain','higuain','Average');" +
                                  "INSERT INTO Invoice VALUES('1','1','1','zlty','2017-12-24',1,5000.00,1,'Pending','Yes');" +
                                  "INSERT INTO Invoice VALUES('2','2','2','biely','2017-12-24',4,10000.00,3,'Pending','Yes');" +
                                  "INSERT INTO Invoice VALUES('3','3','3','hnedy','2017-12-24',6,7500.00,4,'Pending','Yes');" +
                                  "INSERT INTO Invoice VALUES('4','1','1','modry','2017-12-24',7,35000.00,3,'Pending','Yes');" +
                                  "INSERT INTO Invoice VALUES('5','2','2','biely','2017-12-24',4,10000,4,'Pending','Yes');" +
                                  "INSERT INTO Invoice VALUES('6','3','3','hnedy','2017-12-24',1,1250,5,'Pending','Yes');" +
                                  "INSERT INTO Input_Warehouse VALUES('Wood',100,100);" +
                                  "INSERT INTO Input_Warehouse VALUES('Trash',100,100); ";
                                  
            SqlCommand insertCommand = new SqlCommand(insertString, sqlConnection);
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}