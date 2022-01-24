using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Global Var
    [Header("Platform")]

    [SerializeField]  bool is_inner_platform;

    [SerializeField] bool has_been_step_on;

    [SerializeField] Transform our_position;

    [SerializeField] Transform top_left_platform_position;

    [SerializeField] Transform top_right_platform_position;

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
}
