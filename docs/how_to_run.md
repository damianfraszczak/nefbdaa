# NEFBDAA - .NET Environment for Building Dynamic Angular Applications

NEFBDAA is a high-level C#, .NET Core, and Angular web-based framework which encourages rapid development and clean, pragmatic design. It takes care of much of the difficulties that come with web development, therefore, researchers need only focus on developing scientific work and modifying their web-based applications with just a few lines of code change. 
## How to start
Application comes with ready to use app starter template that should be used as a staring point to use the framework.
### Backend
To run application backend go to the starter application and run it in your IDE or via a command line
```
dotnet run --project NEFBDAAStarter
```
and backend services will be available on http://localhost:8090
### Frontend
To run application backend go to the starter application and run it in your IDE or via a command line
```
ng serve --port 4222 --no-progress --proxy-config proxy.config.json
```
then frontend layer will be available on http://localhost:4222. You can observe that there is additional param pass to the command - `proxy-config`. It is used to configure proxing of the http requests from frontend to backend. In dev environment it is needed as both layers are working on different ports.