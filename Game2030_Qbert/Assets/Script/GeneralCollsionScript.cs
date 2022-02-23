using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralCollsionScript : MonoBehaviour
{

    [SerializeField] private CircleCollider2D circle_collider_2d;
    [SerializeField] private Rigidbody2D rigid_body_2d;
    [SerializeField] private GerenalMovement movement_script;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        // we when reach the destination:
        //-> we want turn our collider, so we can get new destinations.
        //-> we turn off our collider, don't get new destinations while travel
        if (movement_script.reach_destination == true)
        {
            circle_collider_2d.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //] Platform Interaction
        if (col.gameObject.CompareTag("Platform"))
        { 

            // get new destinations from the platform we are on
            movement_script.set_bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            movement_script.set_bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
            movement_script.set_top_left_platform_position = col.GetComponent<Platform>().get_top_left_platform_position;
            movement_script.set_top_right_platform_position = col.GetComponent<Platform>().get_top_right_platform_position;


            // turn off box BoxCollider2D 
            // ->we don't want get new destinations while  moving to new platform
            circle_collider_2d.enabled = false;

            Debug.Log("Collsion script is working(you are hit the platform)");

            // this is being use to stop the snake at the bottom of pyramid
            if (this.gameObject.tag == "Snake")
            {
                this.gameObject.GetComponent<snake>().set_our_current_platform(col.gameObject.GetComponent<Platform>());

                this.gameObject.GetComponent<snake>().set_current_platform_colum_id = col.gameObject.GetComponent<Platform>().get_colum_id_number;
                if (col.gameObject.GetComponent<Platform>().get_is_last_platform)
                {
                    this.gameObject.GetComponent<snake>().are_we_at_bottom = true;
                }

            }

        }

        // this is being use to destory anything that fall off the pyramid
        if (col.gameObject.CompareTag("DeathPlatform")) 
        {
          //  Destroy(this.gameObject);
        }

        if (col.gameObject.CompareTag("redirection") )
        {
            if (this.gameObject.CompareTag ("Snake")) 
            {
                if (this.gameObject.GetComponent<snake>().get_has_coily_jump_off) 
                {
                    this.gameObject.GetComponent<snake>().CoilyOffThePyramid();
                    this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0; 
                    this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                    this.gameObject.GetComponent<GerenalMovement>().landPostion( col.GetComponent<RedirectionPlatform>().target, col.GetComponent<RedirectionPlatform>().start);
                    Destroy(col.gameObject);

                }
            }
        }
    }
}