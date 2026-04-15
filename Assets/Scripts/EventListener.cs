﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEvent : UnityEvent<Component, object[]> {};
public class EventListener : MonoBehaviour
{
    public EventSO gameEvent;

    public GameEvent response;


    void OnEnable()
    {
        gameEvent.addListener(this);
    }
    void OnDisable()
    {
        gameEvent.removeListener(this);
    }
    public void OnEventRaised(Component sender, params object[] data)
    {
        response.Invoke(sender, data);
    }
}