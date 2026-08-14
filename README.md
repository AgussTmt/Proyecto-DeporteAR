# DeporteAR

DeporteAR es un sistema de gestión integral y alquiler de canchas deportivas desarrollado para entornos Windows. Esta aplicación centraliza, simplifica y automatiza las tareas operativas de un complejo deportivo, abarcando desde la reserva de turnos en un calendario hasta la administración completa de torneos, clientes, equipos y jugadores.

## Características Principales

* Calendario y Reservas: Visualización de turnos semanales codificados por color, gestión de reservas (libre, reservada, cancelada), registro de pagos y generación automática de horarios futuros.
* Gestión de Competiciones: Ciclo de vida completo de torneos y ligas. Permite crear competiciones, inscribir/desinscribir equipos, generar el fixture automáticamente (algoritmo Round Robin), cargar resultados de partidos y actualizar la tabla de posiciones en tiempo real.
* Módulos de Gestión (ABMs): 
  * Canchas: Configuración de precios, capacidad, duración de turnos y disponibilidad semanal.
  * Clientes: Agenda de contactos con validación de teléfono único.
  * Equipos y Jugadores: Armado de planteles, designación de capitanes y gestión de jugadores libres.
* Administración del Sistema: Herramientas de diagnóstico con visor de logs, configuración de correos de recuperación y utilidades para realizar Backup y Restore de las bases de datos.
* Control de Usuarios: Sistema de seguridad basado en roles (familias) y permisos individuales (patentes) para controlar el acceso a los módulos.
* Reportes: Generación de estadísticas de negocio, incluyendo ranking de clientes, listado de deudores y facturación por período.

## Requisitos del Sistema

* Sistema Operativo: Windows 10 o superior.
* Framework: .NET Framework versión 4.7.2 o superior.
* Base de Datos: Instancia de SQL Server 2014 o superior (Express, Standard, etc.) configurada obligatoriamente en "Modo de Autenticación Mixta".
* Permisos SQL: Un usuario de SQL Server (ej: `sa` u otro personalizado) que tenga el rol de servidor `dbcreator`, necesario para que el sistema construya la infraestructura de datos desde cero.
* Correo Electrónico (Opcional): Una cuenta de GMail con la verificación en 2 pasos activada y una "Contraseña de Aplicación" de 16 caracteres generada para el envío de códigos de recuperación de contraseña.

## Instalación y Configuración Inicial

1. Ejecutar el archivo `setup.exe` proporcionado y seguir las instrucciones del asistente para copiar los archivos.
2. Al ejecutar DeporteAR por primera vez, se iniciará el Asistente de Configuración Inicial.
3. En el asistente, ingresar los datos del Servidor SQL (Host), el Usuario SQL con permisos `dbcreator` y su respectiva Contraseña.
4. (Opcional) Ingresar la dirección de GMail y la Contraseña de Aplicación para habilitar la recuperación de cuentas.
5. Al hacer clic en "Guardar y Continuar", el sistema creará y poblará automáticamente las bases de datos requeridas (`SecurityDB` y `DeporteAR`).

## Acceso por Defecto

Una vez finalizada la configuración inicial, la aplicación se reiniciará y mostrará la pantalla de Login. Las credenciales del administrador por defecto son:
* Usuario: Admin
* Contraseña: Admin

---
Autor: Leandro Agustin Ramos Claus  
Institución: Colegio Leonardo Da Vinci (UAI)
