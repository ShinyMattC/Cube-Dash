using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Triggertype
{
    Move,
    Rotate
}
public class TriggerItem : MonoBehaviour
{
    public Triggertype triggertype;

    [Header("Move Trigger")]
    public Vector3 targetPosition;
    
    [Header("Rotate Trigger")]
    public float rotateH ;
    public float rotateV;
    


    [Header("Base Trigger")]
    public int targetGroupID;
    public int targetInstanceID;
    public int timeInSeconds;
    
    public EventSO onModifierTriggerEntered;

    public bool useGroup, useInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoTrigger(Component sender, params object[] data)
    {
        if(sender is MovePlayer && (string)data[3] == this.tag)
        {
            switch(triggertype)
        {
            case Triggertype.Move:
                MoveTrigger mt = new MoveTrigger
                {
                    onModifierTriggerEntered = onModifierTriggerEntered
                };
                if (useGroup)
                {
                    mt.targetGroupID = targetGroupID;
                    mt.useGroup = true;
                    mt.useInstance = !mt.useGroup;
                    mt.targetPosition = targetPosition;
                    mt.timeInSeconds = timeInSeconds;
                    mt.onModifierTriggerEntered.raise(this, triggertype, useGroup, useInstance, mt.targetGroupID, mt.targetPosition, mt.timeInSeconds);
                }
                else if (useInstance)
                {
                    mt.targetInstanceID = targetInstanceID;
                    mt.useGroup = false;
                    mt.useInstance = !mt.useGroup;
                    mt.targetPosition = targetPosition;
                    mt.timeInSeconds = timeInSeconds;
                    mt.onModifierTriggerEntered.raise(this, triggertype, useGroup, useInstance, mt.targetInstanceID, mt.targetPosition, mt.timeInSeconds);
                    
                }
                
                
            break;
            case Triggertype.Rotate:
            break;
        }
        }

        

    }
}
