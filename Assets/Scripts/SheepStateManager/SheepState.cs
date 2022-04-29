using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SheepState : MonoBehaviour
{
    public abstract void SetSpeed(float moveSpeedVariable);
    
    public abstract SheepState RunCurrentState();
}
