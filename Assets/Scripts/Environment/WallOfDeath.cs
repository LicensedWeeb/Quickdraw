using System;
using UnityEngine;

public class WallOfDeath : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject objective;
    [SerializeField] private AnimationCurve MovementMult;
    private Vector3 StartLoc;

    [SerializeField] private float[] DebugRatios;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < DebugRatios.Length; ++i)
        {
            Gizmos.DrawSphere((objective.transform.position - StartLoc) * DebugRatios[i] + StartLoc, 1.0f);
        }
    }
}
