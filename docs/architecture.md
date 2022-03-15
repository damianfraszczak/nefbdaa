# NEFBDAA - .NET Environment for Building Dynamic Angular Applications

NEFBDAA is a high-level C#, .NET Core, and Angular web-based framework which encourages rapid development and clean, pragmatic design. It takes care of much of the difficulties that come with web development, therefore, researchers need only focus on developing scientific work and modifying their web-based applications with just a few lines of code change. 
## Technology stack
C#, .NET Core, Angular, TypeScript, JavaScript
## Dependencies
- Backend (main dependencies):
  - [Entity Framework Core](https://github.com/dotnet/efcore)
  - [Hangfire](https://www.hangfire.io/)
  - [QueryBuilder](https://github.com/tghamm/dynamic-linq-query-builder)
  - [AgileMapper](https://github.com/agileobjects/AgileMapper)
  - [FluentEmail](https://github.com/lukencode/FluentEmail)
  - [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
  - ...
- Frontend (main dependencies):
  - [Nebular](https://github.com/akveo/nebular) 
  - [Smart table](https://github.com/akveo/ng2-smart-table)
  - [CKEditor](https://github.com/ckeditor/ckeditor5-angular)
  - 
## Architecture
The proposed environment architecture is presented in the below figure. It consists of the three main components: 
![image](https://user-images.githubusercontent.com/19403500/157183726-23b3a9f6-c58b-4854-8eec-d79a3fb5e9c9.png)

- .NET Core module – it provides a set of standard models, utils, and functionalities that supports building the backend layer of web applications. It covers database access and management layers, asynchronous tasks processing, enhanced CRUD layer implementation, including dynamic data filtering, ordering, aggregation, pagination, and security configuration. Besides these, it also provides a set of custom data attributes to improve the dependency on the injection configuration process and build the JSON-based model configuration to be consumed by frontend services to generate dynamic-based forms and tables for domain objects. The introduced services are easily configurable via a configuration model, enabling or disabling them if necessary.
- The angular core module provides a set of standard models, utils, and functionalities that support building the secure, responsive, and maintainable SPA. Besides that, it also provides the components responsible for consuming JSON-based model configuration to display forms and tables.
- .NET Core Angular starter application is a sample starter app that shows how to start with the default settings to work on the project. 