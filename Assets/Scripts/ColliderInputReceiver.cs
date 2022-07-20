using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInputReceiver : MonoBehaviour
{
    private Vector3 clickPosition;
    public GameObject _clickedObject;
    public static ColliderInputReceiver instance;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "Rock"|| hit.transform.gameObject.tag == "Paper" || hit.transform.gameObject.tag == "Scissor")
                    {
                        clickPosition = hit.point;
                        _clickedObject = hit.transform.gameObject;
                        BoardCreation.instance.PossibleMoves();
                    }
                    
                    //possible moves çalıştır
                }
            }
            
        }
    }
    
    
    
    
}
