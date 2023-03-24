# Study-Control-Software

This project was created to improve the skills of working with Angular and ASP.NET Core, and the main goal was to develop software for a "GlobalIT" startup.

# How to Install
**1. Open project [releases](https://github.com/davidkhachatryan123/Study-Control-Software/releases)**

**2. Download from the latest release of application:**

```
study-control-software_setup.exe
```

**3. Install EXE on Windows PC**

**4. Open the directory of the installed application located on the path:**

```
C:\Program Files (x86)\StudyControlSoftware
```

**5. In a text editor, open the file ***"appsettings.json"***, it should contain the following:**

```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=<server ip address>;database=study_control_software;uid=<username>;pwd=<password>"  // change this for your MySQL server settings
  },
  "DefaultUser": {
    "UserName": "<username>",               // default user name
    "Password": "<password>",               // default user password
    "Email": "xdavit7@gmail.com"            // default user email
  },
  "JwtConfig": {
    "validIssuer": "https://localhost:5000/",
    "validAudience": "https://localhost:5000/",
    "secret": "<JWT secret>",
    "expiresIn": 1440
  },
  "MailServer": {
    "From": "xdavit7@gmail.com",            // mail for sending messages to users
    "SmtpClient": "smtp.gmail.com",         // smtp client
    "Secret": ""                            // password for mail or secret key
  },
  "Client": {
    "LoginUrl": "http://localhost:4200/#/auth/login"  // login page url (full)
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**6. Change in this file ***MySQL*** connection string**

```
server=<server ip address>;database=study_control_software;uid=<username>;pwd=<password>
```

**7. Change ***"DefaultUser"*** setting**

```
"DefaultUser": {
    "UserName": "<username>",             // default user name
    "Password": "<password>",             // default user password
    "Email": "<default email for user>"   // default user email
}
```

> Note: The password must be **8 characters long** and contain at least **one uppercase and lowercase** letter, **number** and **symbol**

**8. **Mail server** configuration is a bit of a challenge, but I'm sure you can do it.**

```
"MailServer": {
    "From": "xdavit7@gmail.com",      // mail for sending messages to users
    "SmtpClient": "smtp.gmail.com",   // smtp client
    "Secret": ""                      // password for mail or secret key
}
```
> Note. For Gmail, you can generate a **"Secret"** using the following [resource](https://support.google.com/accounts/answer/185833?hl=en).

**9. Now we can run the main application by clicking in the installed ***EXE*** file**

**10. Go to follwing [link](http://localhost:5000/index.html) in your browser:**

```
http://localhost:5000/index.html
```

**11. Enjoy the Application 🎁🎉✨**
