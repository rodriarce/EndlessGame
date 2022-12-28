using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimController : MonoBehaviour
{
    public InputActionProperty gripAction;
    public InputActionProperty triggerAction;
    //public VRInputManager vrInputManager;
    public bool isLeftHand;
    public Animator handAnim;
    public float triggerValue;
    public float gripValue;   



    private void Awake()
    {
       
    }

    private void OnEnable()
    {
        
        
    }

    private void OnDisable()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
        ////handActionMap.FindAction()
        //if (isLeftHand)
        //{
        //    vrInputManager.XRILeftHandInteraction.Activate.performed += OnTriggerHand;
            
        //}
        //else
        //{
        //    vrInputManager.XRIRightHandInteraction.Activate.performed += OnTriggerHand;
        //}
        
    }

    //public void OnTriggerHand(InputAction.CallbackContext callBack)
    //{
    //    handAnim.SetFloat("Grip", 0f);
    //    handAnim.SetFloat("Trigger", 1f);
        
    //}
    

    




    // Update is called once per frame
    void Update()
    {
             
            
       
       
          triggerValue = triggerAction.action.ReadValue<float>();
          gripValue = gripAction.action.ReadValue<float>();
            
       
        handAnim.SetFloat("Grip", gripValue);
        handAnim.SetFloat("Trigger", triggerValue);

    }
}
