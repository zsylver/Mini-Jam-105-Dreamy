using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : SheepState
{
    //---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------
    [System.NonSerialized]
    public DefaultState defaultState;

//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    float moveSpeed;
    float jumpDuration = 1.0f; // can be randomized later

//---------------------------------------------
// PUBLIC, SHOW in unity inspector
//---------------------------------------------
//---------------------------------------------
// PRIVATE [SF], SHOW in unity inspector
//--------------------------------------------- 

//---------------------------------------------
// FUNCTIONS
//---------------------------------------------       
    public override void SetSpeed(float moveSpeedVariable)
    {
        moveSpeed = moveSpeedVariable;
    }

    public override SheepState RunCurrentState()
    {
        jumpDuration -= Time.deltaTime;

        if (jumpDuration >= 0.0f)
        {
            // y value's "moveSpeed" variable can be changed to specified randomized jumpHeight for balancing later
            transform.Translate(moveSpeed * Time.deltaTime, moveSpeed * Time.deltaTime, 0);
            return this;
        }
        else        
            return defaultState;                       
    }
}
