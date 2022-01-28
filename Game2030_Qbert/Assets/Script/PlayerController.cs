using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region public global variables

    [SerializeField] direction player_direction = direction.Kno_direction;

    public bool reach_destination = false;

    //public bool picked_destination = false;


    public float jump_time = 1;

    public Vector2 start_position;
    #endregion

    #region Private Global variables
    private float timer = 0;

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


       
    }

    // Start is called before the first frame update
    void PlayerMovement(Transform target_destination) 
    {
        Vector2 my_vector_2 = new Vector2(start_position.x , start_position.y);
        Vector2 td_vector_2 = new Vector2( target_destination.position.x, target_destination.position.y);
        Vector3 temp_mid = target_destination.position;
        temp_mid.y += 6.0f;


        timer += Time.deltaTime;

        float ratio = timer / jump_time;

        // reset it
        if (timer >= jump_time)
        {
            timer = 0;
        }

        if (ratio <= 1)
        {
           

           this.gameObject.transform.position = Bezier( ratio , start_position, temp_mid, target_destination.position);
            
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

        Vector2 start_to_mid = Vector3.Lerp(start,mid,ratio);
        Vector2 mid_to_end = Vector3.Lerp(mid,end, ratio);

        return Vector3.Lerp(start_to_mid, mid_to_end, ratio);
    }


    private void PlayerPressedAKey() 
    {
        // player movement key and pick a direction
        if (Input.GetKeyDown(KeyCode.Keypad7) && /*picked_destination == false && */player_direction == direction.Kno_direction)
        {
            Debug.Log("Move top-left-> 7");

            if (top_left_platform_position != null)
            {
                player_direction = direction.Ktop_left;

                /*picked_destination = true;*/

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) && /* picked_destination == false && */player_direction == direction.Kno_direction)
        {
            Debug.Log(" Move top-right-> 9");

            if (top_right_platform_position != null)
            {
                player_direction = direction.Ktop_right;

               // picked_destination = true;

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) && /*picked_destination == false && */player_direction == direction.Kno_direction)
        {
            Debug.Log("Move bottom-left-> 1");

            if (bottom_left_platform_position != null)
            {
                player_direction = direction.Kbottom_left;

              //  picked_destination = true;

                start_position = this.gameObject.transform.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) && /*picked_destination == false && */player_direction == direction.Kno_direction)
        {
            Debug.Log("Move bottom-right-> 3");

            if (bottom_right_platform_position != null)
            {
                player_direction = direction.Kbottom_right;

                //picked_destination = true;

                start_position = this.gameObject.transform.position;
            }
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
#endregion