using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Builder : MonoBehaviour
{
    [SerializeField] private BuildStep[] steps;

    private BuildAction[] currentBuildOrder = new BuildAction[8];

    private int currentStepIndex = -1;
    public int CurrentStepIndex { get => currentStepIndex; }

    public void Initialize()
    {
        gameObject.SetActive(true);
        MusicManager.Instance.currentInstrument.ActionToBuild.CopyTo(currentBuildOrder, 0);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetCurrentStep(int _stepIndex)
    {
        currentStepIndex = _stepIndex;
        //steps[currentStepIndex].Toggle(true);
    }
    public void Build(BuildAction _action, bool _isCorrect)
    {
        try
        {
            steps[currentStepIndex].MovePart(_action, _isCorrect);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }

}
