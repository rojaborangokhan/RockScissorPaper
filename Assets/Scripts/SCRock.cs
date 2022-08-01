using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRock : MonoBehaviour
{
    public List<GameObject> boardCube;
    public GameObject _rockTouchCube;
    public static SCRock instance;
    private void Awake()
    {
        instance = this;
        boardCube = new List<GameObject>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            //StartCoroutine(LateBeginning());
            boardCube.Add(other.gameObject);
            _rockTouchCube = other.gameObject;
            Debug.Log(_rockTouchCube.name);
            for (int i = 0; i < boardCube.Count; i++)
            {
                boardCube[i].GetComponent<Tile>().isThereRock = true;
            }
        }
    }
    
    private IEnumerator LateBeginning()
    {
        yield return new WaitForSecondsRealtime(5f*Time.deltaTime);
    }
}
