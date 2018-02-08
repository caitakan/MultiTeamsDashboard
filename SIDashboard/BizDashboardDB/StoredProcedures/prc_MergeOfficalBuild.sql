CREATE TYPE dbo.OfficialBuildParamType AS TABLE 
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
    [BuildId] INT NOT NULL PRIMARY KEY,	
	[Result] BIT DEFAULT 0,
	[SourceBranch] varchar(256),
	[CreationDate]  DATETIME NULL
)

GO;

CREATE PROC [dbo].[prc_MergeOfficialBuild]
(
	@newBuildInfo AS dbo.OfficialBuildParamType READONLY
)
AS
BEGIN
	MERGE dbo.OfficialBuild AS TARGET  
	USING @newBuildInfo AS SOURCE ON (TARGET.VSO = SOURCE.VSO AND TARGET.Project = SOURCE.Project AND TARGET.BuildId = SOURCE.BuildId)  
	WHEN MATCHED THEN   
	UPDATE SET	TARGET.Result = SOURCE.Result, 
				TARGET.CreationDate = SOURCE.CreationDate, 
				TARGET.SourceBranch = SOURCE.SourceBranch
	WHEN NOT MATCHED THEN  
		INSERT (VSO, Project, BuildId, Result, SourceBranch, CreationDate) 
		VALUES (SOURCE.VSO, SOURCE.Project, SOURCE.BuildId, SOURCE.Result, SOURCE.SourceBranch, SOURCE.CreationDate);
END

GO;