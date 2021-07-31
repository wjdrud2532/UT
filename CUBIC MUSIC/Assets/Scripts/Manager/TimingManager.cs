using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{


    public List<GameObject> boxNoteList = new List<GameObject>();
    //생성된 노트를 담는 List -> 판정범위에 있는 모든 노트를 비교해야함

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null; //판정 범위의 중간을 가리킴
    [SerializeField] RectTransform[] timingRect = null; //다양한 판정 범위를 보여주기 위함
    Vector2[] timingBoxs = null; //실제 판정, 판독에 사용할 Vector2 ,
                                 //판정 범위의 최소값(x), 최대값(y)
                                 //0번째 가 가장 좁은 판정, 3번째가 가장 넓은 판




    EffectManager theEffect;

    ScoreManager theScoreManager;

    ComboManager theComboManager;

    StageManager theStageManager;

    PlayerController thePlayer;

    StatusManager theStatusManager;

    AudioManager theAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStatusManager = FindObjectOfType<StatusManager>();
        theAudioManager = AudioManager.instance;

        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2) ;
            //각각의 판정 범위 -> 최소값 = 중심 - (이미지의 너비 / 2)
            //                최대값 = 중심 + (이미지의 너비 / 2)
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            //리스트에 있는 노드들을 확인해서 판정박스에 있는 노드들을 찾아야 한다.


            float t_nodePosX = boxNoteList[i].transform.localPosition.x;
            //판정범위 최소값 <= 노드의 x값 <= 판정범위 최대

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if(timingBoxs[x].x <= t_nodePosX && t_nodePosX <= timingBoxs[x].y)
                {
                    //노트 제거
                    //Destroy(boxNoteList[i]);
                    boxNoteList[i].GetComponent<Note>().HideNode();
                    boxNoteList.RemoveAt(i);

                    //이펙트 연출
                    if (x < timingBoxs.Length - 2) //bad는 3이므로 2 이하에서만 피격 효과가 나타나도록 
                        theEffect.NoteHitEffect();
                    
                    //Debug.Log("Hit  " + x);


                    
                    if (CheckCanNextPlate())
                    {
                        //점수 증가
                        theScoreManager.IncreaseScore(x);   //판정을 알 수 있는 파라미터 x

                        theStageManager.ShowNextPlate();        //발판 등장

                        theEffect.JudgementEffect(x);   //판정 x를 넘겨서 그에 맞는 이미지가 출력되도록 한다.
                                                        //판정 연출
                        judgementRecord[x]++; //    판정 기록
                                              //ex) 퍼팩트가 뜰 경우 그 값을 ++

                        theStatusManager.CheckShield(); //   쉴드 체크
                    }
                    else
                    {
                        theEffect.JudgementEffect(4); 
                    }

                    theAudioManager.PlaySFX("clap");        //clap와 같은 이름의 효과음 재생

                    return true;
                }
            }
            //판정범위의 개수만큼 반복 -> 어느 범위에 있는지 판

            
        }
        theComboManager.ResetCombo();
        theEffect.JudgementEffect(timingBoxs.Length - 1);
        MissRecord(); //miss가 나왔을 때도 개수를 count한다.
        return false;
        //Debug.Log("MISS");
    }

    bool CheckCanNextPlate()
    {
        if(Physics.Raycast(thePlayer.destPos, Vector3.down, out RaycastHit t_hitinfo, 1.1f))
        {                   //목적지 좌표에서 아래로 레이저 발사, 충돌한 바닥의 좌표값을 가져온다
            if(t_hitinfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitinfo.transform.GetComponent<BasicPlate>();
                if (t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                    
                }
            }
        }
        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
        theStatusManager.ResetShieldCombo();
    }

    public void Initialized()   //게임 재시작시 초기 함수
    {
        for (int i = 0; i < judgementRecord.Length; i++)
            judgementRecord[i] = 0;
    }
}
