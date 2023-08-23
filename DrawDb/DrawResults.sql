CREATE TABLE [dbo].[DrawResults]
(
  [Id] INT  IDENTITY(1,1) NOT NULL PRIMARY KEY,
  [User] NVARCHAR(50) NOT NULL,
  [DrawRequest] NTEXT NOT NULL,
  [DrawResults] NEXT NOT NULL
)
