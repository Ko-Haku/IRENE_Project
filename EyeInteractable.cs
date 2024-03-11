using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class EyeInteractable : MonoBehaviour
{
    public bool isHovered { get; set; }
    [SerializeField] private UnityEvent<GameObject> OnObjectHover;

    [SerializeField]
    private Material OnHoverActiveMaterial; 
    [SerializeField]
    private Material OnHoverInactiveMaterial;

    private MeshRenderer meshRenderer;

    void Start() => meshRenderer = GetComponent<MeshRenderer>();

     private void Update()
     {
         if (isHovered)
         {
             meshRenderer.material = OnHoverActiveMaterial;
             OnObjectHover?.Invoke(gameObject);
         }
         else
         {
             meshRenderer.material = OnHoverInactiveMaterial;
             OnObjectHover?.Invoke(gameObject);
         }
         }
     }
