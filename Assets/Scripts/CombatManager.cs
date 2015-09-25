using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    FSM<CombatStates> _fsm;
    public List<GameObject> Fighters;

    private static CombatManager _instance;

    public static CombatManager Instance
    {
        get
        {
            return _instance;
        }
    }

    enum CombatStates
    {
        eInit,
        eCheckActions,
        ePerformActions,
        eCheckConditions,
        eExit
    }
	
    void Awake()
    {
        _fsm = new FSM<CombatStates>();
        _instance = this;
    }

    void Start()
    {
        StartCoroutine(Transitioning());
    }

    void AddStates()
    {
        _fsm.AddState(CombatStates.eInit);
        _fsm.AddState(CombatStates.eCheckActions);
        _fsm.AddState(CombatStates.ePerformActions);
        _fsm.AddState(CombatStates.eCheckConditions);
        _fsm.AddState(CombatStates.eExit);
    }

    void AddTransitions()
    {
        _fsm.AddTransition(CombatStates.eInit, CombatStates.eCheckActions);
        _fsm.AddTransition(CombatStates.eCheckActions, CombatStates.ePerformActions);
        _fsm.AddTransition(CombatStates.ePerformActions, CombatStates.eCheckConditions);
        _fsm.AddTransition(CombatStates.eCheckConditions, CombatStates.eExit);
        _fsm.AddTransition(CombatStates.eCheckConditions, CombatStates.eCheckActions);
    }

    IEnumerator Transitioning()
    {
        while(Fighters != null)
        {
            _transitions += CheckStates;
            _transitions();
            yield return null;
        }
    }

    delegate void Transitions();
    Transitions _transitions;

    void CheckStates()
    {
        if (_transitions != null)
            _transitions = null;

        switch(_fsm.state)
        {
            case CombatStates.eInit:
                {
                    _transitions += InitToCheckAction;
                    break;
                }
            case CombatStates.eCheckActions:
                {
                    _transitions += CheckActionToPerformAction;
                    break;
                }
            case CombatStates.ePerformActions:
                {
                    _transitions += PerformActionToCheckConditions;
                    break;
                }
            case CombatStates.eCheckConditions:
                {
                    _transitions += CheckConditionsToCheckAction;
                    break;
                }
            case CombatStates.eExit:
                {
                    _transitions += CheckConditionToExit;
                    break;
                }
        }
        if (_transitions != null)
            _transitions();
    }

    void InitToCheckAction()
    {
        _fsm.Transition(CombatStates.eInit, CombatStates.eCheckActions);
    }

    void CheckActionToPerformAction()
    {
        _fsm.Transition(CombatStates.eCheckActions, CombatStates.ePerformActions);
    }

    void PerformActionToCheckConditions()
    {
        _fsm.Transition(CombatStates.ePerformActions, CombatStates.eCheckConditions);
    }
    void CheckConditionToExit()
    {
        _fsm.Transition(CombatStates.eCheckConditions, CombatStates.eExit);
    }

    void CheckConditionsToCheckAction()
    {
        _fsm.Transition(CombatStates.eCheckConditions, CombatStates.eExit);
    }
}
