using UnityEngine;

namespace Actors
{
	public class GrabbableObjectGenerator : MonoBehaviour
	{
		[SerializeField] private GameObject grabbableObjectPrefab;
		[SerializeField] private int objectCount;
		[SerializeField] private float playArea;

		private void OnValidate()
		{
			if (this.playArea < 0) this.playArea = 0;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(this.playArea, 1, this.playArea));
		}

		private void Awake()
		{
			for (int i = 0; i < this.objectCount; i++)
			{
				GameObject go = Instantiate(this.grabbableObjectPrefab, transform, false);
				float s = this.playArea / 2;
				go.transform.localPosition = new Vector3(Random.Range(-s, s), 1, Random.Range(-s, s));
			}
		}
	}
}