using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public GameObject[] cube;
    public Material[] materials;

    private void Start()
    {
        RandomMaterial();
    }

    void RandomMaterial()
    {
        int rastgele = UnityEngine.Random.Range(0,6);
        Debug.Log(rastgele);
        switch (rastgele)
        {
            case 0:
                cube[0].GetComponent<MeshRenderer>().material = materials[0];
                cube[1].GetComponent<MeshRenderer>().material = materials[1];
                cube[2].GetComponent<MeshRenderer>().material = materials[2];
                break;
            
            case 1:
                cube[0].GetComponent<MeshRenderer>().material = materials[0];
                cube[1].GetComponent<MeshRenderer>().material = materials[2];
                cube[2].GetComponent<MeshRenderer>().material = materials[1];
                break;
            
            case 2:
                cube[0].GetComponent<MeshRenderer>().material = materials[1];
                cube[1].GetComponent<MeshRenderer>().material = materials[0];
                cube[2].GetComponent<MeshRenderer>().material = materials[2];
                break;
            
            case 3:
                cube[0].GetComponent<MeshRenderer>().material = materials[1];
                cube[1].GetComponent<MeshRenderer>().material = materials[2];
                cube[2].GetComponent<MeshRenderer>().material = materials[0];
                break;
            
            case 4:
                cube[0].GetComponent<MeshRenderer>().material = materials[2];
                cube[1].GetComponent<MeshRenderer>().material = materials[1];
                cube[2].GetComponent<MeshRenderer>().material = materials[0];
                break;
            
            case 5:
                cube[0].GetComponent<MeshRenderer>().material = materials[2];
                cube[1].GetComponent<MeshRenderer>().material = materials[0];
                cube[2].GetComponent<MeshRenderer>().material = materials[1];
                break;
        }
    }
}
