using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSystem : MonoBehaviour
{

    public delegate void OnEvent();
    List<string> Publishes = new List<string>();

    Dictionary<string, OnEvent> Subscribers;

    private static EventSystem _instance;
    public static EventSystem instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventSystem>();
            }
            return _instance;
        }

    }

    void Start()
    {
        StartCoroutine(EventUpdate());
    }

    public void Subsribe(string sPub, OnEvent onEvent)
    {
        if (Subscribers.ContainsKey(sPub.ToLower()))
            Subscribers[sPub.ToLower()] += onEvent;
        else 
        Subscribers.Add(sPub.ToLower(), onEvent);
    }
    public void AddPublishedEvent(string sPubEvent)
    {
        Publishes.Add(sPubEvent.ToLower());
    }


    public bool CheckForPublisher(string sPubEvent)
    {
        if(Publishes.Contains(sPubEvent.ToLower()))
        {
            return true;
        }
        return false;

    }

    IEnumerator EventUpdate()
    {
        while (true)
        {
            foreach (KeyValuePair<string, OnEvent> dSub in Subscribers)
                        if(CheckForPublisher(dSub.Key.ToLower()))            
                            if(dSub.Value != null)
                                 dSub.Value();


            Publishes.Clear();

            yield return null;
        }
    }

    


}
