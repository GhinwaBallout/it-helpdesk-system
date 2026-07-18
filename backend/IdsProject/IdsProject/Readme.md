# IT Helpdesk System

This project is an IT Helpdesk System.  
For this task, I worked on the backend part of the project.

The backend is responsible for authentication, authorization, and database communication.

## Technologies Used

- ASP.NET Core Web API
- C#
- SQL Server
- Entity Framework Core
- JWT Authentication
- Swagger for API testing

## What I Did in This Task

In this part, I created the backend structure and connected it to SQL Server.

I implemented:

- Database connection
- User model
- Role model
- Password reset token model
- Password hashing
- Login API
- Forgot password API
- Reset password API
- JWT token generation
- Role-based authorization

## User Roles

The system has four roles:

- Admin
- Manager
- ITAgent
- Employee

After login, the backend checks the role of the user and gives access depending on the role.

## Authentication Idea

The user enters email/username and password.

The backend checks:

1. If the user exists
2. If the user is active
3. If the password is correct

The password is not stored directly in the database.  
It is stored as a hashed password for better security.

If the login is successful, the backend generates a JWT token.

## Login Rejection

Login is rejected if:

- The user does not exist
- The account is inactive
- The password is incorrect

## Database Tables

The database contains:

- Users
- Roles
- PasswordResetTokens

Each table has a primary key named `id`.

## API Endpoints

### Authentication APIs

```http
POST /api/auth/login
POST /api/auth/register
POST /api/auth/forgot-password
POST /api/auth/reset-password


## Swagger

I used Swagger to test the backend APIs.
Swagger is not the real frontend of the project.
