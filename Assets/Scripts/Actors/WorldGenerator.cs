using UnityEngine;

namespace Actors
{
	/// <summary>
	/// Defines an area in which objects are generated
	/// </summary>
	public class WorldGenerator : MonoBehaviour
	{
		[SerializeField] private GameObject grabbableObjectPrefab;
		[SerializeField] private int objectCount = 50;
		[SerializeField] private float spawnArea = 300;
		[SerializeField] private float playArea = 400;

		private void OnValidate()
		{
			if (this.playArea < 0) this.playArea = 0;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(this.spawnArea, 1, this.spawnArea));
			Gizmos.DrawWireCube(transform.position, new Vector3(this.playArea, 1, this.playArea));
		}

		private void Awake()
		{
			float s = this.spawnArea / 2;
			float p = this.playArea / 2;

			for (int i = 0; i < this.objectCount; i++)
			{
				GameObject go = Instantiate(this.grabbableObjectPrefab, transform, false);
				go.transform.localPosition = new Vector3(Random.Range(-s, s), 1, Random.Range(-s, s));
			}

			Vector3[] pos = {
				new Vector3(p,40,0), 
				new Vector3(-p,40,0), 
				new Vector3(0,40,p), 
				new Vector3(0,40,-p) 
			};

			Vector3[] len = {
				new Vector3(1,100,s*3),
				new Vector3(1,100,s*3),
				new Vector3(s*3,100,1),
				new Vector3(s*3,100,1)
			};

			for (int i = 0; i < 4; i++)
			{
				BoxCollider c = gameObject.AddComponent<BoxCollider>();
				c.center = pos[i];
				c.size = len[i];
			}
		}
	}
}