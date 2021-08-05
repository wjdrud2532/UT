using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUi = null;

    int currentSong = 0;        //현재 곡의 점수정보를 database에 넘겨주기 위한 변수
    public void SetCurrentSong(int p_songNum)   
    {
        currentSong = p_songNum;
    }

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ScoreManager theScore;
    ComboManager theCombo;
    TimingManager theTiming;
    DatabaseManager theDatabase;

    // Start is called before the first frame update
    void Start()
    {
        theScore = FindObjectOfType<ScoreManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theDatabase = FindObjectOfType<DatabaseManager>();
    }

    public void ShowResult()
    {
        FindObjectOfType<CenterFlame>().ResetMusic();   //재시작시 초기화

        AudioManager.instance.StopBGM();       //bgm정지 

        goUi.SetActive(true);

        for(int i = 0; i < txtCount.Length; i ++)
        {
            txtCount[i].text = "0";
        }
        txtCoin.text = "0";
        txtScore.text = "0";
        txtMaxCombo.text = "0";

        int[] t_judgement = theTiming.GetJudgementRecord();
        int t_currentScore = theScore.GetCurrentScore();            //database에 넣을 값 
        int t_maxCombo = theCombo.GetMaxCombo();
        int t_coin = (t_currentScore / 50);

        for (int i = 0; i < txtCount.Length; i ++)
        {
            txtCount[i].text = string.Format("{0:#,##0}", t_judgement[i]);
        }//                                                기록된 판정들이
         //텍스트에 들어가게 된다.                 string형변환

        txtScore.text = string.Format("{0:#,##0}", t_currentScore);
        txtMaxCombo.text = string.Format("{0:#,##0}", t_maxCombo);
        txtCoin.text = string.Format("{0:#,##0}", t_coin);

        if (t_currentScore > theDatabase.score[currentSong]) //현재 점수가 최고기록보다 높으면 최고기록을 갱신한다.
        {
            theDatabase.score[currentSong] = t_currentScore;
            theDatabase.SaveScore();
        }
    }

    public void BtnMainMene()
    {
        goUi.SetActive(false);
        GameManager.instance.MainMenu();
        theCombo.ResetCombo();
    }
}
