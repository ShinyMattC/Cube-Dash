﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class EventSO : ScriptableObject
{
    public List<EventListener> listeners = new List<EventListener>();
     
    /// <summary>
    /// Raises an event.
    /// </summary>
    /// <param name="sender">Component from which the event was raised</param>
    /// <param name="data">Any data that will (definitely) need to be sent</param>
    public void raise(Component sender, params object[] data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }
    public void addListener(EventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void removeListener(EventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}