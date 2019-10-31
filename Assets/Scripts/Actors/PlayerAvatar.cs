using GameModes;
using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Player binding that possesses a tornado
	/// </summary>
	public class PlayerAvatar : MonoBehaviour, IPlayer
	{
		[SerializeField] private Tornado tornado;
		[SerializeField] private Color[] playerColors;
		[SerializeField] private float maxRageTime = 3;
		[SerializeField] private float rageCooldown = 2;
		[SerializeField] private float pickupRestoreValue = 0.5f;

		private IGameMode gameMode;
		private int playerID;

		private float totalScore = 0;
		private float currentCombo = 0;
		private float currentMultiplier = 1;

		private float currentRageMeter;
		private float currentRageCooldown;
		private bool canUseRage;

		private void Awake()
		{
			this.gameMode = GameStateManager.GetCurrentGame();
			this.playerID = this.gameMode.RegisterPlayer(this);
			this.tornado.SetRageActive(false);
			this.tornado.SetCursorColor(this.playerColors[this.playerID]);
			this.tornado.ObjectGrabbedEvent += OnObjectGrabbed;
			this.tornado.ObjectsReleasedEvent += OnObjectsReleased;
		}
		
		private void Update()
		{
			if (this.tornado.IsRageActive())
			{
				this.currentRageMeter -= Time.deltaTime;
			}
			else
			{
				this.currentRageMeter =
					Mathf.Clamp(this.currentRageMeter + Time.deltaTime, 0, this.maxRageTime);
				this.currentRageCooldown =
					Mathf.Clamp(this.currentRageCooldown - Time.deltaTime, -1, this.currentRageCooldown);
			}

			SetRageState();

			this.tornado.SetMoveDirection(this.gameMode.IsGameActive()
				? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
				: Vector2.zero);
		}

		private void OnDestroy()
		{
			this.tornado.ObjectGrabbedEvent -= OnObjectGrabbed;
			this.tornado.ObjectsReleasedEvent -= OnObjectsReleased;
		}

		private void OnObjectGrabbed(object sender, GrabbableObject grabbable)
		{
			this.currentCombo += grabbable.GetPointValue();
			this.currentMultiplier += 0.1f;
			this.currentRageMeter = Mathf.Clamp(this.currentRageMeter + this.pickupRestoreValue, 0, this.maxRageTime);
		}

		private void OnObjectsReleased(object sender, object args)
		{
			this.totalScore += this.currentCombo * this.currentMultiplier;
			this.currentCombo = 0;
			this.currentMultiplier = 1;
		}

		public int GetPlayerID() => this.playerID;

		public int GetTotalScore() => Mathf.FloorToInt(this.totalScore + this.currentCombo * this.currentMultiplier);

		public int GetCurrentCombo() => Mathf.FloorToInt(this.currentCombo * this.currentMultiplier);

		public float GetCurrentMultiplier() => this.currentMultiplier;

		private void SetRageState()
		{
			bool a = this.gameMode.IsGameActive();
			bool b = this.currentRageMeter > 0;
			bool c = this.currentRageCooldown <= 0;

			this.canUseRage = a && b && c;

			bool d = this.canUseRage && Input.GetButton("Fire1");

			if (this.tornado.IsRageActive() && !d)
				this.currentRageCooldown = this.rageCooldown;

			this.tornado.SetRageActive(d);
		}

		public float GetRagePercentage() => this.currentRageMeter / this.maxRageTime;

		public bool CanUseRage() => this.canUseRage;
	}
}