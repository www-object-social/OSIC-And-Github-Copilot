CREATE TABLE [dbo].[BindingSecurity]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Binding.ID] UNIQUEIDENTIFIER NOT NULL, 
    [Created] DATETIME NOT NULL, 
    CONSTRAINT [FK_BindingSecurity_ToBinding] FOREIGN KEY ([Binding.ID]) REFERENCES [Binding]([ID]) on delete cascade
)
