using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    #region Enemy type and Spawm point
    [SerializeField] GameObject enemy_ball;
    [SerializeField] GameObject enemy_snake;
    [SerializeField]  List<Transform> spawnpoints;
    [SerializeField] List<GameObject>  list_enemies;
    #endregion

    #region Global
    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public bool  is_there_a_snake = false;
    public bool  is_there_enemies = false;
    #endregion

    void Start()
    {
     // spawnpoints = new List<Transform>();
      InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Cancel all Invoke calls
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke(); 
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);
        }
    }

    void SpawnEnemy()
    {
        // Instantiate a random enemy.
        int random_number = Random.Range(1, 10);

        if (random_number <= 5) 
        {
            if (spawnpoints[0] != null)
            {
                if (is_there_a_snake)
                {
                    if (enemy_ball != null)
                    {
                        Instantiate(enemy_ball, spawnpoints[0].position, spawnpoints[0].rotation);
                    }
                }
                else
                {
                    if (enemy_snake != null)
                    {
                        Instantiate(enemy_snake, spawnpoints[0].position, spawnpoints[0].rotation);
                        is_there_a_snake = true;
                    }
                }
            }
        }
        else
        {
            if (spawnpoints[1] != null)
            {
                if (is_there_a_snake)
                {
                    if (enemy_ball != null)
                    {
                        Instantiate(enemy_ball, spawnpoints[1].position, spawnpoints[1].rotation);
                    }
                }
                else
                {
                    if (enemy_snake != null)
                    {
                        Instantiate(enemy_snake, spawnpoints[1].position, spawnpoints[1].rotation);
                        is_there_a_snake = true;
                    }
                } 
            }
        }

    }




}
