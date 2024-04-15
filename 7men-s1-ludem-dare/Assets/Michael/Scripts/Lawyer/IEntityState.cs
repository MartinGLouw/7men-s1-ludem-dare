using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityStates
{
    Spawning,
    Attacking,
    Death
}

public interface ILawyerStates
{
    void OnSpawn();
    void OnAttack();
    void OnDeath();
}
