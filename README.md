Aquí tienes el contenido listo para copiar y pegar en tu archivo README.md. He estructurado la información de manera profesional, incluyendo una sección de Arquitectura que visualiza cómo interactúan los componentes de tu API.

Markdown

# 🚀 User & Task Management API (.NET 10)

Esta es una solución robusta para la gestión de usuarios y tareas, construida con un enfoque en **limpieza de código, escalabilidad y despliegue automatizado**. La API permite administrar el ciclo de vida completo de usuarios y sus tareas asociadas, cumpliendo con los estándares de la industria.

## 🛠️ Stack Tecnológico

* **Framework:** .NET 10 (C#)
* **Acceso a Datos:** Entity Framework Core (Enfoque Code First)
* **Base de Datos:** SQL Server 2022
* **Documentación:** Swagger (OpenAPI) con UI personalizada
* **Contenedorización:** Docker & Docker Compose

---

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

1.  **Docker Desktop** (incluyendo el plugin de Docker Compose).
2.  **Git** (para clonar el repositorio).
3.  *Opcional:* **Postman** o **Insomnia** (aunque puedes usar la interfaz de Swagger).

---

## 🏗️ Arquitectura del Sistema


* **Controllers:** Manejan las peticiones HTTP y la validación de entrada.
* **DTOs:** Objetos de transferencia para desacoplar la base de datos de la respuesta final.
* **Data Layer:** Contexto de EF Core con configuración de relaciones Fluent API.
* **Migrations:** Control de versiones de la base de datos automatizado.

---

## ⚡ Guía de Inicio Rápido (Docker)

No es necesario configurar SQL Server manualmente ni instalar el SDK de .NET. Todo el entorno se levanta con un solo comando.

1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/EnriqueAlfaro97/PruebaTecnicaRicardoAlfaro.git](https://github.com/EnriqueAlfaro97/PruebaTecnicaRicardoAlfaro.git)
    cd PruebaTecnicaRicardoAlfaro
    ```

2.  **Levantar la infraestructura:**
    Ejecuta el siguiente comando en la raíz del proyecto (donde está el `docker-compose.yml`):
    ```bash
    docker-compose up --build -d
    ```

3.  **Verificar el estado:**
    Una vez que los contenedores estén en ejecución, la API aplicará automáticamente las **migraciones de base de datos**.

4.  **Acceso a los servicios:**
    * **Documentación Swagger:** [http://localhost:5000/api-docs](http://localhost:5000/api-docs)
    * **API Base URL:** `http://localhost:5000/api`

---

## 📌 Endpoints Principales

### 👤 Usuarios
| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| **GET** | `/api/Usuarios` | Listar todos los usuarios. |
| **POST** | `/api/Usuarios` | Crear un nuevo usuario (Valida campos requeridos). |
| **GET** | `/api/Usuarios/{id}` | Detalle de un usuario específico. |
| **PUT** | `/api/Usuarios/{id}` | Actualización completa de datos. |
| **DELETE** | `/api/Usuarios/{id}` | Eliminación física del registro. |

### 📝 Tareas
| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| **POST** | `/api/Tareas` | Crear tarea vinculada a un `UsuarioId` existente. |
| **GET** | `/api/Tareas/usuario/{id}` | Filtrar tareas por ID de usuario. |
| **PATCH** | `/api/Tareas/{id}/estado` | Cambio parcial de estado (True/False). |
| **DELETE** | `/api/Tareas/{id}` | Eliminar una tarea específica. |

---

## ⚙️ Configuración Manual (Local)
Si decides ejecutarlo sin Docker:
1.  Asegúrate de tener un servidor de **SQL Server** corriendo.
2.  Actualiza la cadena de conexión en `appsettings.json`.
3.  Ejecuta `dotnet ef database update` en la terminal.
4.  Ejecuta `dotnet run`.

---

## ✍️ Autor
**Ricardo Alfaro** - Desarrollador .NET