Create Database Day33_databaseFirstApproachDB

use Day33_databaseFirstApproachDB

create table Students
( StudentId int primary key,
name varchar(50),
email varchar(50)
)

create table Trainers
( TrainerId int primary key,
name varchar(50)
)

create table Courses
( CourseId int primary key,
Title varchar(50),
TrainerId int foreign key references Trainers(TrainerId)
)

INSERT INTO Trainers VALUES (1, 'Anshika');
INSERT INTO Students VALUES (1, 'Saloni', 'Saloni@email.com');
INSERT INTO Courses VALUES (1, 'Ado .net', 1);