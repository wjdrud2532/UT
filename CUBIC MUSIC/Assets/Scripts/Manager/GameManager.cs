using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 게임 시작시 TimingManager가 AudioManager에서 instance를 받아올 때 TimingManager가 먼저 실행되면 null값을 받아와 오류가 생긴다.
 * 이때 유니티의 Edit -> Project Settings -> Script Execution Order에 가서 새로운 스크립트를 넣어주고 값을 설정하면 된다.
 * 값이 작을수록 (음수일수록) 먼저 실행된다.
 */
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
    NoteManager theNote;
    Result theResult;

    [SerializeField] CenterFlame theMusic = null;  //처음에 CenterFlame가 비활성화 되어있기 때문에 FindObject로 찾을 수 없다. 그래서 [SerializeField]를 사용한다

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        theNote = FindObjectOfType<NoteManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        thetiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStage = FindObjectOfType<StageManager>();
        theResult = FindObjectOfType<Result>();
    }

    public void GameStart(int p_songNum, int p_bpm)
    {
        for(int i = 0; i < goGameUI.Length; i ++)  
        {
            goGameUI[i].SetActive(true);
        }

        theMusic.bgmName = "BGM" + p_songNum;

        theNote.bpm = p_bpm;        //bgm설정

        theStage.RemoveStage();     //기존의 스테이지 삭제
        theStage.SettingStage(p_songNum);    //새롭게 생성

        theCombo.ResetCombo();      //콤보 초기화
        theScore.Initialized();     //점수 초기화
        thetiming.Initialized();    //노트 초기화
        theStatus.Initialized();    //체력 초기화
        thePlayer.Initialized();    //위치 초기화
        theResult.SetCurrentSong(p_songNum);
        AudioManager.instance.StopBGM();

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
