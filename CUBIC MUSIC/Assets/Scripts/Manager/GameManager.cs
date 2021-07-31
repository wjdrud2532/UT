using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;

    public static GameManager instance; //어디서든 호출 가능하도록

    public bool isStartGame = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager thetiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        thetiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStage = FindObjectOfType<StageManager>();
    }

    public void GameStart()
    {
        for(int i = 0; i < goGameUI.Length; i ++)
        {
            goGameUI[i].SetActive(true);
        }

        theStage.RemoveStage();     //기존의 스테이지 삭제
        theStage.SettingStage();    //새롭게 생성

        theCombo.ResetCombo();      //콤보 초기화
        theScore.Initialized();     //점수 초기화
        thetiming.Initialized();    //노트 초기화
        theStatus.Initialized();    //체력 초기화
        thePlayer.Initialized();    //위치 초기화

        isStartGame = true;
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }

        goTitleUI.SetActive(true);
    }
    
}
