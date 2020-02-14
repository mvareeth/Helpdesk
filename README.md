"# Helpdesk"
Angular 8 and .NET Core: Sample. This sample addresses following story points:
•	A logged in Helpdesk user can create new support ticket and modify the existing support tickets 
•	A logged in Helpdesk user can view all the support tickets assigned to the logged-in user. 
•	A logged-in Helpdesk user can view all the existing support tickets.
This sample uses 
•	Angular CLI 
•	JWT token for authentication and pass the token as part of the request 
Different Angular services provided for http, cache, global exception, logger service, authentication, token, dialog etc. It has a wrapper class for http and the interceptor. We really do not need to use interceptor here. Interceptor just deal with request does not do any token validation.
Running it
1.	npm install in web folder
2.	npm start
Server-side .NET Core application is an N layer application with in-memory storage. It uses Mapster instead of Automapper as an ORM.
