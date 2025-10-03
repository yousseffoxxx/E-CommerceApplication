# E-Commerce Talabat App

## 📝 Short Description
A straightforward **e-commerce application** built for ordering food and drinks online. This project demonstrates a clear, layered architecture using **ASP.NET Core** and implements key features necessary for a modern ordering platform.

---

## ✨ Key Features

* **Authentication & Authorization:** Secure user registration, login, and authorization using **JWT** (JSON Web Tokens).
* **Product Catalog:** Browse and view food and drink items.
* **Basket Management:** Add, update, and remove items from the shopping basket.
* **Order & Checkout:** Place orders with various delivery methods.
* **Payments:** Simulated or integrated payment processing via **Stripe**.

---

## 🛠️ Tech Stack & Architecture

### Backend
* **Language/Framework:** C# / **ASP.NET Core 8.0**
* **Architecture Style:** **Onion Architecture** (for robust separation of concerns and maintainability).
* **Database:** **SQL Server**

### Tools & Libraries
| Tool | Purpose |
| :--- | :--- |
| **Swagger** | API Documentation and testing UI. |
| **JWT** | Secure authentication and authorization. |
| **Stripe** | Payment gateway integration. |
| **AutoMapper** | Object-to-object mapping for clean data transfer between layers. |

### Layers / Projects
The solution is structured into distinct projects following the Onion Architecture principles:

1.  **Domain:** Contains core business entities and interfaces (the heart of the application).
2.  **Service Abstractions:** Defines interfaces for application services.
3.  **Services:** Implements the application's business logic.
4.  **Persistence:** Handles data access logic (e.g., Entity Framework Core setup).
5.  **Presentation (E-Commerce.Web):** The entry point, containing the API controllers and configuration.
6.  **WebApp:** (Placeholder for a potential frontend application, if applicable.)

---

## 🚀 Getting Started

### Prerequisites

To run this project locally, you must have the following installed:

* **.NET SDK:** Version **8.0** or higher.
* **SQL Server:** Version **[INSERT SQL SERVER VERSION HERE, e.g., 2019/2022]**
* *Optional:* A tool like **SQL Server Management Studio (SSMS)** or **Azure Data Studio** for database management.

### Running Locally

1.  **Clone the repository:**
    ```bash
    git clone [YOUR REPO URL HERE]
    cd E-Commerce.Web
    ```

2.  **Configure Database Connection:**
    Update the connection string in `appsettings.json` within the **Presentation** layer to point to your local SQL Server instance.

3.  **Run Migrations:**
    * Navigate to the **Persistence** project directory.
    * Apply the database migrations to create the schema:
        ```bash
        dotnet ef database update
        ```

4.  **Seed Initial Data (Optional):**
    * **[INSERT INSTRUCTION TO SEED DATA, e.g., Run a specific seed command or mention it runs automatically on startup.]**

5.  **Run the application:**
    * Navigate back to the **Presentation** project directory.
    * Start the application:
        ```bash
        dotnet run
        ```
    * The application should now be running, typically on `http://localhost:[PORT]`.

---

## 🔗 API Endpoints

The API is fully documented and testable using **Swagger UI**.

### Swagger URL
Once the application is running, access the documentation here:
`http://localhost:[PORT]/swagger`

### Example Endpoints

Based on the API specification:

| Layer | Method | Path | Description |
| :--- | :--- | :--- | :--- |
| **Authentication** | `POST` | `/api/authentication/register` | Create a new user account. |
| **Authentication** | `POST` | `/api/authentication/login` | Authenticate a user and receive a JWT. |
| **Products** | `GET` | `/api/products` | Retrieve a paginated list of all products. |
| **Products** | `GET` | `/api/products/{id}` | Retrieve details for a specific product. |
| **Basket** | `POST` | `/api/basket` | Add an item to the user's shopping basket. |
| **Basket** | `DELETE` | `/api/basket/{key}` | Remove a specific item from the basket. |
| **Orders** | `POST` | `/api/orders` | Submit a new order (Checkout). |
| **Orders** | `GET` | `/api/orders/{id}` | Retrieve details of a specific order. |

---

## 📸 Screenshots

### API Documentation (Swagger UI)

### Frontend / UI Placeholder

---

## 👤 Author

**Youssef Ahmed**
