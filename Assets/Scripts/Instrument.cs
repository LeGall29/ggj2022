using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Instrument : MonoBehaviour
{
    [SerializeField] private List<NoteDictionnary> notesSFX;
    [SerializeField] private List<ActionDictionnary> actionsSFX;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayNote(Note _note)
    {
        audioSource.PlayOneShot(notesSFX.Find(x => x.note == _note).audioClip);
    }

    public void PlayPhrase(MusicalAction _action)
    {
        audioSource.PlayOneShot(actionsSFX.Find(x => x.actionType == _action.actionType).audioClip);
    }
}

[System.Serializable]
public enum IntrumentType
{
    Guitar,
    Xylophone,
    Trumpet
}

[System.Serializable]
public enum Note
{
    C4,
    D4,
    E4,
    F4,
    G4,
    A4,
    B4,
    C5,
    D5
}

/// <summary>
/// A class that represent a specific audioclip for a specific note.
/// </summary>
[System.Serializable]
public class NoteDictionnary
{
    public Note note;
    public AudioClip audioClip;

    public NoteDictionnary(Note Key, AudioClip Value)
    {
        this.note = Key;
        this.audioClip = Value;
    }
}

/// <summary>
/// A class that represent a specific audioclip for a specific action.
/// </summary>
[System.Serializable]
public class ActionDictionnary
{
    public Action actionType;
    public AudioClip audioClip;

    public ActionDictionnary(Action Key, AudioClip Value)
    {
        this.actionType = Key;
        this.audioClip = Value;
    }
}