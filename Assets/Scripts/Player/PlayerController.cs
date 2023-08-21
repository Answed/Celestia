using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float heatlh;
    public int coins; // Could be saved sepratly 

    [SerializeField] private int maxHealth;

    private InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.crouch == 1)
            transform.localScale = new Vector3(1, 0.5f, 1);
        else
            transform.localScale = Vector3.one;
    }
}
