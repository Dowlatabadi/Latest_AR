using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;


public class Anchoring : MonoBehaviour, IMixedRealityPointerHandler 

{
    public GameObject crack;
    public float distanceFromWall = 0.01f;
    [SerializeField] private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Start");

        layerMask = LayerMask.NameToLayer("Spatial Awareness");
        Debug.Log($"layer num:{layerMask}");

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnInputDown(InputEventData eventData)
    {

        Debug.Log("<color=red>inputdown</color>");
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log($"{nameof(OnInputDown)}  OnInputDown happened");

            print("working 2");

            //Move object to point
            print(hit.point);
            crack.transform.position = hit.point;
            print(crack.transform.position);

        }
        else
        {
            Debug.Log($"{nameof(OnInputDown)} OnInputDown else happened");

        }


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
            crack.transform.position = hit.point+hit.normal*distanceFromWall;
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
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
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

}


