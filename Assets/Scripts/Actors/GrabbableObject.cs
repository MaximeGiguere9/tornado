using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actors
{
	public class GrabbableObject : MonoBehaviour
	{
		private static List<GameObject> _objectMeshes;

		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private BoxCollider boxCollider;
		[SerializeField] private int pointValue;
		[SerializeField] private bool autoCalculateValue;
		[SerializeField] private float minScale = 1;
		[SerializeField] private float maxScale = 2;
		[SerializeField] private float releaseVelocity = 50;

		private Tornado source;

		private void Awake()
		{
			if(_objectMeshes == null)
				_objectMeshes = Resources.LoadAll<GameObject>("Prefabs").ToList();

			GameObject go = Instantiate(_objectMeshes[Random.Range(0, _objectMeshes.Count)], transform, false);
			Bounds bounds = go.GetComponentInChildren<Renderer>().bounds;

			this.boxCollider.center = transform.InverseTransformPoint(bounds.center);
			this.boxCollider.size = bounds.size;

			transform.localScale = Random.Range(this.minScale, this.maxScale) * Vector3.one;

			if (this.autoCalculateValue)
				this.pointValue = Mathf.CeilToInt(this.boxCollider.size.magnitude);
		}

		private void FixedUpdate()
		{
			if (this.source == null) return;
			
			float height = GetHashCode() % (this.source.GetHeight()/2) + (this.source.GetHeight()/2);
			float radius = GetHashCode() % (this.source.GetRadius()/4) + (this.source.GetRadius()*3/4);
			float rotationSpeed = GetHashCode() % 8 + 4;
			float angle = GetHashCode() % 2 * Mathf.PI;

			float offset = GetHashCode() + Time.fixedTime * rotationSpeed + angle;
			Vector3 localPos = new Vector3(radius * Mathf.Cos(offset), height, radius * Mathf.Sin(offset));
			transform.position = Vector3.Lerp(transform.position, this.source.GetPosition() + localPos, 0.15f);
		}

		public int GetPointValue() => this.pointValue;

		public bool IsGrabbed() => this.source != null;

		public void Grab(Tornado source)
		{
			if (this.source == source) return;
			if (this.source != null) Release();

			this.source = source;
			this.rigidbody.useGravity = false;
		}

		public void Release()
		{
			Vector3 direction = Vector3.Cross(transform.position - this.source.GetPosition(), Vector3.up).normalized;
			this.rigidbody.velocity = direction * this.releaseVelocity;
			this.rigidbody.useGravity = true;
			this.source = null;
		}
	}
}