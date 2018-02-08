CREATE FUNCTION dbo.fn_IsServerityA ( @VsoServerity VARCHAR(32) )
RETURNS BIT
AS
BEGIN
	IF @VsoServerity LIKE '1 - Critical' 
		RETURN 1
	
	IF @VsoServerity LIKE '2 - High' 
		RETURN 1

	RETURN 0
END
