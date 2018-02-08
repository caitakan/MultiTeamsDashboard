CREATE TYPE dbo.TestCoverageParamType AS TABLE 
(
	[VSO] varchar(32) NOT NULL,
	[Project] varchar(32) NOT NULL,
    [BuildId] INT NOT NULL,
	[ModuleName] varchar(256) NOT NULL,
	[LinesCovered] INT DEFAULT 0,
	[LinesNotCovered]  INT DEFAULT 0,

	PRIMARY KEY([BuildId], [ModuleName])
)

GO;

CREATE PROC [dbo].[prc_MergeTestCoverage]
(
	@newTestCoverageInfo AS dbo.TestCoverageParamType READONLY
)
AS
BEGIN
	MERGE dbo.TestCoverage AS TARGET  
    USING @newTestCoverageInfo AS SOURCE
        ON (TARGET.VSO = SOURCE.VSO AND TARGET.Project = SOURCE.Project AND TARGET.BuildId = SOURCE.BuildId AND TARGET.ModuleName LIKE SOURCE.ModuleName )  
    WHEN MATCHED THEN   
        UPDATE SET TARGET.LinesCovered = SOURCE.LinesCovered,
				   TARGET.LinesNotCovered = SOURCE.LinesNotCovered
    WHEN NOT MATCHED THEN  
        INSERT ([VSO], [Project], [BuildId], [ModuleName], [LinesCovered], [LinesNotCovered])  
        VALUES (SOURCE.VSO, SOURCE.Project, SOURCE.[BuildId], SOURCE.[ModuleName], SOURCE.[LinesCovered], SOURCE.[LinesNotCovered]);
END

GO;