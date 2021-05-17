using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int currentSocre;   //현재 점수

    public int GetCurrentScore() { return currentSocre; }       //public 안하고 함수로 만드는 이유 -> 변수 못바꾸고 노출 최소화
    public static int exterScore;

    [SerializeField] Text txt_Score;

    int distanceScore;
    float maxDistance;      //최대로 간 위치
    float originPosZ;       //플레이어 최초의 z값 

    [SerializeField] Transform tf_Player; // 플레이어의 위치 정보

    public void ResetCurrentScore() { currentSocre = 0; distanceScore = 0; maxDistance = 0; exterScore = 0; }


    private void Start()
    {
        originPosZ = tf_Player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(tf_Player.position.z > maxDistance)
        {
            maxDistance = tf_Player.position.z;
            distanceScore = Mathf.RoundToInt(maxDistance - originPosZ);
        }

        currentSocre = exterScore + distanceScore;
        txt_Score.text = string.Format("{0:000,000}", currentSocre);
                        //Format 특정 형식을 유지시켜줌
                                //위와 같이 하면 10만자리 형태로 유지,
    }
}
