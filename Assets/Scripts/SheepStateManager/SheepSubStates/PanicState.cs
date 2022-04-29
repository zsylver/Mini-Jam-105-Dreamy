using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicState : SheepState
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
    float panicDuration = 1.0f; // can be randomized later

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
        moveSpeed = moveSpeedVariable * 2.0f;
    }

    public override SheepState RunCurrentState()
    {
        panicDuration -= Time.deltaTime;

        if (panicDuration >= 0.0f)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            return this;
        }
        else
            return defaultState;
    }
}
