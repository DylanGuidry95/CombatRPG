using UnityEngine;
using System.Collections;

public class EventPubSub : EventPublisher
{


    protected void Subscribe(string sEvent, EventSystem.OnEvent onEvent)
    {
        esEventSystem.Subsribe(sEvent, onEvent);
    }
}
