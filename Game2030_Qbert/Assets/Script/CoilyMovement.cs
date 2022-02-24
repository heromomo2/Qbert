using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyMovement : GerenalMovement
{
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void FallingMovement(Vector2 start, Vector2 end)
    {
        fall_timer += Time.deltaTime;

        fall_timer = Mathf.Clamp(base.fall_timer, 0, base.fall_time);

        float fall_ratio = fall_timer / fall_time;


        fall_ratio = Mathf.Clamp01(fall_ratio);

        this.gameObject.GetComponent<Collider2D>().enabled = false;

        if (fall_ratio < 1)
        {
            this.gameObject.transform.position = Vector3.Lerp(start, end, curve_fall.Evaluate(fall_ratio));

        }
        else
        {
            this.gameObject.transform.position = end;

            current_direction = Direction.Kno_direction;

            base.fall_timer = 0;

            this.gameObject.GetComponent<Collider2D>().enabled = true;
        }
    }


}
