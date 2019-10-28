﻿using UnityEngine;

namespace Actors
{
	public class GrabbableObject : MonoBehaviour
	{
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private BoxCollider boxCollider;
		[SerializeField] private int pointValue;
		[SerializeField] private bool autoCalculateValue;

		private int playerID;

		private void Awake()
		{
			GameObject go = Instantiate(RandomObjectMeshManager.Instance.GetRandomObject(), transform, false);
			Bounds bounds = go.GetComponent<Renderer>().bounds;

			this.boxCollider.center = transform.InverseTransformPoint(bounds.center);
			this.boxCollider.size = bounds.size;

			transform.localScale = Random.Range(0.6f, 1.6f) * Vector3.one;

			if (this.autoCalculateValue)
				this.pointValue = Mathf.CeilToInt(this.boxCollider.size.magnitude);
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