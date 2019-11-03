using System;
using Player;

namespace GameModes
{
	public interface IGameMode
	{
		event EventHandler GameStartEvent;
		event EventHandler GameEndEvent;
		void StartGame();
		void DestroyGame();
		int RegisterPlayer(IPlayer player);
		IPlayer GetPlayer(int playerID);
		bool IsGameActive();
		float GetRemainingTime();
		float GetCountDownTime();
		float GetMaxRageTime();
		float GetRageCooldown();
		float GetRageRestoreValue();
	}
}