CREATE TABLE [dbo].[Inventory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(20) NOT NULL,
	[Description] VARCHAR(50) NULL,
	[Price] FLOAT NOT NULL,
	[IsDeleted] BIT NOT NULL,
	[CreatedBy] INT NOT NULL,
	[CreatedOn] DATE NOT NULL,
	[ModifiedBy] INT NULL,
	[ModifiedOn] DATE NULL
)
