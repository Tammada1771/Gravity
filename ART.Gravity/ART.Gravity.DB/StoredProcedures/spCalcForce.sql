CREATE PROCEDURE [dbo].[spCalcForce]
	@MassOne float,
	@MassTwo float,
	@Distance float
AS
	DECLARE @Gravity AS decimal
	SET @Gravity = (6.674*POWER(10, -11))
	SELECT @Gravity * ((@MassOne * @MassTwo) / POWER(@Distance, 2)) AS ForceAns
RETURN 0
