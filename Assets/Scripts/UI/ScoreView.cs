using GameModes;
using TMPro;
using UnityEngine;

namespace UI
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI totalScoreField;
		[SerializeField] private TextMeshProUGUI currentScoreField;
		[SerializeField] private TextMeshProUGUI comboField;

		private IGameMode gameMode;

		private void Awake()
		{
			this.gameMode = GameStateManager.GetCurrentGame();
		}

		private void Update()
		{
			int current = Mathf.FloorToInt(this.gameMode.CurrentScore * this.gameMode.CurrentCombo);
			int total = Mathf.FloorToInt(this.gameMode.TotalScore);
			this.totalScoreField.text = $"Total : {total}";
			this.currentScoreField.text = $"Current : {current}";
			this.comboField.text = $"Combo x{this.gameMode.CurrentCombo:f1}!";
		}
	}
}