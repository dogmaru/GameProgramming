using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject sharkPrefab;
    public float spawnRateMin = 20f;
    public float spawnRateMax = 30f;
    public int maxCloneCount = 3;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            int cloneCount = GameObject.FindGameObjectsWithTag("Shark").Length;
            if (cloneCount < maxCloneCount)
            {
                GameObject shark = Instantiate(sharkPrefab, transform.position, transform.rotation);
                shark.transform.LookAt(target);
            }

            

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
