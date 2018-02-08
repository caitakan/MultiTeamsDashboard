
CREATE TYPE dbo.VSOWorkItemParamType AS TABLE 
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

GO;

CREATE PROC [dbo].[prc_MergeVSOWorkItem]
(
	@newInfo AS dbo.VSOWorkItemParamType READONLY
)
AS
BEGIN
	MERGE dbo.[VSOWorkItem] AS TARGET  
	USING @newInfo AS SOURCE ON (TARGET.[Id] = SOURCE.[Id] AND TARGET.[VSO] = SOURCE.[VSO] AND TARGET.[Project] = SOURCE.[Project])  
	WHEN MATCHED THEN   
	UPDATE SET	TARGET.[Title] = SOURCE.[Title], 
				TARGET.[Description] = SOURCE.[Description], 
				TARGET.[State] = SOURCE.[State], 
				TARGET.[Priority] = SOURCE.[Priority], 
				TARGET.[Severity] = SOURCE.[Severity],
				TARGET.[CreatedDate] = SOURCE.[CreatedDate], 
				TARGET.[ResolvedDate] = SOURCE.[ResolvedDate], 
				TARGET.[ClosedDate] = SOURCE.[ClosedDate],
				TARGET.[Tags] = SOURCE.[Tags]
	WHEN NOT MATCHED THEN  
	INSERT ([VSO], [Project], [Id], [Title], [Description], [State], [Priority], [Severity], [CreatedDate], [ResolvedDate], [ClosedDate], [Tags])  
	VALUES (SOURCE.[VSO], SOURCE.[Project], SOURCE.[Id], SOURCE.[Title], SOURCE.[Description], SOURCE.[State], SOURCE.[Priority], SOURCE.[Severity], SOURCE.[CreatedDate], SOURCE.[ResolvedDate], SOURCE.[ClosedDate], SOURCE.[Tags]);

END

GO;