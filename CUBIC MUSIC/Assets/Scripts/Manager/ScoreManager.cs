using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null;   //

    [SerializeField] int increaseScore = 10;    //노트를 맞출 때마다 올라가게 할 디폴트 변수
    int currentScore = 0;                       //현재 점수를 담을 변수

    [SerializeField] float[] weight = null;     //판정에 따라 점수를 다르게 하기 위한 배열
    [SerializeField] int comboBonusScore = 10;

    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManager theCombo;

    // Start is called before the first frame update
    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    public void Initialized()       //게임 재시작시 초기화 함수
    {
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState) //어떤 노트 판정을 받았는지를 인자로 받는다.
    {
        //콤보 증가
        theCombo.IncreaseCombo();

        //콤보 보너스 점수 계산      현재콤보 / 10 * 10      콤보구간 10~19=10점, 20~29-20점
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;
            

        //판정 가중치 계산
        int t_incraseScore = increaseScore + t_bonusComboScore; //증가될 점수
        t_incraseScore = (int)(t_incraseScore * weight[p_JudgementState]);    //판정에 따른 가중치

        //점수 반영
        currentScore += t_incraseScore;     //가중된 점수를 현재 점수에 반영
        txtScore.text = string.Format("{0:#,##0}", currentScore);    //3자리마다 ,를 사용하는 Formet

        //애니메이션 실행
        myAnim.SetTrigger(animScoreUp);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
