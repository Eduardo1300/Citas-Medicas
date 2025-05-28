# 🩺 Sistema de Citas Médicas ASP.NET MVC

Este proyecto es una aplicación web desarrollada en Visual Studio 2022 utilizando ASP.NET MVC y SQL Server como base de datos. El sistema permite gestionar citas médicas de forma eficiente y segura mediante autenticación y control de roles.

---

## 🔑 Funcionalidades principales

- Registro y login de usuarios con validación de credenciales.
- Acceso restringido por roles (Administrador, Médico, Paciente).
- Gestión completa de citas médicas: crear, editar, eliminar y listar.
- Posibilidad de modificar información de usuarios y datos médicos.
- Interfaz amigable con validaciones de formularios en cliente y servidor.
- Conexión segura con base de datos SQL Server.
- Reportes y filtros integrados.

---

## ⚙️ Tecnologías utilizadas

- Visual Studio 2022
- ASP.NET MVC 5
- SQL Server
- Entity Framework
- Bootstrap
- Roles personalizados con filtros `[AuthorizeRoles]`

---

## 🛠️ Instrucciones para desarrolladores

### 🔌 Requisitos previos

- Tener instalado SQL Server y Visual Studio 2022 con ASP.NET MVC.
- Tener instalado `dotnet-ef` para generar modelos si es necesario.

### 📁 Clona este repositorio

```bash
git clone https://github.com/Eduardo1300/Citas-Medicas.git
