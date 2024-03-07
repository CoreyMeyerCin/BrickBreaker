using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public GameObject[] blockPrefabs;
    public float xSpawnRangeHigh = 7.7f;
    public float xSpawnRangeLow = -7.7f;
    public float ySpawnHigh = 4f;
    public float ySpawnLow = -1f;
    public float zSpawn = 0f;
    public float startDelay = 0f;
    public float spawnInterval = 1.5f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnBlock", startDelay, spawnInterval);
    }

    void SpawnBlock()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xSpawnRangeHigh, xSpawnRangeLow), Random.Range(ySpawnHigh, ySpawnLow), zSpawn);
        
        int blockIndex = Random.Range(0, blockPrefabs.Length);
        //Debug.Log("blockIndex: " + blockIndex);

        GameObject newBlock = Instantiate(blockPrefabs[blockIndex], spawnPos, blockPrefabs[blockIndex].transform.rotation);
        
        Vector2 boxSize = new Vector2(newBlock.GetComponent<BoxCollider2D>().size.x, newBlock.GetComponent<BoxCollider2D>().size.y);
        
        Collider2D hitCollider = Physics2D.OverlapBox(spawnPos, boxSize, 0);
        if (hitCollider != null && hitCollider.gameObject != newBlock)
        {
            //Debug.Log("Spawn Blocked");
            //Destroy(newBlock);
            //SpawnBlock();
        }
    }

    public void ReplaceBlock(int hitsToBreak, GameObject oldBlock)
    {
        Destroy(oldBlock);
        SpawnBlockAtPosition(hitsToBreak, oldBlock.transform.position);
    }

    void SpawnBlockAtPosition(int hitsToBreak, Vector3 position)
    {
        int blockIndex = hitsToBreak;
        Instantiate(blockPrefabs[blockIndex], position, blockPrefabs[blockIndex].transform.rotation);
    }
}
