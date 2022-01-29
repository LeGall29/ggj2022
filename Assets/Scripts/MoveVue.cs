using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;


public class MoveVue : MonoBehaviour
{                                           
    public List<Image> listSteps = new List<Image>();
    private int index = 0;
    public InputActionReference moveView; 


    // Start is called before the first frame update
    void Start()
    {
        
        moveView.action.performed += MoveCameraView;
        moveView.action.Enable();
        Vector3 newPosition = new Vector3(listSteps[index].transform.position.x, listSteps[index].transform.position.y, transform.position.z);
        transform.SetPositionAndRotation(newPosition, transform.rotation);
        index++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveCameraView(InputAction.CallbackContext callbackContext) {
        if (index >= listSteps.Count)
        {
            index = 0;
        }

        Vector3 newPosition = new Vector3(listSteps[index].transform.position.x, listSteps[index].transform.position.y, transform.position.z);
        transform.DOMove(newPosition, 1);
        index++;

    }
}
