using System.Collections;
using UnityEngine;

namespace UI
{
	public class MenuSpawner : MonoBehaviour
	{
		[SerializeField] private GameObject grabbableObjectPrefab;
		[SerializeField] private Vector3[] objectSpawnPositions;
		[SerializeField] private Vector3[] objectVelocities;
		[SerializeField] private float objectSpawnInterval;
		[SerializeField] private float objectLifeTime;

		private void OnDrawGizmos()
		{
			if (this.objectSpawnPositions == null) return;
			foreach (Vector3 v in this.objectSpawnPositions)
				Gizmos.DrawWireSphere(v, 1);
		}

		private void Awake()
		{
			InvokeRepeating(nameof(SpawnObject), this.objectSpawnInterval, this.objectSpawnInterval);
		}

		private void SpawnObject()
		{
			GameObject go = Instantiate(this.grabbableObjectPrefab);
			go.transform.position = 
				this.objectSpawnPositions[Random.Range(0, this.objectSpawnPositions.Length)];

			Rigidbody r = go.GetComponent<Rigidbody>();
			Vector3 v = this.objectVelocities[Random.Range(0, this.objectVelocities.Length)];
			r.velocity = v;
			float m = Random.Range(-v.sqrMagnitude, v.sqrMagnitude);
			r.angularVelocity = new Vector3(Random.Range(-m, m), Random.Range(-m, m), Random.Range(-m, m)).normalized;
			StartCoroutine(ClearObject(go));
		}

		private IEnumerator ClearObject(Object go)
		{
			yield return new WaitForSeconds(this.objectLifeTime);
			Destroy(go);
		}
	}
}