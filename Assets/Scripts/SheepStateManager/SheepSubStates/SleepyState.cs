using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyState : SheepState
{
//---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------
    [System.NonSerialized]
    public SleepingState sleepingState;

//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    float moveSpeed;
    float sleepyDuration = 2.0f; // can be randomized later

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
        moveSpeed = moveSpeedVariable * 0.5f;
    }

    public override SheepState RunCurrentState()
    {
        sleepyDuration -= Time.deltaTime;

        if (sleepyDuration >= 0.0f)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            return this;
        }
        else        
            return sleepingState;                
    }
}
