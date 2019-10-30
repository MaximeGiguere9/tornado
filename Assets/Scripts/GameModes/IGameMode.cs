using Actors;

namespace GameModes
{
	public interface IGameMode
	{
		void StartGame();
		int RegisterPlayer(PlayerAvatar player);
		PlayerAvatar GetPlayer(int playerID);
		bool IsGameActive();
		float GetRemainingTime();
		float GetCountDownTime();
	}
}