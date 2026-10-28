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
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].refrence.transform.position.x > frontPlayer.transform.position.x)
                {
                    frontPlayer = players[i].refrence;
                }
            }
        }

        cam.Follow = frontPlayer.transform;
    }

    public void AddPlayer(GameObject passedPlayer)
    {
        player newPlayer = new player(passedPlayer, 0);
        players.Add(newPlayer);
    }

    [System.Serializable] public struct player
    {
        public player(GameObject passedPlayer, int passedPlace)
        {
            this.refrence = passedPlayer;
            this.placement = passedPlace;
        }

        public GameObject refrence;
        public int placement;
    }
}
