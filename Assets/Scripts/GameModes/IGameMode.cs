namespace GameModes
{
	public interface IGameMode
	{
		void StartGame();
		int RegisterPlayer(IPlayer player);
		IPlayer GetPlayer(int playerID);
		bool IsGameActive();
		float GetRemainingTime();
		float GetCountDownTime();
	}
}