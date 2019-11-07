using GameModes;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class RageView : MonoBehaviour
	{
		[SerializeField] private Image meter;
		[SerializeField] private CanvasGroup canvasGroup;

		private IPlayer player;

		private void Start()
		{
			GameMode gameMode = GameStateManager.GetCurrentGame();
			this.player = gameMode.GetPlayer(0);
		}

		private void Update()
		{
			this.meter.fillAmount = this.player.GetRagePercentage();
			this.canvasGroup.alpha = this.player.GetRageAvailability() ? 1 : 0.5f;
		}
	}
}