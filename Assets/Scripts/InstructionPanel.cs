using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] private StepViewer stepViewer;
    [SerializeField] private GameObject instruments;
    [SerializeField] private GameObject UIFront;

    public void Open(Instrument _instrument)
    {
        instruments.SetActive(false);
        MusicManager.Instance.currentInstrument = _instrument;
        GetComponent<RectTransform>().DOAnchorPos3DX(0f, 1.7f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            stepViewer.MoveCameraTo(0);
            stepViewer.ZoomCamera(() =>
            {
                UIFront.SetActive(true);
            });
        });
    }

    public void Close()
    {
        UIFront.SetActive(false);
        stepViewer.UnzoomCamera(() =>
        {
            GetComponent<RectTransform>().DOAnchorPos3DX(-1080f, 1.7f).SetEase(Ease.InBack).OnComplete(() =>
           {
               instruments.SetActive(true);
           });
        });
    }

}