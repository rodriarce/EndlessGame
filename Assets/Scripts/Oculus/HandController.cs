using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public XRRayInteractor rayIntercator;
    public InputActionProperty triggerAction;

    // Start is called before the first frame update
    void Start()
    {
        rayIntercator = GetComponent<XRRayInteractor>();
        triggerAction.action.performed += OnPressedTrigger;
        triggerAction.action.performed += OnReleaseTrigger;

        //SelectEnterEvent selectEvent = new SelectEnterEvent();
        //SelectExitEvent exitEvent = new SelectExitEvent();
        //exitEvent.AddListener(SelectExit);
        //selectEvent.AddListener(SelectEvent);
        //rayIntercator.selectEntered = selectEvent;
        //rayIntercator.selectExited = exitEvent;

    }
    public void OnPressedTrigger(InputAction.CallbackContext contxt)
    {
        if (rayIntercator.TryGetCurrent3DRaycastHit(out RaycastHit rayCasthit))
        {
            if (rayCasthit.transform.CompareTag("box") && triggerAction.action.ReadValue<float>() == 1f)
            {
                Debug.Log("You Hit a Box");
                rayCasthit.transform.GetComponent<CubeController>().SelectEnter();
            }

            //Debug.Log(rayCasthit.transform.tag);
        }

    }
    public void OnReleaseTrigger(InputAction.CallbackContext contxt)
    {

    }
    public void SelectExit(SelectExitEventArgs hoverEvent)
    {
        Debug.Log("You Release a Box");


    }
    public void SelectEvent(SelectEnterEventArgs hoverEvent)
    {
        if (hoverEvent.interactableObject.transform.CompareTag("box"))
        {
            Debug.Log("You hit a box");
            hoverEvent.interactableObject.transform.GetComponent<CubeController>().SelectEnter();

        }


    }


    // Update is called once per frame
    void Update()
    {
        
      
    }
}
