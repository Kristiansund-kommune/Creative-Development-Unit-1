namespace Core.Concepts;

public static class ExperienceCalculation
{
	private const double BaseExpPerMile = 10;
	private const double BaseExpPerMinute = 5;

	public static double CalculateTotalEXP(double elevationGain, double averageSpeed, double distance, double duration)
	{

		// Calculate elevation gain adjustment factor
		double elevationGainAdjustment = 1 + (elevationGain / 100.0 * 0.01);

		// Calculate average speed adjustment factor
		double averageSpeedAdjustment = 1;
		if (averageSpeed > 15)
		{
			averageSpeedAdjustment = 1.2;
		}

		// Calculate total EXP earned
		double totalExp = (BaseExpPerMile * distance * elevationGainAdjustment) +
		                  (BaseExpPerMinute * duration * averageSpeedAdjustment);

		return totalExp;
	}


}
