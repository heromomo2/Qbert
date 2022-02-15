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

    public float set_current_platform_colum_id() 
    {
        set => current_platform_colum_id = values;
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

    void e()
    {
        Platform temp_platform;

        for (int i =0; i < platforms.Count; i++) 
        {
           if(platforms[i].get_is_player_current_this_platform) 
           {
                temp_platform = platforms[i];
           }
        }


    }
}
