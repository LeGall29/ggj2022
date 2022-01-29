using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private InputActionReference changeColorAction;

    // Start is called before the first frame update
    void Start()
    {
        changeColorAction.action.Enable();
        changeColorAction.action.performed += OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext obj)
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }
}
