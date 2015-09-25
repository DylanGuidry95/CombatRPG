using UnityEngine;
using System.Collections;

public class Test_event_Publisher : EventPublisher
{

    // Use this for initialization
    void Start()
    {

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           // Debug.Log("Key W pressed");
            Publish("Input_W");
        }
        
        else if(Input.GetKeyDown(KeyCode.Q))
            Publish("Input_Q");
    }


}
