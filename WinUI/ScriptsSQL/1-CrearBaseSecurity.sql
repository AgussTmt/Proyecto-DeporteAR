IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SecurityDB')
BEGIN
    CREATE DATABASE [SecurityDB];
END;
GO