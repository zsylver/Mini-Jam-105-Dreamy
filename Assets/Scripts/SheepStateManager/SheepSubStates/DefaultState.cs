using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : SheepState
{
//---------------------------------------------
// PUBLIC [S.NS], NOT in unity inspector         
//---------------------------------------------
    [System.NonSerialized]
    public JumpState jumpState;

    [System.NonSerialized]
    public PanicState panicState;

    [System.NonSerialized]
    public SleepyState sleepyState;

//---------------------------------------------
// PRIVATE, NOT in unity inspector
//---------------------------------------------
    float moveSpeed;
    bool panic;    
    bool timeToJump;

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
        if (timeToJump)
        {
            return jumpState;
        }

        transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

        return this;
    }
}
