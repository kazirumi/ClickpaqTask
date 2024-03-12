# Both .NET 6 Api and Angular 14 UI added in same directory

## API set up description

* First clone the repository and go to api solution
* Install .Net 6 SDK
* Install SQL Server and change the connection string according to the DB
* Use "update-database" command to have the changes from migrations
* Open sln with visual studio 2022
* Run it on "http://localhost:5146" or change angular environment base api path according running api port


## Angular 14 UI set up description

* First clone the repository and go to UI solution
* Open it in vs code
* Make sure that node.js version 20.9.0 or any version that support angular 14 should be installed
* Give "npm install" comand from terminal
* Then run "ng serve" command
* Make sure that in environment.ts the rootURL Base path is same as api is running on the port

