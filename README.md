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

![p1](https://user-images.githubusercontent.com/23020718/221434872-c1a7442f-a01f-40dc-abbd-7888e2685e3f.png)

![p2](https://user-images.githubusercontent.com/23020718/221434877-db279f6b-b494-44bc-a8fe-3748151cce1d.png)

![p3](https://user-images.githubusercontent.com/23020718/221434881-c69eb7b3-3582-45fe-a319-a155368aab95.png)

![p4](https://user-images.githubusercontent.com/23020718/221434887-534dcfd7-a4d7-431d-b8d1-413cfe28f202.png)
