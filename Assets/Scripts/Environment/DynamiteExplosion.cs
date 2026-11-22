using UnityEngine;

public class DynamiteExplosion : MonoBehaviour
{
    [SerializeField] private float explosionTime = 1.0f;
    private float explosionTimer = 0.0f;

    [SerializeField] private float explosionRadius = 1.0f;
    [SerializeField] private float explosionImpulse = 1.0f;

    [HideInInspector] public CameraManager cameraManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explosionTimer = explosionTime;
        cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionTimer > 0)
            explosionTimer -= Time.deltaTime;
        else if(cameraManager != null)
        {
            PlayerMovement playerBody;
            Vector3 playerDist = Vector3.zero;
            for (int i = 0; i < cameraManager.players.Count; ++i)
            {
                playerDist = cameraManager.players[i].reference.transform.position - transform.position;
                if (playerDist.magnitude < explosionRadius)
                {
                    playerBody = cameraManager.players[i].reference.GetComponent<PlayerMovement>();
                    if (playerBody != null)
                    {
                        playerBody.AddImpulse(playerDist.normalized * explosionImpulse);
                    }
                }
            }
            Destroy(this.gameObject);
            // Explodes
        }
    }
}
