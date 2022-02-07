using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Global var
    [SerializeField] BallDecision bd;

    [SerializeField] Movement general_movement;

    [SerializeField]  bool is_already_moving;

    [SerializeField] bool is_falling;
    #endregion
    #region Collidsion
    [SerializeField] private CircleCollider2D Cc;
    [SerializeField] private Rigidbody2D rb;
    #endregion

    #region Destination/Setter
    //[SerializeField] Transform bottom_left_platform_position;
   

    //[SerializeField] Transform bottom_right_platform_position;
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
       
    }
    void BallDecisionToMove() 
    {
        int random_number = Random.Range(1,2);

        // check where we can move
        // we have option
          switch (random_number)
          {
                case 1:
                // code block
                is_already_moving = general_movement.CanWeMoveThere(Movement.Direction.Kbottom_left);
                break;
                case 2:
                // code block
               is_already_moving = general_movement.CanWeMoveThere(Movement.Direction.Kbottom_right);
                break;
          }
        
    }
    


   

    
    #region Collision functions
    void OnTriggerEnter2D(Collider2D col)
    {
    
    }


    #endregion
}
#region Enums
public enum BallDecision
{
    Kbottom_right,
    Kbottom_left,
    Kno_movement
}
#endregion