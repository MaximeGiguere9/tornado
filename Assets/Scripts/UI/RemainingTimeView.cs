﻿using GameModes;
using TMPro;
using UnityEngine;

namespace UI
{
	public class RemainingTimeView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI timeField;

		private IGameMode gameMode;

		private void Start()
		{
			this.gameMode = GameStateManager.GetCurrentGame();
		}

		private void Update()
		{
			float time = this.gameMode.GetRemainingTime();
			int seconds = Mathf.FloorToInt(time % 60);
			int minutes = Mathf.FloorToInt(time / 60);
			int decimals = Mathf.FloorToInt((time * 100) % 100);
			this.timeField.text = $"{minutes:d2}:{seconds:d2}.{decimals:d2}";
		}
	}
}