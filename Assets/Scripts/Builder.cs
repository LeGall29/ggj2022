using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Builder : MonoBehaviour
{
    [SerializeField] private BuildStep[] steps;

    private BuildAction[] currentBuildOrder = new BuildAction[8];

    private int lastStepIndex = -1;
    private int currentStepIndex = -1;

    public void Initialize()
    {
        gameObject.SetActive(true);
        lastStepIndex = -1;
        MusicManager.Instance.currentInstrument.ActionToBuild.CopyTo(currentBuildOrder, 0);
    }

    public void SetCurrentStep(int _stepIndex)
    {
        if(lastStepIndex != -1)
        {
            //steps[lastStepIndex].Toggle(false);
        }
        lastStepIndex = currentStepIndex;
        currentStepIndex = _stepIndex;
        //steps[currentStepIndex].Toggle(true);
    }
    public void Build(BuildAction _action, bool _isCorrect)
    {
        steps[currentStepIndex].MovePart(_action, _isCorrect);
    }

}
