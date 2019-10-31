namespace GameModes
{
	public interface IPlayer
	{
		float GetRagePercentage();
		bool CanUseRage();
		int GetTotalScore();
		int GetCurrentCombo();
		float GetCurrentMultiplier();
	}
}