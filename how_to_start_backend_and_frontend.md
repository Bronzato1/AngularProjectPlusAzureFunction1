# How to start backend and frontend

## Start backend

- Right click on the *Backend* folder
- Click on  *Open in Integrated Terminal*
- Because this is an Azure Function, execute `func host start --verbose`
- In case this was a controller, execute `dotnet restore` `dotnet build` `dotnet watch run`

## Start frontend

- Right click on the *Frontend* folder
- Click on  *Open in Integrated Terminal*
- Execute `npm run start`
- Or `ng serve`
- Or open the *package.json* file and click on the *Debug* button

![foo image label](.images/debug-angular-frontend.webp)

![foo image label](.images/debug-angular-frontend-click.webp)