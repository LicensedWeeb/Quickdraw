using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        var gamepad = Gamepad.current;

        if (gamepad == null)
            Debug.Log("Gamepad not detected");
    }
}
