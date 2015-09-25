using UnityEngine;
using System.Collections;
using System;

public enum UnitStates
{
    INIT,
    IDLE,
    ATTACK,
    HIT,
    DEAD
}

public class Enemy : MonoBehaviour, IUnit
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private int _maxHealth = 100;

    private int _health;

    [SerializeField]
    private float _speed = 2;

    FSM<UnitStates> _fsm;                       // Finite State Machine to manage UnitStates 

    // // MonoBehavior Functions
    void Awake()
    {
        _fsm = new FSM<UnitStates>();
    }

    void Start()
    {
        _health = _maxHealth;

        _fsm = new FSM<UnitStates>();
        // Add States for the FSM
        _fsm.AddState(UnitStates.INIT);
        _fsm.AddState(UnitStates.IDLE);
        _fsm.AddState(UnitStates.ATTACK);
        _fsm.AddState(UnitStates.HIT);
        _fsm.AddState(UnitStates.DEAD);
        // Add transistion for the FSM
        _fsm.AddTransition(UnitStates.INIT, UnitStates.IDLE);
        _fsm.AddTransition(UnitStates.IDLE, UnitStates.ATTACK);
        _fsm.AddTransition(UnitStates.ATTACK, UnitStates.IDLE);
        _fsm.AddTransition(UnitStates.IDLE, UnitStates.HIT);
        _fsm.AddTransition(UnitStates.HIT, UnitStates.IDLE);
        _fsm.AddTransition(UnitStates.HIT, UnitStates.DEAD);

        _fsm.Transition(_fsm.state, UnitStates.IDLE);
    }

    void Update()
    {
        // Update State based on Parameters     // Whatever they will be
        if(Input.GetKeyDown( KeyCode.A))
        {
            _fsm.Transition(_fsm.state, UnitStates.HIT);
        }

        // perform Actions based on state
        switch(_fsm.state)
        {
            case UnitStates.IDLE:
                this.OnIdle();
                break;
            case UnitStates.HIT:
                this.OnHit(15);
                break;
            case UnitStates.ATTACK:
                this.OnAttack(_target, 15);
                break;
            case UnitStates.DEAD:
                this.OnDead();
                break;
        }
    }

    // // IUnit inteface Functions
    public void OnIdle()
    {
       Debug.Log(gameObject.name + " : OnIdle()");
    } 

    public void OnDead()
    {
        Destroy(gameObject);
        // add Experience or whatever
        // add Gold
        // add etc.
    }

    public void OnAttack(GameObject go, int a)
    {
        Debug.Log("Nothing Here Yet OnAttack() " + gameObject.name);
    }

    public void OnHit(int a)
    {
        _health -= a;
        if (_health < 0)
        {
            if (!_fsm.Transition(_fsm.state, UnitStates.DEAD))
                Debug.Log("Transition to DEAD Failed " + gameObject.name);
        }
        else
        {
            if (!_fsm.Transition(_fsm.state, UnitStates.IDLE))
                Debug.Log("Transition to IDLE Failed " + gameObject.name);
        }
    }
}
