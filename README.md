🚀 Stupid Stock Exchange
A fun, interactive web application built with C# ASP.NET Core MVC and MySQL. This app simulates a "stupid" stock market where users can buy ridiculous items like "Used Napkins" or "Left Socks" using a virtual wallet.

✨ Features
Real-time Price Fluctuation: Prices change by -10% to +10% every time the page is refreshed, simulated using C# logic.

Virtual Wallet: Players start with a balance (e.g., $1000) and can spend it on various "stocks."

Persistent Storage: Uses a MySQL (XAMPP) database to save player balances and stock prices.

Responsive UI: A dark-themed, modern dashboard built with Bootstrap.

Dynamic Inventory: Automatically displays any number of products added to the database.

🛠️ Tech Stack
Backend: C# / ASP.NET Core 8.0 (MVC)

Database: MySQL (via XAMPP)

Frontend: HTML5, CSS3 (Bootstrap), Razor Pages

Database Driver: MySql.Data (NuGet Package)

⚙️ Setup & Installation
1. Database Configuration (XAMPP)
Start Apache and MySQL in your XAMPP Control Panel.

Open phpMyAdmin and create a database named StupidStockDB.

Import the following SQL to create your tables:

CREATE TABLE Stocks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ItemName VARCHAR(50) NOT NULL,
    CurrentPrice DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Player (
    Id INT PRIMARY KEY,
    Balance DECIMAL(10, 2) NOT NULL
);

INSERT INTO Player (Id, Balance) VALUES (1, 1000.00);
INSERT INTO Stocks (ItemName, CurrentPrice) VALUES 
('Used Napkins', 5.50), 
('Meme Templates', 45.00), 
('Left Socks', 12.00);

2. Project Setup
Clone this repository to your local machine.

Open the .sln file in Visual Studio 2022.

Open appsettings.json and ensure the connection string matches your XAMPP settings:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=StupidStockDB;Uid=root;Pwd=;"
}

Press F5 to run the application.

This project is for educational purposes. Feel free to use and modify it!
