using System;
using System.Collections;
using System.Collections.Generic;
using Player;
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
		[SerializeField] private float maxRageTime;
		[SerializeField] private float rageCooldown;
		[SerializeField] private float pickupRageRestore;

		private bool isGameActive;
		private float remainingTime;

		public event EventHandler GameStartEvent;
		public event EventHandler GameEndEvent;

		private readonly List<IPlayer> players = new List<IPlayer>();

		private void Awake()
		{
			GameStateManager.ResetGame();
			GameStateManager.SetGame(this);
			DontDestroyOnLoad(gameObject);
		}

		public void StartGame()
		{
			StartCoroutine(UpdateGameState());
		}

		public void DestroyGame()
		{
			Destroy(gameObject);
		}

		private IEnumerator UpdateGameState()
		{
			if (this.isGameActive) yield break;
			this.isGameActive = true;
			GameStartEvent?.Invoke(this, null);

			this.remainingTime = this.maxTime;
			while (this.remainingTime > 0)
			{
				this.remainingTime -= Time.deltaTime;
				yield return null;
			}

			this.remainingTime = 0;
			this.isGameActive = false;
			GameEndEvent?.Invoke(this, null);
		}

		public int RegisterPlayer(IPlayer player)
		{
			int i = this.players.IndexOf(player);
			if (i > -1) return i;
			i = this.players.Count;
			this.players.Add(player);
			return i;
		}

		public IPlayer GetPlayer(int playerID) =>
			playerID < this.players.Count && playerID >= 0 ? this.players[playerID] : null;

		public bool IsGameActive() => this.isGameActive;

		public float GetRemainingTime() => this.remainingTime;

		public float GetCountDownTime() => this.countDown;

		public float GetMaxRageTime() => this.maxRageTime;

		public float GetRageCooldown() => this.rageCooldown;

		public float GetRageRestoreValue() => this.pickupRageRestore;
	}
}