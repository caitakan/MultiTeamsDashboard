CREATE TYPE dbo.TestRunParamType AS TABLE 
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
	[BuildOrReleaseId] INT NOT NULL,
	[RunId] INT NOT NULL,
	[PassedTestNum] INT DEFAULT 0,
	[TotalTestNum] INT DEFAULT 0,
	[CreationDate] DATETIME,	
	[TriggeredBy] VARCHAR(16)

	PRIMARY KEY([VSO], [Project], [BuildOrReleaseId], [RunId])
)

GO;

CREATE PROC [dbo].prc_MergeTestRun
(
	@newInfo AS dbo.TestRunParamType READONLY
)
AS
BEGIN
	MERGE dbo.TestRun AS TARGET  
    USING @newInfo AS SOURCE
        ON (TARGET.VSO = SOURCE.VSO AND TARGET.Project = SOURCE.Project AND TARGET.[BuildOrReleaseId] = SOURCE.[BuildOrReleaseId] AND TARGET.RunId LIKE SOURCE.RunId )  
    WHEN MATCHED THEN   
        UPDATE SET TARGET.[PassedTestNum] = SOURCE.[PassedTestNum],
				   TARGET.[TotalTestNum] = SOURCE.[TotalTestNum],
				   TARGET.[CreationDate] = SOURCE.[CreationDate],
				   TARGET.[TriggeredBy] = SOURCE.[TriggeredBy]
    WHEN NOT MATCHED THEN  
        INSERT ([VSO], [Project], [BuildOrReleaseId], [RunId], [PassedTestNum], [TotalTestNum], [CreationDate], [TriggeredBy])  
        VALUES (SOURCE.VSO, SOURCE.Project, SOURCE.[BuildOrReleaseId], SOURCE.[RunId], SOURCE.[PassedTestNum], SOURCE.[TotalTestNum], SOURCE.[CreationDate], SOURCE.[TriggeredBy]);
END

GO;