using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionChildobject : MonoBehaviour
{
    [SerializeField] public GameObject parenent_object;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Rigidbody2D rb;




    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Snake"))
        {
            if (this.gameObject.CompareTag("Player"))
            {
                if (!parenent_object.GetComponent<PlayerController>().Is_go_to_elevator)
                {
                    Debug.Log("player's Childobject has touch snake");
                    this.gameObject.GetComponentInParent<PlayerController>().DeathByfoes();
                }
            }
        }

        if (col.gameObject.CompareTag("RedBall"))
        {
            if (this.gameObject.CompareTag("Player"))
            {
                if (!parenent_object.GetComponent<PlayerController>().Is_go_to_elevator) 
                {
                    Debug.Log("player's Childobject has touch Redball");
                    this.gameObject.GetComponentInParent<PlayerController>().DeathByfoes();
                }
            }
        }



        if (col.gameObject.CompareTag("Greenball"))
        {
            if (this.gameObject.CompareTag("Player"))
            {
                if (!parenent_object.GetComponent<PlayerController>().Is_go_to_elevator)
                {
                    Debug.Log("player's Childobject has touch Greenball");
                    this.gameObject.GetComponentInParent<PlayerController>().ChangeQberState(Qbert_Event_states.Ktouch_greenball);
                    Destroy(col.transform.parent.gameObject);
                }
            }
        }
    }





}
