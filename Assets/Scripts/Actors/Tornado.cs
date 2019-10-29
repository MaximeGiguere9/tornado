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
		[SerializeField] private Collider rangeTrigger;
		[SerializeField] private float turnRate;
		[SerializeField] private float maxSpeed;
		[SerializeField] private PlayerAvatar player;

		private HashSet<GrabbableObject> orbitingObjects;
		private Vector3 moveDirection;
		private bool isRageActive = true;

		private void Start()
		{
			this.orbitingObjects = new HashSet<GrabbableObject>();
		}

		private void FixedUpdate()
		{
			//move tornado
			this.rigidbody.AddForce(this.moveDirection * this.turnRate);
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.rigidbody.velocity, this.maxSpeed);

			//define orbit of each object randomly based on their hash code
			foreach (GameObject go in this.orbitingObjects.Select(grabbable => grabbable.gameObject))
			{
				float height = go.GetHashCode() % 200 + 100;
				float radius = go.GetHashCode() % 80 + 30;
				float rotationSpeed = go.GetHashCode() % 5 + 2;
				float angle = go.GetHashCode() % 2 * Mathf.PI;

				Vector3 localPos = new Vector3(
					radius * Mathf.Cos(Time.fixedTime * rotationSpeed + angle),
					height,
					radius * Mathf.Sin(Time.fixedTime * rotationSpeed + angle)
				);

				go.transform.position = Vector3.Lerp(
					go.transform.position,
					gameObject.transform.position + localPos,
					0.15f
				);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!this.isRageActive) return;

			//add object to tornado
			GrabbableObject grabbable = other.GetComponent<GrabbableObject>();
			if (grabbable == null) return;

			this.orbitingObjects.Add(grabbable);
			grabbable.Grab(this.player.GetPlayerID());
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

		private void ReleaseOrbitingObjects()
		{
			foreach (GrabbableObject grabbable in this.orbitingObjects)
				grabbable.Release();
			this.orbitingObjects.Clear();
		}
	}
}

