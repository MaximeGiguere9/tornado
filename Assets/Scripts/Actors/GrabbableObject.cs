using UnityEngine;

namespace Actors
{
	public class GrabbableObject : MonoBehaviour
	{
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private new Collider collider;
		[SerializeField] private int pointValue;
		[SerializeField] private bool autoCalculateValue;

		private int playerID;

		private void OnValidate()
		{
			if (this.autoCalculateValue)
				this.pointValue = Mathf.CeilToInt(collider.bounds.size.magnitude);
		}

		private void Start()
		{
			if(this.autoCalculateValue)
				this.pointValue = Mathf.CeilToInt(collider.bounds.size.magnitude);
		}

		public int GetPointValue() => this.pointValue;

		public bool IsGrabbed() => this.playerID > -1;

		public void Grab(int playerID)
		{
			if (this.playerID == playerID) return;
			if (this.playerID > -1) Release();

			this.playerID = playerID;
			this.rigidbody.useGravity = false;
		}

		public void Release()
		{
			this.playerID = -1;
			this.rigidbody.useGravity = true;
		}
	}
}