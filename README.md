# Project Setup

This project includes a frontend and backend application, along with a database using Microsoft SQL Server. Follow the steps below to set up and run the project.


### 1. Initialize the Database

1. Pull the SQL Server Docker image: 
   ```bash
   docker pull mcr.microsoft.com/mssql/server:2022-latest
   ```
2. run docker compose up
3. login to your db gui (azure studio etc)



### 2. Run The Project
run BankMegaTechTest-FE for the frontend - .NET CORE 6 MVC
- dont forget to change the url settings for http request on appsettings.json


run BankMegaTechTest-FE for the backend - .NET CORE 6 API

the sql query test is on Soal Test Query Folder


## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [.NET Core 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
