using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Global var
    
    [SerializeField] GerenalMovement general_movement;

    [SerializeField]  bool is_already_moving = false;

    [SerializeField] bool is_falling;

    [SerializeField] snake coily_event_received = null;
    #endregion

    #region Animation stuff

    public Animator ball_animator;
    public float delay_before_pick_move = 3.5f;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_already_moving)
        {
            BallDecisionToMove();
        }

        if (general_movement.get_reach_destination)
        {
            is_already_moving = false;
            if (ball_animator != null)
            {
                ball_animator.SetBool("Is_green_ball_jumping", false);
            }
        }

        if (this.gameObject.transform.position.y <= -3.00f) 
        {
            Destroy(this.gameObject);
        }

        if (coily_event_received == null) 
        {
            GameObject temp_snake;
            temp_snake = GameObject.FindGameObjectWithTag("Snake");
            if (temp_snake != null)
            {
                coily_event_received = temp_snake.GetComponent<snake>();
                coily_event_received.On_coily_event += CoilyEventListener;
            }
        }
    }
    void BallDecisionToMove() 
    {
        int random_number = Random.Range(1,100);

        // check where we can move
        // we have option
        if (random_number <= 50)
        {
            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
            if (ball_animator != null)
            {
                ball_animator.SetBool("Is_green_ball_jumping", true);
            }
        }
        else 
        {
            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
            if (ball_animator != null)
            {
                ball_animator.SetBool("Is_green_ball_jumping", true); 
            }
        }
       

    }

    private void OnDestroy()
    {
        if (coily_event_received != null)
        {
            coily_event_received.On_coily_event -= CoilyEventListener;
        }

    }
    private void CoilyEventListener(bool does_coily_exist)
    {
        if (does_coily_exist)
        {
           // do notthing
        }
        else
        {
            ///
            Destroy(this.gameObject);
        }

    }

    private IEnumerator WaitBeforeMoving(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
