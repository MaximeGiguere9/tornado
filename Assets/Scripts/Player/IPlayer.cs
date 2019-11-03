namespace Player
{
	public interface IPlayer
	{
		int GetPlayerID();
		float GetRagePercentage();
		bool GetRageAvailability();
		int GetTotalScore();
		int GetCurrentCombo();
		float GetCurrentMultiplier();
		int GetTotalObjectsGrabbed();
	}
}