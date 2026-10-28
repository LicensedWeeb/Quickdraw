using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public List<player> players;
    [SerializeField] private CinemachineCamera cam;
    private GameObject frontPlayer;


    // Update is called once per frame
    void Update()
    {
        if (frontPlayer == null && players[0].refrence != null)
        {
            frontPlayer = players[0].refrence;
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].refrence.transform.position.x > frontPlayer.transform.position.x)
            {
                frontPlayer = players[i].refrence;
            }
        }

        cam.Follow = frontPlayer.transform;
    }

    public struct player
    {
        public GameObject refrence;
        public int placement;
    }
}
