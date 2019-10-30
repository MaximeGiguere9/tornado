using GameModes;
using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Player binding that possesses a tornado
	/// </summary>
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField] private Tornado tornado;
		[SerializeField] private Color[] playerColors;

		private IGameMode gameMode;
		private int playerID;

		private float totalScore = 0;
		private float currentCombo = 0;
		private float currentMultiplier = 1;

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
			bool a = this.gameMode.IsGameActive();
			this.tornado.SetRageActive(a && Input.GetButton("Fire1"));
			this.tornado.SetMoveDirection(a ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) : Vector2.zero);
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
	}
}