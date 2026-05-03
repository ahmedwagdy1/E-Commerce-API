# 🛒 E-Commerce API

A RESTful API for an E-Commerce platform built with **ASP.NET Core**,
covering all essential e-commerce operations.

---

## 🚀 Features

- 🔐 Authentication & Authorization (JWT)
- 📦 Products management (Add, Edit, Delete, View)
- 🗂️ Categories management
- 🛍️ Orders management
- ⭐ Reviews & Ratings

---

## 🛠️ Tech Stack

| Layer      | Technology              |
|------------|--------------------------|
| Framework  | ASP.NET Core Web API     |
| Language   | C#                       |
| Database   | SQL Server               |
| ORM        | Entity Framework Core    |
| Auth       | JWT Bearer Token         |

---

## 📡 API Endpoints

### 🔐 Auth
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login & get token |

### 📦 Products
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/products` | Get all products |
| GET | `/api/products/{id}` | Get product by ID |
| POST | `/api/products` | Create product |
| PUT | `/api/products/{id}` | Update product |
| DELETE | `/api/products/{id}` | Delete product |

### 🗂️ Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories |
| POST | `/api/categories` | Create category |

### 🛍️ Orders
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/orders` | Get all orders |
| POST | `/api/orders` | Create order |

### ⭐ Reviews
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/reviews/{productId}` | Get product reviews |
| POST | `/api/reviews` | Add review |

---
