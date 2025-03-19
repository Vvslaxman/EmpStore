
### **1. Setting IIS (Internet Information Services) and Configuring Its Properties/Options via CMD**

IIS can be configured via `iisreset`, `appcmd`, and other related commands. Here are the steps and properties you can configure.

#### **a. Install IIS (Internet Information Services)**

1. **Using DISM (CMD) for IIS Installation:**
    ```cmd
    dism /online /enable-feature /featurename:IIS-WebServerRole /all
    ```

2. **Using PowerShell:**
    ```powershell
    Install-WindowsFeature -name Web-Server -IncludeManagementTools
    ```

#### **b. Start/Stop IIS Services (CMD)**
- **Start IIS:**
    ```cmd
    iisreset /start
    ```
- **Stop IIS:**
    ```cmd
    iisreset /stop
    ```
- **Restart IIS:**
    ```cmd
    iisreset
    ```

#### **c. Configure IIS via CMD (`appcmd`)**

- **View Current Sites:**
    ```cmd
    appcmd list site
    ```

- **Create a New Site:**
    ```cmd
    appcmd add site /name:MySite /bindings:http/*:80: /physicalPath:"C:\inetpub\wwwroot\StorePro\publish"
    ```

- **Start/Stop a Specific Site:**
    ```cmd
    appcmd start site /site.name:"MySite"
    appcmd stop site /site.name:"MySite"
    ```

- **Configure Directory Browsing (Enable/Disable):**
    ```cmd
    appcmd set config /section:directoryBrowse /enabled:true
    ```

#### **d. IIS Features and Configuration via CMD (`appcmd`)**

- **Enable Directory Browsing:**
    ```cmd
    appcmd set config /section:directoryBrowse /enabled:true
    ```

- **Enable SSL (HTTPS) for Site:**
    ```cmd
    appcmd set site /site.name:MySite /+bindings.[protocol='https',bindingInformation='*:443:']
    ```

- **Set Default Document:**
    ```cmd
    appcmd set config /section:defaultDocument /enabled:true
    ```

#### **e. Checking IIS Server Configurations (CMD)**
To check the status of various IIS configurations, you can use:
- **Check if the service is running:**
    ```cmd
    sc qc w3svc
    ```
    sc->manage services qc->queries configuration w3svc->service name
 

- **View App Pool Status:**
    ```cmd
    appcmd list apppool
    ```

- **Get Detailed Information About IIS Settings:**
    ```cmd
    appcmd list config
    ```

---

### **2. Publish and Build via PowerShell Commands (MVC/Web App)**

As you use Visual Studio for development, below are the PowerShell commands for building and publishing your MVC application.

#### **a. Publish an Application (ASP.NET Core) via PowerShell**
To publish an MVC application from your local directory to a specific path:
```powershell
dotnet publish -c Release -o C:\Users\Administrator\Desktop\proj\EmpStore\EmpStore\bin\Release\net9.0\publish 
```
```powershell
dotnet publish -c Release -o C:\path\to\output\folder -p:UseAppHost=false 
```
Use UseAppHost=false:
If you are publishing a self-contained executable and you want to avoid generating the AppHost.
If you're not using IIS, or if you're not using Kestrel.
Do not use UseAppHost=false:
If you are deploying to IIS and the application is running using Kestrel and the ASP.NET Core Module.
IIS uses the ASP.NET Core Module to host your application, and the app host is not needed.


#### **b. Build an Application (ASP.NET Core) via PowerShell**
To build an application:
```powershell
dotnet build
```

#### **c. Other Options for Publishing with PowerShell**
Here are some additional publishing-related commands you might find useful:

- **Publish for a Specific Framework:**
    ```powershell
    dotnet publish -f netcoreapp3.1 -o C:\path\to\output
    ```

- **Publish for a Specific Runtime:**
    ```powershell
    dotnet publish -r win-x64 -c Release -o C:\path\to\output
    ```

---

### **3. DDL Backup and Restore Commands in SQL Server**

Here are the commands to backup and restore databases in SQL Server.

#### **a. Backup Database (SQL Server)**
This command backs up a SQL Server database to a file:

```sql
BACKUP DATABASE [YourDatabaseName]
TO DISK = 'C:\path\to\backup\yourdatabase.bak'
WITH FORMAT;
```

#### **b. Restore Database (SQL Server)**
This command restores a SQL Server database from a backup file:

```sql
RESTORE DATABASE [YourDatabaseName]
FROM DISK = 'C:\path\to\backup\yourdatabase.bak'
WITH REPLACE;
```

---

### **4. Administrator and Local Users Account Management in Windows**

#### **a. Create Local User (CMD)**

To create a new local user on Windows:
```cmd
net user NewUsername NewPassword /add
```

#### **b. Add User to Local Group (CMD)**
To add the user to a group (e.g., `Administrators`):
```cmd
net localgroup Administrators NewUsername /add
```

#### **c. View All Local Users (CMD)**
To view all the local users:
```cmd
net user
```

#### **d. Delete Local User (CMD)**
To delete a user:
```cmd
net user NewUsername /delete
```

#### **e. Disable Local User (CMD)**
To disable a user account:
```cmd
net user NewUsername /active:no
```

---

### **5. SQL Server's User Roles and Testing a Normal User Without DB Access**

#### **a. Creating SQL Server User**

1. **Create SQL Login:**
    ```sql
    CREATE LOGIN MySQLLogin WITH PASSWORD = 'YourPassword';
    ```

2. **Create SQL User:**
    ```sql
    USE YourDatabase;
    CREATE USER MySQLUser FOR LOGIN MySQLLogin;
    ```

#### **b. Granting SQL Server Permissions**
To give a user access to a specific database:
```sql
USE YourDatabase;
ALTER ROLE db_datareader ADD MEMBER MySQLUser;
```

#### **c. Deny Database Access**
If you want to test a normal user without access to the database:
```sql
USE master;
DENY CONNECT TO MySQLLogin;
```

#### **d. Checking User Permissions**
To check the permissions granted to a user:
```sql
SELECT * FROM fn_my_permissions(NULL, 'USER');
```

#### **e. Test SQL Server Login for User Without DB Access**
To test a user without DB access, you can attempt to connect to the SQL Server using SQL Server Management Studio (SSMS) or via command line with that user's credentials. If they don’t have `CONNECT` permission, it will deny access.

---

### **6. Other Useful CMD and PowerShell Commands for IIS & SQL Server**

#### **a. IIS:**
- **Check the status of IIS:**
    ```cmd
    iisreset /status
    ```

- **View IIS Configuration:**
    ```cmd
    appcmd list config
    ```

#### **b. SQL Server:**
- **View SQL Server databases:**
    ```sql
    SELECT name FROM sys.databases;
    ```

- **Get the current SQL Server version:**
    ```sql
    SELECT @@VERSION;
    ```

####Knowing the difference between classic MVC and core MVC:

ASP.NET MVC (Classic): It doesn’t use Program.cs or Startup.cs for configuration. It relies on Global.asax and the Application_Start method for configuration, routing, etc.
Example (Global.asax):
csharp
Copy
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    RouteConfig.RegisterRoutes(RouteTable.Routes);
}
ASP.NET Core MVC: In an ASP.NET Core MVC app, you will have Program.cs and Startup.cs files that are responsible for configuring services and the application's request pipeline.
Example (Program.cs):
```csharp

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```
Framework:

ASP.NET MVC: It is built on the .NET Framework, which is Windows-only and does not support cross-platform development.
ASP.NET Core MVC: It is built on .NET Core or .NET 5/6+, which supports cross-platform development, meaning it can run on Windows, Linux, and macOS.
Use of appsettings.json:

ASP.NET MVC (Classic): It typically uses web.config for configuration, such as connection strings, app settings, etc.

ASP.NET Core MVC: It uses appsettings.json for configuration, along with appsettings.Development.json for environment-specific configurations.

Example (appsettings.json in ASP.NET Core MVC):

```json

{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  }
}
```
Web Server (Kestrel vs IIS):

ASP.NET MVC (Classic): Runs only on IIS and uses the IIS web server to serve the application.
ASP.NET Core MVC: Runs on Kestrel, which is the cross-platform web server built into .NET Core. IIS is still used, but it acts as a reverse proxy to Kestrel.
