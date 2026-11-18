using Unity.Multiplayer.PlayMode;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private CameraManager camManager;

    public GameObject CurrentPlayer;
    [SerializeField] private GameObject[] characterModels;

    private void Start()
    {
        camManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (CurrentPlayer == null)
        {
            CurrentPlayer = camManager.players[0].refrence;
        }
    }

    public void CowGirl()
    {
        Debug.Log("Cowgirl");
    }

    public void Prospector()
    {
        Debug.Log("Prospector");
    }

    public void Outlaw()
    {
        Debug.Log("Outlaw");
    }

    public void Barkeeper()
    {
        Debug.Log("Barkeeper");
    }

    public void NextPlayer()
    {
        Debug.Log("Next");
    }
}
