using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;


public class StepViewer : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private List<Image> listSteps = new List<Image>();

    [Header("Settings")]
    [SerializeField]
    private Vector2 offset;

    [SerializeField]
    private float zoomSize = 90;

    [SerializeField]
    private float unzoomSize = 250;

    private Camera mainCamera;

    private int currentStep;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void MoveCameraTo(int _step)
    {
        currentStep = _step;
        Vector3 newPosition = new Vector3(listSteps[_step].transform.position.x + offset.x, listSteps[_step].transform.position.y + offset.y, transform.position.z);
        transform.DOMove(newPosition, 1).SetEase(Ease.OutQuart);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Return new step</returns>
    public int MoveCameraToNextStep()
    {
        int temp = currentStep + 1;
        MoveCameraTo(temp);
        return temp;
    }

    public void ZoomCamera(System.Action _callbackOnZoomed)
    {
        mainCamera.DOOrthoSize(zoomSize, 1f).SetEase(Ease.OutQuart).OnComplete(() => _callbackOnZoomed?.Invoke());
    }
    public void UnzoomCamera(System.Action _callbackOnUnzoomed)
    {
        mainCamera.DOOrthoSize(unzoomSize, 1f).SetEase(Ease.OutQuart).OnComplete(() => _callbackOnUnzoomed?.Invoke());
        mainCamera.transform.DOMove(new Vector3(0, 0, -10f), 1f).SetEase(Ease.OutQuart);
    }
}
