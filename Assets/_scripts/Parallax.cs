using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(GameManager.IsGameRunning == false) return;
        meshRenderer.material.mainTextureOffset += Vector2.right * (speed * Time.deltaTime);
    }
}
