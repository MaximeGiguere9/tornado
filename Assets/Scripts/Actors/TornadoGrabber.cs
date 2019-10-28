using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Component that grabs objects and makes them orbit around the tornado
	/// </summary>
	public class TornadoGrabber : MonoBehaviour
	{
		[SerializeField] private Tornado tornado;
		[SerializeField] private PlayerAvatar player;

		private HashSet<GrabbableObject> orbitingObjects;

		private void Start()
		{
			this.orbitingObjects = new HashSet<GrabbableObject>();
		}

		private void Update()
		{
			if (!this.tornado.IsRageActive())
				ReleaseOrbitingObjects();
		}

		private void FixedUpdate()
		{
			foreach (GameObject go in this.orbitingObjects.Select(grabbable => grabbable.gameObject))
			{
				float height = go.GetHashCode() % 200 + 100;
				float radius = go.GetHashCode() % 80 + 30;
				float rotationSpeed = go.GetHashCode() % 5 + 2;
				float angle = go.GetHashCode() % 2*Mathf.PI;

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
			if (!this.tornado.IsRageActive()) return;

			GrabbableObject grabbable = other.GetComponent<GrabbableObject>();
			if (grabbable == null) return;

			this.orbitingObjects.Add(grabbable);
			grabbable.Grab(this.player.GetPlayerID());
		}

		private void ReleaseOrbitingObjects()
		{
			foreach (GrabbableObject grabbable in this.orbitingObjects)
				grabbable.Release();
			this.orbitingObjects.Clear();
		}
	}
}