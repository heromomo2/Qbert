using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class snake : MonoBehaviour
{
    #region Global
    [SerializeField] Sprite Ball_sprite;

    [SerializeField] Sprite snake_sprite;

    [SerializeField] GerenalMovement general_movement;

    [SerializeField] bool is_already_moving = false;

    public bool are_we_at_bottom = false;
    public bool did_we_hatch = false;

    [SerializeField] List<Platform> platforms;

    [SerializeField] Transform player;

    [SerializeField] float current_platform_colum_id;

    [SerializeField] float bottom_colum_id = 6;
    public float set_current_platform_colum_id
    {
        set => current_platform_colum_id = value;
    }

    [SerializeField] List<Elevator> elevators_event_received = null;

    [SerializeField] bool is_coily_in_death_mode = false;

    [SerializeField] private Platform our_current_platform = null;

    public void set_our_current_platform(Platform platform)
    {
        our_current_platform = platform;
    }

    [SerializeField] bool has_coily_jump_off = false;
    public bool get_has_coily_jump_off
    {
        get => has_coily_jump_off;
    }

    private Action<bool> coily_event = null;

    #endregion


    public event Action<bool> On_coily_event
    {
        add
        {
            coily_event -= value;
            coily_event += value;
        }

        remove
        {
            coily_event -= value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //if (elevators_event_received != null)
        //{
        //    foreach (Elevator elevator in elevators_event_received)
        //    {
        //        elevator.On_elevator_event += ElevatorEventListener;
        //    }
        //}

        if (coily_event != null)
        {
            coily_event(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_already_moving && !are_we_at_bottom)
        {
            BallDecisionToMove();
        }

        if (general_movement.get_reach_destination)
        {
            is_already_moving = false;
        }

        if (are_we_at_bottom && !did_we_hatch)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = snake_sprite;
            did_we_hatch = true;
        }



        if (!is_already_moving && are_we_at_bottom && !has_coily_jump_off)
        {
            Snakebehave();
        }

        if (this.gameObject.transform.position.y <= -3.00f)
        {
            Destroy(this.gameObject);

        }

    }

    void BallDecisionToMove()
    {
        int random_number = UnityEngine.Random.Range(1, 100);

        // check where we can move
        // we have option
        if (random_number <= 50)
        {
            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);

        }
        else
        {
            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
        }

    }

    public void CoilyOffThePyramid()
    {
        if (coily_event != null)
        {
            coily_event(false);

            Debug.LogWarning("CoilyOffThePyramid");
        }

    }

    private void ElevatorEventListener(bool is_the_player_on_elvator, Transform target)
    {
        if (is_the_player_on_elvator)
        {
            // player is now is player last postion on the pyramid
            player = target;
            is_coily_in_death_mode = true;
        }
        else
        {
            /// switch back to the player
            if (!has_coily_jump_off)
            {
                player = target;

                is_coily_in_death_mode = false;
            }
        }

    }
    private void OnDestroy()
    {
        if (elevators_event_received != null)
        {
            foreach (Elevator elevator in elevators_event_received)
            {
                elevator.On_elevator_event -= ElevatorEventListener;
            }
        }

    }

    void Snakebehave()
    {
        Platform player_current_platform = null;

        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].get_is_player_current_this_platform)
            {
                player_current_platform = platforms[i];
            }
        }



        if (player_current_platform != null)
        {
            if (is_coily_in_death_mode && our_current_platform.get_is_player_current_this_platform)
            {


                if (general_movement.get_top_right_platform_position != null
                    && general_movement.get_top_right_platform_position.gameObject.tag == "redirection")
                {


                    is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                    is_coily_in_death_mode = false;
                    has_coily_jump_off = true;


                }
                else if (general_movement.get_top_left_platform_position != null &&
                    general_movement.get_top_left_platform_position.gameObject.tag == "redirection")
                {

                    is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                    is_coily_in_death_mode = false;
                    has_coily_jump_off = true;

                }

            }
            else
            {
                // if the player is above us
                if (player_current_platform.get_colum_id_number < current_platform_colum_id && !our_current_platform.get_is_player_current_this_platform)
                {
                    if (general_movement.get_top_left_platform_position == null && general_movement.get_top_right_platform_position != null)
                    {
                        is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                    }
                    else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position == null)
                    {
                        is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                    }
                    else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position != null)
                    {
                        if (Vector3.Distance(general_movement.get_top_right_platform_position.position, player.position) <
                            Vector3.Distance(general_movement.get_top_left_platform_position.position, player.position))
                        {

                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                        }
                        else
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                        }
                    }
                }// if the player is below us
                else if (player_current_platform.get_colum_id_number > current_platform_colum_id && !our_current_platform.get_is_player_current_this_platform)
                {

                    if (general_movement.get_bottom_left_platform_position == null && general_movement.get_bottom_right_platform_position != null)
                    {
                        is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
                    }
                    else if (general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position == null)
                    {
                        is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                    }
                    else if (general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position != null)
                    {
                        if (Vector3.Distance(general_movement.get_bottom_right_platform_position.position, player.position) <
                            Vector3.Distance(general_movement.get_bottom_left_platform_position.position, player.position))
                        {

                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
                        }
                        else
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                        }
                    }

                } // if player is on the same row/level as us
                else if (player_current_platform.get_colum_id_number == current_platform_colum_id && !our_current_platform.get_is_player_current_this_platform)
                {
                    /// this random_number is use when there way path to the player and it does matter which way to
                    int random_nubmer = UnityEngine.Random.Range(1, 10);

                    /// at the bottom and on same level as player
                    /// we want move up
                    if (current_platform_colum_id == bottom_colum_id && !our_current_platform.get_is_player_current_this_platform && !our_current_platform.get_is_player_current_this_platform)
                    {
                        if (general_movement.get_top_left_platform_position == null && general_movement.get_top_right_platform_position != null)
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                        }
                        else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position == null)
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                        }
                        else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position != null)
                        {
                            if (Vector3.Distance(general_movement.get_top_right_platform_position.position, player.position) <
                                Vector3.Distance(general_movement.get_top_left_platform_position.position, player.position))
                            {

                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                            }
                            else
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                            }
                        }
                    }// all possible directions are give to snake
                    else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position != null && general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position != null && !our_current_platform.get_is_player_current_this_platform)
                    {
                        // checking which one is closer to the player(left or right)
                        // than randmon numer that  decide top or botton
                        if (Vector3.Distance(general_movement.get_bottom_right_platform_position.position, player.position) <
                                 Vector3.Distance(general_movement.get_bottom_left_platform_position.position, player.position))
                        {
                            if (random_nubmer >= 5)
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
                            }
                            else
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                            }
                        }
                        else
                        {
                            if (random_nubmer >= 5)
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                            }
                            else
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                            }
                        }
                    }// everything movement options, but top left
                    else if (general_movement.get_top_left_platform_position == null && general_movement.get_top_right_platform_position != null && general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position != null)
                    {
                        if (Vector3.Distance(general_movement.get_bottom_right_platform_position.position, player.position) <
                                 Vector3.Distance(general_movement.get_bottom_left_platform_position.position, player.position))
                        {
                            if (random_nubmer >= 5)
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);
                            }
                            else
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_right);
                            }
                        }
                        else
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                        }
                    }// everything movement options, but top right
                    else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position == null && general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position != null)
                    {
                        if (Vector3.Distance(general_movement.get_bottom_right_platform_position.position, player.position) <
                                 Vector3.Distance(general_movement.get_bottom_left_platform_position.position, player.position))
                        {

                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_right);


                        }
                        else
                        {
                            if (random_nubmer >= 5)
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                            }
                            else
                            {
                                is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                            }
                        }
                    }

                }
            }

        }
    }



    public void GetPlayerPosition(Transform m_player)
    {
        player = m_player;
    }

    public void GetPlatforms(Platform m_platform)
    {
        platforms.Add(m_platform);
    }

    public void GetElevator(Elevator m_elevator)
    {
        elevators_event_received.Add(m_elevator);

        m_elevator.On_elevator_event += ElevatorEventListener;
    }

}
