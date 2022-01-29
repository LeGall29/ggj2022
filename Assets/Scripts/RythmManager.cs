using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioClip))]
public class RythmManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI[] beatLabels;
    [SerializeField] private Transform[] cursorPoses;
    [SerializeField] private GameObject cursor;

    [Header("Inputs")]
    [SerializeField] InputActionReference[] keyboardInputs;

    int cursorIndex = -1;
    int lastCursorIndex = -1;

    private List<int> playerMelody = new List<int>();
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
            playerMelody.Add(noteID);
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
            Debug.Log("The end");
            cursorIndex = -1;
            lastCursorIndex = -1;
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

        //Debug.Log("Cursor Index = " + cursorIndex);
        int noteToPlay = int.Parse(beatLabels[cursorIndex].text) - 1;
        //Debug.Log("note to play : " + noteToPlay);
        Note currentNote = (Note)noteToPlay;

        //Debug.Log("Playing note = " + currentNote);
        MusicManager.Instance.currentInstrument.PlayNote(currentNote);

        beatLabels[cursorIndex].color = Color.green;

        if (lastCursorIndex >= 0)
            beatLabels[lastCursorIndex].color = Color.black;

        cursor.transform.position = cursorPoses[cursorIndex].position;
    }
}
