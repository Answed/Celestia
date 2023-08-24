using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Element
{
    public GameObject basicAttack;
    public GameObject Spell; 
    public GameObject Spell2;
    public GameObject Ultimate; 
}

public class ElementsManager : MonoBehaviour
{

    public Element[] elements;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
