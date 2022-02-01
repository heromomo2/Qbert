using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    #region Enemy type
    [SerializeField] GameObject enemy_ball;
    [SerializeField] GameObject enemy_snake;
    #endregion
    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    #region

    #endregion

    void Start()
    {
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
    }

    void SpawnEnemy()
    {
        // Instantiate a random enemy.
        int random_number = Random.Range(1, 5);

        if (random_number < 7) 
        {
            Instantiate(enemy_ball, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        else
        {
            Instantiate(enemy_snake, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }

    }




}
