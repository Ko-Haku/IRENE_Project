using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class EyeTrackingRay : MonoBehaviour
{


     public Stopwatch cronometro = new Stopwatch();
    [SerializeField] 
    private float rayDistance = 1.0f;

    [SerializeField] 
    private float rayWidth = 0.01f;

    [SerializeField] 
    private LayerMask layersToInclude;

    [SerializeField] private Color rayColorHoverState = Color.yellow;
    [SerializeField] private Color rayColorDefaultState = Color.red;
    private LineRenderer lineRenderer;
    private List<EyeInteractable> eyeInteractables = new List<EyeInteractable>();

void Start()
{
    lineRenderer = GetComponent<LineRenderer>();
    SetupRay();
}

void SetupRay()
{
    lineRenderer.useWorldSpace = false;
    lineRenderer.positionCount = 2;
    lineRenderer.startWidth = rayWidth;
    lineRenderer.endWidth = rayWidth;
    lineRenderer.startColor = rayColorDefaultState;
    lineRenderer.endColor = rayColorDefaultState;
    lineRenderer.SetPosition(0, transform.position);
    lineRenderer.SetPosition(1, new Vector3(transform.position.x,transform.position.y, transform.position.z+rayDistance));

}

private void FixedUpdate()
{
    TimeSpan ts = cronometro.Elapsed;
//    print(ts);
    RaycastHit hit;
    Vector3 rayCastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;
    if (Physics.Raycast(transform.position, rayCastDirection, out hit, Mathf.Infinity, layersToInclude))
    {

       

        Unselect();
        lineRenderer.startColor = rayColorHoverState;
        lineRenderer.endColor = rayColorHoverState;
        var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
        eyeInteractables.Add(eyeInteractable);
        eyeInteractable.isHovered = true;
      cronometro.Start();
    }
    else
    {cronometro.Stop();
        lineRenderer.startColor = rayColorDefaultState;
        lineRenderer.endColor = rayColorDefaultState;
        Unselect(true);
    }
}

void Unselect(bool clear =false)
{
    foreach (var interactable  in eyeInteractables)
    {
        interactable.isHovered = false;
        
    }
    if (clear)
        eyeInteractables.Clear();
}

void Update()
    {
        
    }
}
