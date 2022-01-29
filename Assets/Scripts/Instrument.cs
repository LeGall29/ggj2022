using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Instrument : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private InstructionPanel instructionPanel;

    [Header("Music Setup")]
    [SerializeField] private List<NoteDictionnary> notesSFX;
    [SerializeField] private List<ActionDictionnary> actionsSFX;
    [SerializeField] private BuildAction[] actionsToBuild;
    [SerializeField] private AudioClip fullMusic;

    private AudioSource audioSource;
    private int currentBuildActionIndex;

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

    public void PlayAction(MusicalAction _action)
    {
        //TODO play the correct animation


        if(_action.actionType == actionsToBuild[currentBuildActionIndex])
        {
            PlayPhrase(_action);
            currentBuildActionIndex++;
            if(currentBuildActionIndex == actionsToBuild.Length)
            {
                audioSource.PlayOneShot(fullMusic);
                Debug.Log("you win!");
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        instructionPanel.Open(this);
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
    public BuildAction actionType;
    public AudioClip audioClip;

    public ActionDictionnary(BuildAction Key, AudioClip Value)
    {
        this.actionType = Key;
        this.audioClip = Value;
    }
}