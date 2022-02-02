using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region Globla var
    [SerializeField] elevator_states current_state = elevator_states.Kwaiting_for_player;
    [SerializeField] Transform cirect_transform;// the object that will parnent the player
    [SerializeField] Transform first_platform_position;// our tagert destination

    public float travel_time;
    [SerializeField] AnimationCurve curve_t;
    #endregion

    #region Prviate var
    private float timer = 0;
    private GameObject player_child;
    private Vector3 top_of_pyramid_vector_3;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current_state == elevator_states.Kmoving_to_top) 
        {
            MoveElevator();
        }
        else if (current_state == elevator_states.Kat_the_top)
        {
           RemovePlayerFromElevator();
        }
    }


    public void PlayerOnElevator(GameObject player)
    {
        if ( player.tag == "Player")
        {
            player.transform.parent = cirect_transform.parent;

            player_child = player;

            top_of_pyramid_vector_3 = new Vector3 (first_platform_position.position.x, (first_platform_position.position.y + 1.5f), first_platform_position.position.z);

            current_state = elevator_states.Kmoving_to_top;
        }

    }

    public void MoveElevator()
    {
        timer += Time.deltaTime;

        float ratio = timer / travel_time;

        ratio = Mathf.Clamp01(ratio);


        if (ratio < 1)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, top_of_pyramid_vector_3 ,curve_t.Evaluate (ratio));
        }
        else 
        {
            Debug.Log("We at top of pyramid and elevator");

            current_state = elevator_states.Kat_the_top;
            
        }

    }

    
    void RemovePlayerFromElevator()
    {
        player_child.transform.parent = null;

        player_child.GetComponent<PlayerController>().set_is_drop_from_elevator = true;

        Destroy(this.gameObject);

        

        Debug.Log("Destroy the Elevator");  
    }
}
