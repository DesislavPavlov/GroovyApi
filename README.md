# 🎧 Groovy API  
> Backend API for the Groovy audio platform. Built with ASP.NET Core Web API & plain MySQL

![Swagger UI Preview](readme_resources/swagger_preview.gif)

---

## 🚀 Overview

The **Groovy API** powers the backend of the [Groovy web app](https://github.com/DesislavPavlov/Groovy), handling:

- 🔐 Authentication & User Management  
- 🎵 Audio Uploads & Metadata  
- ❤️ User Favourites 
- 📂 Image File Storage
- 📊 Full Swagger documentation for all endpoints  

This project exposes a **RESTful API** for use by frontend clients.

---

## 🛠️ Technologies Used

- ASP.NET Core 7 Web API  
- [Free SQL Database (MySQL)](https://www.freesqldatabase.com/)  
- [Swagger](https://swagger.io/) for API documentation  
- CORS configuration for frontend connectivity  

---

## 📦 Installation & Setup


> ⚠️ The application connects to a private hosted MySQL database using [FreeSQLDatabase.com](https://www.freesqldatabase.com/).  
> If you're interested in testing the API with live data, feel free to contact me for access to the credentials used in the [Groovy Demo](https://github.com/DesislavPavlov/Groovy).

### 🔧 Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet)
- [FreeSQLDatabase](https://www.mysql.com/downloads/)

### ⚙️ Setup Steps

1️⃣ **Clone the repository**  
```sh
git clone https://github.com/DesislavPavlov/GroovyApi.git
cd GroovyApi
```

2️⃣ **Create your appsettings.json**  

Since the file is gitignored, create a new one manually using the sample file:
```sh
cp appsettings.example.json appsettings.json
```
Update it with valid credentials:
```json
"StoredFilesPath": "Uploads",
"ConnectionStrings": {
  "DefaultConnection": "your-freesqlserver-connection-string"
},
"YouTube": {
  "ApiKey": "AIzaSyB7gUvF-zC7HzHGpf5RgHN-7w1c7bW_EiQ"
}
```

3️⃣ Run the application
```sh
dotnet run
```

Visit:  
📍 https://localhost:7021/swagger – Swagger UI  
📍 https://localhost:7021/api – Base API endpoint  

---

## 📌 API Endpoints
Here’s a high-level summary of available routes (see Swagger for full details):
| Feature            | Endpoint           |
| ------------------ | ------------------ |
| Authentication     | `/api/users/*`     |
| Song management    | `/api/songs/*`     |
| Genre management   | `/api/genres/*`     |
| Artist management  | `/api/artists/*`  |

---

## 📩 Contact
💡 **Developers:** Desislav Pavlov, Ivan Momchilov  
📧 **Email:** makotashako@gmail.com, vankomomchilov@gmail.com  
🐙 **GitHub:** [DesislavPavlov](https://github.com/DesislavPavlov), [IvanMomchilov123](https://github.com/IvanMomchilov123)  
🔗 **LinkedIn:** [Desislav Pavlov](https://www.linkedin.com/in/developer-d-pavlov/), [Ivan Momchilov](https://www.linkedin.com/in/ivan-momchilov-059a0236a/)  
