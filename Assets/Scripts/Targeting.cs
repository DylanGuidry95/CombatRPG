using UnityEngine;
using System.Collections;

public class Targeting : EventPubSub
{
    
    public GameObject Target;
	// Use this for initialization
	void Start ()
    {
        //Subscribe("Combat_Manager_Select_Action", Target);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        if(Input.GetMouseButtonUp(0))
        {
            if(hit.transform && hit.transform.gameObject.GetComponent<Enemy>())
            Target = hit.transform.gameObject;
            Publish("Targeting_Target");
        }
    }
}

/*


public GameObject Target;

    Subscribe(PPLAKJFD:LKFDJ:LKJLKFJSDA:LFKJA:LKJF:LASKDJF:K, OnTargetedGui)
    void OnTargetedGui()
    {
    //Once gui signuls target the player will later do this
    GameObject.gui.target
    }

    void update()
    {

    Hit Target

    }



*/