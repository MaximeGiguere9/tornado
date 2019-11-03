using System.Collections;
using GameModes;
using TMPro;
using UnityEngine;

namespace UI
{
	public class CountDownView : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private TextMeshProUGUI countDownField;

		private IGameMode gameMode;

		private void Start()
		{
			this.gameMode = GameStateManager.GetCurrentGame();
			this.gameMode.GameEndEvent += OnGameEnd;
			StartCoroutine(CountDown());
		}

		private void OnGameEnd(object sender, object args)
		{
			StartCoroutine(ShowFinish());
		}

		private IEnumerator CountDown()
		{
			this.canvasGroup.alpha = 1;
			float count = this.gameMode.GetCountDownTime();
			while (count > 0)
			{
				this.countDownField.text = Mathf.CeilToInt(count).ToString();
				count -= Time.deltaTime;
				yield return null;
			}

			this.countDownField.text = "Go!";
			this.gameMode.StartGame();

			while (this.canvasGroup.alpha > 0)
			{
				this.canvasGroup.alpha -= Time.deltaTime;
				yield return null;
			}

		}

		private IEnumerator ShowFinish()
		{
			this.canvasGroup.alpha = 1;
			this.countDownField.text = "Finish!";
			yield return new WaitForSeconds(this.gameMode.GetCountDownTime());
			GameStateManager.EndGame();
		}

	}
}