using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;

public class Manager : MonoBehaviour, IMixedRealityPointerHandler
    //, IMixedRealityGestureHandler<Vector3>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject crack;
    public float distanceFromWall;
    //void instantiate(Vector3 position)
    //{
       
    //    var cube = 
    //    //cube.transform.localScale = Vector3.one * 0.2f;
    //    //cube.transform.position = Vector3.forward * 0.7f;

    //    //var tapToPlace = cube.GetComponent<TapToPlace>();
    //    //tapToPlace.StartPlacement();
    //    //tapToPlace.StopPlacement();
    //}
    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnInputDown(InputEventData eventData)
    {

        //Debug.Log("<color=red>inputdown</color>");
        //RaycastHit hit;
        //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        //{
        //    Debug.Log($"{nameof(OnInputDown)}  OnInputDown happened");
        //    Instantiate(crack, hit.point + hit.normal * distanceFromWall, Quaternion.identity);
        //    //print("working 2");

        //    //Move object to point
        //    //print(hit.point);
        //    //crack.transform.position = hit.point;
        //    print(crack.transform.position);

        //}
        //else
        //{
        //    Debug.Log($"{nameof(OnInputDown)} OnInputDown else happened");

        //}


    }

    public void OnInputUp(InputEventData eventData)
    {
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("<color=red>PointerDown</color>");

        RaycastHit hit;
        if (GetPointerRayHit(out hit))
        {
            Debug.Log($" happened");

            print(hit.point);
            Instantiate(crack, hit.point + hit.normal * distanceFromWall,Quaternion.identity);
            
            //Debug.Log($"<color=yellow>position : { hit.point + hit.normal * distanceFromWall}</color>");
            //crack.transform.rotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
            print(crack.transform.position);
        }
        else
        {
            Debug.Log($" else happened");

        }

    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        //Debug.Log("OnPointerDragged");

    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerUp");

    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerClicked");

    }
    private bool GetPointerRayHit(out RaycastHit hit)
    {
        Vector3 startPoint = Vector3.zero;
        Vector3 endPoint = Vector3.zero;
        bool isHit = false;
        foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
        {
            // Ignore anything that is not a hand because we want articulated hands
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand )
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        Debug.Log("ignored near pointer");
                        continue; // Ignore near pointers, we only want the rays
                    }
                    else if (p.Result != null)
                    {
                        startPoint = p.Position;
                        endPoint = p.Result.Details.Point;
                        isHit = p.Result.Details.Object;
                        Debug.Log("Detected Non-near pointer");

                        continue;
                    }
                }
            }
        }
        Physics.Raycast(startPoint, (endPoint - startPoint), out hit, 20.0f);
        return isHit;
    }




    //public void OnGestureStarted(InputEventData eventData)
    //{
    //    //Debug.Log($"OnGestureStarted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    var action = eventData.MixedRealityInputAction.Description;
    //    if (action == "Hold Action")
    //    {
    //        RaycastHit hit;
    //        if (GetPointerRayHit(out hit))
    //        {
    //            Debug.Log($" happened");

    //            print(hit.point);
    //            Instantiate(crack, hit.point + hit.normal * distanceFromWall, Quaternion.identity);

    //            //Debug.Log($"<color=yellow>position : { hit.point + hit.normal * distanceFromWall}</color>");
    //            //crack.transform.rotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
    //            print(crack.transform.position);
    //        }
    //        else
    //        {
    //            Debug.Log($" else happened");

    //        }
    //    }
    //    //else if (action == "Manipulate Action")
    //    //{
    //    //    SetIndicator(ManipulationIndicator, $"Manipulation: started {Vector3.zero}", ManipulationMaterial, Vector3.zero);
    //    //}
    //    //else if (action == "Navigation Action")
    //    //{
    //    //    SetIndicator(NavigationIndicator, $"Navigation: started {Vector3.zero}", NavigationMaterial, Vector3.zero);
    //    //    ShowRails(Vector3.zero);
    //    //}

    //    //SetIndicator(SelectIndicator, "Select:", DefaultMaterial);
    //}

    //public void OnGestureUpdated(InputEventData eventData)
    //{
    //    //Debug.Log($"OnGestureUpdated [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    //var action = eventData.MixedRealityInputAction.Description;
    //    //if (action == "Hold Action")
    //    //{
    //    //    SetIndicator(HoldIndicator, "Hold: updated", DefaultMaterial);
    //    //}
    //}

    //public void OnGestureUpdated(InputEventData<Vector3> eventData)
    //{
    //    //Debug.Log($"OnGestureUpdated [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    //var action = eventData.MixedRealityInputAction.Description;
    //    //if (action == "Manipulate Action")
    //    //{
    //    //    SetIndicator(ManipulationIndicator, $"Manipulation: updated {eventData.InputData}", ManipulationMaterial, eventData.InputData);
    //    //}
    //    //else if (action == "Navigation Action")
    //    //{
    //    //    SetIndicator(NavigationIndicator, $"Navigation: updated {eventData.InputData}", NavigationMaterial, eventData.InputData);
    //    //    ShowRails(eventData.InputData);
    //    //}
    //}

    //public void OnGestureCompleted(InputEventData eventData)
    //{
    //    //Debug.Log($"OnGestureCompleted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    //var action = eventData.MixedRealityInputAction.Description;
    //    //if (action == "Hold Action")
    //    //{
    //    //    SetIndicator(HoldIndicator, "Hold: completed", DefaultMaterial);
    //    //}
    //    //else if (action == "Select")
    //    //{
    //    //    SetIndicator(SelectIndicator, "Select: completed", SelectMaterial);
    //    //}
    //}

    //public void OnGestureCompleted(InputEventData<Vector3> eventData)
    //{
    //    //Debug.Log($"OnGestureCompleted [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    //var action = eventData.MixedRealityInputAction.Description;
    //    //if (action == "Manipulate Action")
    //    //{
    //    //    SetIndicator(ManipulationIndicator, $"Manipulation: completed {eventData.InputData}", DefaultMaterial, eventData.InputData);
    //    //}
    //    //else if (action == "Navigation Action")
    //    //{
    //    //    SetIndicator(NavigationIndicator, $"Navigation: completed {eventData.InputData}", DefaultMaterial, eventData.InputData);
    //    //    HideRails();
    //    //}
    //}

    //public void OnGestureCanceled(InputEventData eventData)
    //{
    //    //Debug.Log($"OnGestureCanceled [{Time.frameCount}]: {eventData.MixedRealityInputAction.Description}");

    //    //var action = eventData.MixedRealityInputAction.Description;
    //    //if (action == "Hold Action")
    //    //{
    //    //    SetIndicator(HoldIndicator, "Hold: canceled", DefaultMaterial);
    //    //}
    //    //else if (action == "Manipulate Action")
    //    //{
    //    //    SetIndicator(ManipulationIndicator, "Manipulation: canceled", DefaultMaterial);
    //    //}
    //    //else if (action == "Navigation Action")
    //    //{
    //    //    SetIndicator(NavigationIndicator, "Navigation: canceled", DefaultMaterial);
    //    //    HideRails();
    //    //}
    //}
}
