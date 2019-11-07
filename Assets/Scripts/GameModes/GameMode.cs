using System;
using Player;
using UnityEngine;

namespace GameModes
{
	public abstract class GameMode : ScriptableObject
	{
		public abstract event EventHandler GameStartEvent;
		public abstract event EventHandler GameEndEvent;
		public abstract void StartGame();
		public abstract void Reset();
		public abstract int RegisterPlayer(IPlayer player);
		public abstract IPlayer GetPlayer(int playerID);
		public abstract bool IsGameActive();
		public abstract float GetRemainingTime();
		public abstract float GetCountDownTime();
		public abstract float GetMaxRageTime();
		public abstract float GetRageCooldown();
		public abstract float GetRageRestoreValue();
	}
}