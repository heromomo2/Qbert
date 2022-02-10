using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collsion : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circle_collider_2d;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Movement movement_script;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (movement_script.reach_destination == true)
        {
            circle_collider_2d.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Player and Platform Interaction
        if (col.gameObject.CompareTag("Platform"))
        {

            
            //If the GameObject's name matches the one you suggest, output this message in the console


            // remove all prev destinations
            // clear all the old  destinations
            // movement_script.ClearAllDestination();

            // get new destinations from the platform we are on
            movement_script.set_bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            movement_script.set_bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
            movement_script.set_top_left_platform_position = col.GetComponent<Platform>().get_top_left_platform_position;
            movement_script.set_top_right_platform_position = col.GetComponent<Platform>().get_top_right_platform_position;

            

            //col.GetComponent<Platform>().PlayerOnFirstPlatformChange();

            // turn off box BoxCollider2D 
            // ->we don't want get new destinations while  moving to new platform
            circle_collider_2d.enabled = false;

            Debug.Log("Collsion script is working(you are hit the platform)");

            if (this.gameObject.tag == "Snake")
            {
                if (col.gameObject.GetComponent<Platform>().get_is_last_platform)
                {
                    this.gameObject.GetComponent<snake>().are_we_at_bottom = true;
                }
            }

        }

        if (col.gameObject.CompareTag("DeathPlatform")) 
        {
            Destroy(this.gameObject);
        }
    }
}