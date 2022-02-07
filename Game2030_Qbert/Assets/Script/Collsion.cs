using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collsion : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circle_collider_2d;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Movement m;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Player and Platform Interaction
        if (col.gameObject.CompareTag("Platform"))
        {
            //If the GameObject's name matches the one you suggest, output this message in the console


            // remove all prev destinations
            // clear all the old  destinations
            m.ClearAllDestination();

            // get new destinations from the platform we are on
            m.set_bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            m.set_bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
            m.set_top_left_platform_position = col.GetComponent<Platform>().get_top_left_platform_position;
            m.set_top_right_platform_position = col.GetComponent<Platform>().get_top_right_platform_position;


            //col.GetComponent<Platform>().PlayerOnFirstPlatformChange();

            // turn off box BoxCollider2D 
            // ->we don't want get new destinations while  moving to new platform
            circle_collider_2d.enabled = false;

            // platform change 
            // only first platform  will not change 
            // first platform  on second time you step on it

            if (!col.GetComponent<Platform>().get_has_been_step_on && !col.GetComponent<Platform>().get_is_first_platform)
            {
                col.GetComponent<Platform>().set_has_been_step_on = true;
                col.GetComponent<Platform>().PlayerOnPlatformChange();
                // Debug.Log(" platform change");
            }

            if (col.GetComponent<Platform>().get_is_first_platform)
            {
                col.GetComponent<Platform>().set_is_first_platform = false;
            }

        }
    }
}