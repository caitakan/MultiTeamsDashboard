CREATE PROCEDURE [dbo].[prc_GetFunctionTestPassRate]
AS
	DECLARE @now DATE = GETDATE();
	DECLARE @currYear INT = YEAR(@now);
	DECLARE @currMonth INT = MONTH(@now);
	DECLARE @currMonthStart DATE = DATEFROMPARTS(@currYear, @currMonth, 1)
	DECLARE @numDaysInCurrMonth FLOAT = DATEDIFF(DAY, @currMonthStart, @now) + 1
	
	DECLARE @prevMonthDate DATE = DATEADD(MONTH, -1, @now)
	DECLARE @prevMonthYear INT = YEAR(@prevMonthDate);
	DECLARE @prevMonthMonth INT = MONTH(@prevMonthDate);
	DECLARE @prevMonthStart DATE = DATEFROMPARTS(@prevMonthYear,@prevMonthMonth,1)
	DECLARE @numDaysInPrevMonth FLOAT = DATEDIFF(DAY, @prevMonthStart, @currMonthStart)
		
	DECLARE @minStart DATE;
	SELECT @minStart = MIN(CreatedDate) FROM VSOWorkItem
	DECLARE @numDaysInOldMonth FLOAT = DATEDIFF(DAY, @minStart, @prevMonthStart);
	

	WITH OldFunctionTestPass AS
	(
		SELECT SUM( CASE WHEN [TotalTestNum] = 0 THEN 0 ELSE [PassedTestNum] / CAST([TotalTestNum] AS FLOAT) END ) / COUNT(*) As Passrate
		FROM dbo.TestRun			
		WHERE TriggeredBy = 'Release'
			AND CreationDate < @prevMonthStart
	),
	PrevFunctionTestPass AS
	(
		SELECT SUM( CASE WHEN [TotalTestNum] = 0 THEN 0 ELSE [PassedTestNum] / CAST([TotalTestNum] AS FLOAT) END ) / COUNT(*) As Passrate
		FROM dbo.TestRun			
		WHERE TriggeredBy = 'Release'
			AND CreationDate >= @prevMonthStart AND CreationDate < @currMonthStart
	),
	CurrFunctionTestPass AS
	(
		SELECT SUM( CASE WHEN [TotalTestNum] = 0 THEN 0 ELSE [PassedTestNum] / CAST([TotalTestNum] AS FLOAT) END ) / COUNT(*) As Passrate
		FROM dbo.TestRun			
		WHERE TriggeredBy = 'Release'
			AND CreationDate >= @currMonthStart
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OldFunctionTestPass UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevFunctionTestPass UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrFunctionTestPass 

GO