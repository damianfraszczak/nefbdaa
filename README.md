# NEFBDAA - .NET Environment for Building Dynamic Angular Applications

NEFBDAA is a high-level C#, .NET Core, and Angular web-based framework which encourages rapid development and clean, pragmatic design. It takes care of much of the difficulties that come with web development, therefore, researchers need only focus on developing scientific work and modifying their web-based applications with just a few lines of code change. 

## Features
- encourages rapid development and clean, pragmatic design for both backend and frontend layers.
- Services configuration via predefined configuration model including: Swagger, entity tracing or security.
- Enhanced dependency injection process via a predefined set of data attributes.
- Swagger REST API documentation generation in OpenApi format.
- Database enhancements: database configuration, auditing and versioning entities, generic entities models, custom data models.
- Service enhancements: generic CRUD service and HTTP controller's implementation including dynamic filtering, ordering, aggregation and pagination, cache layer, logger configuration, entity tracking, storing file-based objects like documents
- UI form and tables components generation based on domain objects:
   - configuration on ViewModel objects that can be ovveriden on the frontend
   - computed fields on frontend based on configuration on backend
   - inline add/edit in dropdowns for associations and tables
   - 25+ field mappings that can be extended
- UI components based on Nebular framework, that includes:
  - 4 coloristic themes
  - Responsive layout
  - RTL support
  - High resolution
- Asynchronus tasks management on the backend with Hangfire including backend dashboard
- Email triggers system with template rendering
- Dynamic data filtering utylizng QueryBuilder based on the following conditions:
  - in
  - not in
  - equal
  - not equal
  - between
  - not between
  - less
  - less or equal
  - greater
  - greater or equal
  - begins with
  - not begins with
  - contains
  - not contains
  - ends with
  - not ends with
  - is empty
  - is not empty
  - is null
  - is not null
- Predefined set of ready to use components:
  - Login
  - Registration
  - Password reset
  - Forgot password
  - User management
  - Environment configuraturaion management
  - App log viewer
  - Documents management
  - Notes management
  - Files management
- ...

## How to start
The procedure for running NEFBDAA is described in [README](docs/how_to_run.md) file.

## Architecture
For a project architecture, refer to its [README](docs/architecture.md) file.

## Integration models
NEFBDAA comes with a set of data models that are shared accross the whole environment. They are used for different purposes, more about them can be found in [README](docs/models.md) file.

## Sample codes 
Sample codes how to use library and consume its best features is presented [here](docs/sample_codes.md).

## Showcase
NEFBDAA has been employed in several commercial and scientific projects, some examples are presented [here](docs/showcase.md).

## Maintainers

Project maintainers are:

- Damian Frąszczak



