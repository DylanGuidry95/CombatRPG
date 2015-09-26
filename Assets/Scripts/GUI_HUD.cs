using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUI_HUD : MonoBehaviour
{
    public GameObject GUI_partyMember;
    public List<GameObject> GUI_partyElements = new List<GameObject>();
    public Button attackButton, waitButton;
    public string s_charName;
    public int s_hpValue, s_mpValue;
    public List<GameObject> partyMembers = new List<GameObject>();

    void Awake()
    {
        Vector3 pos = new Vector3(-155, 54, 0);
        for (int i = 0; i < partyMembers.Count; ++i)
        {            
            GameObject go = Instantiate(GUI_partyMember,new Vector3(pos.x, pos.y - (i*15), pos.z), Quaternion.identity) as GameObject;
            go.transform.SetParent(gameObject.transform, false);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            GUI_partyElements.Add(go);
        }
    }

    void Start()
    {
        for (int i = 0; i < partyMembers.Count; ++i)
        {
            //set the properties of the members in the party to the corresponding GUI_partyMemberInfo canvas object
            GUI_partyElements[i].gameObject.GetComponent<GUI_partyMemberInfo>().text_charName.text = partyMembers[i].gameObject.name;
            GUI_partyElements[i].gameObject.GetComponent<GUI_partyMemberInfo>().text_hpValue.text = " " + partyMembers[i].gameObject.GetComponent<GUI_TestChar>().health;
            GUI_partyElements[i].gameObject.GetComponent<GUI_partyMemberInfo>().text_mpValue.text = " " + partyMembers[i].gameObject.GetComponent<GUI_TestChar>().mana;
        }
    }

    void Update()
    {
        for (int i = 0; i < partyMembers.Count; ++i)
        {
            GUI_partyElements[i].gameObject.GetComponent<GUI_partyMemberInfo>().text_hpValue.text = " " + partyMembers[i].gameObject.GetComponent<GUI_TestChar>().health;
            GUI_partyElements[i].gameObject.GetComponent<GUI_partyMemberInfo>().text_mpValue.text = " " + partyMembers[i].gameObject.GetComponent<GUI_TestChar>().mana;
        }
    }
    
}
