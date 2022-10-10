using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Vertical";
    public string rotateAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";
    public string grenadeButtonName = "GRB1";
    //public string rolling = "Roll";   ±¸¸£±â

    public float move{get; private set;}
    public float rotate { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }

    public bool grenade { get; private set; }

    private void Update()
    {
       if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            //grenade = false;

            return;
        }
        move = Input.GetAxis(moveAxisName);
        //rotate = Input.GetAxis(rotateAxisName);
        //fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
        //grenade = CrossPlatformInputManager.GetButton(grenadeButtonName);
        //grenade = Input.GetButtonDown(grenadeButtonName);

    }
}
