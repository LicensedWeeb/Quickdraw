using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerPowerups : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [HideInInspector] public GameObject powerupObj;
    [SerializeField] private GameObject goldenNuggetPrefab;
    [SerializeField] private GameObject quickDrawPrefab;
    [SerializeField] private GameObject slowDrawPrefab;
    [SerializeField] private GameObject dashPrefab;
    [SerializeField] private GameObject playerPrefab;


    [SerializeField] private float goldenNuggetTime = 1.0f;
    private float goldenNuggetTimer = 0.0f;
    bool isNuggetTime;
    bool isLockingOn;
    [SerializeField] private float lockOnTime = 1.0f;
    private float lockOnTimer = 0.0f;
    [SerializeField] private float quickDrawForce = 6.0f;
    [SerializeField] private float dashForce = 6.0f;

    PlayerMovement body;
    CameraManager cameraManager;



    void Start()
    {
        body = GetComponent<PlayerMovement>();
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
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
            else if (powerupObj == quickDrawPrefab)
            {
               // body.AddImpulse(Vector3.Normalize(-transform.forward - transform.up) * quickDrawForce * body.mass);
                powerupObj = null;
            }
            else if (powerupObj == dashPrefab)
            {
                //body.AddImpulse(transform.forward * dashForce * body.mass); // Might need to just make this 
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

    void ShootBullet(Vector3 PlayerPos)
    {
        Vector3 RayDisplacement = PlayerPos - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, RayDisplacement.normalized, out hit, RayDisplacement.magnitude) && hit.collider.gameObject == playerPrefab)
        {
            PlayerMovement PlayerScript = hit.collider.gameObject.GetComponent<PlayerMovement>();   
            PlayerScript.isStunned = true;
            PlayerScript.stunTimer = PlayerScript.stunTime;
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
                isLockingOn = false;    // Shouldn't work with 
                ShootBullet(cameraManager.FindPlayer(gameObject).reference.transform.position); 
            }
        }
       
    }
    private void OnDestroy()
    {
        cameraManager.players.Remove(cameraManager.FindPlayer(gameObject));
    }
}
