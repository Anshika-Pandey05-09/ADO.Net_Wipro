using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = @"Data Source=DELL;Initial Catalog=AuthDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

using (SqlConnection conn = new SqlConnection(connectionString))
{
    conn.Open();
    Console.WriteLine("Connection Opened Successfully");

    // Insert
    string insertQuery = "INSERT INTO Products (Name, Price) VALUES (@name, @price)";
    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
    {
        cmd.Parameters.AddWithValue("@name", "Sample Product");
        cmd.Parameters.AddWithValue("@price", 19.99);
        int rowsAffected = cmd.ExecuteNonQuery();
        Console.WriteLine($"{rowsAffected} row(s) inserted.");
    }

    // Select
    string selectQuery = "SELECT * FROM Products";
    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
    {
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet dataSet = new DataSet();
        adapter.Fill(dataSet);

        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            Console.WriteLine($"ID: {row["Id"]}, Name: {row["Name"]}, Price: {row["Price"]}");
        }
    }

    conn.Close();
    Console.WriteLine("Connection Closed Successfully");
}