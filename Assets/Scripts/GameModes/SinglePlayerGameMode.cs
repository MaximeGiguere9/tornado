using System;
using System.Collections;
using System.Collections.Generic;
using Core.Initialization;
using Player;
using UnityEngine;

namespace GameModes
{
	/// <summary>
	/// Single player time-attack game mode tracks score for a unique player
	/// </summary>
	[CreateAssetMenu(fileName = "SinglePlayerGameMode", menuName = "Game Modes/Single Player")]
	public class SinglePlayerGameMode : GameMode
	{
		[SerializeField] private float countDown = 3;
		[SerializeField] private float maxTime = 60;
		[SerializeField] private float maxRageTime = 3;
		[SerializeField] private float rageCooldown = 2;
		[SerializeField] private float pickupRageRestore = 0.5f;

		private bool isGameActive;
		private float remainingTime;

		public override event EventHandler GameStartEvent;
		public override event EventHandler GameEndEvent;

		private readonly List<IPlayer> players = new List<IPlayer>();

		public override void Reset()
		{
			this.isGameActive = false;
			this.remainingTime = this.maxTime;
			this.players.Clear();
		}

		public override void StartGame() => MonoBehaviourHelper.Start(UpdateGameState());

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

		public override int RegisterPlayer(IPlayer player)
		{
			int i = this.players.IndexOf(player);
			if (i > -1) return i;
			i = this.players.Count;
			this.players.Add(player);
			return i;
		}

		public override IPlayer GetPlayer(int playerID) =>
			playerID < this.players.Count && playerID >= 0 ? this.players[playerID] : null;

		public override bool IsGameActive() => this.isGameActive;

		public override float GetRemainingTime() => this.remainingTime;

		public override float GetCountDownTime() => this.countDown;

		public override float GetMaxRageTime() => this.maxRageTime;

		public override float GetRageCooldown() => this.rageCooldown;

		public override float GetRageRestoreValue() => this.pickupRageRestore;
	}
}