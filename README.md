# URl Shortening Service
This project was generated with [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). 

## Description
This project provides a simple Url shortening service as a webapi service. Basically you can use this project to get more shorter urls. 
Once you send a url throughout post method you are going to see the response as service based shortened url. If you go to the url that returned from the post method 
on the browser this will redirect you to the original url.

Additionally, database is used as an embedded db. SQLite is used so you can see the database file under the root direction of the project.
Note: No need to execute any migration process. If you need you can run the following commands on the terminal.

`dotnet ef migrations add "MyFirstMigration"`
`dotnet ef database update`

## Install and run the project

Run `dotnet watch run` for a dev server. Navigate to `https://localhost:3000/`. The application will automatically reload if you change any of the source files.
