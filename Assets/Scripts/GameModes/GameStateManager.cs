using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameModes
{
	/// <summary>
	/// Handles game state (connected players and their scores, ranking, time, etc.)
	/// </summary>
	public static class GameStateManager
	{
		private static HiScoresData _scores;
		private static GameMode _gameMode;

		private static void LoadHiScores() => _scores = PlayerPrefs.HasKey("HiScores")
			? JsonUtility.FromJson<HiScoresData>(PlayerPrefs.GetString("HiScores"))
			: new HiScoresData();

		private static void SaveHiScores()
		{
			string str = JsonUtility.ToJson(_scores);
			PlayerPrefs.SetString("HiScores", str);
		}

		public static void AddScore(string name, int score)
		{
			if (_scores == null) LoadHiScores();
			_scores?.Add(name, score);
			SaveHiScores();
		}

		public static HiScoresData GetHiScores() => _scores;

		public static GameMode GetGameMode() => _gameMode;

		public static void SetGameMode(GameMode gameMode) => _gameMode = gameMode;

		public static void LoadMenuScene() => SceneManager.LoadScene("Menu");

		public static void LoadGameScene() => SceneManager.LoadScene("Game");

		public static void LoadResultsScene() => SceneManager.LoadScene("Results");
	}
}