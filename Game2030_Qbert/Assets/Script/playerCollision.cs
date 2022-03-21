using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // turn on box BoxCollider2D 
        // we have reach the platform and now we need to get new destinations
        if (pc.reach_destination == true)
        {
            bc.enabled = true;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Player and Platform Interaction
        if (col.gameObject.CompareTag("Platform"))
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            // Debug.Log(" player is touch a Platform");

            // remove all prev destinations
            // clear all the old  destinations
            pc.ClearAllDestination();

            // get new destinations from the platform we are on
            pc.set_bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            pc.set_bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
            pc.set_top_left_platform_position = col.GetComponent<Platform>().get_top_left_platform_position;
            pc.set_top_right_platform_position = col.GetComponent<Platform>().get_top_right_platform_position;


            //col.GetComponent<Platform>().PlayerOnFirstPlatformChange();

            // turn off box BoxCollider2D 
            // ->we don't want get new destinations while  moving to new platform
            bc.enabled = false;

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

            col.gameObject.GetComponent<Platform>().set_is_player_current_this_platform = true;
            pc.GetCurrentPlatform(col.gameObject.GetComponent<Platform>());
        }

        // Player and Elevator Interaction
        if (col.gameObject.CompareTag("Elevator"))
        {
            if (this.gameObject.GetComponent<PlayerController>().Is_go_to_elevator)
            {
                Debug.Log("player is touch a Elevator");
                // remove all prev destinations
                // clear all the old  destinations
                pc.ClearAllDestination();

                col.GetComponent<Elevator>().PlayerOnElevator(pc.gameObject);
            }

        }
        

        if (col.gameObject.CompareTag("redirection"))
        {
            if (this.gameObject.CompareTag("Player"))
            {
                if (!this.gameObject.GetComponent<PlayerController>().Is_go_to_elevator)
                {
                    this.gameObject.GetComponent<PlayerController>().ChangeQberState(Qbert_Event_states.Kdeath_off_pyramid);
                    this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    this.gameObject.GetComponent<PlayerController>().start_position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                    this.gameObject.GetComponent<PlayerController>().tagert_position = new Vector2(col.GetComponent<RedirectionPlatform>().target.position.x, col.GetComponent<RedirectionPlatform>().target.position.y);
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    this.gameObject.GetComponent<PlayerController>().Is_drop_from_platform = true; 
                }
            }
        }
    }



    ////void OnCollisionEnter2D(Collision2D col)
    ////{
    ////    if (col.gameObject.CompareTag("Snake"))
    ////    {
    ////        Destroy(this.gameObject);
    ////        Debug.Log("Snake touch player");
    ////    }
    ////}
}



