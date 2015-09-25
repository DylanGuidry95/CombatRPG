using UnityEngine;
using System.Collections;

public class EventPublisher : MonoBehaviour
{
    [SerializeField]
    protected EventSystem esEventSystem;

    /// <summary>
    /// Notifies EventSystem of a publish
    /// </summary>
    /// <param name="sEvent">Message for the EventSystem to know what subscribers to start</param>
    protected void Publish(string sEvent)
    {
        esEventSystem.AddPublishedEvent(sEvent);
    }

}
