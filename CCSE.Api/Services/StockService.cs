using CCSE.Api.Domain;
using Npgsql;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace CCSE.Api.Services
{
    public class StockService
    {
        public bool SaveStock(Stock stock)
        {
            //string input = new StringReader("");
            

            // Load the stream
            //string yaml = new YamlStream();
            //yaml.Load(input);

             //Examine the stream
            //var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;

            //foreach (var entry in mapping.Children)
            //{
            //    Console.WriteLine(((YamlScalarNode)entry.Key).Value);
           // }

            // List all the items
            //var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("aruco_bc_markers")];

        


        string strConnString = "Server=http://database-1.cfv10rew3vrj.us-east-1.rds.amazonaws.com/;Port=5432;User Id=postgres;Password=isuruCcamsciit;Database=CC_CSE";
            try
            {
                using var con = new NpgsqlConnection(strConnString);
                con.Open();

                var sql = "INSERT INTO Stock(name,currency,symbol) VALUES(@name,@currency,@symbol)";
                using var cmd = new NpgsqlCommand(sql, con);

                //cmd.Parameters.AddWithValue("id", stock.id);
                cmd.Parameters.AddWithValue("name", stock.name);
                cmd.Parameters.AddWithValue("currency", stock.currency);
                cmd.Parameters.AddWithValue("symbol", stock.symbol);
                
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                // System.Windows.Forms.MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        



    }
}
