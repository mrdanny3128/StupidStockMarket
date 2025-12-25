using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using StupidStockMarket.Models;
using System;
using System.Collections.Generic;

namespace StupidStockMarket.Controllers
{
    public class StockController : Controller
    {
        private string connectionString = "Server=localhost;Database=StupidStockDB;Uid=root;Pwd=;";

        public IActionResult Index()
        {
            List<Stock> stocks = new List<Stock>();
            decimal balance = 0;
            Random rng = new Random();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // 1. UPDATE: Change prices randomly (-10% to +10%)
                string updateSql = "UPDATE Stocks SET CurrentPrice = CurrentPrice + (CurrentPrice * @change)";
                MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                double randomChange = (rng.NextDouble() * 0.2) - 0.1;
                updateCmd.Parameters.AddWithValue("@change", randomChange);
                updateCmd.ExecuteNonQuery();

                // 2. GET WALLET: Fetch player balance
                string balSql = "SELECT Balance FROM Player WHERE Id = 1";
                MySqlCommand balCmd = new MySqlCommand(balSql, conn);
                balance = Convert.ToDecimal(balCmd.ExecuteScalar());

                // 3. GET STOCKS: Fetch all items
                string sql = "SELECT * FROM Stocks";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stocks.Add(new Stock
                        {
                            Id = reader.GetInt32("Id"),
                            ItemName = reader.GetString("ItemName"),
                            CurrentPrice = Math.Round(reader.GetDecimal("CurrentPrice"), 2)
                        });
                    }
                }
            }

            ViewBag.PlayerBalance = balance; // Pass balance to the UI
            return View(stocks);
        }

        [HttpPost]
        public IActionResult Buy(int stockId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Get item price
                string priceSql = "SELECT CurrentPrice FROM Stocks WHERE Id = @id";
                MySqlCommand priceCmd = new MySqlCommand(priceSql, conn);
                priceCmd.Parameters.AddWithValue("@id", stockId);
                decimal price = Convert.ToDecimal(priceCmd.ExecuteScalar());

                // Get wallet balance
                string balanceSql = "SELECT Balance FROM Player WHERE Id = 1";
                MySqlCommand balanceCmd = new MySqlCommand(balanceSql, conn);
                decimal balance = Convert.ToDecimal(balanceCmd.ExecuteScalar());

                // Process purchase if enough money exists
                if (balance >= price)
                {
                    string updateSql = "UPDATE Player SET Balance = Balance - @price WHERE Id = 1";
                    MySqlCommand updateCmd = new MySqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@price", price);
                    updateCmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}