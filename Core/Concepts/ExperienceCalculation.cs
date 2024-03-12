namespace Core.Concepts;

public static class ExperienceCalculation
{
	public static double CalculateTotalEXP(double elevationGain, double distance, double duration, int count)
	{

		double elevation = elevationGain / count;
		double durationTrans = duration / count;
		double distanceTrans = distance / count;

		double totalExperience = (elevation * 0.5 + distanceTrans * 0.1 + durationTrans * 0.3) / count;
		// Scale the total experience to fit within the range 0 to 100000
		double scaledExperience = (totalExperience / 100) * 100000;
		return scaledExperience;
	}


}
