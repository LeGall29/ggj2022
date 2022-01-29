using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenInstrument : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private InstructionPanel instructionPanel;
    [SerializeField] private Instrument instrument;

    public void OnPointerClick(PointerEventData eventData)
    {
        instructionPanel.Open(instrument);
    }
}
