using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    #region Enemy type and Spawm point
    [SerializeField] GerenalMovement red_ball;
    [SerializeField] GerenalMovement green_ball;
    [SerializeField] GerenalMovement enemy_snake;
    [SerializeField]  List<Transform> spawnpoints;
    [SerializeField] List<Transform>  land_Platforms;
    [SerializeField] List<Platform> Platforms;
    [SerializeField]  Transform  player;
    [SerializeField] List<Elevator> elevators;
    [SerializeField] snake coily_event_received = null;
    #endregion

    #region Global
    public float spawn_delay_coily_time = 5f;
    public float spawn_delay = 3f;
    public bool  is_there_a_snake = false;
    public bool  is_there_enemies = false;
    #endregion

    void Start()
    {
        // spawnpoints = new List<Transform>();
        //InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);

        StartCoroutine(WaitAndSpawn(spawn_delay));
    }

    // Update is called once per frame
    void Update()
    {
        // Cancel all Invoke calls
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // CancelInvoke(); 
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
           // InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);
        }

        if (coily_event_received == null)
        {
            GameObject temp_snake;
            temp_snake = GameObject.FindGameObjectWithTag("Snake");
            if (temp_snake != null)
            {
                coily_event_received = temp_snake.GetComponent<snake>();
                coily_event_received.On_coily_event += CoilyEventListener;
            }
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
                        current_movement.landPostion(land_Platforms[0], spawnpoints[0]);
                    }
                }
                else if (random_number_spawn_entity >= 21 && random_number_spawn_entity <= 50 && !is_there_a_snake) 
                {
                    if (enemy_snake != null)
                    {
                        GerenalMovement current_movement = Instantiate(enemy_snake, spawnpoints[0].position, spawnpoints[0].rotation);
                        current_movement.landPostion(land_Platforms[0], spawnpoints[0]);

                        coily_event_received = current_movement.GetComponent<snake>();

                        coily_event_received.On_coily_event += CoilyEventListener;

                        coily_event_received.GetPlayerPosition(player);
                        
                        foreach(Platform m_platfrom in Platforms) 
                        {
                            coily_event_received.GetPlatforms(m_platfrom);
                        }

                        foreach (Elevator m_elevator in elevators)
                        {
                            coily_event_received.GetElevator(m_elevator);
                        }

                        is_there_a_snake = true;

                    }
                }
                else 
                {
                    if (red_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(red_ball, spawnpoints[0].position, spawnpoints[0].rotation);
                        current_movement.landPostion(land_Platforms[0], spawnpoints[0]);
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
                        current_movement.landPostion(land_Platforms[1], spawnpoints[1]);
                    }
                }
                else if (random_number_spawn_entity >= 21 && random_number_spawn_entity <= 50 && !is_there_a_snake)
                {
                    if (enemy_snake != null)
                    {
                        GerenalMovement current_movement = Instantiate(enemy_snake, spawnpoints[1].position, spawnpoints[1].rotation);
                        current_movement.landPostion(land_Platforms[1], spawnpoints[1]);

                        coily_event_received = current_movement.GetComponent<snake>();

                        coily_event_received.On_coily_event += CoilyEventListener;

                        coily_event_received.GetPlayerPosition(player);

                        foreach (Platform m_platfrom in Platforms)
                        {
                            coily_event_received.GetPlatforms(m_platfrom);
                        }
                        foreach (Elevator m_elevator in elevators)
                        {
                            coily_event_received.GetElevator(m_elevator);
                        }


                        is_there_a_snake = true;

                    }
                }
                else
                {
                    if (red_ball != null)
                    {
                        GerenalMovement current_movement = Instantiate(red_ball, spawnpoints[1].position, spawnpoints[1].rotation);
                         current_movement.landPostion(land_Platforms[1], spawnpoints[1]);
                    }
                }
            }
        }

    }

    private void OnDestroy()
    {
        if (coily_event_received != null)
        {
            coily_event_received.On_coily_event -= CoilyEventListener;
        }

    }

    private void CoilyEventListener(bool does_coily_exist)
    {
        if (does_coily_exist)
        {
            // 
            is_there_a_snake = true;
        }
        else
        {
            ///
            is_there_a_snake = false;

            StopAllCoroutines();
            StartCoroutine(WaitAndSpawn(spawn_delay_coily_time));

        }

    }


    private IEnumerator WaitAndSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       // print("Coroutine ended: " + Time.time + " seconds");
        SpawnEnemy();
        yield return new WaitForSeconds( 1);

        StartCoroutine(WaitAndSpawn(spawn_delay));
    }
}
