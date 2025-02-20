# Sistema de Inventario

Este es un **sistema de inventario** desarrollado en **ASP.NET Core** con **Entity Framework Core**. Permite gestionar productos, categorías y mantener un historial de inventario con soporte para **soft deletes** y búsqueda por slugs en las URLs.

---

## Tabla de Contenidos
1. [Descripción General](#descripción-general)
2. [Características](#características)
3. [Instalación](#instalación)
4. [Uso](#uso)
5. [Estructura de Endpoints](#estructura-de-endpoints)
6. [Tecnologías Usadas](#tecnologías-usadas)
7. [Contribuciones](#contribuciones)

---

## Descripción General

Este sistema de inventario permite registrar, modificar y consultar productos en una base de datos utilizando ASP.NET Core. Se utilizan slugs para las URLs amigables y un soft delete para evitar la eliminación permanente de productos.

---

## Características

- **CRUD de Productos:** Crear, consultar, actualizar y eliminar (soft delete) productos.
- **Búsqueda por Slug:** Las URLs usan slugs para proteger datos sensibles y mejorar la seguridad.
- **Soft Delete:** Los productos no se eliminan físicamente de la base de datos, solo se marcan como inactivos.

---

## Instalación

### Dependencias:
Se requiere tener instalado el SDK de .NET 8 y `Pomelo.EntityFrameworkCore.MySql`. Se recomienda crear una base de datos llamada `Inventario` en MySQL antes de ejecutar la aplicación, ya que el ORM se encargará de mapear las tablas y restricciones.

### Comandos:
- **Compilar programa:** `dotnet build`
- **Correr programa:** `dotnet run`
- **Instalación de ORM:** `dotnet add package Pomelo.EntityFrameworkCore.MySql`
- **Añadir migración:** `dotnet ef migrations add NombreMigracion`
- **Actualizar base de datos y aplicar migraciones:** `dotnet ef database update`

**Aviso importante:**  
Los comandos proporcionados son exclusivos del CLI de Visual Studio Code. Asegúrate de estar en la carpeta raíz y luego ingresar a la carpeta `SistemaInventario` para ejecutar el programa.

---

## Uso

El programa cuenta con múltiples endpoints para gestionar productos, categorías y transacciones. A continuación se detallan los endpoints disponibles.

---

## Estructura de Endpoints

### **Endpoints para productos**

- **Listar todos los productos:**  
  `GET /productos`

- **Obtener información de un producto por slug:**  
  `GET /productos/{slug}`

- **Añadir un nuevo producto:**  
  `POST /productos`  
  Valida el producto antes de añadirlo para evitar duplicados.

- **Cambiar el estado de un producto:**  
  `PATCH /productos/{slug}/status`  
  Permite cambiar el estado del producto (activo/inactivo).

- **Entrada o salida de stock:**  
  `PATCH /productos/{slug}/stock`  
  Se requiere especificar la acción (`ENTRADA` o `SALIDA`) y la cantidad positiva.

### **Endpoints para categorías**

- **Listar todas las categorías:**  
  `GET /categorias`

- **Añadir una nueva categoría:**  
  `POST /categorias`  
  Valida la categoría para evitar duplicados.

### **Endpoints para transacciones**

- **Listar todas las transacciones:**  
  `GET /transacciones`

- **Obtener una transacción por ID:**  
  `GET /transacciones/{id}`

---

## Tecnologías Usadas

- **ASP.NET Core:** Framework para el desarrollo del backend.
- **Entity Framework Core:** ORM utilizado para interactuar con la base de datos.
- **MySQL:** Base de datos relacional.
- **Pomelo.EntityFrameworkCore.MySql:** Proveedor de MySQL para Entity Framework.

---

## Contribuciones

Si deseas contribuir al proyecto, por favor abre un issue o crea un pull request con tus sugerencias o mejoras.
