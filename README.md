# Tortilla Emporium Web Application

## Project Overview
Welcome to Tortilla Emporium, a fictional business where we sell a variety of tortilla-related products! 
This application is designed to mimic aspects of some online shopping centers in order to gain experience in ASP.NET MVC and related technologies as described in the Revature .NET Full Stack Training. 
The user assumes the role of both manager and customer and is able to interact with the application to perform functions such as creating a store, placing orders, and listing all customers. 

## Features
Currently Implemented Features:
* Lists all available stores and customers
* The ability to create stores and customers with relevant information
* Lists a selected store's inventory and placed orders
* Lists a selected customer's placed orders
* The ability for a customer to place orders based on a selected store and its current inventory

Feature To-Do List:
* Separate the manager and customer functionalities
* Ability to add inventory items to stores
* Add proper user authentication to differentiate from manager and customer
* Fix a bug related to the store inventory list not being able to connect to the database

## Technologies Used
Frontend:
* ASP.NET MVC
* HTML5
* CSS3

Backend:
* C# Programming
* Microsoft SQL Server Database
* ADO.NET Entity Framework

Testing and Logging:
* Xunit
* Serilog
* SonarCloud

DevOps:
* GitHub Actions
* Azure App Services
* Azure Database

## Getting Started
* git clone https://github.com/210628-UTA-NET/iram-olivares-P1
* Navigate to StoreAppWebUI directory in a terminal
* Enter in the following command to run on local machine (requires .Net 5): dotnet run
* Deployed Application link: https://tortilla-emporium.azurewebsites.net/
