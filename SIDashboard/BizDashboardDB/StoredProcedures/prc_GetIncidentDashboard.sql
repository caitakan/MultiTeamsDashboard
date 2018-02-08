
CREATE PROCEDURE [dbo].[prc_GetIncidentDashboard]
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
	DECLARE @numMonthInOldMonth FLOAT = DATEDIFF(MONTH, @minStart, @prevMonthStart);


	DECLARE @Tag VARCHAR(32) = 'Customer Incident';

	WITH OldIncident AS
	(
		SELECT CASE WHEN (@numMonthInOldMonth = 0 ) THEN 0 ELSE ( COUNT(*) / @numMonthInOldMonth ) END AS IncidentNum
		FROM dbo.VSOWorkItem			
		WHERE Tags = @Tag
			AND CreatedDate < @prevMonthStart
	),
	PrevIncident AS
	(
		SELECT COUNT(*) AS IncidentNum
		FROM dbo.VSOWorkItem		
		WHERE Tags = @Tag
			AND CreatedDate >= @prevMonthStart AND CreatedDate < @currMonthStart
	),
	CurrIncident AS
	(
		SELECT COUNT(*) AS IncidentNum
		FROM dbo.VSOWorkItem		
		WHERE Tags = @Tag
			AND CreatedDate >= @currMonthStart
	)
	SELECT 'N-2Month' AS DateLabel, * FROM OldIncident UNION
	SELECT 'PrevMonth' AS DateLabel, * FROM PrevIncident UNION 
	SELECT 'CurrMonth' AS DateLabel, * FROM CurrIncident 
GO