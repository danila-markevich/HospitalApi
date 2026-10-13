# HospitalApi

REST API для больницы на ASP.NET Core Web API.

## Стек

- C# / .NET 10
- ASP.NET Core Web API — контроллеры, DI
- Entity Framework Core — ORM, миграции
- SQL Server — база данных
- Service Layer — бизнес-логика
- Global Exception Handling — middleware

## Функциональность

- CRUD для врачей, пациентов, приёмов
- Связь многие-ко-многим с дополнительным полем (Doctor ↔ Patient через Appointment)
- Количество приёмов у врача и пациента
- Сервисный слой — бизнес-логика отделена от контроллеров
- Глобальная обработка исключений через middleware
- DTO для запросов и ответов
- Валидация входных данных
- Swagger UI

## Эндпоинты

### Doctors

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/doctors | Все врачи с количеством приёмов |
| GET | /api/doctors/{id} | Врач по Id |
| POST | /api/doctors | Создать врача |
| PUT | /api/doctors/{id} | Обновить врача |
| DELETE | /api/doctors/{id} | Удалить врача |

### Patients

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/patients | Все пациенты с количеством приёмов |
| GET | /api/patients/{id} | Пациент по Id |
| POST | /api/patients | Создать пациента |
| PUT | /api/patients/{id} | Обновить пациента |
| DELETE | /api/patients/{id} | Удалить пациента |

### Appointments

| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/appointments | Все приёмы |
| GET | /api/appointments/{id} | Приём по Id |
| POST | /api/appointments | Создать приём |
| PUT | /api/appointments/{id} | Обновить приём |
| DELETE | /api/appointments/{id} | Удалить приём |
| GET | /api/appointments/doctor/{doctorId} | Приёмы врача |
| GET | /api/appointments/patient/{patientId} | Приёмы пациента |

## Архитектура

- **Models** — сущности БД (Doctor, Patient, Appointment)
- **DTOs** — контракты API
- **Data** — DbContext
- **Services** — бизнес-логика
- **Exceptions** — собственные исключения + middleware
- **Controllers** — обработка HTTP-запросов

## Как запустить

1. Установить .NET 10 SDK и SQL Server Express.
2. Клонировать репозиторий: git clone https://github.com/danila-markevich/HospitalApi.git
3. Настроить строку подключения в Program.cs.
4. Применить миграции: dotnet ef database update
5. Запустить: dotnet run
6. Открыть Swagger

## Автор

Danila Markevich — https://github.com/danila-markevich