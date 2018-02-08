CREATE PROCEDURE [dbo].[prc_GetOfficialBuildDashboard]
AS
	DECLARE @now DATE = GETDATE();
	DECLARE @currYear INT = YEAR(@now);
	DECLARE @currMonth INT = MONTH(@now);
	DECLARE @prevMonthDate DATE = DATEADD(MONTH, -1, @now)

	DECLARE @prevMonthYear INT = YEAR(@prevMonthDate);
	DECLARE @prevMonthMonth INT = MONTH(@prevMonthDate);

	DECLARE @prevMonthStart DATE = DATEFROMPARTS(@prevMonthYear,@prevMonthMonth,1)
	
	DECLARE @prBuildBranch VARCHAR(32) = 'refs/pull/';

	;WITH AllBuild AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [AllBuildCount]
		FROM dbo.OfficialBuild
		WHERE ( CHARINDEX(@prBuildBranch, SourceBranch) = 0 OR CHARINDEX(@prBuildBranch, SourceBranch) IS NULL )
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	GoodBuild AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [FailedBuildCount]
		FROM dbo.OfficialBuild
		WHERE ( CHARINDEX(@prBuildBranch, SourceBranch) = 0 OR CHARINDEX(@prBuildBranch, SourceBranch) IS NULL )
			  AND Result = 0
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	CurrMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllBuildCount,
				B.FailedBuildCount
		FROM AllBuild AS A
			LEFT JOIN GoodBuild AS B ON A.Year = B.Year and A.Month = B.Month
		WHERE A.year = @currYear AND A.Month = @currMonth
	),
	PrevMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllBuildCount,
				B.FailedBuildCount
		FROM AllBuild AS A
			LEFT JOIN GoodBuild AS B ON A.Year = B.Year and A.Month = B.Month
		WHERE A.year = @prevMonthYear AND A.Month = @prevMonthMonth
	),
	OlderMonthAllBuildData AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [AllBuildCount]
		FROM dbo.OfficialBuild
		WHERE CreationDate < @prevMonthStart
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	OlderMonthGoodBuild AS
	(
		SELECT YEAR(CreationDate) AS [Year],
			   MONTH(CreationDate) AS [Month],
			   COUNT(*) AS [FailedBuildCount]
		FROM dbo.OfficialBuild
		WHERE CreationDate < @prevMonthStart
				AND Result = 0
		GROUP BY  YEAR(CreationDate), MONTH(CreationDate)
	),
	OlderMonthData AS
	(
		SELECT	A.Year, 
				A.Month,
				A.AllBuildCount,
				B.FailedBuildCount
		FROM OlderMonthAllBuildData AS A
			LEFT JOIN OlderMonthGoodBuild AS B ON A.Year = B.Year and A.Month = B.Month
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OlderMonthData UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevMonthData UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrMonthData 
GO
