using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public int totalItemCount;
    public Text stageCountText;
    public Text playerCountText;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlay;
    
    public int stage = 0;

    private void Awake()
    {
        main = this;
        stageCountText.text = "  /" + totalItemCount;
    }

    public void GetItem(int count)
    {
        playerCountText.text = count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(stage);
        }
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void RePlayYes()
    {
        SceneManager.LoadScene("Game_0");
        Time.timeScale = 1;
    }

    public void RePlayNo()
    {
        NoRePlay.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void ExitGameYes()
    {
        UnityEditor.EditorApplication.isPlaying = false;    //ºôµå Àü
        //Application.Quit(); //ºôµå
    }

    public void ExitGameNo()
    {
        NoRePlay.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
