CREATE DATABASE SIMS_DB;
USE SIMS_DB;

CREATE TABLE Students(
    StudentID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50),
    Email NVARCHAR(100),
    Course INT REFERENCES Courses(CourseID)
);

CREATE TABLE Courses(
    CourseID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Credits INT
);

CREATE TABLE ExamResults(
    ResultID INT PRIMARY KEY IDENTITY,
    StudentID INT REFERENCES Students(StudentID),
    Course INT REFERENCES Courses(CourseID),
    Marks FLOAT
);


SELECT * FROM Students
SELECT * FROM Courses
SELECT * FROM ExamResults

INSERT INTO Students(Name, Email, Course) VALUES('test','test',1)