using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemy : MonoBehaviour
{
    #region Global var
   
    [SerializeField] BallDecision bd;

    [SerializeField] bool are_we_done_moving ;

    [SerializeField] Vector2 ball_start_point;

    [SerializeField] Vector2 ball_end_point;

    #endregion
    #region Collidsion
    [SerializeField] private CircleCollider2D Cc;
    [SerializeField] private Rigidbody2D rb;
    #endregion
    #region Destination/Setter
    [SerializeField] Transform bottom_left_platform_position;
   

    [SerializeField] Transform bottom_right_platform_position;

    
    #endregion


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void BallDecisionToMove() 
    {
        int random_number = Random.Range(1,2);

        // check where we can move
        // we have option
        if (bottom_left_platform_position != null && bottom_right_platform_position != null) 
        {
            switch (random_number)
            {
                case 1:
                    // code block
                    bd = BallDecision.Kbottom_left;
                    break;
                case 2:
                    // code block
                    bd = BallDecision.Kbottom_right;
                    break;
            }
        }// only one option: right
        else if(bottom_left_platform_position == null && bottom_right_platform_position != null)
        {
            bd = BallDecision.Kbottom_right;
        }//only one option: left
        else if (bottom_left_platform_position != null && bottom_right_platform_position == null)
        {
            bd = BallDecision.Kbottom_left;
        }
        //We  have no option
        else if (bottom_left_platform_position == null && bottom_right_platform_position == null)
        {
            bd = BallDecision.Kno_movement;
        }
    }
    void MoveBall() 
    {
        
    }


    #region Collision functions
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            bottom_left_platform_position = col.GetComponent<Platform>().get_bottom_left_platform_position;
            bottom_right_platform_position = col.GetComponent<Platform>().get_bottom_right_platform_position;
        }
    }

    void ClearAllDestination()
    {
        bottom_left_platform_position = null;
        bottom_right_platform_position = null;
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