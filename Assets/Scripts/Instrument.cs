using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class Instrument : MonoBehaviour
{
    [Header("Settings")]
    public InstrumentType type;

    [Header("References")]
    [SerializeField] private InstructionPanel instructionPanel;
    [SerializeField] private Builder builder;
    [SerializeField] private GameObject instrumentVisual;
    [SerializeField] private GameObject instrumentVisualCompleted;

    [Header("Music Setup")]
    [SerializeField] private List<NoteDictionnary> notesSFX;
    [SerializeField] private List<ActionDictionnary> actionsSFX;
    [SerializeField] private BuildAction[] actionsToBuild;
    public BuildAction[] ActionToBuild { get => actionsToBuild; }

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
        if (_action.actionType == actionsToBuild[currentBuildActionIndex])
        {
            PlayPhrase(_action);
            instructionPanel.Build(_action.actionType, true);
            currentBuildActionIndex++;
            if (currentBuildActionIndex == actionsToBuild.Length)
            {
                //audioSource.PlayOneShot(fullMusic);
                instrumentVisual.SetActive(false);
                instrumentVisualCompleted.SetActive(true);
                //instructionPanel.WaitAndClose(fullMusic.length);
                instructionPanel.Close();

                MusicManager.Instance.AddInstrumentMusic(type);
            }
            else
            {
                instructionPanel.NextStep();
            }
        }
        else
        {
            instructionPanel.Build(_action.actionType, false);
        }

    }
}

[System.Serializable]
public enum InstrumentType
{
    Guitar,
    Vibraphone,
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