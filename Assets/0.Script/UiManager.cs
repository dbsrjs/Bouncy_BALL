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
    public int totalItemCount;  //현재 갖고 있는 루비
    private GameObject player;

    [Header("Scene 0")]
    [SerializeField] private GameObject StartButton;    //시작 버튼

    [Header("All Scene")]
    public GameObject SettingPanel;       //설정 판넬
    public GameObject shortcutKeyPanel;   //단축키 판넬
    [SerializeField] private Text timerText;                //타이머 Text
    [SerializeField] private Text stageCountText;           //스테이지 Text
    [SerializeField] private Slider soundSlider;            //사운드 슬라이더

    [Header("Scene 3")]
    [SerializeField] private GameObject GameOverPanel; //게임 오버 판넬
    [SerializeField] private GameObject NoRePlayPanel; //진짜 게임 다시 안 함? 판넬

    [Header("ETC")]
    [SerializeField] private Button myButton; // 설정 버튼


    private AudioSource soundManager;

    [HideInInspector]public bool gameStart = false;//게임을 시작헀냐?

    int sceneIndex; //현재 씬 인덱스

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
        timerText.text = Timer.instance.timer.ToString("F3");   //Timer 증가(소수점 셋째 자리까지 표시)

        if(!gameStart && sceneIndex == 0 && Input.GetKeyDown(KeyCode.Space))
            GameStart();
            
    }

    /// <summary>
    /// 루비를 먹었을 때 루비 개수 변경
    /// </summary>
    public void GetItem(int count)
    {
        stageCountText.text = $"{count.ToString()} / {totalItemCount}";
    }

    #region 게임 시작/끝 버튼
    /// <summary>
    /// 게임 시작 버튼
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = 1;
        if(StartButton != null)
            StartButton.SetActive(false);
        gameStart = true;
    }

    /// <summary>
    /// 게임 끝날을 때
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }
    #endregion

    #region 게임 재시작 버튼
    /// <summary>
    /// 재시작 버튼
    /// </summary>
    public void RePlayYes() 
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 재시작 안 하는 버튼
    /// </summary>
    public void RePlayNo() 
    {
        NoRePlayPanel.SetActive(true);
        GameOverPanel.SetActive(false);
    }
    #endregion

    #region 게임 종료 버튼
    /// <summary>
    /// 게임 종료 버튼
    /// </summary>
    public void ExitGameYes()
    {
        Debug.Log("ExitGame");
        //UnityEditor.EditorApplication.isPlaying = false;    //빌드 전
        Application.Quit(); //빌드
    }

    /// <summary>
    /// 게임 종료 안 하는 버튼
    /// </summary>
    public void ExitGameNo()    
    {
        NoRePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
    #endregion

    /// <summary>
    /// 설정 창 여닫기
    /// </summary>
    public void OpenSetting()
    {
        if (!gameStart && sceneIndex == 0)  //게임을 시작하지 않았고, 1스테이지가 아닐 때
        {
            // 삼항 연산자를 사용하여 상태에 따라 SetActive 호출
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
    /// 사운드 조절
    /// </summary>
    public void Sound()
    {
        soundManager.volume = soundSlider.value;

        Debug.Log(soundManager.volume);
        PlayerPrefs.SetFloat("Volume", soundManager.volume);
    }

    /// <summary>
    /// 단축키 창 여닫기
    /// </summary>
    public void OpenShortcutKey()
    {
         shortcutKeyPanel.SetActive(shortcutKeyPanel.activeSelf ? false : true);
        SettingPanel.SetActive(shortcutKeyPanel.activeSelf ? false : true);
    }

    /// <summary>
    /// 스페이스바로 버튼을 못 누르게 함
    /// </summary>
    void OnButtonClick()
    {
        // 버튼이 클릭될 때 수행할 작업
        Debug.Log("Button Clicked!");

        // 포커스 해제
        EventSystem.current.SetSelectedGameObject(null);
    }
}