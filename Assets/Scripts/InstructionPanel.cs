using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] private StepViewer stepViewer;
    [SerializeField] private GameObject instruments;
    [SerializeField] private GameObject instructionFrontUI;
    [SerializeField] private GameObject foreground;
    [SerializeField] private Builder guitarBuilder;
    [SerializeField] private Builder vibraphoneBuilder;
    [SerializeField] private Builder trumpetBuilder;

    private Builder currentBuilder;


    public void NextStep()
    {
        StartCoroutine(WaitThenNextStep());
    }

    private IEnumerator WaitThenNextStep()
    {
        yield return new WaitForSeconds(2f);
        int newStep = stepViewer.MoveCameraToNextStep();
        currentBuilder.SetCurrentStep(newStep);

    }

    public void Build(BuildAction _action, bool _isCorrect)
    {
        currentBuilder.Build(_action, _isCorrect);
    }

    public void Open(Instrument _instrument)
    {
        MusicManager.Instance.Mute();
        instruments.SetActive(false);
        foreground.SetActive(false);
        MusicManager.Instance.currentInstrument = _instrument;

        if (MusicManager.Instance.currentInstrument.type == InstrumentType.Guitar)
        {
            guitarBuilder.Initialize();
            currentBuilder = guitarBuilder;
        }
        else if (MusicManager.Instance.currentInstrument.type == InstrumentType.Trumpet)
        {
            trumpetBuilder.Initialize();
            currentBuilder = trumpetBuilder;
        }
        else if (MusicManager.Instance.currentInstrument.type == InstrumentType.Vibraphone)
        {
            vibraphoneBuilder.Initialize();
            currentBuilder = vibraphoneBuilder;
        }
        currentBuilder.SetCurrentStep(0);


        GetComponent<RectTransform>().DOAnchorPos3DX(0f, 1.7f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            stepViewer.MoveCameraTo(0);
            stepViewer.ZoomCamera(() =>
            {
                instructionFrontUI.SetActive(true);
            });
        });
    }

    public void Close()
    {
        MusicManager.Instance.Unmute();
        instructionFrontUI.SetActive(false);
        stepViewer.UnzoomCamera(() =>
        {
            GetComponent<RectTransform>().DOAnchorPos3DX(-1080f, 1.7f).SetEase(Ease.InBack).OnComplete(() =>
            {
                instruments.SetActive(true);
                foreground.SetActive(true);
            });
        });
    }

    public void WaitAndClose(float _time)
    {
        Invoke(nameof(Close), _time);
    }


}
