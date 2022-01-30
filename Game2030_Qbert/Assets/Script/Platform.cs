using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    #region Global Var
    [Header("Platform")]

    [SerializeField] bool is_first_platform;
    public bool get_is_first_platform // the Name property
    {
        get => is_first_platform;
    }
    public bool set_is_first_platform // the Name property
    {
        set => is_first_platform = false;
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

    [SerializeField] Transform our_position;

    [SerializeField] Transform top_left_platform_position;

    public Transform get_top_left_platform_position // the Name property
    {
        get => top_left_platform_position;  
    }

    [SerializeField] Transform top_right_platform_position;

    public Transform get_top_right_platform_position // the Name property
    {
        get => top_right_platform_position;
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

    [SerializeField] Sprite inner_platform_sprite_on;

    [SerializeField] Sprite platform_sprite_on;

    [SerializeField] SpriteRenderer spriterenderer;

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



