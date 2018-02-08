CREATE TABLE [dbo].[VSOWorkItem]
(
	[VSO] VARCHAR(64) NOT NULL,
	[Project] VARCHAR(64) NOT NULL,
	[Id] INT NOT NULL,
	[Title] VARCHAR(256),
	[Description] VARCHAR(MAX),
	[State] VARCHAR(16),
	[Priority] INT,
	[Severity] varchar(16),
	[CreatedDate] DATETIME,
	[ResolvedDate] DATETIME,
	[ClosedDate] DATETIME,
	[Tags] VARCHAR(256) NOT NULL,

	PRIMARY KEY([VSO], [Project], [Id])
)
