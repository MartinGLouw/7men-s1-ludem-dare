using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public PlayerEvents PlayerEvents { get; private set; }
    public EnemyEvents EnemyEvents { get; private set; }
    public TimeEvents TimeEvents { get; private set; }
    public GameManagerEvents GameManagerEvents { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerEvents = new PlayerEvents();
        EnemyEvents = new EnemyEvents();
        TimeEvents = new TimeEvents();
        GameManagerEvents = new GameManagerEvents();
    }
}
