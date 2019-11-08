using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameModes
{
	/// <summary>
	/// Handles game state (connected players and their scores, ranking, time, etc.)
	/// </summary>
	public static class GameStateManager
	{
		private static List<KeyValuePair<string, int>> _scores;

		private static GameMode _gameMode;

		private static void LoadHiScores() => _scores = PlayerPrefs.HasKey("HiScores")
			? JsonUtility.FromJson<List<KeyValuePair<string, int>>>(PlayerPrefs.GetString("HiScores"))
			: new List<KeyValuePair<string, int>>();

		private static void SaveHiScores() => PlayerPrefs.SetString("HiScores", JsonUtility.ToJson(_scores));

		public static void AddScore(string name, int score)
		{
			if (_scores == null) LoadHiScores();
			_scores?.Add(new KeyValuePair<string, int>(name, score));
			_scores = _scores?.OrderByDescending(s => s.Value).ToList();
			SaveHiScores();
		}

		public static IEnumerable<KeyValuePair<string, int>> GetScores() => _scores;

		public static GameMode GetGameMode() => _gameMode;

		public static void SetGameMode(GameMode gameMode) => _gameMode = gameMode;

		public static void LoadMenuScene() => SceneManager.LoadScene("Menu");

		public static void LoadGameScene() => SceneManager.LoadScene("Game");

		public static void LoadResultsScene() => SceneManager.LoadScene("Results");
	}
}