using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data; // needed for DataSet, DataTable

class Program
{
    static string connectionString = @"Data Source=DELL;Initial Catalog=BookstoreDB;Integrated Security=True;";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== Bookstore Menu ===");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. View Books (Reader)");
Console.WriteLine("7. View Books (Adapter)");
            Console.Write("Choose option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: AddBook(); break;
                case 2: ViewBooks_Disconnected(); break;
                case 3: UpdateBook_Disconnected(); break;
                case 4: DeleteBook_Disconnected(); break;
                case 5: return;
                
case 6: ViewBooks_WithReader(); break;
case 7: ViewBooks_WithAdapter(); break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    static void ViewBooks_WithReader()
{
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string query = "SELECT * FROM Books";
        SqlCommand cmd = new SqlCommand(query, conn);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("\n--- Books (Using SqlDataReader: Connected) ---");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["BookId"]} | {reader["Title"]} | {reader["Author"]} | {reader["Price"]} | Qty: {reader["Quantity"]}");
        }
    }
}


static void ViewBooks_WithAdapter()
{
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string query = "SELECT * FROM Books";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Books");

        Console.WriteLine("\n--- Books (Using SqlDataAdapter/DataSet: Disconnected) ---");
        foreach (DataRow row in ds.Tables["Books"].Rows)
        {
            Console.WriteLine($"{row["BookId"]} | {row["Title"]} | {row["Author"]} | {row["Price"]} | Qty: {row["Quantity"]}");
        }
    }
}


    // 1. ADD BOOK
    static void AddBook()
    {
        Console.Write("Enter Title: ");
        string title = Console.ReadLine();
        Console.Write("Enter Author: ");
        string author = Console.ReadLine();
        Console.Write("Enter Price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Enter Quantity: ");
        int qty = int.Parse(Console.ReadLine());

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_AddBook", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Author", author);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Quantity", qty);

            conn.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("✅ Book added using Stored Procedure!");
        }
    }


    // 2. VIEW BOOKS
    // static void ViewBooks()
    // {
    //     using (SqlConnection conn = new SqlConnection(connectionString))
    //     {
    //         string query = "SELECT * FROM Books";
    //         SqlCommand cmd = new SqlCommand(query, conn);

    //         conn.Open();
    //         SqlDataReader reader = cmd.ExecuteReader();

    //         Console.WriteLine("\n--- Book List ---");
    //         while (reader.Read())
    //         {
    //             Console.WriteLine($"{reader["BookId"]} | {reader["Title"]} | {reader["Author"]} | {reader["Price"]} | Qty: {reader["Quantity"]}");
    //         }
    //     }
    // }

    // VIEW BOOKS (Disconnected using DataSet)
    static void ViewBooks_Disconnected()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Books";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();

            // Fill DataSet with Books table
            adapter.Fill(ds, "Books");

            Console.WriteLine("\n--- Book List (From DataSet) ---");
            foreach (DataRow row in ds.Tables["Books"].Rows)
            {
                Console.WriteLine($"{row["BookId"]} | {row["Title"]} | {row["Author"]} | {row["Price"]} | Qty: {row["Quantity"]}");
            }
        }
    }

    // 3. UPDATE BOOK
    // static void UpdateBook()
    // {
    //     Console.Write("Enter BookId to update: ");
    //     int id = int.Parse(Console.ReadLine());
    //     Console.Write("Enter new Price: ");
    //     decimal price = decimal.Parse(Console.ReadLine());
    //     Console.Write("Enter new Quantity: ");
    //     int qty = int.Parse(Console.ReadLine());

    //     using (SqlConnection conn = new SqlConnection(connectionString))
    //     {
    //         SqlCommand cmd = new SqlCommand("sp_UpdateBook", conn);
    //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //         cmd.Parameters.AddWithValue("@BookId", id);
    //         cmd.Parameters.AddWithValue("@Price", price);
    //         cmd.Parameters.AddWithValue("@Quantity", qty);

    //         conn.Open();
    //         int rows = cmd.ExecuteNonQuery();
    //         if (rows > 0)
    //             Console.WriteLine("✅ Book updated using Stored Procedure!");
    //         else
    //             Console.WriteLine("❌ Book not found.");
    //     }
    // }

    // UPDATE BOOK (Disconnected using DataSet/DataTable)
    static void UpdateBook_Disconnected()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", conn);

            // Auto-generate INSERT/UPDATE/DELETE
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Books");

            Console.Write("Enter BookId to update: ");
            int id = int.Parse(Console.ReadLine());

            // Find row in DataTable
            DataRow[] rows = ds.Tables["Books"].Select("BookId=" + id);
            if (rows.Length > 0)
            {
                Console.Write("Enter new Price: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Enter new Quantity: ");
                int qty = int.Parse(Console.ReadLine());

                rows[0]["Price"] = price;
                rows[0]["Quantity"] = qty;

                // Push changes back to DB
                adapter.Update(ds, "Books");

                Console.WriteLine("✅ Book updated (Disconnected mode).");
            }
            else
            {
                Console.WriteLine("❌ Book not found.");
            }
        }
    }

    // 4. DELETE BOOK
    // static void DeleteBook()
    // {
    //     Console.Write("Enter BookId to delete: ");
    //     int id = int.Parse(Console.ReadLine());

    //     using (SqlConnection conn = new SqlConnection(connectionString))
    //     {
    //         SqlCommand cmd = new SqlCommand("sp_DeleteBook", conn);
    //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //         cmd.Parameters.AddWithValue("@BookId", id);

    //         conn.Open();
    //         int rows = cmd.ExecuteNonQuery();
    //         if (rows > 0)
    //             Console.WriteLine("✅ Book deleted using Stored Procedure!");
    //         else
    //             Console.WriteLine("❌ Book not found.");
    //     }
    // }
    
    // DELETE BOOK (Disconnected using DataSet/DataTable)
static void DeleteBook_Disconnected()
{
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", conn);
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

        DataSet ds = new DataSet();
        adapter.Fill(ds, "Books");

        Console.Write("Enter BookId to delete: ");
        int id = int.Parse(Console.ReadLine());

        DataRow[] rows = ds.Tables["Books"].Select("BookId=" + id);
        if (rows.Length > 0)
        {
            rows[0].Delete(); // mark row for deletion
            adapter.Update(ds, "Books"); // sync back
            Console.WriteLine("✅ Book deleted (Disconnected mode).");
        }
        else
        {
            Console.WriteLine("❌ Book not found.");
        }
    }
}

}