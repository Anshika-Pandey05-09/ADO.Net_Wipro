using System.Data;
using Microsoft.Data.SqlClient;

namespace MVC_ADO_DemoDay31.Data
{
    public class ProductsRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductsRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Methods to interact with the database can be added here


        // CRUD operations can be added here
        //InsertProduct
        public int InsertProduct(string name, decimal price)
        {
            //Insert OPerations : 
            //Step 1: Creating the connection 
            using var connection = _dbConnection;
            //Step 2: Creating the command Object
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Name, Price) VALUES (@name, @price); SELECT SCOPE_IDENTITY();";
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@price", price));
            //opening the connection
            connection.Open();
            //Step 3: Executing the command
            var result = command.ExecuteScalar(); // This will return the newly inserted product ID
                                                  //Step 4: Closing the connection
            connection.Close();
            return Convert.ToInt32(result);
        }
        //Filling Dataset snapshot( Adapter manages Open/Close)

        public DataSet GetProducts()
        {
            var dataSet = new DataSet(); // dataset is used to hold the result set
            using var connection = _dbConnection;
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Products"; // Selecting all products
            using var adapter = new SqlDataAdapter((SqlCommand)command);
            //using Adapter with DataSet helps us to open/close automatically
            adapter.Fill(dataSet); // Filling the dataset with the result set
            return dataSet;
        }// we can also have data table as return type
    }
}