using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(0, -28.4f, -0.5f);
    private float startDelay = 0f;
    private float repeatRate = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle ()
    {
        if (playerControllerScript.gameOver == false)
        {
            var randompos = Random.Range(-7f, 7f);
            spawnPos = new Vector3(randompos, spawnPos.y, spawnPos.z);
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        //Time.timeScale = 0;
    }
}
