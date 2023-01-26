CREATE TABLE [dbo].[BindingAuthentication]
(
    [ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [IsActive] BIT NOT NULL, 
    [IsVerified] BIT NOT NULL,
    [Binding.ID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_BindingAuthentication_ToBinding] FOREIGN KEY ([Binding.ID]) REFERENCES [Binding]([ID]) on delete cascade, 
)
