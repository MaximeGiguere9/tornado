using GameModes;
using Player;
using TMPro;
using UnityEngine;

namespace UI
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI totalScoreField;
		[SerializeField] private TextMeshProUGUI currentScoreField;
		[SerializeField] private TextMeshProUGUI comboField;

		private IPlayer player;

		private void Start()
		{
			GameMode gameMode = GameStateManager.GetGameMode();
			this.player = gameMode.GetPlayer(0);
		}

		private void Update()
		{
			this.totalScoreField.text = $"Total : {this.player.GetTotalScore()}";
			this.currentScoreField.text = $"Current : {this.player.GetCurrentCombo()}";
			this.comboField.text = $"Combo x{this.player.GetCurrentMultiplier():f1}!";
		}
	}
}