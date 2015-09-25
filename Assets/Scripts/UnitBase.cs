using UnityEngine;
using System.Collections;
using System;

public class UnitBase : EventPubSub, IUnit
{
    FSM<UnitStates> _fsm;
    GameObject Target;

    private static UnitBase _instance;

    public static UnitBase Instance
    {
        get
        {
            return _instance;
        }
    }

    enum UnitStates
    {
        eInit,
        eIdle,
        eCombat,
        eDead,
    }

    void Awake()
    {
        _fsm = new FSM<UnitStates>();
        AddStates();
        AddTransitions();
        _instance = this;
    }
    // Use this for initialization
    void Start ()
    {
        _transitions += CheckState;
	}
	
    void AddStates()
    {
        _fsm.AddState(UnitStates.eInit);
        _fsm.AddState(UnitStates.eIdle);
        _fsm.AddState(UnitStates.eCombat);
        _fsm.AddState(UnitStates.eDead);
    }

    void AddTransitions()
    {
        _fsm.AddTransition(UnitStates.eInit, UnitStates.eIdle);
        _fsm.AddTransition(UnitStates.eIdle, UnitStates.eCombat);
        _fsm.AddTransition(UnitStates.eCombat, UnitStates.eIdle);
        _fsm.AddTransition(UnitStates.eCombat, UnitStates.eDead);
    }

	// Update is called once per frame
	IEnumerator Transitioning()
    {
        while(true)
        {
            
            yield return null;
        }
    }

    delegate void Transitions();
    Transitions _transitions;

    void CheckState()
    {
        switch(_fsm.state)
        {
            case UnitStates.eInit:
                {
                    _fsm.Transition(_fsm.state, UnitStates.eIdle);
                    break;
                }
            case UnitStates.eIdle:
                {

                    break;
                }
            case UnitStates.eCombat:
                {
                    break;
                }
            case UnitStates.eDead:
                {
                    Destroy(this.gameObject);
                    break;
                }
        }
    }

    public void OnReady()
    {
        Publish("Unit_Ready_For_Combat");
    }

    public void OnAttack()
    {
        Debug.Log("Hit target for 2 dmg");
    }

    public void OnWait()
    {
        Debug.Log("Do nothing this turn");
    }

    public void OnUseItem()
    {
        Debug.Log("USe item to heal for 5 hp");
    }
}
