using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameModes
{
	[System.Serializable]
	public class HiScoresData : IEnumerable<HiScoreEntry>
	{
		public List<HiScoreEntry> HiScores;

		public HiScoresData()
		{
			this.HiScores = new List<HiScoreEntry>();
		}

		public void Add(string name, int score)
		{
			this.HiScores.Add(new HiScoreEntry(name, score));
			this.HiScores = this.HiScores.OrderByDescending(s => s.Score).ToList();
		}

		public IEnumerator<HiScoreEntry> GetEnumerator() => this.HiScores.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.HiScores.GetEnumerator();
	}

	[System.Serializable]
	public class HiScoreEntry
	{
		public string Name;
		public int Score;

		public HiScoreEntry(string name, int score)
		{
			this.Name = name;
			this.Score = score;
		}
	}
}