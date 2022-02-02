using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public global variables

    [SerializeField] AnimationCurve curve_start_to_mid;
    [SerializeField] AnimationCurve curve_mid_to_end;
    [SerializeField] AnimationCurve curve_fall;

    [SerializeField] direction player_direction = direction.Kno_direction;

    public bool reach_destination = false;

    //public bool picked_destination = false;


    public float jump_time = 1;

    public float fall_time = 1;

    public Vector2 start_position;
    #endregion

    #region Private Global variables
    private float timer = 0;
    private float timer2 = 0;
    private bool is_drop_from_elevator = false;
    private bool is_allow_to_move = true;
    public bool set_is_drop_from_elevator // the Name property
    {
        set => is_drop_from_elevator = value;
    }
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

    #endregion

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
                if (reach_destination /*&& picked_destination */ )
                {
                    //Debug.Log("error: I didn't reach our destination and kno_direction");
                    reach_destination = false;
                }
                break;
        };

        if (is_drop_from_elevator) 
        {
            //start_position = this.gameObject.transform.position;

            Vector2 start_position_vector_2 = new Vector2( this.gameObject.transform.position.x, this.gameObject.transform.position.y);
            Vector2 first_platform_position_vector_2 = new Vector2(-0.02f, 2.55f);

            PlayerDropFromElevator(start_position_vector_2, first_platform_position_vector_2);
        }
       
    }

    // Start is called before the first frame update
    #region PlayermovementAndPlatforms
    void PlayerMovement(Transform target_destination) 
    {
        Vector2 start_position_vector_2 = new Vector2(start_position.x , start_position.y);
        Vector2 target_position_vector_2 = new Vector2( target_destination.position.x, target_destination.position.y);

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
            this.gameObject.transform.position = Bezier( ratio , start_position, mid_position_vector_3, target_destination.position);
        }
        // when we get to our new location
        else 
        {
           this.gameObject.transform.position = target_destination.position;

            player_direction = direction.Kno_direction;

            reach_destination = true;

            Debug.Log(" ratio is at 1");
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


    public Vector3 Bezier(float ratio, Vector2 start, Vector2 mid, Vector2 end)
    {
        ratio = Mathf.Clamp01(ratio);


        Vector2 start_to_mid = Vector3.Lerp(start,mid, curve_start_to_mid.Evaluate(ratio));


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
                player_direction = direction.Ktop_left;              

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log(" Move top-right-> 9");

            if (top_right_platform_position != null)
            {
                player_direction = direction.Ktop_right;

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log("Move bottom-left-> 1");

            if (bottom_left_platform_position != null)
            {
                player_direction = direction.Kbottom_left;

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) && player_direction == direction.Kno_direction && is_allow_to_move)
        {
            Debug.Log("Move bottom-right-> 3");

            if (bottom_right_platform_position != null)
            {
                player_direction = direction.Kbottom_right;

                start_position = this.gameObject.transform.position;
            }
        }
    }
#endregion

    public void PlayerDropFromElevator( Vector2 start, Vector2 end) 
    {
        

        float ratio = timer2 / fall_time;

        timer2 += Time.deltaTime;

        ratio = Mathf.Clamp01(ratio);

        //reset timer one it's bigger than fall time

        if (timer2 >= fall_time)
        {
            timer2 = 0;
        }

        Debug.Log("ratio:->" + ratio);
        if (ratio <= 1)
        {
            
            this.gameObject.transform.position = Vector3.Lerp(start, end, curve_fall.Evaluate(  ratio));
            //is_allow_to_move = false;
        }
        else 
        {
           this.gameObject.transform.position = end;

            //start_position = this.gameObject.transform.position;
            player_direction = direction.Kno_direction;
            is_drop_from_elevator = false;
            is_allow_to_move = true;
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
    Kno_direction
}


public enum elevator_states
{
    Kwaiting_for_player,
    Kmoving_to_top,
    Kat_the_top,
}
#endregion