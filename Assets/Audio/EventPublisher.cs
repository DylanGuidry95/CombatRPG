using UnityEngine;
using System.Collections;

public class EventPublisher : MonoBehaviour
{
    protected EventSystem esEventSystem;

    protected void Publish(string sEvent)
    {
        esEventSystem.AddPublishedEvent(sEvent);
    }

}
