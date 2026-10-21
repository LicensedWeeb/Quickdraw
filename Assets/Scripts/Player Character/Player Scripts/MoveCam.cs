using UnityEngine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] private Transform headPos;

    private void Update()
    {
        transform.position = headPos.position;
    }
}
