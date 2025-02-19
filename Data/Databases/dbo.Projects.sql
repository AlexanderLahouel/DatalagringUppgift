CREATE TABLE [dbo].[Projects] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [ProjectNumber]    NVARCHAR (MAX) NOT NULL,
    [Name]             NVARCHAR (MAX) NOT NULL,
    [StartDate]        DATE           NOT NULL,
    [EndDate]          DATE           NOT NULL,
    [ProjectManagerId] INT            NOT NULL,
    [CustomerId]       INT            NOT NULL,
    [ServiceId]        INT            NOT NULL,
    [StatusTypeId]     INT            NOT NULL,
    [StatusTypeId1]    INT            NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Projects_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Projects_Employees_ProjectManagerId] FOREIGN KEY ([ProjectManagerId]) REFERENCES [dbo].[Employees] ([Id]),
    CONSTRAINT [FK_Projects_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]),
    CONSTRAINT [FK_Projects_StatusTypes_StatusTypeId] FOREIGN KEY ([StatusTypeId]) REFERENCES [dbo].[StatusTypes] ([Id]),
    CONSTRAINT [FK_Projects_StatusTypes_StatusTypeId1] FOREIGN KEY ([StatusTypeId1]) REFERENCES [dbo].[StatusTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Projects_CustomerId]
    ON [dbo].[Projects]([CustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Projects_ProjectManagerId]
    ON [dbo].[Projects]([ProjectManagerId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Projects_ServiceId]
    ON [dbo].[Projects]([ServiceId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Projects_StatusTypeId]
    ON [dbo].[Projects]([StatusTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Projects_StatusTypeId1]
    ON [dbo].[Projects]([StatusTypeId1] ASC);

