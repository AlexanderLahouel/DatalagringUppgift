CREATE TABLE [dbo].[Employees] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL ,
    [FirstName]   NVARCHAR (50)  NOT NULL ,
    [LastName]    NVARCHAR (50)  NOT NULL ,
    [Email]       NVARCHAR (100) NULL,
    [PhoneNumber] NVARCHAR (20)  NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

