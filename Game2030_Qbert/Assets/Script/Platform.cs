using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] Sprite inner_platform_sprite_on;

    [SerializeField] Sprite platform_sprite_on;

    [SerializeField] SpriteRenderer spriterenderer;

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

    [Header("Death Direction")]
    [SerializeField] Transform top_left_death_platform_position;
    public Transform get_top_left_death_platform_position // the Name property
    {
        get => top_left_death_platform_position;
    }

    [SerializeField] Transform top_right_death_platform_position;
    public Transform get_top_right_death_platform_position // the Name property
    {
        get => top_right_death_platform_position;
    }

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
            if (spriterenderer != null)
            {
                spriterenderer.sprite = inner_platform_sprite_on;
            }
        }

        if (!is_inner_platform && has_been_step_on && !is_first_platform)
        {
            if (spriterenderer != null)
            {
                spriterenderer.sprite = platform_sprite_on;
            }
        }
        

    }


}



