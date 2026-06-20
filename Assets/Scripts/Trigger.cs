using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trigger : Block
{
    public int targetGroupID;
    public int targetInstanceID;
    
    public EventSO onModifierTriggerEntered;

    public bool useGroup, useInstance;
}
