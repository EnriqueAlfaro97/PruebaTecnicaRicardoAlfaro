# Prueba Técnica: API de Gestión de Usuarios y Tareas (.NET 10)

Esta solución consiste en una API RESTful desarrollada para la gestión eficiente de usuarios y sus tareas asignadas. El proyecto ha sido diseñado siguiendo las mejores prácticas de desarrollo, modularidad y contenedorización solicitadas en los requerimientos.

## 🛠️ Especificaciones Técnicas
* **Framework:** .NET 10 (C#)
* **ORM:** Entity Framework Core (Code First)
* **Base de Datos:** SQL Server 2022
* **Documentación:** Swagger UI personalizado
* **Contenedores:** Docker & Docker Compose

## 🚀 Instrucciones de Ejecución (Docker)

El proyecto está configurado para ejecutarse completamente mediante contenedores, facilitando su despliegue en cualquier entorno.

1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/tu-usuario/tu-repositorio.git](https://github.com/tu-usuario/tu-repositorio.git)
    cd tu-repositorio
    ```
2.  **Levantar los servicios:**
    Desde la raíz del proyecto (donde se encuentra el archivo `docker-compose.yml`), ejecuta:
    ```bash
    docker-compose up --build -d
    ```
3.  **Acceso a los servicios:**
    * **API Base URL:** `http://localhost:5000`
    * **Documentación Swagger:** `http://localhost:5000/api-docs` (Requerimiento técnico cumplido)

> **Nota:** No es necesario configurar manualmente la base de datos. La API ejecuta automáticamente las migraciones pendientes al iniciar el contenedor.

## 📌 Requerimientos Funcionales Cumplidos

### Gestión de Usuarios
* **Crear usuario:** Endpoint POST con validación de datos.
* **Obtener detalle:** Endpoint GET por ID.
* **Actualizar usuario:** Endpoint PUT para modificación de datos existentes.
* **Eliminar usuario:** Endpoint DELETE de registros.

### Gestión de Tareas
* **Crear tarea:** Asociada a un `UsuarioId` existente.
* **Listar tareas por usuario:** Endpoint especializado para filtrar tareas por el ID de un usuario.
* **Actualizar estado:** Implementado mediante `PATCH /api/Tareas/{id}/estado`.
    * *Lógica de estados:* `True` (Activa/Pendiente), `False` (Completada/Terminada).
* **Eliminar tarea:** Endpoint DELETE funcional.

## 📂 Estructura del Código
* **/Controllers**: Controladores con inyección de dependencias y lógica asíncrona.
* **/DTOs**: Objetos de transferencia de datos para separar los modelos de BD de las entradas/salidas de la API.
* **/Models**: Entidades con configuración de relaciones uno a muchos (1:N).
* **/Data**: Contexto de base de datos (ApplicationDbContext).

## ✍️ Autor
**Ricardo Alfaro** - Desarrollador .NET