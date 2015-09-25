using UnityEngine;
using System.Collections;
using System;

public class EventSubscriber : MonoBehaviour
{
    
    protected EventSystem esEventSystem;

    protected void Subscribe(string sEvent, EventSystem.OnEvent onEvent)
    {
        esEventSystem.Subsribe(sEvent, onEvent);
    }

    
}
