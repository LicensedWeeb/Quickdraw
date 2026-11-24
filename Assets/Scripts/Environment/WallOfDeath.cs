using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject objective;
    [SerializeField] private AnimationCurve MovementMult;
    private Vector3 StartLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartLoc = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (objective != null)
        {
            transform.position += transform.forward * movementSpeed * MovementMult.Evaluate((StartLoc - transform.position).sqrMagnitude / (StartLoc - objective.transform.position).sqrMagnitude) * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
