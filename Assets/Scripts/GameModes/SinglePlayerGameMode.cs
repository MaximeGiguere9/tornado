using System;
using System.Collections;
using UnityEngine;

namespace GameModes
{
	/// <summary>
	/// Single player time-attack game mode tracks score for a unique player
	/// </summary>
	public class SinglePlayerGameMode : MonoBehaviour, IGameMode
	{
		[SerializeField] private float countDown;
		[SerializeField] private float maxTime;

		public bool IsGameActive { get; private set; }
		public float RemainingTime { get; private set; }
		public float TotalScore { get; set; }
		public float CurrentScore { get; set; }
		public float CurrentCombo { get; set; }

		public event EventHandler GameStartEvent;
		public event EventHandler GameEndEvent;

		private void Start()
		{
			StartGame();
		}

		public void StartGame()
		{
			this.TotalScore = 0;
			this.CurrentScore = 0;
			this.CurrentCombo = 1;
			StartCoroutine(UpdateGameState());
		}

		private IEnumerator UpdateGameState()
		{
			if (this.IsGameActive) yield break;
			this.IsGameActive = true;

			float init = this.countDown;
			while (init > 0)
			{
				init -= Time.deltaTime;
				yield return null;
			}

			GameStartEvent?.Invoke(this, null);

			this.RemainingTime = this.maxTime;
			while (this.RemainingTime > 0)
			{
				this.RemainingTime -= Time.deltaTime;
				yield return null;
			}

			this.RemainingTime = 0;
			this.IsGameActive = false;
			GameEndEvent?.Invoke(this, null);
		}
	}
}