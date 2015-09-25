using UnityEngine;
using System.Collections;

public class EventPubSub : MonoBehaviour
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

    /// <summary>
    /// Updates the EventSystem for a new subscriber
    /// </summary>
    /// <param name="sEvent">String of desided event to wait for</param>
    /// <param name="onEvent">Delegate of OnEvent Void to play when Event Trigger happens</param>
    protected void Subscribe(string sEvent, EventSystem.OnEvent onEvent)
    {
        esEventSystem.Subsribe(sEvent, onEvent);
    }

    /// <summary>
    /// Unsubscribes an event from the selected string
    /// </summary>
    /// <param name="sEvent">Target message</param>
    /// <param name="onEvent">onEvent intended to be removed</param>
    protected void UnSubscribe(string sEvent, EventSystem.OnEvent onEvent)
    {
        esEventSystem.RemoveSubscription(sEvent, onEvent);
    }

}
