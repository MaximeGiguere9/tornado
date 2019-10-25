using System;
using System.Collections.Generic;
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

		private HashSet<GameObject> orbitingObjects;

		private void Start()
		{
			this.orbitingObjects = new HashSet<GameObject>();
		}

		private void Update()
		{
			if (!this.tornado.IsRageActive())
				ReleaseOrbitingObjects();
		}

		private void FixedUpdate()
		{
			foreach (GameObject go in this.orbitingObjects)
			{
				float height = go.GetHashCode() % 200 + 100;
				float angle = go.GetHashCode() % 2*Mathf.PI;

				Vector3 localPos = new Vector3(100 * Mathf.Cos(Time.fixedTime*2 + angle), height, 100 * Mathf.Sin(Time.fixedTime*2 + angle));

				go.transform.position = gameObject.transform.position + localPos;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!this.tornado.IsRageActive()) return;

			Debug.Log(other.gameObject.name);
			this.orbitingObjects.Add(other.gameObject);
		}

		private void ReleaseOrbitingObjects()
		{
			this.orbitingObjects.Clear();
		}
	}
}