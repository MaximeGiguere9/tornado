using Actors;
using GameModes;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Player binding that possesses a tornado
	/// </summary>
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField] private Tornado tornado;
		[SerializeField] private Color[] playerColors;

		private PlayerController controller;
		private GameMode gameMode;

		private float currentRageMeter;
		private float currentRageCooldown;
		private bool canUseRage;

		private void Start()
		{
			this.controller = new PlayerController();
			this.gameMode = GameStateManager.GetCurrentGame();
			this.tornado.SetRageActive(false);
			this.tornado.SetCursorColor(this.playerColors[this.controller.GetPlayerID()]);
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
					Mathf.Clamp(this.currentRageMeter + Time.deltaTime, 0, this.gameMode.GetMaxRageTime());
				this.currentRageCooldown =
					Mathf.Clamp(this.currentRageCooldown - Time.deltaTime, -1, this.currentRageCooldown);
			}

			bool a = this.gameMode.IsGameActive();
			bool b = this.currentRageMeter > 0;
			bool c = this.currentRageCooldown <= 0;

			this.canUseRage = a && b && c;

			bool d = this.canUseRage && Input.GetButton("Fire1");

			if (this.tornado.IsRageActive() && !d)
				this.currentRageCooldown = this.gameMode.GetRageCooldown();

			this.tornado.SetRageActive(d);

			this.controller.SetRageAvailability(this.canUseRage);
			this.controller.SetRage(this.currentRageMeter);

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
			this.controller.AddToCombo(grabbable.GetPointValue());
			this.currentRageMeter = Mathf.Clamp(this.currentRageMeter + this.gameMode.GetRageRestoreValue(), 0, this.gameMode.GetMaxRageTime());
		}

		private void OnObjectsReleased(object sender, object args)
		{
			this.controller.ResetCombo();
		}
	}
}