using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Actors
{
	public class RandomObjectMeshManager
	{
		private static RandomObjectMeshManager _instance;
		public static RandomObjectMeshManager Instance = (_instance ?? new RandomObjectMeshManager());

		private readonly List<GameObject> objects;

		private RandomObjectMeshManager()
		{
			this.objects = Resources.LoadAll<GameObject>("Prefabs").ToList();
		}

		public GameObject GetRandomObject() => this.objects[Random.Range(0, this.objects.Count)];
	}
}