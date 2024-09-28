# How to integrate Azure SQL Database witn Azure Function

Source here: https://firattonak.medium.com/deploying-angular-app-using-asp-net-core-and-azure-sql-5c6e2d7ee8c0

- Go to the search box, type “SQL database,” and select “SQL databases.”

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database.webp" style="width: 33%; height: 33%;">
</div>

- Click on the “Create” link.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-create-link.webp" style="width: 33%; height: 33%;">
</div>

- Select Subscription and Resource group. 

> If you do not have a resource group, click on the “Create new” button and create a new resource group.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-resource-group.webp" style="width: 63%; height: 63%;">
</div>

- Type a database name that you like, and if you do not have a server, click on the “Create a new” link.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-server-details.webp" style="width: 63%; height: 63%;">
</div>

- Fill in the details like below.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-authentication.webp" style="width: 63%; height: 63%;">
</div>

- We do not need SQL elastic pool at this time, which is why keep it as “No.” 

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-elastic-pool.webp" style="width: 33%; height: 33%;">
</div>

- The workload environment will be set to “Development”.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-workload-environment.webp" style="width: 33%; height: 33%;">
</div>

> Now, you should configure your database; otherwise, you might face a big bill at the end of the month. 

- Go to “Configure database” and then select “General Purpose” service tier.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-service-and-compute-tier.webp" style="width: 93%; height: 93%;">
</div>

- You do not need backup storage right now, and that is why the third option will be okay for you. 

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-backup-storage-redundancy.webp" style="width: 63%; height: 63%;">
</div>

- Click on the “Review + create” button and continue.

> After the SQL database is created, you should be able to see the new SQL database in the list. 

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-list.webp" style="width: 63%; height: 63%;">
</div>

- Click on the database name, and then check the details of the SQL database.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-one-plus-one.webp" style="width: 63%; height: 63%;">
</div>

> In order to connect to the SQL DB on Azure, you have to create a firewall rule for the Backend. 

- Go to your backend app (Function App in this case) and copy the IP.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-copy-ip.webp" style="width: 63%; height: 63%;">
</div>

- Create a firewall rule, stating that your backend IP can access the SQL Database.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-selected-networks.webp" style="width: 63%; height: 63%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-firewall-rule.webp" style="width: 43%; height: 43%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-firewall-rule-save.webp" style="width: 63%; height: 63%;">
</div>

> Now, it’s time to configure our backend project.

- Click on the “Show database connection string” link, and then copy the connection string.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-show-database-connection-strings.webp" style="width: 63%; height: 63%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-connection-string.webp" style="width: 63%; height: 63%;">
</div>

- Open the backend project, go to the appsettings.Development.json file, and enter the connection string.

xxxxxx

xxxxxx

- To test your database connection, go to Microsoft SQL Management Studio and attempt to connect to the server.

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-test-management-studio.webp" style="width: 43%; height: 43%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-new-firewall-rule.webp" style="width: 43%; height: 43%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-se-connecter.webp" style="width: 43%; height: 43%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-new-firewall-rule-completed.webp" style="width: 43%; height: 43%;">
</div>

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-object-explorer.webp" style="width: 43%; height: 43%;">
</div>

You will notice that a new firewall rule has been created

<div style="display: flex; justify-content: left; padding: 10px;">
  <img src=".images/azure-sql-database-firewall-rule-created.webp" style="width: 73%; height: 73%;">
</div>