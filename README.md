
# 📦 StockManagementApi

API robusta de gestión de stock y marcas, desarrollada en **ASP.NET Core** con arquitectura **Clean + Onion**, diseñada para proyectos escalables y mantenibles. Incluye patrones modernos, caché distribuido, mensajería, seguridad y pruebas.

---

## 🚀 Características principales

- ✅ ASP.NET Core 8 con Clean Architecture + Onion
- ✅ CQRS + Mediator (MediatR)
- ✅ Entity Framework Core con SQL Server
- ✅ Redis Cache usando StackExchange.Redis
- ✅ JWT Authentication
- ✅ Validaciones con FluentValidation
- ✅ Manejo global de errores
- ✅ Unit Testing básico
- ✅ Logging con Serilog
- ✅ Swagger UI para pruebas
- ✅ Separación de capas: Application, Domain, Infrastructure, Persistence, Presentation

---

## 📁 Estructura del proyecto

```
Tests/StockManagement.UnitTest
├── Controllers # Prueba de los endpoints
├── Handlers #prueba de los handlers
├── Services #probando la logica de infraestructura 
├── Validators #probando las validaciones de entrada

src/
├── Application/        # Casos de uso, DTOs, CQRS, validaciones
├── Domain/             # Entidades y reglas de negocio
├── Infrastructure/     # Redis, logging, correo, mensajería, servicios externos
├── Persistence/        # EF Core, DbContext, migraciones
├── Presentation/       # Controllers, Middlewares, Swagger
├── Shared/             # Clases comunes (ej. Response<T>)
```

---

## 🛠️ Tecnologías usadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Redis (via StackExchange.Redis)
- MediatR (CQRS)
- FluentValidation
- Serilog
- AutoMapper
- xUnit / NUnit (testing)
- Swagger

---

## 🔐 Seguridad

- Autenticación vía **JWT**
- Validaciones centralizadas
- Control de errores global

---

## ⚙️ Configuración inicial

1. **Clona el repositorio**  
   ```bash
   git clone https://github.com/tu-usuario/StockManagementApi.git
   cd StockManagementApi
   ```

2. **Instala dependencias**  
   Asegúrate de tener Redis corriendo localmente o vía Docker:
   ```bash
   docker run -d -p 6379:6379 redis
   ```

3. **Actualiza el `appsettings.Development.json`**  
   Configura la conexión a SQL Server y Redis:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=StockDb;Trusted_Connection=True;"
   },
   "Redis": {
     "ConnectionString": "localhost:6379"
   },
   "JWTSettings": {
     "Key": "...",
     "Issuer": "...",
     "Audience": "...",
     "DurationInMinutes": 60
   }
   ```

4. **Aplica migraciones y ejecuta la API**
   ```bash
   dotnet ef database update
   dotnet run --project src/Presentation
   ```

---

## 🧪 Pruebas

Incluye pruebas básicas de unidades:
```bash
dotnet test
```

---

## 🧰 Endpoints principales

Usa Swagger para probar:
```
https://localhost:{puerto}/swagger
```

### Brands
- `GET /api/brands` → Listado paginado de marcas (con caché y paginacion)
- `GET /api/brands/{name}` → Buscar marca por nombre (con caché)
- `POST /api/brands` → Crear nueva marca

- `GET /api/brands` → Listado paginado de movimientos de inventario (con caché y paginacion)

- `GET /api/products` → Listado paginado de productos (con caché)
- `GET /api/brands/{name}` → Buscar producto por nombre (con caché)
- `POST /api/brands` → Crear nuevo producto

---

## 💡 Consideraciones

- Los datos de `Brand` incluyen productos asociados, pero al usar `BrandDto` se evita la sobrecarga de serialización cíclica.
- El caché está controlado manualmente por clave.
- La limpieza del caché se debe hacer si los datos cambian (ej. en un create/delete/update).

---

## 📬 Contacto

Proyecto desarrollado por **Raudy Lara V.**  
Si tienes dudas, puedes abrir un issue!

---
