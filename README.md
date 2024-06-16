TechTest.sln is the .net Framerwork 4.8 Version of the Technical Test, one Api with JWT, roles and Users. The Database being used is SQL Server Locally.


Opening NetCore Folders has the version with .Net 8, Has Authentication with JWT, Roles, Users and Products Controllers with CRUD.
This project runs on Docker using also Redis and PostgreSQL on the same docker compose file, Redis being used only on ProductsController.

## Steps to migrate from .Net Framework to .Net

- Create another Project With .Net
- Copy Files from .Net Framework to .Net Project
- Install Nuget Packages on new Versions on new .NetProject
- Fix Dependecy Injections on .Net Project
- Runt Tests and Application to Test Behaviors
