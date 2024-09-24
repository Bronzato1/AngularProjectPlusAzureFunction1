# How to create a new project for Azure Function 

> This document relates to Visual Studio Code

- Open Visual Studio Code 
- Open an empty folder
- Press `F1` key to show the Command Palette
- Type *Azure Function*
- In the list, select **Create new project** 
- Select the folder that will contain the project
- Select a language: **C#**
- Select a NET runtime: **.NET 8.0 LTS**
- Select a template: **HTTP trigger**
- Provide a function name: **HttpTrigger1**
- Provide a namespace: **Company.Function**
- Choose access rights: **Anonymous**

The project is created with the first function.

**Start debugging** for testing by pressing `F5`

or 

**Launch the runtime** for Azure functions with  `func host start` 