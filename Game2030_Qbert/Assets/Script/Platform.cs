using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Global Var
    [Header("Platform var")]

    [SerializeField] bool is_first_platform;

    public bool get_is_first_platform // the Name property
    {
        get => is_first_platform;
    }

    public bool set_is_first_platform // the Name property
    {
        set => is_first_platform = false;
    }

    [SerializeField] bool is_last_platform;
    public bool get_is_last_platform // the Name property
    {
        get => is_last_platform;
    }
    
    public bool set_is_last_platform // the Name property
    {
        set => is_last_platform = false;
    }

    [SerializeField]  bool is_inner_platform;

    [SerializeField] private bool has_been_step_on = false;

    public bool set_has_been_step_on // the Name property
    {
        set => has_been_step_on = value;
    }

    public bool get_has_been_step_on // the Name property
    {
        get => has_been_step_on ;
    }

    [SerializeField] bool is_player_current_this_platform = false;
    public bool get_is_player_current_this_platform// the Name property
    {
        get => is_player_current_this_platform;
    }

    public bool set_is_player_current_this_platform // the Name property
    {
        set => is_player_current_this_platform = value;
    }


    [SerializeField] float Colum_id_number;

    public float get_colum_id_number// the Name property
    {
        get => Colum_id_number;
    }
    #endregion

    [Header("Direction")]
    #region Direction
    [SerializeField] Transform top_left_platform_position;

    public Transform get_top_left_platform_position // the Name property
    {
        get => top_left_platform_position;  
    }
    public Transform set_top_left_platform_position // the Name property
    {
        set => top_left_platform_position = value;
    }

    [SerializeField] Transform top_right_platform_position;

    public Transform get_top_right_platform_position // the Name property
    {
        get => top_right_platform_position;
    }
    public Transform set_top_right_platform_position // the Name property
    {
        set => top_right_platform_position = value;
    }

    [SerializeField] Transform bottom_left_platform_position;
    public Transform get_bottom_left_platform_position // the Name property
    {
        get => bottom_left_platform_position;
    }

    [SerializeField] Transform bottom_right_platform_position;

    public Transform get_bottom_right_platform_position // the Name property
    {
        get => bottom_right_platform_position;
    }

    [Header("re Direction")]
    [SerializeField] Transform top_left_redirection_point;
    public Transform get_top_left_redirection_point // the Name property
    {
        get => top_left_redirection_point;
    }

    [SerializeField] Transform top_right_redirection_point;
    public Transform get_top_right_redirection_point // the Name property
    {
        get => top_right_redirection_point;
    }

    #endregion

    private Action <bool> platform_event = null;

    [Header("Animator")]
    #region Animator
    [SerializeField] private Animator platform_anim = null;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void PlayerOnPlatformChange() 
    {
        if (is_inner_platform && has_been_step_on) 
        {
            if (platform_anim != null)
            {
                platform_anim.SetTrigger("Step");

                if (platform_event != null)
                {
                    platform_event(true);
                }
            }

        }

        if (!is_inner_platform && has_been_step_on && !is_first_platform)
        {
            if (platform_anim != null)
            {
                platform_anim.SetTrigger("Step");
                if (platform_event != null)
                {
                    platform_event(true);
                }
            }
        }


    }

    public event Action<bool> On_platform_event
    {
        add
        {
            platform_event -= value;
            platform_event += value;
        }

        remove
        {
            platform_event -= value;
        }
    }

    public void GetPlatformToPlayWinAnimation()
    {
        if (platform_anim != null)
        {
            platform_anim.SetTrigger("win");
        }
    }
    public void GetPlatformStopPlayingWinAnimation()
    {
        if (platform_anim != null)
        {
            platform_anim.SetTrigger("Step");
        }
    }

}



