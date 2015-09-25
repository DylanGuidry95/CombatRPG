using UnityEngine;
using System.Collections;

public interface IUnit
{
    void OnIdle();
    void OnAttack(GameObject go, int a);
    void OnHit(int a);
    void OnDead();
}