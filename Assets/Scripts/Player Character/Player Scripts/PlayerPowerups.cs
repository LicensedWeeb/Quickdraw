using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPowerups : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject powerupObj;
    [SerializeField] private GameObject goldenNuggetPrefab;
    [SerializeField] private GameObject quickDrawPrefab;
    [SerializeField] private GameObject slowDrawPrefab;
    [SerializeField] private GameObject dashPrefab;


    [SerializeField] private float goldenNuggetTime = 1.0f;
    private float goldenNuggetTimer = 0.0f;
    bool isNuggetTime;
    bool isLockingOn;
    [SerializeField] private float lockOnTime = 1.0f;
    private float lockOnTimer = 0.0f;
    [SerializeField] private float quickDrawForce = 6.0f;
    [SerializeField] private float dashForce = 6.0f;


    Rigidbody body;



    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void UseAbility(InputAction.CallbackContext context)
    {
        if (context.performed && powerupObj != null)
        {
            if (powerupObj == goldenNuggetPrefab)
            {
                isNuggetTime = true;
                goldenNuggetTimer = goldenNuggetTime;
                powerupObj = null;
            }
            else if(powerupObj == quickDrawPrefab)
            {
                body.AddForce(Vector3.Normalize(-transform.forward - transform.up) * quickDrawForce, ForceMode.Impulse);
                powerupObj = null;
            }
            else if(powerupObj == dashPrefab)
            {
                body.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                powerupObj = null;
            }
            else if (powerupObj == slowDrawPrefab)
            {
                isLockingOn = true;
                lockOnTimer = lockOnTime;
                powerupObj = null;
            }

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isNuggetTime)
        {
            if (goldenNuggetTimer > 0.0f)
            {
                goldenNuggetTimer -= Time.fixedDeltaTime;
            }
            else if (goldenNuggetTimer <= 0.0f)
            {
                isNuggetTime = false;
            }
        }
        if (isLockingOn)
        {
            if (lockOnTimer > 0.0f)
            {
                lockOnTimer -= Time.fixedDeltaTime;
            }
            else if (lockOnTimer <= 0.0f)
            {
                isLockingOn = false;
            }
        }
    }
}
