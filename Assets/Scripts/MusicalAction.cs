using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MusicalAction", menuName = "ScriptableObjects/MusicalAction", order = 1)]
public class MusicalAction : ScriptableObject
{
    public Note[] phrase = new Note[4];
    public BuildAction actionType;
}

public enum BuildAction
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
