using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : Singleton<GameController>
{
    public Transform tile;

    public Transform obstacle;

    public Vector3 startPoint = new Vector3(0, 0, -5.5f);

    // number tile create
    [Range (1,15)]
    public int initSpawnNum = 10;

    //number tiles to spawn before spawn obstacles
    public int initNoObstacles = 4;

    //next tile spawn at
    private Vector3 nextTileLocation;
    //rotation next tile
    private Quaternion nextTileRotation;


    public GameObject endScreen;
    public GameObject inGameScreen;
    public TextMeshProUGUI scoreText;
    public PlayerBehaviour player;

    void Start()
    {
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; i++)
        {
            SpawnNextTile(i >= initNoObstacles);
        }

        endScreen.SetActive(false);
    }

    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);
        var nextTile = newTile.Find("Next Spawn Point");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if (spawnObstacles)
        {
            spawnObstacle(newTile);
        }
    }

    private void spawnObstacle(Transform newTile)
    {
        //all places to spawn the obstacle
        var obstacleSpawnPoints = new List<GameObject>();
        
        foreach ( Transform child in newTile)
        {
            if (child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        if (obstacleSpawnPoints.Count > 0)
        {
            //random spawn point
            var spawnPoint = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Count)];

            var spawnPos = spawnPoint.transform.position;

            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);
            newObstacle.SetParent(spawnPoint.transform);
        }
    }

    public void EndGame()
    {
        inGameScreen.SetActive(false);
        endScreen.SetActive(true);
        scoreText.text = string.Format("{0:0}", player.Score);

    }
}
