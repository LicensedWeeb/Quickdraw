using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public List<player> players;
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject Objective;


    // Update is called once per frame
    void Update()
    {
        if (players.Count < 1)
            return;
        player swappingPlayer;

        for (int i = 0; i < players.Count; ++i)
        {
            // Added -1 so it doesn't go to last element as it already does thrpough + 1
            for (int j = 0; j < players.Count - 1; ++j) // Could add dist val to player struct to avoid multiple overlapping Dist calcs
            {
                if ((players[j].reference.transform.position - Objective.transform.position).magnitude > (players[j + 1].reference.transform.position - Objective.transform.position).magnitude)
                {
                    swappingPlayer = players[j]; 
                    players[j] = players[j + 1];
                    players[j + 1] = swappingPlayer;
                }
            }
        }
        for(int i = 0; i < players.Count; ++i)
        {
            player structToChange = players[i];
            structToChange.placement = i + 1;
            players[i] = structToChange;
        }
  
        cam.Follow = players[0].reference.transform;
    }

    public void AddPlayer(GameObject passedPlayer)
    {
        player newPlayer = new player(passedPlayer, 0);
        players.Add(newPlayer);
    }

    public player FindPlayer(int placement)
    {
        for (int i = 0; i < players.Count; ++i)
        {
            if (players[i].placement == placement)
            {
                return players[i];
            }
        }
        return new player();
    }

    public player FindPlayer(GameObject Player)
    {
        for (int i = 0; i < players.Count; ++i)
        {
            if (players[i].reference == Player)
            {
                return players[i];
            }
        }
        return new player();
    }

    [System.Serializable] public struct player
    {
        public player(GameObject passedPlayer, int passedPlace)
        {
            this.reference = passedPlayer;
            this.placement = passedPlace;
        }

        public GameObject reference;
        public int placement;
    }
}
