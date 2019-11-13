using GameModes;
using Player;
using System.Text;
using TMPro;
using UnityEngine;

namespace UI
{
	public class ResultsView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI resultsField;
		[SerializeField] private TextMeshProUGUI nameField;
		[SerializeField] private Color cursorColor;
		[SerializeField] private GameObject hiScoresView;

		private IPlayer player;
		private char[] playerName;
		private int pos;

		private void Awake()
		{
			this.player = GameStateManager.GetGameMode()?.GetPlayer(0);
			StringBuilder sb = new StringBuilder();
			sb.Append("Total Objects Grabbed : ")
				.Append(this.player?.GetTotalObjectsGrabbed() ?? 0)
				.Append("\nLargest Combo : ")
				.Append(this.player?.GetLargestObjectGrabCombo() ?? 0)
				.Append("\nTotal Score : ")
				.Append(this.player?.GetTotalScore() ?? 0);
			this.resultsField.text = sb.ToString();

			this.playerName = new[] { (char)65, (char)65, (char)65 };
			this.pos = 0;
		}

		private void Update()
		{
			if (Input.GetButtonDown("Horizontal"))
			{
				if (Input.GetAxis("Horizontal") > 0)
					MovePos(1);
				else if (Input.GetAxis("Horizontal") < 0)
					MovePos(-1);
			}

			if (Input.GetButtonDown("Vertical"))
			{
				if (Input.GetAxis("Vertical") > 0)
					IncrementChar(1);
				else if (Input.GetAxis("Vertical") < 0)
					IncrementChar(-1);
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("Enter Name : <mspace=26px>");
			for (int i = 0; i < this.playerName.Length; i++)
			{
				if (i == this.pos)
					sb.Append($"<u><color=#{ColorUtility.ToHtmlStringRGB(this.cursorColor)}>")
						.Append(this.playerName[i])
						.Append("</color></u>");
				else
					sb.Append(this.playerName[i]);
			}

			sb.Append("</mspace>");
			this.nameField.text = sb.ToString();

			if(Input.GetButtonDown("Fire")) SaveScore();
		}

		private void MovePos(int dir)
		{
			this.pos = Mathf.Clamp(this.pos + dir, 0, this.playerName.Length - 1);
		}

		private void IncrementChar(int dir)
		{
			int i = this.playerName[this.pos] + dir;

			if (i > 90) i = 65;
			if (i < 65) i = 90;

			this.playerName[this.pos] = (char) i;
		}

		private void SaveScore()
		{
			if (this.player == null)
			{
				Debug.LogWarning("Invalid game state.");
				return;
			}

			StringBuilder sb = new StringBuilder();
			foreach (char c in this.playerName)
				sb.Append(c);

			GameStateManager.AddScore(sb.ToString(), this.player.GetTotalScore());
			this.hiScoresView.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}