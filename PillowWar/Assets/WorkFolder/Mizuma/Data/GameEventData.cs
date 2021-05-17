using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    TeacherAttack,
    Event2,
    Event3
}

[CreateAssetMenu(fileName = "Data",menuName = "ScriptableObject/GameEventData")]
public class GameEventData : ScriptableObject
{
    [SerializeField] private EventInfo[] gameEvents;
    [SerializeField] private EventInfo finalEventInfo;
}

[System.Serializable]
public class EventInfo
{
    public float stopEventInterval;
    public float eventGenerationInterval;
    public EVENT_TYPE type;
}
