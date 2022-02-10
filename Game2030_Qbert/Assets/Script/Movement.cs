using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  

    #region Direction
    public enum Direction
    {
        Kbottom_right,
        Kbottom_left,
        Ktop_right,
        Ktop_left,
        Kno_direction,
        Kfall_start,
        Kfall_action,
    }

    public Direction current_direction;

    public Vector2 start_position;

    public Vector2 tagert_position;

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

    [SerializeField] Transform top_left_platform_position;
    public Transform set_top_left_platform_position// the Name property
    {
        set => top_left_platform_position = value;
    }


    [SerializeField] Transform top_right_platform_position;

    public Transform set_top_right_platform_position// the Name property
    {
        set => top_right_platform_position = value;
    }

    public bool reach_destination = false;

    public bool get_reach_destination// the Name property
    {
        get => reach_destination;
    }
    #endregion


    #region
    [SerializeField] AnimationCurve curve_start_to_mid;
    [SerializeField] AnimationCurve curve_mid_to_end;
    [SerializeField] AnimationCurve curve_fall;

    public float jump_time = 1;
    public float fall_time = 1;

    public float jump_timer = 0;

    public float fall_timer = 0;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        switch (current_direction)
        {
            case Direction.Kbottom_right:
                if (!reach_destination)
                {
                    MovementOnPlatform();
                }
                break;

            case Direction.Kbottom_left:
                // code block
                if (!reach_destination)
                {
                    MovementOnPlatform();
                }
                break;
            case Direction.Ktop_left:
                if (!reach_destination)
                {
                    MovementOnPlatform();
                }
                break;

            case Direction.Ktop_right:
                // code block
                if (!reach_destination)
                {
                    MovementOnPlatform();
                }
                break;

            case Direction.Kno_direction:
                if (reach_destination)
                {
                    reach_destination = false;
                }
                break;

            case Direction.Kfall_action:
                FallingMovement(start_position, tagert_position);
                break;
        }


    }

    public bool CanWeMoveThere(Direction d)
    {
        if (d == Direction.Ktop_left)
        {
            if (top_left_platform_position != null)
            {
               
                current_direction = Direction.Ktop_left;
                start_position = this.gameObject.transform.position;
                tagert_position = new Vector2(top_left_platform_position.position.x, top_left_platform_position.position.y);

                ClearAllDestination();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (d == Direction.Ktop_right)
        {
            if (top_right_platform_position != null)
            {
               
                current_direction = Direction.Ktop_right;
                start_position = this.gameObject.transform.position;
                tagert_position = new Vector2(top_right_platform_position.position.x, top_right_platform_position.position.y);

                ClearAllDestination();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (d == Direction.Kbottom_right)
        {
            if (bottom_right_platform_position != null)
            {
                

                current_direction = Direction.Kbottom_right;
                start_position = this.gameObject.transform.position;
                tagert_position = new Vector2(bottom_right_platform_position.position.x, bottom_right_platform_position.position.y);

                ClearAllDestination();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (d == Direction.Kbottom_left)
        {
            if (bottom_left_platform_position != null)
            {
               

                current_direction = Direction.Kbottom_left;
                start_position = this.gameObject.transform.position;
                tagert_position = new Vector2(bottom_left_platform_position.position.x, bottom_left_platform_position.position.y);

               ClearAllDestination();
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void landPostion(Transform targert_land, Transform spawn_start_positon)
    {
        
        start_position = spawn_start_positon.position;
        tagert_position = targert_land.position;
        current_direction = Direction.Kfall_action;
        reach_destination = false;

    }

    void MovementOnPlatform()
    {
        Vector2 start_position_vector_2 = new Vector2(start_position.x, start_position.y);
        Vector2 target_position_vector_2 = tagert_position;

        Vector3 mid_position_vector_3 = target_position_vector_2;

        mid_position_vector_3.y += 0.7f;


        // timer is increase
        jump_timer += Time.deltaTime;

        float ratio = jump_timer / jump_time;

        ratio = Mathf.Clamp01(ratio);

        if (ratio < 1)
        {
            this.gameObject.transform.position = Bezier(ratio, start_position, mid_position_vector_3, target_position_vector_2);
        }
        // when we get to our new location
        else
        {
            this.gameObject.transform.position = target_position_vector_2;

            reach_destination = true;
            current_direction = Direction.Kno_direction;



            // reset timer one it's bigger than jump time
            if (jump_timer >= jump_time)
            {
                jump_timer = 0;
            }

        }


    }
    /// <summary>
    ///  arc / mulit lerp
    /// </summary>
    /// <param name="ratio"></param>
    /// <param name="start"></param>
    /// <param name="mid"></param>
    /// <param name="end"></param>
    /// <returns></returns>

    Vector3 Bezier(float ratio, Vector2 start, Vector2 mid, Vector2 end)
    {
        ratio = Mathf.Clamp01(ratio);


        Vector2 start_to_mid = Vector3.Lerp(start, mid, curve_start_to_mid.Evaluate(ratio));


        Vector2 mid_to_end = Vector3.Lerp(mid, end, curve_mid_to_end.Evaluate(ratio));

        return Vector3.Lerp(start_to_mid, mid_to_end, ratio);
    }

    /// <summary>
    /// one lerp
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void FallingMovement(Vector2 start, Vector2 end)
    {
        fall_timer += Time.deltaTime;

        float ratio = fall_timer / fall_time;

        

        ratio = Mathf.Clamp01(ratio);

        if (ratio < 1)
        {

            this.gameObject.transform.position = Vector3.Lerp(start, end, curve_fall.Evaluate(ratio));
        }
        else
        {
            this.gameObject.transform.position = end;

            current_direction = Direction.Kno_direction;

            if (fall_timer >= fall_time)
            {
                fall_timer = 0;
            }

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
