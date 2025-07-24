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

Prueba Unitaria
Pruebas unitarias básicas usando **xUnit** y **Moq**
Ejemplo: `EmployeesControllerTests`
- Verifica que el endpoint `GET /api/employees` devuelva un resultado `200 OK`.
- Usa `Moq` para simular dependencias (`IEmployeeRepository`, `IStoreRepository`, `IMapper`).
- Aísla la lógica del controlador para no depender de una base de datos real.

Ejecución
1. Abrir el **Test Explorer** (`Prueba > Ejecutar todas las pruebas`).