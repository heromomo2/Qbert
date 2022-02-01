using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    #region Globla var
    [SerializeField] bool is_player_on_elevator;
    [SerializeField] bool reach_top_pyramid;
    [SerializeField] Transform cirect_transform;
    [SerializeField] Transform first_platform_position;

  
    public float travel_time;
    public float drop_time;
    #endregion

    #region Prviate var
    private float timer = 0;
    [SerializeField] GameObject player_child;
    private Vector3 top_of_pyramid_vector_3;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (is_player_on_elevator && !reach_top_pyramid) 
        {
            MoveElevator();
        }
        else if (!is_player_on_elevator && reach_top_pyramid)
        {
           DropPlayerDownToFirstPlatform();
        }
    }


    public void PlayerOnElevator(GameObject player)
    {
        if ( player.tag == "Player")
        {
            player.transform.parent = cirect_transform.parent;
            
            is_player_on_elevator = true;
            reach_top_pyramid = false;

            player_child = player;

            top_of_pyramid_vector_3 = new Vector3 (first_platform_position.position.x, (first_platform_position.position.y + 1.5f), first_platform_position.position.z);
        }

    }

    public void MoveElevator()
    {
        timer += Time.deltaTime;

        float ratio = timer / travel_time;
        ratio = Mathf.Clamp01(ratio);



        if (ratio < 1)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, top_of_pyramid_vector_3 , ratio);
        }
        else
        {
            Debug.Log("We at top of pyramid and elevator");
            is_player_on_elevator = false;
            reach_top_pyramid = true;

        }

    }


    void DropPlayerDownToFirstPlatform()
    {
        timer += Time.deltaTime;

        float ratio = timer / drop_time;

        player_child.transform.parent = null;

        Destroy(this.gameObject);

        player_child.transform.position = Vector3.Lerp(player_child.transform.position, first_platform_position.position, ratio);

        Debug.Log("Destroy the Elevator");
        
        
    }
}
