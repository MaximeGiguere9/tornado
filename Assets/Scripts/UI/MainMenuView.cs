using GameModes;
using UnityEngine;

namespace UI
{
	public class MainMenuView : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetButton("Fire1")) GameStateManager.BeginGame();
		}
	}
}