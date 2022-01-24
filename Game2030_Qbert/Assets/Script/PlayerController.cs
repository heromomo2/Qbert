using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // player movement key
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Debug.Log("Move top-left-> 7");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Debug.Log(" Move top-right-> 9");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Move bottom-left-> 1");
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Debug.Log("Move bottom-right-> 3");
        }
    }


}
