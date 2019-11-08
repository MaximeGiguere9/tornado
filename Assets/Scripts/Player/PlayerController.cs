using GameModes;
using UnityEngine;

namespace Player
{
	public class PlayerController : IPlayer
	{
		private readonly int playerID;
		private readonly float maxRage;
		private float currentRage;
		private bool rageAvailable;
		private float totalScore;
		private float currentCombo;
		private float currentMultiplier;
		private int totalObjectsGrabbed;
		private int currentGrabCombo;
		private int largestGrabCombo;

		public PlayerController()
		{
			this.playerID = GameStateManager.GetGameMode().RegisterPlayer(this);
			this.maxRage = GameStateManager.GetGameMode().GetMaxRageTime();
		}

		public int GetPlayerID() => this.playerID;
		
		public void SetRage(float value) => this.currentRage = Mathf.Clamp(value, 0, this.maxRage);

		public void AddRage(float value) => this.currentRage = Mathf.Clamp(this.currentRage + value, 0, this.maxRage);

		public float GetRage() => this.currentRage;

		public float GetRagePercentage() => this.currentRage / this.maxRage;

		public bool GetRageAvailability() => this.rageAvailable;

		public bool SetRageAvailability(bool value) => this.rageAvailable = value;

		public int GetTotalScore() => Mathf.FloorToInt(this.totalScore + this.currentCombo * this.currentMultiplier);

		public int GetCurrentCombo() => Mathf.FloorToInt(this.currentCombo * this.currentMultiplier);

		public float GetCurrentMultiplier() => this.currentMultiplier;

		public int GetTotalObjectsGrabbed() => this.totalObjectsGrabbed;

		public int GetLargestObjectGrabCombo() => this.largestGrabCombo;

		public void AddToCombo(int score)
		{
			this.currentCombo += score;
			this.currentMultiplier += 0.1f;
			this.totalObjectsGrabbed += 1;
			this.currentGrabCombo += 1;
			if (this.currentGrabCombo > this.largestGrabCombo)
				this.largestGrabCombo = this.currentGrabCombo;
		}

		public void ResetCombo()
		{
			this.totalScore += this.currentCombo * this.currentMultiplier;
			this.currentCombo = 0;
			this.currentMultiplier = 1;
			this.currentGrabCombo = 0;
		}
	}
}