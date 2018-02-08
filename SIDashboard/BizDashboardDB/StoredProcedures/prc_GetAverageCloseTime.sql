CREATE PROCEDURE [dbo].[prc_GetAverageCloseTime]
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

	DECLARE @Tag VARCHAR(32) = 'Customer Incident';

	WITH OldResolveTime AS
	(
		SELECT SUM ( dbo.GetTimeSpanInDay(CreatedDate, ClosedDate) ) / SUM( CASE WHEN ClosedDate IS NOT NULL THEN 1 ELSE 0 END ) AS AvgClosedTimeInDays
		FROM dbo.VSOWorkItem			
		WHERE Tags = @Tag
			AND CreatedDate < @prevMonthStart
	),
	PrevResolveTime AS
	(
		SELECT SUM ( dbo.GetTimeSpanInDay(CreatedDate, ClosedDate) ) / SUM( CASE WHEN ClosedDate IS NOT NULL THEN 1 ELSE 0 END ) AS AvgClosedTimeInDays
		FROM dbo.VSOWorkItem		
		WHERE Tags = @Tag
			AND CreatedDate >= @prevMonthStart AND CreatedDate < @currMonthStart
	),
	CurrResolveTime AS
	(
		SELECT SUM ( dbo.GetTimeSpanInDay(CreatedDate, ClosedDate) ) / SUM( CASE WHEN ClosedDate IS NOT NULL THEN 1 ELSE 0 END ) AS AvgClosedTimeInDays
		FROM dbo.VSOWorkItem	
		WHERE Tags = @Tag
			AND CreatedDate >= @currMonthStart
	)
	SELECT 'N-2Month' AS DateLabel, * FROM  OldResolveTime UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevResolveTime UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrResolveTime 
GO
