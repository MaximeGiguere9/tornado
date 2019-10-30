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
		[SerializeField] private Color playerColor;
		[SerializeField] private int playerID;

		private IGameMode gameMode;

		private void Awake()
		{
			this.gameMode = GameStateManager.GetCurrentGame();
			this.tornado.SetRageActive(false);
			this.tornado.SetCursorColor(this.playerColor);
			this.tornado.ObjectGrabbedEvent += OnObjectGrabbed;
			this.tornado.ObjectsReleasedEvent += OnObjectsReleased;
		}
		
		private void Update()
		{
			bool a = this.gameMode.IsGameActive;
			this.tornado.SetRageActive(a && Input.GetButton("Fire1"));
			this.tornado.SetMoveDirection(a ? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) : Vector2.zero);
		}

		private void OnDestroy()
		{
			this.tornado.ObjectGrabbedEvent -= OnObjectGrabbed;
			this.tornado.ObjectsReleasedEvent -= OnObjectsReleased;
		}

		public int GetPlayerID() => this.playerID;

		public int SetPlayerID(int playerID) => this.playerID = playerID;

		private void OnObjectGrabbed(object sender, GrabbableObject grabbable)
		{
			this.gameMode.CurrentScore += grabbable.GetPointValue();
			this.gameMode.CurrentCombo += 0.1f;
		}

		private void OnObjectsReleased(object sender, object args)
		{
			this.gameMode.TotalScore += this.gameMode.CurrentScore * this.gameMode.CurrentCombo;
			this.gameMode.CurrentScore = 0;
			this.gameMode.CurrentCombo = 1;
		}
	}
}