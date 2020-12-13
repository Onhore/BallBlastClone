using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Spawn options")]
    [SerializeField] private GameObject[] MeteorPrefabs;
	[SerializeField] private int MeteorsCount;
	[SerializeField] private float SpawnDelay;

	private GameObject[] meteors;

	private void Start ()
	{
		PrepareMeteors ();
		StartCoroutine(SpawnMeteors());
	}

	private IEnumerator SpawnMeteors()
	{
		for (int i = 0; i < MeteorsCount; i++) 
        {
			meteors[i].SetActive(true);
			yield return new WaitForSeconds(SpawnDelay);
		}
	}

	private void PrepareMeteors ()
	{
		meteors = new GameObject[MeteorsCount];

		int prefabsCount = MeteorPrefabs.Length;
		
        for (int i = 0; i < MeteorsCount; i++) 
        {
			meteors[i] = Instantiate(MeteorPrefabs[Random.Range(0, prefabsCount)], transform);
			meteors[i].GetComponent<Meteor>().IsResultOfFission = false;
			meteors[i].SetActive(false);
		}
	}
}
