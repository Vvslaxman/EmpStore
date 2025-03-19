

## + **1. Setting Up IIS and Configuring Properties (via CMD)**
### > **Install IIS using CMD:**
1. Open CMD as Administrator  
2. Install IIS:
```shell
dism /online /enable-feature /featurename:IIS-WebServer /all
```

3. Verify installation:
```shell
iisreset
```

---

### > **Enable Additional IIS Features:**
👉 **Common IIS features**:
| Feature | CMD Command |
|---------|-------------|
| Static Content | `dism /online /enable-feature /featurename:IIS-StaticContent` |
| Directory Browsing | `dism /online /enable-feature /featurename:IIS-DirectoryBrowsing` |
| ASP.NET 4.7 | `dism /online /enable-feature /featurename:IIS-ASPNET45` |
| CGI | `dism /online /enable-feature /featurename:IIS-CGI` |
| Logging | `dism /online /enable-feature /featurename:IIS-LoggingLibraries` |

---

### > **Start/Stop IIS:**
- Start IIS:
```shell
net start W3SVC
```
- Stop IIS:
```shell
net stop W3SVC
```
- Restart IIS:
```shell
iisreset
```

---

### > **List All IIS Configurations:**
```shell
appcmd list site
```

- Example output:
```shell
SITE "Default Web Site" (id:1,bindings:http/*:80:,state:Started)
```

---

### > **Create New IIS Website:**
```shell
appcmd add site /name:"MySite" /physicalPath:"C:\inetpub\wwwroot\MyApp" /bindings:http/*:8080:
```

---

### > **Set Application Pool for .NET Framework:**
1. Create an app pool:
```shell
appcmd add apppool /name:"MyAppPool" /managedRuntimeVersion:"v4.0"
```

2. Assign app pool to site:
```shell
appcmd set app /app.name:"MySite" /applicationPool:"MyAppPool"
```

---

### > **Check Status of IIS App Pool:**
```shell
appcmd list apppool
```

---

### > **Set Permissions for IIS User:**
Give full access to `IIS_IUSRS`:
```shell
icacls "C:\inetpub\wwwroot\MyApp" /grant IIS_IUSRS:(OI)(CI)F
```

---

## + **2. Publish and Build via PowerShell**
### > **Navigate to Project Folder:**
```shell
cd "C:\path\to\project"
```

---

### > **Build Solution (.sln):**
```shell
msbuild MyApp.sln /p:Configuration=Release
```

---

### > **Publish to IIS:**
```shell
dotnet publish -c Release -o "C:\inetpub\wwwroot\MyApp"
```

---

### > **Check Build Status:**
- Successful:
```shell
Build succeeded.
```
- If errors, add `-verbosity:diagnostic`:
```shell
msbuild MyApp.sln /p:Configuration=Release -verbosity:diagnostic
```

---

## + **3. DDL Backup and Restore (via SQL Server CMD)**
### > **Connect to SQL Server:**
```shell
sqlcmd -S .\sqlexpress -U sa -P YourStrongPassword
```

---

### > **Backup Database:**
```sql
BACKUP DATABASE EmpDb TO DISK = 'C:\Backups\EmpDb.bak' WITH FORMAT;
```

---

### > **Restore Database:**
```sql
RESTORE DATABASE EmpDb FROM DISK = 'C:\Backups\EmpDb.bak' WITH REPLACE;
```

---

### > **List All Databases:**
```sql
SELECT name FROM sys.databases;
```

---

### > **Check Backup History:**
```sql
SELECT * FROM msdb.dbo.backupset WHERE database_name = 'EmpDb';
```

---

## + **4. SQL Server User Roles**
### > **Create New Login (SQL Server Authentication):**
1. Login to SQL Server:
```shell
sqlcmd -S .\sqlexpress -U sa -P YourStrongPassword
```

2. Create User:
```sql
CREATE LOGIN testUser WITH PASSWORD = 'TestPassword123';
```

---

### > **Grant Database Access:**
```sql
USE EmpDb;
CREATE USER testUser FOR LOGIN testUser;
ALTER ROLE db_datareader ADD MEMBER testUser;
ALTER ROLE db_datawriter ADD MEMBER testUser;
```

---

### > **Test User Access (No Permission):**
```shell
sqlcmd -S .\sqlexpress -U testUser -P TestPassword123
```

- If it works, the user has access.  
- If denied, you'll get:
```
Login failed for user 'testUser'.
```

---

### > **Drop User:**
```sql
USE EmpDb;
DROP USER testUser;
DROP LOGIN testUser;
```

---

## + **5. Administrator and Local User Account Management**
### > **List Local Users:**
```shell
net user
```

---

### > **Create New Local User:**
```shell
net user TestUser P@ssw0rd /add
```

---

### > **Grant Admin Rights:**
```shell
net localgroup administrators TestUser /add
```

---

### > **Remove Admin Rights:**
```shell
net localgroup administrators TestUser /delete
```

---

###  **Delete User:**
```shell
net user TestUser /delete
```

---

### > **Enable/Disable User Account:**
- Disable:
```shell
net user TestUser /active:no
```
- Enable:
```shell
net user TestUser /active:yes
```

---

##  **6. Common IIS Configuration Options (Advanced)**
| Command | Purpose |
|---------|---------|
| `appcmd list site` | List all running sites |
| `appcmd list apppool` | List all app pools |
| `appcmd stop site /site.name:"MySite"` | Stop a website |
| `appcmd start site /site.name:"MySite"` | Start a website |
| `appcmd delete site /site.name:"MySite"` | Delete a site |
| `appcmd add apppool /name:"MyAppPool"` | Create new app pool |
| `appcmd set apppool /name:"MyAppPool" /state:Started` | Start an app pool |
| `appcmd recycle apppool /name:"MyAppPool"` | Recycle app pool |
| `appcmd list config` | List all IIS configurations |
| `appcmd set config /section:system.webServer/security/authentication/anonymousAuthentication /enabled:true` | Enable anonymous auth |

---

##  **7. Checking Status and Configurations**
### > **List IIS Features (Installed):**
```shell
dism /online /get-features | findstr "IIS"
```

---

### > **Check Current .NET Version Installed:**
```shell
dotnet --list-runtimes
```

---

### > **Check Running Ports:**
```shell
netstat -ano | findstr "8080"
```

