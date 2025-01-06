# Real-time chat application with sentiment analysis

![Azure](https://img.shields.io/badge/Azure-%230072C6.svg?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-%230072C6.svg?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![Cognitive Services](https://img.shields.io/badge/Cognitive%20Services-%230072C6.svg?style=for-the-badge&logo=microsoft-azure&logoColor=white)

This project is a real-time chat application built using **ASP.NET Core** as the backend and **Blazor WebAssembly** as the frontend. It integrates **Azure SignalR Service** for real-time communication and includes an optional integration of **Azure Cognitive Services Text Analytics API** for sentiment analysis of chat messages. The application is deployed on **Azure Web App** and uses **Azure SQL Database** for data storage.

## Features

- **Real-time chat**: users can send and receive messages in real-time using Azure SignalR Service.
- **Sentiment Analysis**: optional integration with Azure Cognitive Services to analyze the sentiment of chat messages.
- **Data Storage**: chat messages and sentiment analysis results are stored in Azure SQL Database.
- **UI Enhancements**: messages are highlighted based on their sentiment (positive, negative, or neutral) using colors.

## Screenshot

![Chat Interface](https://github.com/user-attachments/assets/7aa2099c-6259-418d-8557-3e219169b0ba)
*Real-time chat interface with sentiment analysis results.*


## Prerequisites

Before you begin, ensure you have the following:

- **Azure Account**: You need an Azure account to create and manage Azure resources.
- **.NET SDK**: Install the .NET SDK from [here](https://dotnet.microsoft.com/download).

## Setup and deployment

### 1. Clone the Repository

```bash
git clone https://github.com/hrytsenkooo/SignalRChat.git
cd SignalRChat
```
### 2. Create Azure resources

- **Azure Web App**: create an Azure Web App for hosting the ASP.NET Core application.
- **Azure SignalR Service**: create a SignalR Service instance for real-time communication.
- **Azure SQL Database**: create an Azure SQL Database for storing chat messages and sentiment analysis results.
- **Azure Cognitive Services**: create a Text Analytics resource for sentiment analysis.

### 3. Configure the application

Update the appsettings.json or create .env (according to example.env) file with your Azure resource connection strings and keys. 

### 4. Database Migration

Run the following commands to apply database migrations.
```bash
dotnet ef database update
```

## How to use

- **Sign in**: Enter your username to join the chat.
- **Send messages**: type your message in the input box and press Send.
- **Data Storage**: chat messages and sentiment analysis results are stored in Azure SQL Database.
- **View sentiment**:  messages will be highlighted based on their sentiment (positive (green), negative (red), or neutral (gray)).

# Happy Chatting! 🚀

**[Link on website](https://chatsappui.azurewebsites.net)**
