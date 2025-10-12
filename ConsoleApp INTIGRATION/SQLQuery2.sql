USE Stored_Procedure_Assignment;

CREATE TABLE UsersLogin (

    UserId INT PRIMARY KEY IDENTITY,

    Username NVARCHAR(50) UNIQUE,

    Password NVARCHAR(50)

);

SELECT * FROM UsersLogin;

CREATE PROC usp_InsertUser
    @username NVARCHAR(50),
    @password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(SELECT 1 FROM UsersLogin WHERE Username = @username)
    BEGIN
        RAISERROR('UserName Already In use', 16,1)
        RETURN
    END
    INSERT INTO UsersLogin(Username, Password) VALUES(@username,@password);
END;

CREATE PROCEDURE usp_LoginUser
    @username NVARCHAR(50),
    @password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Username
    FROM UsersLogin
    WHERE Username = @username AND Password = @password;
END;

SELECT * FROM Students

CREATE PROCEDURE usp_CheckStudent
    @studentId INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Students WHERE StudentID = @studentId)
        RETURN 1;
    ELSE
        RETURN 0;
END;

SELECT * FROM Books

CREATE PROCEDURE usp_SearchBookById
    @BookId INT
AS
BEGIN
    SELECT BookId, Title, Author
    FROM Books
    WHERE BookId = @BookId;
END;


