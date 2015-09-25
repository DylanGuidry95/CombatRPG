using UnityEngine;
using System.Collections;

public interface IUnit
{
    void OnAttack();

    void OnWait();

    void OnUseItem();

    void OnReady();
}