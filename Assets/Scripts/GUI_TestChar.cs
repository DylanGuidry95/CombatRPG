using UnityEngine;
using System.Collections;

public class GUI_TestChar : MonoBehaviour
{
    //public GUI_partyMemberInfo pm;
    public GUI_HUD hud;
    public int health, mana;

	void Awake ()
    {
        health = 100;
        mana = 25;
        hud = FindObjectOfType<GUI_HUD>();

        hud.partyMembers.Add(gameObject);
        //hud.s_charName = gameObject.name;
        //hud.s_hpValue = health;
        //hud.s_mpValue = mana;
	}
	
}
