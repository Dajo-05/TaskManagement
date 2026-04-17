# Task Management API

Aplicación ASP.NET Core 8.0 para gestión de tareas y usuarios con SQL Server.

## 📋 Requisitos Previos

- **.NET 8.0 SDK** - [Descargar](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** - SQL Server Express o superior
- **Visual Studio 2022** (opcional, para GUI)
- **Visual Studio Code** (opcional, con extensión C#)
- **Git** (opcional, para clonar el repositorio)

### Verificar la Instalación

```bash
dotnet --version
```

## 🔧 Configuración de la Base de Datos

### Actualizar la Cadena de Conexión

Edita el archivo `appsettings.json` y actualiza la cadena de conexión según tu servidor SQL Server:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TaskDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**Ejemplos:**
- **Servidor local**: `Server=.;` o `Server=(localdb)\\mssqllocaldb;`
- **Servidor específico**: `Server=DAVID-PC;`
- **SQL Server Express**: `Server=.\\SQLEXPRESS;`

### Crear la Base de Datos (Migrations)

Las migraciones están incluidas en el proyecto. Solo necesitas aplicarlas:

#### Opción 1: Visual Studio (Package Manager Console)
```powershell
Update-Database
```

#### Opción 2: Línea de Comandos
```bash
dotnet ef database update
```

---

## ▶️ Ejecutar el Proyecto

### 🖱️ Opción 1: Visual Studio 2022

1. **Abrir el proyecto**
   - Abre `TaskManagement.sln` en Visual Studio 2022

2. **Restaurar dependencias**
   - Visual Studio lo hace automáticamente
   - Si no, ve a `Tools` → `NuGet Package Manager` → `Manage NuGet Packages for Solution` → Click en `Restore`

3. **Aplicar migraciones**
   - Abre `Tools` → `NuGet Package Manager` → `Package Manager Console`
   - Ejecuta: `Update-Database`

4. **Ejecutar la aplicación**
   - Presiona **F5** o click en el botón **Start** (▶️) en la barra de herramientas
   - La API se abrirá en `https://localhost:7175` con Swagger UI

5. **Acceder a Swagger**
   - Se abrirá automáticamente en `https://localhost:7175/swagger`
   - Aquí puedes probar todos los endpoints de la API

---

### 💻 Opción 2: Visual Studio Code

1. **Abrir el proyecto**
   ```bash
   code c:\Desarrollo\pruebas\tareas\TaskManagement
   ```

2. **Instalar extensiones necesarias**
   - Presiona `Ctrl+Shift+X` (Extensions)
   - Busca e instala:
     - **C# DevKit** (Microsoft)
     - **.NET Runtime Install Tool** (Microsoft)
     - **REST Client** (humao) - opcional, para probar la API

3. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

4. **Aplicar migraciones**
   ```bash
   dotnet ef database update
   ```

5. **Ejecutar el proyecto**
   ```bash
   dotnet run
   ```
   - O presiona `F5` si tienes C# DevKit instalado
   - La API estará disponible en `https://localhost:7175`

6. **Acceder a Swagger**
   - Abre en el navegador: `https://localhost:7175/swagger`

---

### 🖥️ Opción 3: Línea de Comandos (CMD/PowerShell)

1. **Navegar al directorio del proyecto**
   ```bash
   cd c:\Desarrollo\pruebas\tareas\TaskManagement\TaskManagement
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Compilar el proyecto**
   ```bash
   dotnet build
   ```

4. **Aplicar migraciones a la base de datos**
   ```bash
   dotnet ef database update
   ```

5. **Ejecutar el proyecto**
   ```bash
   dotnet run
   ```

   **Con un perfil específico:**
   ```bash
   dotnet run --launch-profile https
   ```
   o
   ```bash
   dotnet run --launch-profile http
   ```

6. **Acceder a la API**
   - **HTTPS**: `https://localhost:7175`
   - **HTTP**: `http://localhost:5111`
   - **Swagger UI**: `https://localhost:7175/swagger`

---

## 🚀 Endpoints Disponibles

### Usuarios (Users)
- `POST /api/users` - Crear usuario
- `GET /api/users` - Obtener todos los usuarios
- `GET /api/users/{id}` - Obtener usuario por ID

### Tareas (Tasks)
- `POST /api/tasks` - Crear tarea
- `GET /api/tasks` - Obtener todas las tareas
- `GET /api/tasks/{id}` - Obtener tarea por ID

---

## 📝 Ejemplos de Uso

### Crear un Usuario (POST)
```json
{
  "name": "Juan Pérez",
  "email": "juan@example.com"
}
```

### Crear una Tarea (POST)
```json
{
  "title": "string",
  "userId": 0,
  "extraData": "{ "Prioridad": "Alta", "FechaEntrega": "2024-12-31", "Etiquetas": "Trabajo en casa" }"
}
```

---

## 🔧 Comandos Útiles

### Dotnet CLI
```bash
# Compilar
dotnet build

# Ejecutar
dotnet run

# Restaurar dependencias
dotnet restore

# Limpiar artifacts
dotnet clean

# Ver proyectos disponibles
dotnet sln list

# Ver variables de entorno
dotnet run -- --help
```

### Entity Framework Core
```bash
# Crear una nueva migración
dotnet ef migrations add NombreMigracion

# Actualizar base de datos
dotnet ef database update

# Revertir última migración
dotnet ef database update NombreMigracionAnterior

# Ver migraciones
dotnet ef migrations list

# Remover última migración
dotnet ef migrations remove
```

---

## 🛠️ Solución de Problemas

### Error: "Database connection failed"
- Verifica que SQL Server esté ejecutándose
- Comprueba que la cadena de conexión en `appsettings.json` sea correcta
- Asegúrate que el nombre del servidor es accesible

### Error: "Migration not found"
```bash
# Actualiza la base de datos manualmente
dotnet ef database update
```

### Puerto ocupado
- El proyecto usa por defecto el puerto `7175` (HTTPS) y `5111` (HTTP)
- Puedes cambiarlos en `Properties/launchSettings.json`

### Cambiar perfil de ejecución
En el archivo `launchSettings.json` hay 3 perfiles disponibles:
- `http` - HTTP en puerto 5111
- `https` - HTTPS en puerto 7175
- `IIS Express` - Para IIS Express

---

## 📁 Estructura del Proyecto

```
TaskManagement/
├── Controllers/          # Controladores de API
│   ├── TasksController.cs
│   └── UsersController.cs
├── Entities/             # Modelos de base de datos
│   ├── TaskItem.cs
│   └── User.cs
├── DTOs/                 # Data Transfer Objects
│   ├── TaskDto.cs
│   └── UserDto.cs
├── Repositories/         # Patrón Repository
│   ├── IUserRepository.cs
│   └── UserRepository.cs
├── Services/             # Servicios de negocio
│   └── TaskService.cs
├── Migrations/           # Migraciones de Entity Framework
├── Properties/
│   ├── launchSettings.json
├── appsettings.json      # Configuración
├── Program.cs            # Punto de entrada
└── TaskManagement.csproj # Configuración del proyecto
```

---

## 🎯 Próximos Pasos

1. Ejecuta el proyecto siguiendo uno de los métodos anteriores
2. Accede a Swagger UI para probar los endpoints
3. Revisa los controladores para entender la lógica
4. Personaliza según tus necesidades

---

## 📞 Soporte

Para más información sobre ASP.NET Core:
- [Documentación oficial](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [OpenAPI/Swagger](https://swagger.io/)

---

**Última actualización:** 16 de abril de 2026