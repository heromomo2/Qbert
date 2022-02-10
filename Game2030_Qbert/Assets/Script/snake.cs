using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    [SerializeField] Sprite Ball_sprite;

    [SerializeField] Sprite snake_sprite;

    [SerializeField] GerenalMovement general_movement;

    [SerializeField] bool is_already_moving = false;

    public bool are_we_at_bottom = false;



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
}
