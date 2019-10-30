using UnityEngine;

namespace GameModes
{
	/// <summary>
	/// Handles game state (connected players and their scores, ranking, time, etc.)
	/// </summary>
	public static class GameStateManager
	{
		public static IGameMode GetCurrentGame() => GameObject.Find("GameMode").GetComponent<SinglePlayerGameMode>();
	}
}