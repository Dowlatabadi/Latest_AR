using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Linq;


public class spawner : MonoBehaviour
{
    public GameObject Crack;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn_crack", 1,2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> cracks;
    void spawn_crack()
    {
        RaycastHit hit;

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
       if (isHit)
        {

       var go= Instantiate(Crack, hit.point /*+ hit.normal * .1f*/, Quaternion.FromToRotation(Vector3.back, hit.normal));
            cracks.Add(go);
            if (cracks.Count() > 10)
            {
             GameObject.Destroy(   cracks.ElementAt(0));
            }
        }
    }
}
