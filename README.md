# Study-Control-Software

This project was created to improve the skills of working with Angular and ASP.NET Core, and the main goal was to develop software for a **"Global IT"** startup.

## Install Study-Control-Software with docker

### Requirements

| Name   | Description |
| ------ | ----------- |
| Docker | Please, note that `docker-compose` is needed too and is included in the Docker Desktop installation. Docker Desktop is available for [Mac](https://docs.docker.com/desktop/install/mac-install/), [Windows](https://docs.docker.com/desktop/install/windows-install/) and [Linux](https://docs.docker.com/desktop/install/linux-install/). |

### Guide

1. Clone this repository

2. For application configuration open file named as ***.env*** in project directory

3. Change in this file **Default user configuration** section:

```
DefaultUser:UserName=admin            # required minimum length 5 symbols
DefaultUser:Password=x0aVMyf4@        # req. min length 8, one lowercase and uppercase character, one number and symbol
DefaultUser:Email=<email>             # set your own email for sending 2FA login codes from application
```

4. Also you need to configure the **Mail server configuration**:

```
MailServer:From=<email>               # application will send emails to users from this email
MailServer:SmtpClient=smtp.gmail.com  # smtp client for mail server
```

5. Create the new file named as **secrets.env** and put "Secret" for smtp client

```
MailServer__Secret=<secret or password> # set this value for your email
```

> Note. For Gmail doesn't work password, you can generate a **"Secret"** using the following [resource](https://support.google.com/accounts/answer/185833?hl=en).


6. Run in project directory the following command:

```
docker-compose up -d
```

7. Go to follwing [link](http://localhost) in your browser:

```
http://localhost
```

**Enjoy the Application 🎁🎉✨**