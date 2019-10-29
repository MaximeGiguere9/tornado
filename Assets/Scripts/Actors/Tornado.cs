using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Controls a tornado avatar (movements, rage, strength, grab)
	/// </summary>
	public class Tornado : MonoBehaviour
	{
		[SerializeField] private ParticleSystem rageParticles;
		[SerializeField] private ParticleSystem cursorParticles;
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private CapsuleCollider rangeTrigger;
		[SerializeField] private float turnRate;
		[SerializeField] private float maxSpeed;

		private HashSet<GrabbableObject> orbitingObjects;
		private Vector3 moveDirection;
		private bool isRageActive = true;

		public event EventHandler<GrabbableObject> ObjectGrabbedEvent;
		public event EventHandler ObjectsReleasedEvent;

		private void Start()
		{
			this.orbitingObjects = new HashSet<GrabbableObject>();
		}

		private void FixedUpdate()
		{
			//move tornado
			this.rigidbody.AddForce(this.moveDirection * this.turnRate);
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.rigidbody.velocity, this.maxSpeed);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!this.isRageActive) return;

			//add object to tornado
			GrabbableObject grabbable = other.GetComponent<GrabbableObject>();
			if (grabbable == null) return;

			this.orbitingObjects.Add(grabbable);
			grabbable.Grab(this);
			ObjectGrabbedEvent?.Invoke(this, grabbable);
		}

		private void ReleaseOrbitingObjects()
		{
			foreach (GrabbableObject grabbable in this.orbitingObjects)
				grabbable.Release();
			this.orbitingObjects.Clear();
			ObjectsReleasedEvent?.Invoke(this, null);
		}

		public bool IsRageActive() => this.isRageActive;

		public void SetRageActive(bool active)
		{
			if (this.isRageActive == active) return;
			this.isRageActive = active;

			if (active)
			{
				this.rageParticles.Play();
			}
			else
			{
				this.rageParticles.Stop();
				this.rageParticles.Clear();
				ReleaseOrbitingObjects();
			}

			this.rangeTrigger.enabled = active;
		}

		public void SetCursorColor(Color color)
		{
			ParticleSystem.MainModule m = this.cursorParticles.main;
			m.startColor = color;
		}

		public void SetMoveDirection(Vector2 direction)
		{
			this.moveDirection = new Vector3(direction.x, 0, direction.y);
			this.moveDirection = this.moveDirection.normalized;
		}

		public float GetHeight() => this.rangeTrigger.height + this.rangeTrigger.center.y;

		public float GetRadius() => this.rangeTrigger.radius;

		public Vector3 GetPosition() => gameObject.transform.position;

	}
}

