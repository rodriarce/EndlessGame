using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using BotaLab;

public class HandController : MonoBehaviour
{
       
    public XRRayInteractor rayIntercator;
    public InputActionProperty triggerAction;
    private Transform lastTrigger;
    private Transform lastTriggerSpecial;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rayIntercator = GetComponent<XRRayInteractor>();
        triggerAction.action.performed += OnPressedTrigger;
        triggerAction.action.canceled += OnReleaseTrigger;
        

        //SelectEnterEvent selectEvent = new SelectEnterEvent();
        //SelectExitEvent exitEvent = new SelectExitEvent();
        //exitEvent.AddListener(SelectExit);
        //selectEvent.AddListener(SelectEvent);
        //rayIntercator.selectEntered = selectEvent;
        //rayIntercator.selectExited = exitEvent;

    }
    public void OnPressedTrigger(InputAction.CallbackContext contxt)
    {
        lastTrigger = null;
        lastTriggerSpecial = null;
        if (rayIntercator.TryGetCurrent3DRaycastHit(out RaycastHit rayCasthit))
        {
            if (rayCasthit.transform.CompareTag("box") && triggerAction.action.ReadValue<float>() == 1f)
            {
                Debug.Log("You Hit a Box");
                rayCasthit.transform.GetComponent<CubeController>().SelectEnter();
            }
            if (rayCasthit.transform.CompareTag("Key"))
            {
                rayCasthit.transform.GetComponent<BotaLab.Key>().OnPressedKey();
                Debug.Log("You Presed a Key");
                lastTrigger = rayCasthit.transform;
            }
            if (rayCasthit.transform.CompareTag("keySpecial"))
            {
                rayCasthit.transform.GetComponent<BotaLab.KeySpecial>().OnEspecialKeyEnter();
                Debug.Log("You Presed a Special Key");
                lastTriggerSpecial = rayCasthit.transform;
            }

            //Debug.Log(rayCasthit.transform.tag);
        }

    }
    public void OnReleaseTrigger(InputAction.CallbackContext contxt)
    {
     
            if (lastTrigger != null)
        {
            if (lastTrigger.CompareTag("Key"))
            {
                lastTrigger.GetComponent<BotaLab.Key>().OnReleaseKey();
                Debug.Log("You Release a  Key");
            }

        }
        if (lastTriggerSpecial != null)
        {
            if (lastTriggerSpecial.CompareTag("keySpecial"))
            {
                lastTriggerSpecial.GetComponent<BotaLab.KeySpecial>().OnEspecialKeyExit();
                Debug.Log("You  Release a special Key");
            }
        }
            

            //Debug.Log(rayCasthit.transform.tag);
        
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
