namespace GameModes
{
	public interface IGameMode
	{
		bool IsGameActive { get; }
		float RemainingTime { get; }
		float TotalScore { get; set; }
		float CurrentScore { get; set; }
		float CurrentCombo { get; set; }

		void StartGame();
	}
}