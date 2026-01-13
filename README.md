
# 🧩 WFM.EventIngestor.API

Proyecto en **.NET 8** que implementa un patrón clásico **Clean Architecture** (inspirado en Onion Architecture), con una separación clara de capas para mantener orden, mantenibilidad y escalabilidad.

---

## 📂 Estructura del Proyecto

src/
├── WFM.EventIngestor.API/ → Capa de Presentación (Controllers, DTOs, Middlewares)
├── WFM.EventIngestor.Application/ → Capa de Aplicación (Casos de uso, Interfaces, Validaciones)
├── WFM.EventIngestor.Domain/ → Capa de Dominio (Entidades, Value Objects, Interfaces)
├── WFM.EventIngestor.Infrastructure/→ Capa de Infraestructura (Persistencia, Integraciones)
└── WFM.EventIngestor.sln

---

## 🏗 Descripción de Capas

### 1. **Domain** (`WFM.EventIngestor.Domain`)
- Núcleo del negocio.
- Contiene **Entidades**, **Value Objects**, **Enums** y **Contratos (Interfaces)**.
- No depende de otras capas.

### 2. **Application** (`WFM.EventIngestor.Application`)
- Contiene **Casos de uso** (Commands/Queries) y la **lógica de orquestación**.
- Depende solo del **Domain**.
- Define interfaces para la infraestructura.

### 3. **Infrastructure** (`WFM.EventIngestor.Infrastructure`)
- Implementa las interfaces definidas en **Application**.
- Contiene persistencia (EF Core), integraciones externas y servicios técnicos.

### 4. **API** (`WFM.EventIngestor.API`)
- Punto de entrada HTTP.
- Contiene **Controllers**, **DTOs**, configuración de **Swagger/OpenAPI** y **DI**.

---

## 📜 Principios Clásicos Respetados
- **Separación estricta** de responsabilidades.
- **Dominio independiente** de frameworks.
- **Inyección de dependencias** para evitar acoplamiento.
- **Código limpio y mantenible**.

---

## 📊 Diagrama y creación de Arquitectura

```mermaid
graph TD;
    A[API] 
        --> B[Application];
    B --> C[Domain];
    B --> D[Infrastructure];
    D --> C;

1️⃣  Establecer referencias entre capas
mkdir WFM.EventIngestor
cd WFM.EventIngestor

2️⃣ Crear la solución
dotnet new sln -n WFM.EventIngestor

3️⃣ Crear los proyectos

# Capa de dominio
dotnet new classlib -n WFM.EventIngestor.Domain -f net8.0

# Capa de aplicación
dotnet new classlib -n WFM.EventIngestor.Application -f net8.0

# Capa de infraestructura
dotnet new classlib -n WFM.EventIngestor.Infrastructure -f net8.0

# Capa API
dotnet new webapi -n WFM.EventIngestor.API -f net8.0


4️⃣ Agregar proyectos a la solución

dotnet sln add WFM.EventIngestor.Domain/WFM.EventIngestor.Domain.csproj
dotnet sln add WFM.EventIngestor.Application/WFM.EventIngestor.Application.csproj
dotnet sln add WFM.EventIngestor.Infrastructure/WFM.EventIngestor.Infrastructure.csproj
dotnet sln add WFM.EventIngestor.API/WFM.EventIngestor.API.csproj

5️⃣ Establecer referencias entre capas

# Application depende de Domain
dotnet add WFM.EventIngestor.Application/WFM.EventIngestor.Application.csproj reference WFM.EventIngestor.Domain/WFM.EventIngestor.Domain.csproj

# Infrastructure depende de Application y Domain
dotnet add WFM.EventIngestor.Infrastructure/WFM.EventIngestor.Infrastructure.csproj reference WFM.EventIngestor.Application/WFM.EventIngestor.Application.csproj
dotnet add WFM.EventIngestor.Infrastructure/WFM.EventIngestor.Infrastructure.csproj reference WFM.EventIngestor.Domain/WFM.EventIngestor.Domain.csproj

# API depende de Application e Infrastructure
dotnet add WFM.EventIngestor.API/WFM.EventIngestor.API.csproj reference WFM.EventIngestor.Application/WFM.EventIngestor.Application.csproj
dotnet add WFM.EventIngestor.API/WFM.EventIngestor.API.csproj reference WFM.EventIngestor.Infrastructure/WFM.EventIngestor.Infrastructure.csproj

6️⃣ Establecer referencias entre capas

# Infrastructure
dotnet add package Microsoft.Extensions.Configuration.Abstractions --version 8.0.0
dotnet add package Oracle.ManagedDataAccess.Core --version 23.9.1
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions --version 8.0.0
dotnet add package Microsoft.Extensions.Logging.Abstractions --version 8.0.0

# Application
dotnet add package Newtonsoft.Json --version 13.0.3

# API   
dotnet add package AspNetCore.HealthChecks.Oracle
dotnet add package NLog --version 6.0.3


```

## 1. Configuración de Logging en Oracle

Para habilitar el registro de logs en una tabla de Oracle, primero debes crear la tabla, secuencia y trigger en el esquema de base de datos que vayas a utilizar:

```sql
CREATE TABLE ta_app_log (
    id NUMBER PRIMARY KEY,
    logged TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    log_level VARCHAR2(20),
    logger VARCHAR2(255),
    message VARCHAR2(4000),
    exception VARCHAR2(4000),
    callsite VARCHAR2(1000),
    thread VARCHAR2(100),
    username VARCHAR2(100),
    properties VARCHAR2(1000)
);

CREATE SEQUENCE seq_app_log
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

CREATE OR REPLACE TRIGGER ta_app_log_bi
BEFORE INSERT ON ta_app_log
FOR EACH ROW
BEGIN
    IF :NEW.id IS NULL THEN
        SELECT seq_app_log.NEXTVAL INTO :NEW.id FROM dual;
    END IF;
END;
/

CREATE OR REPLACE PACKAGE PKG_APP_LOG AS
  PROCEDURE INSERT_LOG(
    p_log_level  IN VARCHAR2,
    p_logger     IN VARCHAR2,
    p_message    IN VARCHAR2,
    p_exception  IN VARCHAR2 DEFAULT NULL,
    p_callsite   IN VARCHAR2 DEFAULT NULL,
    p_thread     IN VARCHAR2 DEFAULT NULL,
    p_username   IN VARCHAR2 DEFAULT NULL,
    p_properties IN VARCHAR2 DEFAULT NULL
  );
END PKG_APP_LOG;
/

CREATE OR REPLACE PACKAGE BODY PKG_APP_LOG AS
  PROCEDURE INSERT_LOG(
    p_log_level  IN VARCHAR2,
    p_logger     IN VARCHAR2,
    p_message    IN VARCHAR2,
    p_exception  IN VARCHAR2 DEFAULT NULL,
    p_callsite   IN VARCHAR2 DEFAULT NULL,
    p_thread     IN VARCHAR2 DEFAULT NULL,
    p_username   IN VARCHAR2 DEFAULT NULL,
    p_properties IN VARCHAR2 DEFAULT NULL
  ) IS
  BEGIN
    INSERT INTO ta_app_log (
      id,
      logged,
      log_level,
      logger,
      message,
      exception,
      callsite,
      thread,
      username,
      properties
    ) VALUES (
      seq_app_log.NEXTVAL,
      CURRENT_TIMESTAMP,
      p_log_level,
      p_logger,
      p_message,
      p_exception,
      p_callsite,
      p_thread,
      p_username,
      p_properties
    );
  END INSERT_LOG;
END PKG_APP_LOG;
/

```
.
prueba ejecutar







