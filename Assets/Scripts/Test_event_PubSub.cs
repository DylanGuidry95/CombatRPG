using UnityEngine;
using System.Collections;

public class Test_event_PubSub : EventPubSub
{

    void Start()
    {
        Subscribe("Input_W", keypressed); //Creates a subscription for "Input_W" and will eventualy play keypressed when the EventSystem gets a publification of the same characters (EventSystem Turns all upercase into lowercase)
    }

    void keypressed()
    {
        Publish("Test_event_PubSub_keypressed"); //Publishes a message to EventSystem as Test_event_PubSub_keypressess (As said above the EventSystem will turn the string to lower case)
    }

   

}
