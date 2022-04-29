using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStateManager : TestMob
{    
    
    void Start()
    {
        // Empty by design
    }
   
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        SheepState nextState = currentState?.RunCurrentState();
        if(nextState != null)
        {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(SheepState nextStateVariable)
    {
        currentState = nextStateVariable;
    }
}
