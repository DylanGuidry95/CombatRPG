using UnityEngine;
using System.Collections;

public class Test_event_Subscriber : EventSubscriber
{

    // Use this for initialization
    void Start()
    {
        Subscribe("Test_event_PubSub_keypressed", keyw);

        Subscribe("Input_Q", keyany);
        Subscribe("Input_W", keyw);
        
    }


    void keyw()
    {
        print("Key W was pressed");
    }
    void keyany()
    {
        print("Some random Key was Hit..or not hit... i realy do not know. what will cause this XD");
    }


}
