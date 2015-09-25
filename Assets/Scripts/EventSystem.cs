using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour
{
    /// <summary>
    /// Void Delegate with No Params
    /// </summary>
    public delegate void OnEvent();

    /// <summary>
    /// List of "Published" strings
    /// </summary>
    List<string> Publishes = new List<string>();

    /// <summary>
    /// Dictionary of Subscribers
    /// String value is what message the "Subscriber" is looking for
    /// OnEvent is the function the "Subscriber" wants played in its corresponding event
    /// </summary>
    Dictionary<string, OnEvent> Subscribers = new Dictionary<string, OnEvent>();


    void Awake()
    {
        StartCoroutine(EventUpdate());
    }

    /// <summary>
    /// Adds a String and OnEvent to the Susbribers Dictionary
    /// </summary>
    /// <param name="sPub"> As stated before. The awaited message from publisher</param>
    /// <param name="onEvent">OnEvent functions to be called when message is later resieved</param>
    public void Subsribe(string sPub, OnEvent onEvent)
    {
        if (Subscribers != null && Subscribers.ContainsKey(sPub.ToLower()))
            Subscribers[sPub.ToLower()] += onEvent;
        else
            Subscribers.Add(sPub.ToLower(), onEvent);
    }
    /// <summary>
    /// Adds message to the List to later be used to invoke Subscriber funcitons
    /// </summary>
    /// <param name="sPubEvent"></param>
    public void AddPublishedEvent(string sPubEvent)
    {
        Publishes.Add(sPubEvent.ToLower());
    }

    public void RemoveSubscription(string sPub, OnEvent onEvent)
    {
        if(Subscribers.ContainsKey(sPub) && Subscribers[sPub] != null)
        Subscribers[sPub] -= onEvent;

    }


    /// <summary>
    ///  Bool used to reduce code use in EventUpdate()
    /// Checks to see if message exists inside of the "Published" List
    /// </summary>
    /// <param name="sPubEvent"></param>
    /// <returns></returns>
    bool CheckForPublisher(string sPubEvent)
    {
        if (Publishes.Contains(sPubEvent.ToLower()))
        {
            return true;
        }
        return false;

    }

    IEnumerator EventUpdate()
    {
        while (true)
        {
            foreach (KeyValuePair<string, OnEvent> dSub in Subscribers) //Sift threw Dictionary
                if (CheckForPublisher(dSub.Key.ToLower()))       //Checks to see if there is a subscriber for each published in list
                    if (dSub.Value != null)                     //Checks for null
                        dSub.Value();                         //Plays Delegate


            Publishes.Clear();

            yield return null;
        }
    }




}
