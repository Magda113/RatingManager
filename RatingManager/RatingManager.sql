create database RatingManager;
go

use RatingManager;

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Role INT NOT NULL,
	Department NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    ModifiedBy INT,
    ModifiedAt DATETIME,
);

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Status INT NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ModifiedBy INT,
    ModifiedAt DATETIME,
    
);

CREATE TABLE Ratings (
    RatingId INT IDENTITY(1,1) PRIMARY KEY,
    Status INT NOT NULL,
    CreatedBy INT NOT NULL,
    CreatedAt DATETIME NOT NULL,
    ModifiedBy INT,
    ModifiedAt DATETIME,
	CallId NVARCHAR(100) NOT NULL,
	UserId INT NOT NULL,
	Safety NVARCHAR(1000),
	Knowledge NVARCHAR(1000),
	Communication NVARCHAR(1000),
	Result INT NOT NULL,
	CategoryId INT NOT NULL,
    
);

GO;

insert into Users(UserName, Email, Role, Department, CreatedAt, PasswordHash)
    values
	('Jan Nowak', 'jan.nowak@bank.pl', 1, 'CC', getdate(), ''),
	('Andrzej Kowalski', 'andrzej.kowalski@bank.pl', 2, 'CC', getdate(), '');
	
   