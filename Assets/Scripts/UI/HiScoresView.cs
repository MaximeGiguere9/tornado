using GameModes;
using System.Collections.Generic;
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
			foreach (KeyValuePair<string, int> kvp in GameStateManager.GetScores())
			{
				i++;
				if (i > maxScores) break;
				this.textField.text += $"{kvp.Key}    {kvp.Value:d5}\n";
			}

			while (i < maxScores)
			{
				this.textField.text += "AAA    00000\n";
				i++;
			}
		}

		private void Update()
		{
			if(Input.GetButtonDown("Fire1")) GameStateManager.LoadMenuScene();
		}
	}
}