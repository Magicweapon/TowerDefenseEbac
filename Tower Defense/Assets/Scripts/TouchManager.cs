using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public InputActionAsset input;

    private InputAction touch;
    private InputAction touchPosition;
    private Camera mainCamera;

    public delegate void PlatformClicked(GameObject platform);
    public event PlatformClicked OnPlatformClicked;

    private void OnEnable()
    {
        TouchSimulation.Enable();
        input.Enable();
        touch = input.FindAction("Touch");
        touchPosition = input.FindAction("TouchPosition");

        touch.performed += Touch;
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
        input.Disable();

        touch.performed -= Touch;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Touch(InputAction.CallbackContext context)
    {
        Vector2 touchPos2D = touchPosition.ReadValue<Vector2>();
        Vector3 touchPos3D = new Vector3(touchPos2D.x, touchPos2D.y, mainCamera.farClipPlane);
        Ray screenRay = mainCamera.ScreenPointToRay(touchPos3D);
        //Debug.Log("Screen was touched on position: " + touchPos2D);

        RaycastHit hit;

        if (Physics.Raycast(screenRay, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Platform")
            {
                //Debug.Log("Platform clicked");
                OnPlatformClicked?.Invoke(hit.transform.gameObject);
            }
        }
        else
        {
            Debug.Log("The ray didn't touch any object");
        }

    }
}
