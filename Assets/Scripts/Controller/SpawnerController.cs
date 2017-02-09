using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public List<GameObject> EnemiesToSpawn;
    public int waves;
    public float waveTime;
     
	// Use this for initialization
	void Start () {
        StartCoroutine(this.Spawn());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        for (int i = 0; i < waves; i++)
        {
            var enemy = Instantiate(EnemiesToSpawn.FirstOrDefault(), transform.position, transform.rotation);

            yield return new WaitForSeconds(waveTime);
        }
    }
}
