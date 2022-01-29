using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RythmManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] beatLabels;
    [SerializeField] private Transform[] cursorPoses;
    [SerializeField] private GameObject cursor;

    [Header("Inputs")]
    [SerializeField] InputActionReference[] keyboardInputs;

    int cursorIndex = 0;
    int lastCursorIndex = 0;

    private List<int> playerMelody = new List<int>();
    private bool partitionIsPlaying = false;

    bool canAddNotes = true;

    private void OnEnable()
    {
        beatLabels[0].color = Color.green;
        foreach (InputActionReference i in keyboardInputs)
        {
            i.action.Enable();
            i.action.performed += KeyboardNotePressed;
        }
    }

    private void KeyboardNotePressed(InputAction.CallbackContext obj)
    {
        if (canAddNotes)
        {
            int noteID = int.Parse(obj.action.name[obj.action.name.Length - 1].ToString());
            playerMelody.Add(noteID);
            Debug.Log("playerMelody.Count = " + playerMelody.Count);
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

        if (cursorIndex < 3)
            cursorIndex++;
        else
        {
            cursorIndex = 0;
            playerMelody.Clear();
            canAddNotes = true;
            partitionIsPlaying = false;
            foreach (TextMeshProUGUI t in beatLabels)
            {
                t.text = "";
                t.color = Color.black;
            }
        }

        beatLabels[cursorIndex].color = Color.green;
        beatLabels[lastCursorIndex].color = Color.black;

        cursor.transform.position = cursorPoses[cursorIndex].position;
    }
}
