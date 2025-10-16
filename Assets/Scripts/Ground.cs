using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float delayTimer;
    [SerializeField] private bool canFall;
    private void Start()
    {
        InitializeRigidbody();
    }
    private void InitializeRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        rb.useGravity = false;

        if (!canFall)
            rb.isKinematic = true;

    }

    public void ChangeColorSoftRed()
    {
        meshRenderer.material.color = Color.softRed;
    }
}
