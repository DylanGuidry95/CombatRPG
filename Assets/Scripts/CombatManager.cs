using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    FSM<CombatStates> _fsm;
    List<GameObject> Fighters;
    List<GameObject> AlliesTeam;
    List<GameObject> EnemiesTeam;

    int iUnitsReady; //keeps track of how may players have selected an actions

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
        eSearch,
        eCheckActions,
        ePerformActions,
        eCheckConditions,
        eExit
    }
	
    void Awake()
    {
        _fsm = new FSM<CombatStates>();
        AddStates();
        AddTransitions();
        _instance = this;
    }

    void Start()
    {
        _transitions += CheckStates;
        StartCoroutine("Transitioning");
    }

    void AddStates()
    {
        _fsm.AddState(CombatStates.eInit);
        _fsm.AddState(CombatStates.eSearch);
        _fsm.AddState(CombatStates.eCheckActions);
        _fsm.AddState(CombatStates.ePerformActions);
        _fsm.AddState(CombatStates.eCheckConditions);
        _fsm.AddState(CombatStates.eExit);
    }

    void AddTransitions()
    {
        _fsm.AddTransition(CombatStates.eInit, CombatStates.eSearch);
        _fsm.AddTransition(CombatStates.eSearch, CombatStates.eCheckActions);
        _fsm.AddTransition(CombatStates.eCheckActions, CombatStates.ePerformActions);
        _fsm.AddTransition(CombatStates.ePerformActions, CombatStates.eCheckConditions);
        _fsm.AddTransition(CombatStates.eCheckConditions, CombatStates.eExit);
        _fsm.AddTransition(CombatStates.eCheckConditions, CombatStates.eCheckActions);
        _fsm.AddTransition(CombatStates.eExit, CombatStates.eSearch);
    }

    public IEnumerator Transitioning()
    {
        while(true)
        {
            if(_transitions != null)
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
                    ChangeState(CombatStates.eSearch);
                    break;
                }
            case CombatStates.eSearch:
                {
                    if(MaxFighters())
                    {
                        ChangeState(CombatStates.eCheckActions);
                    }
                    break;
                }

            case CombatStates.eCheckActions:
                {
                    /*
                        loops through all the fighters in the list
                        if they have all selected an action we will 
                        change state
                    */
                    if (ActionsSelected())
                    {
                        ChangeState(CombatStates.ePerformActions);
                    }
                    break;
                }

            case CombatStates.ePerformActions:
                {
                    iUnitsReady = 0;
                    ChangeState(CombatStates.eCheckConditions);
                    break;
                }

            case CombatStates.eCheckConditions:
                {
                    /*
                        Checks the see if the wincondition has been met and if it has
                        Combat is ended
                        If it hasn't we start the combat phase all over again at the 
                        select action state
                    */
                    if(WinCondition())
                        ChangeState(CombatStates.eExit);

                    else
                        ChangeState(CombatStates.eCheckActions);

                    break;
                }

            case CombatStates.eExit:
                {
                    LeaveCombat();
                    if(Fighters == null)
                        ChangeState(CombatStates.eSearch);
                    break;
                }
            default:
                break;
        }
        if(_transitions != null)
            _transitions();
    }

    void AttackOrder()
    {
        GameObject temp;
        foreach(GameObject enemy in EnemiesTeam)
        {
            //if(enemy.GetComponent<Enemy>().)
        }
    }
    void FighterKilled()
    {
        /*
            When a fighters HP hits 0 the Combat manager will be notifed 
            and remove the character from combat
        */
    }

    bool WinCondition()
    {
        /*
            If a team is out of members the other team will win

            When a teams size is = null then the combat manager should be
            notified and the winner wil be declared as the other team with remaining 
            members
        */
        if ((EnemiesTeam.Capacity == 0 && AlliesTeam.Capacity > 0) ||
            (AlliesTeam.Capacity == 0 && EnemiesTeam.Capacity > 0))
            return true;
        else
            return false;
    }

    bool ActionsSelected()
    {
        /*
            as fighters select there action they get flagged as having thier 
            action selected. When every character has readied up the combat system
            will perform the actions
        */
        if(iUnitsReady == 4)
        {
            return true;
        }
        else
        {
            iUnitsReady += 1;
            return false;
        }

    }

    bool MaxFighters()
    {
        if (Fighters.Capacity == 4)
        {
            foreach(GameObject fighter in Fighters)
            {
                /*
                    checks the list then breaks the in units into thier
                    appropriate team based on what scripts are attached to
                    them
                */
            }
            return true;
        }
        else
            return false;
    }

    void LeaveCombat()
    {
        foreach(GameObject fighter in Fighters)
        {
            Fighters.Remove(fighter);
        }
    }

    void ChangeState(CombatStates To)
    {
        _fsm.Transition(_fsm.state, To);
        if (_transitions != null)
        {
            _transitions = null;
        }
        _transitions += CheckStates;
    }
}
