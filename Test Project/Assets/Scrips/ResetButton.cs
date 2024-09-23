using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RestartButton : MonoBehaviour
{
    public AudioSource loseSFX;
    public Button restartButton;
    public float playerPos;
    public bool loseGame = false, playedSFX = false;
    public Canvas endCanvas;
    public GameObject player;
    public TextMeshProUGUI winLabel, loseLabel;
    public PlayerMovement playerScript;


    // Start is called before the first frame update

    private void OnEnable()
    {
        restartButton.onClick.AddListener(() => ButtonPress(restartButton));
        playerScript = FindObjectOfType<PlayerMovement>();
    }

    private void ButtonPress(Button buttonPressed)
    {
        if (buttonPressed == restartButton)
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene("GravFlip");
        }
    }
    public void Update()
    {
        if(playerScript != null)
        {
            playerPos = player.transform.position.x;

            if (playerPos <= -250 )
            {
                winLabel.gameObject.SetActive(true);
            }
            if (playerScript.loseGame == true)
            {
                loseGame = true;
            }
        }
        if(playerScript == null)
        {
            loseGame = true;
        }

        if(loseGame == true)
        {
            loseLabel.gameObject.SetActive(true);
            if(playedSFX == false)
            {
                Debug.Log("Play SFX");
                loseSFX.Play();
                playedSFX = true;
            }
            else if (playedSFX == true)
            {

            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GravFlip");
        }
    }
}
