# MasterCompany
## Descripción
Este proyecto es la implementación de una API en .NET Core para gestionar información de empleados en el contexto de recursos humanos para MasterCompany. La API proporciona funcionalidades como la gestión de empleados, consultas salariales, ajustes salariales, estadísticas de género y más.

## Funcionalidades Principales
- **Obtener la Lista Completa de Empleados:**
  - Endpoint: `/Employee/GetAllEmployees`
  - Método: `GET`

- **Añadir un Nuevo Empleado:**
  - Endpoint: `/Employee/AddEmployee`
  - Método: `POST`
  - Cuerpo de la Solicitud: Objeto JSON representando al nuevo empleado.

- **Eliminar un Empleado:**
  - Endpoint: `/Employee/RemoveEmployee/{document}`
  - Método: `DELETE`
  - Parámetro de Ruta: `document` - Documento del empleado a eliminar.

- **Consultar Empleados por Rango Salarial:**
  - Endpoint: `/Employee/GetEmployeesBySalaryRange/{minSalary}/{maxSalary}`
  - Método: `GET`
  - Parámetros de Ruta: `minSalary` y `maxSalary` - Rango salarial.

- **Consultar Empleados sin Duplicados:**
  - Endpoint: `/Employee/GetNonDuplicateEmployees`
  - Método: `GET`

- **Calcular Ajustes Salariales:**
  - Endpoint: `/Employee/GetSalaryAdjustments`
  - Método: `GET`

- **Estadísticas de Género:**
  - Endpoint: `/Employee/GetGenderPercentages`
  - Método: `GET`

![image](https://github.com/IvnL23/MasterCompany/assets/98356366/7ec01b13-6492-4548-983c-3a6ae133681d)
![image](https://github.com/IvnL23/MasterCompany/assets/98356366/bf1bd5c5-bfb2-4b15-bdd3-07d84a8864ba)

