# DIGITAL


create database DBEMPLEADOS

GO

USE DBEMPLEADOS

GO 

CREATE TABLE CARGO(
IdCargo int primary key identity(1,1),
Descripcion varchar(50)
)

go

create table EMPLEADO(
IdEmpleado int primary key identity(1,1),
NombreCompleto varchar(60),
Correo varchar(60),
Telefono varchar(60),
IdCargo int,
CONSTRAINT FK_Cargo FOREIGN KEY (IdCargo) REFERENCES CARGO(IdCargo)
)


INSERT INTO CARGO(Descripcion) VALUES
('Desarrollador'),
('Diseñador de marketing'),
('Ama de llaves')

GO

select * from CARGO

INSERT INTO EMPLEADO(NombreCompleto,Correo,Telefono,IdCargo) VALUES
('Homero Cabrera','homero@gmail.com','123123',1)

select * from EMPLEADO
