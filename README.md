# DustSuckerApi

## 🚀 Описание проекта
DustSuckerApi - это API для управления продажами пылесосов. Разработано с использованием **ASP.NET Core**, хранит данные в **SQLite** и поддерживает контейнеризацию через **Docker**.

## 🛠 Используемые технологии

![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

## 🗄 База данных
![Database Schema](https://raw.githubusercontent.com/Tonitruc/DustSuckerWebApp/master/DbSchema/db.png)

## 📦 Установка и запуск

### 1. Клонирование репозитория
```bash
git clone https://github.com/Tonitruc/DustSuckerWebApp.git
cd DustSuckerApi 
```

### 2. Запуск через докер
```bash
docker build -t dustsuckerapi .
docker run -p 5000:5000 dustsuckerapi
```

