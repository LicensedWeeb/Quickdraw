using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public List<GameObject> players;
    [SerializeField] private CinemachineCamera cam;
    private GameObject frontPlayer;


    // Update is called once per frame
    void Update()
    {
        if (frontPlayer == null && players[0] != null)
        {
            frontPlayer = players[0].gameObject;
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].transform.position.x > frontPlayer.transform.position.x)
            {
                frontPlayer = players[i];
            }
        }

        cam.Follow = frontPlayer.transform;
    }
}
