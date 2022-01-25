using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour
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
        if (pc.reach_destination == true) 
        {
            bc.enabled = true;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log(" player is touch a Platform");

            // remove all prev destinations
            pc.ClearAllDestination();

            // get destinations from the platform we are on
            pc.set_bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            pc.set_bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
            pc.set_top_left_platform_position = col.GetComponent<Platform>().get_top_left_platform_position;
            pc.set_top_right_platform_position = col.GetComponent<Platform>().get_top_right_platform_position;

            // turn off box BoxCollider2D 

            bc.enabled = false;

        }
    }
}
