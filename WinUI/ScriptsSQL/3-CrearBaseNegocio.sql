IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DeporteAR')
BEGIN
    CREATE DATABASE [DeporteAR];
END;
GO