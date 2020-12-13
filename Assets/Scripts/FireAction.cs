using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAction : MonoBehaviour
{

	#region Singleton
    private static FireAction _instance;
    public static FireAction Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FireAction>();

            }
                
            return _instance;
        }
    }

    #endregion

    [Header("Fire options")]
	[SerializeField] private GameObject MissilePrefab;
	[SerializeField] private int MissilesCount;

	[Space]

	[SerializeField] private float Delay = 0.3f;
	[SerializeField] private float Speed = 0.3f;

    private Queue<GameObject> missilesQueue;
	private GameObject missile;

    private void Start()
	{
		PrepareMissiles();

		StartCoroutine(Fire());
	}

	private void PrepareMissiles()
	{
		missilesQueue = new Queue<GameObject>();

		for (int i = 0; i < MissilesCount; i++) 
        {
			missile = Instantiate(MissilePrefab, transform.position, Quaternion.identity, transform);
			missile.SetActive(false);
			missilesQueue.Enqueue(missile);
		}
	}

	public GameObject SpawnMissile(Vector2 position)
	{
		if (missilesQueue.Count > 0) 
        {
			missile = missilesQueue.Dequeue();
			missile.transform.position = position;
			missile.SetActive(true);
			return missile;
		}

		return null;
	}

	public void DestroyMissile(GameObject missile)
	{
		missilesQueue.Enqueue(missile);
		missile.SetActive(false);
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Equals("Missile")) 
        {
			DestroyMissile(other.gameObject);
		}
	}

	private IEnumerator Fire()
	{
		missile = SpawnMissile(transform.position);

		if (missile != null)
			missile.GetComponent <Rigidbody2D>().velocity = Vector2.up * Speed;

		yield return new WaitForSeconds(Delay);

		StartCoroutine(Fire());
	}
}
