using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject game_Object = null;
    [SerializeField] private float m_timer = 2f;
    [SerializeField] private float m_time = 2f;
    [SerializeField] private bool is_active = true;
    void Start()
    {
         StartCoroutine(WaitAndAnimationArrow( m_time));
       // m_timer = m_time;
    }

    // Update is called once per frame
    void Update()
    {

        //if (game_Object != null)
        //{
        //    if (is_active == false)
        //    {
        //        m_timer -= Time.deltaTime;
        //        if (m_timer < 0)
        //        {
        //            is_active = true;
        //            game_Object.GetComponent<Image>().enabled = true; 
        //            m_timer = m_time;
        //            Debug.Log("arrow is showing");
        //        }
               
                
        //    }
        //    else if(is_active == true)
        //    {
               
        //        m_timer -= Time.deltaTime;
        //        if (m_timer < 0)
        //        {
        //            is_active = false;
        //            game_Object.GetComponent<Image>().enabled = false;
        //            m_timer = m_time;
        //            Debug.Log("arrow is hiding");
        //        }

        //    }

        //}
    }

    IEnumerator WaitAndAnimationArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (game_Object != null)
        {
            if (is_active == false)
            {
                is_active = true;
                game_Object.GetComponent<Image>().enabled = true;
                StartCoroutine(WaitAndAnimationArrow(m_time));
            }
            else if(is_active == true)
            {
                is_active = false;
                game_Object.GetComponent<Image>().enabled = false;
                StartCoroutine(WaitAndAnimationArrow(m_time));
            }

        }
    }

    public void StopArrow()
    {
        StopAllCoroutines();
        is_active = false;
        game_Object.GetComponent<Image>().enabled = false;
        
    }
}
