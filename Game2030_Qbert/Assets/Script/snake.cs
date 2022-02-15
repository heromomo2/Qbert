using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    #region Global
    [SerializeField] Sprite Ball_sprite;

    [SerializeField] Sprite snake_sprite;

    [SerializeField] GerenalMovement general_movement;

    [SerializeField] bool is_already_moving = false;

    public bool are_we_at_bottom = false;

    [SerializeField] List<Platform> platforms;

    [SerializeField] Transform  player;

    [SerializeField] float current_platform_colum_id;

    public float set_current_platform_colum_id 
    {
        set => current_platform_colum_id = value;
    }
    #endregion


    // public int G, h, f;


    // Start is called before the first frame update
    void Start()
    {
        
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

        if (are_we_at_bottom) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = snake_sprite;
        }

        if (!is_already_moving && are_we_at_bottom) 
        {
            Snakebehave();
        }
    }

    void BallDecisionToMove()
    {
        int random_number = Random.Range(1, 100);

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

    void Snakebehave()
    {
        Platform player_current_platform = null;

        for (int i =0; i < platforms.Count; i++) 
        {
           if(platforms[i].get_is_player_current_this_platform) 
           {
                player_current_platform = platforms[i];
           }
        }

        if (player_current_platform != null)
        {
            // if the player is above us
            if (player_current_platform.get_colum_id_number < current_platform_colum_id)
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
            else if (player_current_platform.get_colum_id_number > current_platform_colum_id)
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
            else if (player_current_platform.get_colum_id_number == current_platform_colum_id)
            {
                int random_nubmer = Random.Range(1, 10);
                /// at the bottom and on same level as player
                /// we want move up
                if ( current_platform_colum_id == 6)
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
                }// all dist
               else if (general_movement.get_top_left_platform_position != null && general_movement.get_top_right_platform_position != null && general_movement.get_bottom_left_platform_position != null && general_movement.get_bottom_right_platform_position != null)
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
                        if (random_nubmer >= 5)
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Kbottom_left);
                        }
                        else
                        {
                            is_already_moving = general_movement.SelectADirectionForTheMovement(GerenalMovement.Direction.Ktop_left);
                        }
                    }
                }// everything move, but top left
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
                }// everything move, but top right
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
