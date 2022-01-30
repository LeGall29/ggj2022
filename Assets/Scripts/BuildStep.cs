using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStep : MonoBehaviour
{
    [SerializeField] private Transform movingPart;
    [SerializeField] private Transform modelRoot;
    [SerializeField] private float speed = 5f;

    public void Toggle(bool _boolean)
    {
        gameObject.SetActive(_boolean);
    }

    private void Update()
    {
        modelRoot.Rotate(Vector3.up, speed * Time.deltaTime, Space.Self);
    }

    public void MovePart(BuildAction _action, bool _isCorrect)
    {
        Vector3 originalPos = movingPart.localPosition;
        Vector3 originalRot = movingPart.localRotation.eulerAngles;

        switch (_action)
        {
            case BuildAction.Pull:
                movingPart.DOLocalMoveZ(originalPos.z - 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Push:
                movingPart.DOLocalMoveZ(originalPos.z + 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Right:
                movingPart.DOLocalMoveX(originalPos.x + 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Left:
                movingPart.DOLocalMoveX(originalPos.x - 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Lift:
                movingPart.DOLocalMoveY(originalPos.y + 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Lower:
                movingPart.DOLocalMoveY(originalPos.y - 2, 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Clockwise:
                movingPart.DOLocalMoveZ(originalPos.z + 0.5f, 1f);
                movingPart.DOLocalRotate(new Vector3(originalRot.x, originalRot.y + 360, originalRot.z), 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
            case BuildAction.Unclockwise:
                movingPart.DOLocalMoveZ(originalPos.z - 0.5f, 1f);
                movingPart.DOLocalRotate(new Vector3(originalRot.x, originalRot.y - 360, originalRot.z), 1f).OnComplete(() => { if (!_isCorrect) ResetPosRot(originalPos, originalRot); });
                break;
        }
    }

    private void ResetPosRot(Vector3 _pos, Vector3 _rot)
    {
        movingPart.DOLocalMove(_pos, 0.5f).SetEase(Ease.OutElastic);
        movingPart.DOLocalRotate(_rot, 0.5f).SetEase(Ease.OutElastic);
    }
}
