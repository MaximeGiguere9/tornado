using GameModes;
using UnityEngine;

namespace UI
{
	public class MainMenuView : MonoBehaviour
	{
		[SerializeField] private SinglePlayerGameMode gameMode;

		private void Update()
		{
			if (Input.GetButton("Fire")) BeginGame();
		}

		private void BeginGame()
		{
			this.gameMode.Reset();
			GameStateManager.SetGameMode(this.gameMode);
			GameStateManager.LoadGameScene();
		}
	}
}