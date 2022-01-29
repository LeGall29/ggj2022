using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MusicalAction", menuName = "ScriptableObjects/MusicalAction", order = 1)]
public class MusicalAction : ScriptableObject
{
    public Note[] phrase = new Note[4];
    public Action actionType;
}

public enum Action
{
    Pull,
    Push,
    Right,
    Left,
    Lift,
    Lower,
    Clockwise,
    Unclockwise
}
