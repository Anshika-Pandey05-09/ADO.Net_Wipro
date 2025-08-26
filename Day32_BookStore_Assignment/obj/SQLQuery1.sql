CREATE DATABASE BookstoreDB;
GO

USE BookstoreDB;
GO

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Quantity INT NOT NULL
);

-- 1. Add Book
CREATE PROCEDURE sp_AddBook
    @Title NVARCHAR(100),
    @Author NVARCHAR(100),
    @Price DECIMAL(10,2),
    @Quantity INT
AS
BEGIN
    INSERT INTO Books (Title, Author, Price, Quantity)
    VALUES (@Title, @Author, @Price, @Quantity);
END
GO

-- 2. Update Book
CREATE PROCEDURE sp_UpdateBook
    @BookId INT,
    @Price DECIMAL(10,2),
    @Quantity INT
AS
BEGIN
    UPDATE Books
    SET Price = @Price, Quantity = @Quantity
    WHERE BookId = @BookId;
END
GO

-- 3. Delete Book
CREATE PROCEDURE sp_DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM Books WHERE BookId = @BookId;
END
GO