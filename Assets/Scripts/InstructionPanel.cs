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

    public void NextStep()
    {
        stepViewer.MoveCameraToNextStep();
    }

    public void Open(Instrument _instrument)
    {
        MusicManager.Instance.Mute();
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
        MusicManager.Instance.Unmute();
        UIFront.SetActive(false);
        stepViewer.UnzoomCamera(() =>
        {
            GetComponent<RectTransform>().DOAnchorPos3DX(-1080f, 1.7f).SetEase(Ease.InBack).OnComplete(() =>
           {
               instruments.SetActive(true);
           });
        });
    }

    public void WaitAndClose(float _time)
    {
        Invoke(nameof(Close), _time);
    }


}
