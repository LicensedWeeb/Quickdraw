using UnityEngine;

public class GenerateRandomItem : MonoBehaviour
{
    [SerializeField] private GameObject[] ItemArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            
            if(player != null)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
