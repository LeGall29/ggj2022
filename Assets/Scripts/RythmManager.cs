using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioClip))]
public class RythmManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject cursor;
    [SerializeField] private TextMeshProUGUI[] beatLabels;
    [SerializeField] private Transform[] cursorPoses;
    [SerializeField] private MusicalAction[] buildActions;

    [Header("Inputs")]
    [SerializeField] InputActionReference[] keyboardInputs;

    int cursorIndex = -1;
    int lastCursorIndex = -1;

    private List<Note> playerMelody = new List<Note>();
    private bool partitionIsPlaying = false;
    bool canAddNotes = true;

    private AudioSource audioSource;

    public event System.Action OnMelodyEnd;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        OnMelodyEnd += StartBeat;
    }

    private void OnEnable()
    {
        beatLabels[0].color = Color.green;
        foreach (InputActionReference i in keyboardInputs)
        {
            i.action.Enable();
            i.action.performed += KeyboardNotePressed;
        }
    }

    private void StartBeat()
    {
        audioSource.Play();
        OnMelodyEnd -= StartBeat;
    }

    private void KeyboardNotePressed(InputAction.CallbackContext obj)
    {
        if (canAddNotes)
        {
            int noteID = int.Parse(obj.action.name[obj.action.name.Length - 1].ToString());
            playerMelody.Add((Note)noteID-1);
            //Debug.Log("playerMelody.Count = " + playerMelody.Count);
            beatLabels[playerMelody.Count - 1].text = noteID.ToString();
        }

        if (playerMelody.Count == 4)
        {
            canAddNotes = false;
            partitionIsPlaying = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!partitionIsPlaying)
            return;

        lastCursorIndex = cursorIndex;
        cursorIndex++;

        if (cursorIndex >= 4)
        {
            OnMelodyEnd?.Invoke();
            //Debug.Log("The end");
            cursorIndex = -1;
            lastCursorIndex = -1;
            CheckIfMelodyIsAction();
            playerMelody.Clear();
            canAddNotes = true;
            partitionIsPlaying = false;
            foreach (TextMeshProUGUI t in beatLabels)
            {
                t.text = "";
                t.color = Color.black;
            }
            return;
        }

        Note currentNote = playerMelody[cursorIndex];

        Debug.Log("Playing note = " + currentNote);
        MusicManager.Instance.currentInstrument.PlayNote(currentNote);

        beatLabels[cursorIndex].color = Color.green;

        if (lastCursorIndex >= 0)
            beatLabels[lastCursorIndex].color = Color.black;

        cursor.transform.position = cursorPoses[cursorIndex].position;
    }

    public void CheckIfMelodyIsAction()
    {
        foreach(MusicalAction a in buildActions)
        {
            //Debug.Log($"Trying to know if you played {a.actionType} ({a.phrase.ToString()})");
            if(Enumerable.SequenceEqual(a.phrase,playerMelody.ToArray()))
            {
                Debug.Log("you are doing a " + a.actionType.ToString());
                MusicManager.Instance.currentInstrument.PlayAction(a);
            }
        }
    }
}
