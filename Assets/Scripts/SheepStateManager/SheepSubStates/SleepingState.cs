using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : SheepState
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
    float sleepDuration = 3.0f; // can be randomized later

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
        moveSpeed = 0;
    }

    public override SheepState RunCurrentState()
    {
        sleepDuration -= Time.deltaTime;

        // can apply sleeping animation here
        if (sleepDuration >= 0)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            return this;
        }
        else        
            return defaultState;        
    }
}
