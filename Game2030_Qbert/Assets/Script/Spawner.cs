using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    #region Enemy type and Spawm point
    [SerializeField] GerenalMovement red_ball;
    [SerializeField] GerenalMovement green_ball;
    [SerializeField] GameObject enemy_snake;
    [SerializeField]  List<Transform> spawnpoints;
    [SerializeField] List<Transform>  Platforms;
    [SerializeField] List<GameObject> list_enemies;
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
        int random_number_spawn_entity = Random.Range(1, 50);

        if (random_number <= 5) 
        {
            if (spawnpoints[0] != null)
            {
                if (random_number_spawn_entity <= 20)
                {
                    if (green_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(green_ball, spawnpoints[0].position, spawnpoints[0].rotation);
                        current_movement.landPostion(Platforms[0], spawnpoints[0]);
                    }
                }
                else
                {
                    if (red_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(red_ball, spawnpoints[0].position, spawnpoints[0].rotation);
                        current_movement.landPostion(Platforms[0], spawnpoints[0]);
                    }
                }

            }
        }
        else
        {
            if (spawnpoints[1] != null)
            {
                if (random_number_spawn_entity <= 20)
                {
                    if (green_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(green_ball, spawnpoints[1].position, spawnpoints[1].rotation);
                        current_movement.landPostion(Platforms[1], spawnpoints[1]);
                    }
                }
                else
                {
                    if (red_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(red_ball, spawnpoints[1].position, spawnpoints[1].rotation);
                         current_movement.landPostion(Platforms[1], spawnpoints[1]);
                    }
                }
            }
        }

    }




}
