# Gestion_Empleados
Sistema básico de gestión de empleados para una tienda
# Base de Datos
Configiración
1. Restaurar la base de datos ubicada en la carpeta Database
# Backend
Configuración
1. Clonar repositorio
2. Configurar connectionString en `appsettings.json`
3. Ejecutar migraciones: `Add-Migration InitialCreate -Project EmployeeManagement.Infrastructure -StartupProject EmployeeManagement.API`

Endpoints
* Autenticación (JWT)
- `POST /api/Auth/login`  
  Body: `{ "username": "admin", "password": "123" }`

* Empleados
- `GET /api/Employees`
- `GET /api/Employees/{"id"}`  
- `POST /api/Employees`  
  Body: `{ "firstName": "string","lastName": "string","email": "string","position": "string","hireDate": "DateTime","active": bool,"storeId": "StoreId" }`
- `PUT /api/Employees/{"id"}`  
  Body: `{ "firstName": "string","lastName": "string","email": "string","position": "string","hireDate": "DateTime","active": bool,"storeId": "StoreId" }`
- `DELETE /api/Employees/{"id"}` 

* Tiendas
- `GET /api/Stores`
- `GET /api/Stores/{"id"}`  
- `POST /api/Stores`  
  Body: `{ "name": "string", "address": "string", "active": bool }`
- `PUT /api/Stores/{"id"}`  
  Body: `{ "name": "string", "address": "string", "active": bool }`
- `DELETE /api/Stores/{"id"}` 