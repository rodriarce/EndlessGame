using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    public XRRayInteractor rayIntercator;
    // Start is called before the first frame update
    void Start()
    {
        rayIntercator = GetComponent<XRRayInteractor>();
        
               
    }
    public void SelectEnterd(SelectEnterEventArgs selectedEvent)
    {
        Debug.Log("On Select Event");
    }


    // Update is called once per frame
    void Update()
    {
        if (rayIntercator.TryGetCurrent3DRaycastHit(out RaycastHit rayCasthit))
        {
            Debug.Log(rayCasthit.transform.tag);
        }
    }
}
