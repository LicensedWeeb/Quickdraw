using Unity.Multiplayer.PlayMode;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterSelect : MonoBehaviour
{
    private CameraManager camManager;

    public GameObject CurrentPlayer;
    [SerializeField] private int playerNum;
    [SerializeField] private Mesh[] characterModels;
    [SerializeField] private TMP_Text nextBtn;
    [SerializeField] private GameObject selectUI;

    private void Start()
    {
        camManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (CurrentPlayer == null)
        {
            CurrentPlayer = camManager.players[playerNum].reference;
        }
    }

    public void CowGirl()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().model.GetComponent<MeshFilter>().mesh = characterModels[0];
        CurrentPlayer.GetComponent<PlayerMovement>().SelectedCharacter = "Cowgirl";
    }

    public void Outlaw()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().model.GetComponent<MeshFilter>().mesh = characterModels[1];
        CurrentPlayer.GetComponent<PlayerMovement>().SelectedCharacter = "Outlaw";
    }

    public void Prospector()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().model.GetComponent<MeshFilter>().mesh = characterModels[2];
        CurrentPlayer.GetComponent<PlayerMovement>().SelectedCharacter = "Prospector";
    }

    public void Barkeeper()
    {
        CurrentPlayer.GetComponent<PlayerMovement>().model.GetComponent<MeshFilter>().mesh = characterModels[3];
        CurrentPlayer.GetComponent<PlayerMovement>().SelectedCharacter = "Barkeeper";
    }

    public void NextPlayer()
    {
        if (camManager.players.Count > 0)
        {
            if (playerNum < camManager.players.Count)
            {
                playerNum++;

                if (playerNum >= camManager.players.Count)
                {
                    nextBtn.text = "Start";
                }
                else
                {
                    CurrentPlayer = camManager.players[playerNum].reference;
                }
            }
            else
            {
                Time.timeScale = 1f;
                selectUI.SetActive(false);
                
            }
        }
    }
}
