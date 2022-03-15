using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public global variables

    [SerializeField] AnimationCurve curve_start_to_mid;
    [SerializeField] AnimationCurve curve_mid_to_end;
    [SerializeField] AnimationCurve curve_fall;

    [SerializeField] public direction player_direction = direction.Kno_direction;

    public bool reach_destination = false;

    //public bool picked_destination = false;


    public float jump_time = 1;

    public float fall_time = 1;

    public Vector2 start_position;

    public Vector2 tagert_position;

    [SerializeField] Platform current_Platform;
    #endregion

    #region Private Global variables
    private float timer = 0;
    private float timer2 = 0;
    private bool is_drop_from_elevator = false;
    private bool is_drop_from_platform = false;
    public bool Is_drop_from_platform    // the Name property
    {
        get => is_drop_from_platform;
        set => is_drop_from_platform = value;
    }

    private bool is_go_to_elevator = false;

    public bool Is_go_to_elevator    // the Name property
    {
        get => is_go_to_elevator;
        set => is_go_to_elevator = value;
    }

    private bool is_allow_to_move = true;

    private Action<Qbert_Event_states> qbert_event = null;
    #endregion

    #region Destination/Setter
    [SerializeField] Transform top_left_platform_position;

    public Transform set_top_left_platform_position // the Name property
    {
        set => top_left_platform_position = value;
    }

    [SerializeField] Transform top_right_platform_position;

    public Transform set_top_right_platform_position // the Name property
    {
        set => top_right_platform_position = value;
    }

    [SerializeField] Transform bottom_left_platform_position;
    public Transform set_bottom_left_platform_position // the Name property
    {
        set => bottom_left_platform_position = value;
    }

    [SerializeField] Transform bottom_right_platform_position;

    public Transform set_bottom_right_platform_position // the Name property
    {
        set => bottom_right_platform_position = value;
    }
    #endregion

    #region Animation variables
    [SerializeField] private Animator player_anim;
    [SerializeField] float horizontal_value;
    [SerializeField] bool facing_right = true;

    #endregion



    public event Action<Qbert_Event_states> On_qbert_event
    {
        add
        {
            qbert_event -= value;
            qbert_event += value;
        }

        remove
        {
            qbert_event -= value;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerPressedAKey();

        // move the player in direction
        switch (player_direction)
        {
            case direction.Ktop_left:
                if (!reach_destination) // if we haven't reach our disnation call the move the player function
                {
                    PlayerMovement(top_left_platform_position);
                }
                else
                {
                    reach_destination = false;
                }
                break;
            case direction.Ktop_right:
                if (!reach_destination)
                {
                    PlayerMovement(top_right_platform_position);
                }
                else
                {
                    reach_destination = false;
                }
                break;
            case direction.Kbottom_left:
                if (!reach_destination)
                {
                    PlayerMovement(bottom_left_platform_position);
                }
                else
                {
                    reach_destination = false;
                }
                break;
            case direction.Kbottom_right:
                if (!reach_destination)
                {
                    PlayerMovement(bottom_right_platform_position);
                }
                else
                {

                    reach_destination = false;
                }
                break;
            case direction.Kno_direction:
                if (reach_destination)
                {
                    player_anim.SetTrigger("landed");
                    reach_destination = false;
                }
                break;
            case direction.Kfall_from_elavator_start:

                start_position = this.gameObject.transform.position;
                tagert_position = new Vector2(-0.062f, 2.4f);
                is_drop_from_elevator = true;
                break;
            case direction.kfall_from_platform:
                is_drop_from_platform = true;         
                break;
        };

        //this code activated when we reach the top of the pyrmid and elevator is gone
        if (is_drop_from_elevator)
        {
          //  player_direction = direction.Kfall_from_elavator_action;
            PlayerDropFromElevator(start_position, tagert_position);
        }

        //this code activated when collide redirtection point
        if (is_drop_from_platform)
        {
            is_allow_to_move = false;// don't move when fall
            PlayerDropFromElevator(start_position, tagert_position);// call the falling funtion
        }

        if (this.gameObject.transform.position.y <= -3.00f)
        {
            // this if statement for when you jump off (top-lift, -top-right)
            //- turn on the boxcolluder, so you can get new destinations from the platfrom
            //- put your sortingorder back to normarl(qbert in on pyramid and not bebind it)
            //- reset the timer for PlayerDropFromElevator function becuse we are never go reach the location we are lerpping to
            if (is_drop_from_platform)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                Is_drop_from_platform = false;
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                timer2 = 0;
            }
            is_allow_to_move = true;// so we can move again
            player_anim.SetTrigger("initia");// back to inita animation state
            this.gameObject.transform.position = new Vector2(-0.062f, 2.4f);// positon on top of the pyramid
           

        }
    }

    // Start is called before the first frame update
    #region PlayermovementAndPlatforms
    void PlayerMovement(Transform target_destination)
    {
        Vector2 start_position_vector_2 = new Vector2(start_position.x, start_position.y);
        Vector2 target_position_vector_2 = new Vector2(target_destination.position.x, target_destination.position.y);

        Vector3 mid_position_vector_3 = target_destination.position;

        mid_position_vector_3.y += 0.5f;


        // timer is increase
        timer += Time.deltaTime;

        float ratio = timer / jump_time;

        // reset timer one it's bigger than jump time

        if (timer >= jump_time)
        {
            timer = 0;
        }

        if (ratio < 1)
        {
            this.gameObject.transform.position = Bezier(ratio, start_position, mid_position_vector_3, target_destination.position);
        }
        // when we get to our new location
        else
        {
            this.gameObject.transform.position = target_destination.position;

            player_direction = direction.Kno_direction;

            reach_destination = true;

            Debug.Log(" ratio is at 1");

            if (current_Platform != null)
            {
                current_Platform.set_is_player_current_this_platform = false;
            }

        }
        //Debug.Log("MoveThePlayer is been all call");
    }



    public void ClearAllDestination()
    {
        top_left_platform_position = null;
        top_right_platform_position = null;
        bottom_left_platform_position = null;
        bottom_right_platform_position = null;
    }

    public void ClearPlatformData()
    {
        current_Platform = null;
    }
    public void GetCurrentPlatform(Platform current)
    {
        current_Platform = current;
    }

    public Vector3 Bezier(float ratio, Vector2 start, Vector2 mid, Vector2 end)
    {
        ratio = Mathf.Clamp01(ratio);


        Vector2 start_to_mid = Vector3.Lerp(start, mid, curve_start_to_mid.Evaluate(ratio));


        Vector2 mid_to_end = Vector3.Lerp(mid, end, curve_mid_to_end.Evaluate(ratio));

        return Vector3.Lerp(start_to_mid, mid_to_end, ratio);
    }

    public static float Spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }


    private void PlayerPressedAKey()
    {
        // player movement key and pick a direction
        if (Input.GetKeyDown(KeyCode.Keypad7) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log("Move top-left-> 7");

            if (top_left_platform_position != null)
            {
                if (top_left_platform_position.gameObject.tag == "Elevator")
                {
                    is_go_to_elevator = true;
                }
                player_direction = direction.Ktop_left;

                start_position = this.gameObject.transform.position;
                SoundManager.Instance.PlaySoundEffect("Qbertjump");

                player_anim.SetTrigger("jumpBack");

                if (!facing_right)
                {
                    ProperFilp();
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log(" Move top-right-> 9");

            if (top_right_platform_position != null)
            {
                if (top_right_platform_position.gameObject.tag == "Elevator")
                {
                    is_go_to_elevator = true;
                }
                player_direction = direction.Ktop_right;

                start_position = this.gameObject.transform.position;
                SoundManager.Instance.PlaySoundEffect("Qbertjump");

                player_anim.SetTrigger("jumpBack");
                if (facing_right)
                {
                    ProperFilp();
                }

            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log("Move bottom-left-> 1");

            if (bottom_left_platform_position != null)
            {
                player_direction = direction.Kbottom_left;

                start_position = this.gameObject.transform.position;
                SoundManager.Instance.PlaySoundEffect("Qbertjump");

                player_anim.SetTrigger("jumpFront");
                if (!facing_right) 
                {
                    ProperFilp();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log("Move bottom-right-> 3");

            if (bottom_right_platform_position != null)
            {
                player_direction = direction.Kbottom_right;

                start_position = this.gameObject.transform.position;
                SoundManager.Instance.PlaySoundEffect("Qbertjump");

                player_anim.SetTrigger("jumpFront");

                if (facing_right) 
                {
                    ProperFilp(); 
                }
            }
        }
    }
    #endregion

    public void PlayerDropFromElevator(Vector2 start, Vector2 end)
    {


        float ratio = timer2 / fall_time;

        timer2 += Time.deltaTime;

        ratio = Mathf.Clamp01(ratio);



        //  Debug.Log(" fall ratio :->" + ratio);
        if (ratio < 1)
        {

            this.gameObject.transform.position = Vector3.Lerp(start, end, curve_fall.Evaluate(ratio));

        }
        else
        {
            this.gameObject.transform.position = end;


            player_direction = direction.Kno_direction;
            is_drop_from_elevator = false;
            is_go_to_elevator = false;
            is_allow_to_move = true;

            //reset timer one it's bigger than fall time
            if (timer2 >= fall_time)
            {
                timer2 = 0;
            }
        }
    }


    void ProperFilp()
    {
        if (!facing_right || facing_right)
        {
            facing_right = !facing_right;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    public void ChangeQberState(Qbert_Event_states qbert_event_state) 
    {
        Debug.Log(" ChangeQberState");
        if (qbert_event !=  null) 
        {
            qbert_event(qbert_event_state);
        } 
    }
}

#region Enums
public enum direction
{
    Kbottom_right,
    Kbottom_left,
    Ktop_right,
    Ktop_left,
    Kno_direction,
    Kfall_from_elavator_start,
    kfall_from_platform,
}


public enum elevator_states
{
    Kwaiting_for_player,
    Kstartmoving_to_top,
    Kin_action_moving_to_top,
    Kat_the_top,
}
public enum Qbert_Event_states
{
    Kdeath,
    Ktouch_greenball,
 
}
#endregion