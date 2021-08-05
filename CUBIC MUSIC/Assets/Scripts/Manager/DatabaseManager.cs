    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public int[] score;

    private void Start()
    {
        LoadScore();
    }

    public void SaveScore()
    {
        //설치된 기기에 자체적으로 데이터를 저장한다, 앱을 지우면 복구가 불가능하다.
        PlayerPrefs.SetInt("Score1", score[0]);
        PlayerPrefs.SetInt("Score2", score[1]);
        PlayerPrefs.SetInt("Score3", score[2]);
    }

    public void LoadScore()
    {
        if(PlayerPrefs.HasKey("Score1"))  //기존의 정보가 존재하는지 확인
        {
            score[0] = PlayerPrefs.GetInt("Score1");
            score[1] = PlayerPrefs.GetInt("Score2");
            score[2] = PlayerPrefs.GetInt("Score3");
        }
    }
}
