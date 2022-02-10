using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Global var
    
    [SerializeField] Movement general_movement;

    [SerializeField]  bool is_already_moving = false;

    [SerializeField] bool is_falling;

    [SerializeField] float delaytimer = 0;

    [SerializeField] float delaytime = 0;

    #endregion




    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_already_moving )
        {
            BallDecisionToMove();
        }

        if (general_movement.get_reach_destination) 
        {
            is_already_moving = false;
        }
         
        
        
       
    }
    void BallDecisionToMove() 
    {
        int random_number = Random.Range(1,100);

        // check where we can move
        // we have option
        if (random_number <= 50)
        {
            is_already_moving = general_movement.CanWeMoveThere(Movement.Direction.Kbottom_left);

        }
        else 
        {
            is_already_moving = general_movement.CanWeMoveThere(Movement.Direction.Kbottom_right);
        }
       

    }
    


}
