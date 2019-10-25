using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Controls a tornado avatar (movements, rage, strength)
	/// </summary>
	public class Tornado : MonoBehaviour
	{
		[SerializeField] private ParticleSystem rageParticles;
		[SerializeField] private ParticleSystem cursorParticles;
		[SerializeField] private new Rigidbody rigidbody;
		[SerializeField] private Collider rangeTrigger;
		[SerializeField] private float turnRate;
		[SerializeField] private float maxSpeed;
		private Vector3 moveDirection;
		private bool rageActive = true;

		private void FixedUpdate()
		{
			this.rigidbody.AddForce(this.moveDirection * this.turnRate);
			this.rigidbody.velocity = Vector3.ClampMagnitude(this.rigidbody.velocity, this.maxSpeed);
		}

		public bool IsRageActive() => this.rageActive;

		public void SetRageActive(bool active)
		{
			if (this.rageActive == active) return;
			this.rageActive = active;

			if (active)
			{
				this.rageParticles.Play();
			}
			else
			{
				this.rageParticles.Stop();
				this.rageParticles.Clear();
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
	}
}

