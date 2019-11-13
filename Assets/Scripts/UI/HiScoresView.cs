using GameModes;
using TMPro;
using UnityEngine;

namespace UI
{
	public class HiScoresView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textField;
		[SerializeField] private int maxScores;

		private void OnEnable()
		{
			int i = 0;
			this.textField.text = "";
			foreach (HiScoreEntry entry in GameStateManager.GetHiScores())
			{
				i++;
				if (i > maxScores) break;
				this.textField.text += $"{entry.Name}    {entry.Score:d5}\n";
			}

			while (i < maxScores)
			{
				this.textField.text += "AAA    00000\n";
				i++;
			}
		}

		private void Update()
		{
			if(Input.GetButtonDown("Fire")) GameStateManager.LoadMenuScene();
		}
	}
}