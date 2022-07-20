using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    [SerializeField] private GameObject _paper;
    [SerializeField] private GameObject _rock;
    [SerializeField] private GameObject _scissor;

    private void Awake()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    
    void Update()
    {
        
    }
}
