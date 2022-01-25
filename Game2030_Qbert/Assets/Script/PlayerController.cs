using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    #region global var 
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

    public float speed = 1.0f;

    enum direction
    {
        Kbottom_right,
        Kbottom_left,
        Ktop_right,
        Ktop_left,
        Kno_direction
    }
   [SerializeField] direction player_direction = direction.Kno_direction;

    public bool reach_destination = false;

    public bool picked_destination = false;
    #endregion


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // player movement key and pick a direction
        if (Input.GetKeyDown(KeyCode.Keypad7) && picked_destination == false)
        {
            Debug.Log("Move top-left-> 7");
            player_direction = direction.Ktop_left;
            picked_destination = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9) && picked_destination == false)
        {
            Debug.Log(" Move top-right-> 9");
            player_direction = direction.Ktop_right;
            picked_destination = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) && picked_destination == false)
        {
            Debug.Log("Move bottom-left-> 1");
            player_direction = direction.Kbottom_left;
            picked_destination = true;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3) && picked_destination == false)
        {
            Debug.Log("Move bottom-right-> 3");
            player_direction = direction.Kbottom_right;
            picked_destination = true;
        }


        //call move funtion and give the position
        if (player_direction == direction.Kbottom_left && player_direction != direction.Kno_direction)
        {
            if (bottom_left_platform_position != null)
            {
                MoveThePlayer(bottom_left_platform_position);
            }
            else
            {
                Debug.Log("no target_destination ");
            }
        }
        else if ( player_direction == direction.Kbottom_right && player_direction != direction.Kno_direction) 
        {
            if (bottom_right_platform_position != null)
            {
                MoveThePlayer(bottom_right_platform_position);
            }
            else 
            {
                Debug.Log("no target_destination ");
            }
        }
        else if (player_direction == direction.Ktop_right && player_direction != direction.Kno_direction)
        {
            if (top_right_platform_position != null)
            {
                MoveThePlayer(top_right_platform_position);
            }
            else
            {
                Debug.Log("no target_destination ");
            }
        }
        else if (player_direction == direction.Ktop_left && player_direction != direction.Kno_direction)
        {
            if (top_left_platform_position != null)
            {
                MoveThePlayer(top_left_platform_position);
            }
            else 
            {
                Debug.Log("no target_destination ");
            }
        }
    }

    // Start is called before the first frame update
    void MoveThePlayer(Transform target_destination) 
    {
        //
        if (target_destination != null && this.transform.position != target_destination.position)
        {
            // To do: movemont to position

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, target_destination.position, step);
            reach_destination = false;
        }
        // when we get the location
        else if (this.transform.position == target_destination.position)
        {
            player_direction = direction.Kno_direction;
            reach_destination = true;
            picked_destination = false;
        }
       
    }

    public void ClearAllDestination() 
    {
        top_left_platform_position = null;
        top_right_platform_position = null;
        bottom_left_platform_position = null;
        bottom_right_platform_position = null;
    }
}
