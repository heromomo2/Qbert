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

    [SerializeField] PlayerController qbert_event_received = null;

    public string name_sound_effect;

    // green ball stuff
    public float freeze_time = 3.5f;
    public float freeze_timer = 3.5f;
    public bool  is_frozen = false;
    public bool is_ignore_frozen = false;

    #endregion

    #region Animation stuff

    public Animator ball_animator;
    public float delay_before_pick_move = 3.5f;
    public string name_Anim_bool;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if (qbert_event_received == null)
        {
            GameObject temp_player;
            temp_player = GameObject.FindGameObjectWithTag("Player");
            if (temp_player != null)
            {
                qbert_event_received = temp_player.GetComponent<PlayerController>();
                qbert_event_received.On_qbert_event += QbertEventListener;
            }
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

    // Update is called once per frame
    void Update()
    {

        if (is_frozen) 
        { 
          
            if (freeze_timer > 0) 
            {
                freeze_timer -= Time.deltaTime;
                this.gameObject.GetComponent<GerenalMovement>().Is_movement_stop = true;
            }
            else
            {
                if (!is_ignore_frozen)
                {
                    is_frozen = false;
                    freeze_timer = freeze_time;
                    this.gameObject.GetComponent<GerenalMovement>().Is_movement_stop = false;
                }
            }
        }

        if (!is_already_moving)
        {
            BallDecisionToMove();
        }

        if (general_movement.get_reach_destination)
        {
           
            if (ball_animator != null)
            {
                ball_animator.SetBool(name_Anim_bool, false);
                
            }
            is_already_moving = false;
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

        if (qbert_event_received == null)
        {
            GameObject temp_player;
            temp_player = GameObject.FindGameObjectWithTag("Player");
            if (temp_player != null)
            {
                qbert_event_received = temp_player.GetComponent<PlayerController>();
                qbert_event_received.On_qbert_event += QbertEventListener;
            }
        }
    }
    void BallDecisionToMove() 
    {
        int random_number = Random.Range(1,100);

        // check where we can move
        // we have option

        float temp_delay_before_pick_move = delay_before_pick_move;

        delay_before_pick_move -= Time.deltaTime;
        if (delay_before_pick_move < 0)
        {
            if (random_number <= 50)
            {
                if (general_movement.get_bottom_left_platform_position != null)
                {
                    if (ball_animator != null)
                    {
                        ball_animator.SetBool(name_Anim_bool, true);
                        delay_before_pick_move = 0.3f;
                    }
                    SoundManager.Instance.PlaySoundEffect(name_sound_effect);
                    is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                    
                }
            }
            else
            {
                if (general_movement.get_bottom_right_platform_position != null)
                {
                    if (ball_animator != null)
                    {
                        ball_animator.SetBool(name_Anim_bool, true);
                        delay_before_pick_move = 0.3f;
                    }
                    SoundManager.Instance.PlaySoundEffect(name_sound_effect);
                    is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
                    
                }
            }
        }

    }

    private void OnDestroy()
    {
        if (coily_event_received != null)
        {
            coily_event_received.On_coily_event -= CoilyEventListener;
        }

        if (qbert_event_received != null)
        {
            qbert_event_received.On_qbert_event -= QbertEventListener;
        }       

    }
    private void CoilyEventListener(bool does_coily_exist)
    {
        if (!does_coily_exist)
        {
            Destroy(this.gameObject);
        }

    }


    private void QbertEventListener(Qbert_Event_states qbert_event)
    {
        switch (qbert_event)
        {
            case Qbert_Event_states.Kdeath_off_pyramid:
                is_ignore_frozen = true;
                is_frozen = false;
                
                Destroy(this.gameObject);
                break;
            case Qbert_Event_states.Kdeath_on_pyramid:
              //  is_ignore_frozen = t;
           //     is_frozen = true;
                this.gameObject.GetComponent<GerenalMovement>().Is_movement_stop = true;
                this.gameObject.GetComponent<GerenalMovement>().Is_movement_stop = true;
                // Destroy(this.gameObject);
                break;
            case Qbert_Event_states.Krevive_player_pyramid:
                is_frozen = false;
                Destroy(this.gameObject);
                break;
            case Qbert_Event_states.Ktouch_greenball:
                is_ignore_frozen = false;
                is_frozen = true;
                break;
            case Qbert_Event_states.kplayer_has_won:
                is_ignore_frozen = true;
                is_frozen = false;
               
                Destroy(this.gameObject);
                break;
            case Qbert_Event_states.kplayer_has_lost:
                is_ignore_frozen = true;
                is_frozen = false;
                
                Destroy(this.gameObject);
                break;
        }

    }

    public void SubToCoily() 
    {
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

    private IEnumerator WaitBeforeMoving(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
