CREATE TABLE [dbo].[BindingConnection]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Binding.ID] UNIQUEIDENTIFIER NOT NULL, 
    [SignalrConnectionID] NVARCHAR(MAX) NOT NULL, 
    [ProjectSoftware] INT NOT NULL, 
    [MachineName] NVARCHAR(MAX) NOT NULL, 
    [IsDeveloper] BIT NOT NULL, 
    CONSTRAINT [FK_BindingConnection_ToBinding] FOREIGN KEY ([Binding.ID]) REFERENCES [Binding]([ID]) on delete cascade, 
)