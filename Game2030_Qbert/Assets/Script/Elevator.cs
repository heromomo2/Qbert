using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region Globla var
    [SerializeField] elevator_states current_state = elevator_states.Kwaiting_for_player;
    [SerializeField] Transform our_circle_transform;// the object that will parnent the player
    [SerializeField] Transform first_platform_position;// our tagert destination
    [SerializeField] Transform adjecent_Platform_circle;// our tagert destination

    public float travel_time;
    [SerializeField] AnimationCurve curve_t;
    #endregion

    #region Prviate var
    private float timer = 0;
    private GameObject player_child;
    private Vector3 top_of_pyramid_vector_3;
    private Vector3 elevator_start_position_vectotor_3;

    private Action<bool,Transform> elevator_event = null;
    #endregion



    public event Action <bool ,Transform> On_elevator_event
    {
        add
        {
            elevator_event -= value;
            elevator_event += value;
        }

        remove
        {
            elevator_event -= value;
        }
    }

    public string name_sound_effect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (current_state == elevator_states.Kstartmoving_to_top) 
        {
            elevator_start_position_vectotor_3 = this.gameObject.transform.position;
            top_of_pyramid_vector_3 = new Vector3(first_platform_position.position.x, (first_platform_position.position.y + 1.5f), first_platform_position.position.z);
            current_state = elevator_states.Kin_action_moving_to_top;
        }
        else if (current_state == elevator_states.Kin_action_moving_to_top)
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

            if (adjecent_Platform_circle.GetComponentInParent<Platform>().get_top_right_platform_position == our_circle_transform)
            {
                adjecent_Platform_circle.GetComponentInParent<Platform>().set_top_right_platform_position = adjecent_Platform_circle.GetComponentInParent<Platform>().get_top_right_death_platform_position;
            }
            else if (adjecent_Platform_circle.GetComponentInParent<Platform>().get_top_left_platform_position == our_circle_transform)
            {
                adjecent_Platform_circle.GetComponentInParent<Platform>().set_top_left_platform_position = adjecent_Platform_circle.GetComponentInParent<Platform>().get_top_left_death_platform_position;
            }

            player.transform.parent = our_circle_transform.parent;

            player_child = player;


            current_state = elevator_states.Kstartmoving_to_top;
            

            if (elevator_event != null)
            {
                elevator_event(true, adjecent_Platform_circle);
                adjecent_Platform_circle.GetComponentInParent<Platform>().set_is_player_current_this_platform = true;

            }
            SoundManager.Instance.PlaySoundEffect(name_sound_effect);
        }

    }

    public void MoveElevator()
    {
        timer += Time.deltaTime;

        float ratio = timer / travel_time;

        ratio = Mathf.Clamp01(ratio);


        if (ratio < 1)
        {
            this.gameObject.transform.position = Vector3.Lerp(elevator_start_position_vectotor_3, top_of_pyramid_vector_3 ,curve_t.Evaluate (ratio));
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

        player_child.GetComponent<PlayerController>().player_direction = direction.Kfall_from_elavator_start;


        adjecent_Platform_circle.GetComponentInParent<Platform>().set_is_player_current_this_platform = false;
       // first_platform_position.GetComponentInParent<Platform>().set_is_player_current_this_platform = true;
        if (elevator_event != null)
        {
            elevator_event(false, player_child.transform);
        }

        Destroy(this.gameObject);
 

        Debug.Log("Destroy the Elevator");  
    }



    
}
