using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public int totalItemCount;  //���� ���� �ִ� ���
    private GameObject player;

    [Header("Scene 0")]
    [SerializeField] private GameObject StartButton;    //���� ��ư

    [Header("All Scene")]
    public GameObject SettingPanel;       //���� �ǳ�
    public GameObject shortcutKeyPanel;   //����Ű �ǳ�
    [SerializeField] private Text timerText;                //Ÿ�̸� Text
    [SerializeField] private Text stageCountText;           //�������� Text
    [SerializeField] private Slider soundSlider;            //���� �����̴�

    [Header("Scene 3")]
    [SerializeField] private GameObject GameOverPanel; //���� ���� �ǳ�
    [SerializeField] private GameObject NoRePlayPanel; //��¥ ���� �ٽ� �� ��? �ǳ�

    [Header("ETC")]
    [SerializeField] private Button myButton; // ���� ��ư


    private AudioSource soundManager;

    [HideInInspector]public bool gameStart = false;//������ ��������?

    int sceneIndex; //���� �� �ε���

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        soundManager = player.GetComponent<AudioSource>();
        stageCountText.text = $"0 / {totalItemCount}";
    }

    private void Start()
    {
        soundManager.volume = PlayerPrefs.GetFloat("Volume");
        soundSlider.value = soundManager.volume;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        gameStart = (sceneIndex == 0 ? false : true);

        myButton.onClick.AddListener(OnButtonClick);
    }

    private void Update()
    {
        timerText.text = Timer.instance.timer.ToString("F3");   //Timer ����(�Ҽ��� ��° �ڸ����� ǥ��)

        if(!gameStart && sceneIndex == 0 && Input.GetKeyDown(KeyCode.Space))
            GameStart();
            
    }

    /// <summary>
    /// ��� �Ծ��� �� ��� ���� ����
    /// </summary>
    public void GetItem(int count)
    {
        stageCountText.text = $"{count.ToString()} / {totalItemCount}";
    }

    #region ���� ����/�� ��ư
    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = 1;
        if(StartButton != null)
            StartButton.SetActive(false);
        gameStart = true;
    }

    /// <summary>
    /// ���� ������ ��
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }
    #endregion

    #region ���� ����� ��ư
    /// <summary>
    /// ����� ��ư
    /// </summary>
    public void RePlayYes() 
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    /// <summary>
    /// ����� �� �ϴ� ��ư
    /// </summary>
    public void RePlayNo() 
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }
    #endregion

    #region ���� ���� ��ư
    /// <summary>
    /// ���� ���� ��ư
    /// </summary>
    public void ExitGameYes()
    {
        Debug.Log("ExitGame");
        //UnityEditor.EditorApplication.isPlaying = false;    //���� ��
        Application.Quit(); //����
    }

    /// <summary>
    /// ���� ���� �� �ϴ� ��ư
    /// </summary>
    public void ExitGameNo()    
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
    #endregion

    /// <summary>
    /// ���� â ���ݱ�
    /// </summary>
    public void OpenSetting()
    {
        if (!gameStart && sceneIndex == 0)  //������ �������� �ʾҰ�, 1���������� �ƴ� ��
        {
            // ���� �����ڸ� ����Ͽ� ���¿� ���� SetActive ȣ��
            SettingPanel.SetActive(SettingPanel.activeSelf ? false : true);
        }

        else
        {
            Debug.Log("test");
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                SettingPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                SettingPanel?.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Sound()
    {
        soundManager.volume = soundSlider.value;

        Debug.Log(soundManager.volume);
        PlayerPrefs.SetFloat("Volume", soundManager.volume);
    }

    /// <summary>
    /// ����Ű â ���ݱ�
    /// </summary>
    public void OpenShortcutKey()
    {
         shortcutKeyPanel.SetActive(shortcutKeyPanel.activeSelf ? false : true);
        SettingPanel.SetActive(shortcutKeyPanel.activeSelf ? false : true);
    }

    /// <summary>
    /// �����̽��ٷ� ��ư�� �� ������ ��
    /// </summary>
    void OnButtonClick()
    {
        // ��ư�� Ŭ���� �� ������ �۾�
        Debug.Log("Button Clicked!");

        // ��Ŀ�� ����
        EventSystem.current.SetSelectedGameObject(null);
    }
}