using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public int totalItemCount;  //���� ���� �ִ� ���

    [SerializeField] private GameObject StartButton;

    public Text stageCountText; //�������� Text
    [SerializeField] private Text timerText;    //Ÿ�̸� Text
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject NoRePlayPanel;

    private void Awake()
    {
        instance = this;
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Update()
    {
        timerText.text = Timer.main.timer.ToString("F3");   //Timer ����(�Ҽ��� ��° �ڸ����� ǥ��)
    }

    public void GetItem(int count)//��� �Ծ��� �� ��� ���� ����
    {
        stageCountText.text = $"{count.ToString()} / {totalItemCount}";
    }

    public void GameOver()  //���� ������ ��
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    public void RePlayYes() //����� ��ư
    {
        SceneManager.LoadScene("Game_0");
        Time.timeScale = 1;
    }

    public void RePlayNo()  //����� �� �ϴ� ��ư
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }

    public void ExitGameYes()//���� ���� ��ư
    {
        UnityEditor.EditorApplication.isPlaying = false;    //���� ��
        //Application.Quit(); //����
    }

    public void ExitGameNo()    //���� ���� �� �ϴ� ��ư
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void GameStart() //���� ���� ��ư
    {
        Time.timeScale = 1;
        StartButton.SetActive(false);
    }
}
