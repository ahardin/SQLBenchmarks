using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
namespace PrimaryKeyBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Plain"].ConnectionString))
            {
                conn.Open();
                using (System.IO.StreamReader reader = new System.IO.StreamReader("database.sql", true))
                {
                    string[] statements = reader.ReadToEnd().Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    var command = conn.CreateCommand();
                    command.CommandType = System.Data.CommandType.Text;

                    foreach (string statement in statements)
                    {
                        command.CommandText = statement;
                        command.ExecuteNonQuery();
                    }
                    
                    conn.Close();
                }
            }

            Console.WriteLine("Starting integer test...");

            int numberOfTrials = 10;
            int numberOfInserts = 1000;
            double[] integerRunTimes = new double[numberOfTrials];
            double[] guidRunTimes = new double[numberOfTrials];
            DateTime start;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Plain"].ConnectionString))
            {
                conn.Open();
                var command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;

                for (int x = 0; x < numberOfTrials; x++)
                {
                    Console.WriteLine("Deleting {0}", x);

                    command.CommandText = "TRUNCATE TABLE [Integer]";
                    command.ExecuteNonQuery();

                    StringBuilder b = new StringBuilder();

                    Console.WriteLine("Building query {0}", x);
                    for (int i = 0; i < numberOfInserts; i++)
                    {
                        b.AppendLine(String.Format("INSERT INTO [Integer] ([Value]) VALUES ({0})", i.ToString()));
                    }
                    
                    Console.WriteLine("Executing query {0}", x);
                    start = DateTime.Now;
                    command.CommandText = b.ToString();
                    command.ExecuteNonQuery();

                    DateTime end = DateTime.Now;

                    Console.WriteLine("Integer run {0} time: {1}ms", x, (end - start).TotalMilliseconds);
                    integerRunTimes[x] = (end - start).TotalMilliseconds;
                }

                for (int x = 0; x < numberOfTrials; x++)
                {
                    Console.WriteLine("Deleting {0}", x);

                    command.CommandText = "TRUNCATE TABLE [Guid]";
                    command.ExecuteNonQuery();

                    StringBuilder b = new StringBuilder();

                    Console.WriteLine("Building query {0}", x);
                    for (int i = 0; i < numberOfInserts; i++)
                    {
                        b.AppendLine(String.Format("INSERT INTO [Guid] ([Value]) VALUES ({0})", i.ToString()));
                    }

                    Console.WriteLine("Executing query {0}", x);
                    start = DateTime.Now;
                    command.CommandText = b.ToString();
                    command.ExecuteNonQuery();

                    DateTime end = DateTime.Now;

                    Console.WriteLine("Guid run {0} time: {1}ms", x, (end - start).TotalMilliseconds);
                    guidRunTimes[x] = (end - start).TotalMilliseconds;
                }

                conn.Close();
            }

            Console.WriteLine("Average Integer time: {0}ms", integerRunTimes.Where(i => i > 0).Average());
            Console.WriteLine("Average Guid time: {0}ms", guidRunTimes.Where(i => i > 0).Average());
            Console.ReadLine();
        }
    }
}
