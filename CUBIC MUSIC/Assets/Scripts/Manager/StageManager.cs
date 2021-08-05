using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    GameObject currentStage;

    [SerializeField] GameObject[] stageArray = null;
    Transform[] stagePlates;

    [SerializeField] float offsetY = 3;
    [SerializeField] float plateSpeed = 10;

    int stepCount = 0;
    int totalPlateCount = 0;

    // Start is called before the first frame update
    //void Start()

    public void RemoveStage()   //기존의 스테이지를 삭제
    {
        if (currentStage != null)    //이전에 스테이지를 호출했다면
            Destroy(currentStage);  //삭제
    }

    public void SettingStage(int p_songNum)
    {
        //스테이지 새로 생성시 stepcount를 0으로 초기화
        stepCount = 0;

        //프리팹 생성                        //위치        //회전값 X
        currentStage = Instantiate(stageArray[p_songNum], Vector3.zero, Quaternion.identity);

        stagePlates = currentStage.GetComponent<Stage>().plates;
        totalPlateCount = stagePlates.Length;

        for(int i = 0; i < totalPlateCount; i ++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlate()
    {
        if (stepCount < totalPlateCount)
            StartCoroutine(MovePlateCo(stepCount++));
    }

    IEnumerator MovePlateCo(int p_num)
    {
        stagePlates[p_num].gameObject.SetActive(true);
        Vector3 t_destPos = new Vector3(stagePlates[p_num].position.x,
                                        stagePlates[p_num].position.y - offsetY,
                                        stagePlates[p_num].position.z);

        while(Vector3.SqrMagnitude(stagePlates[p_num].position - t_destPos) >= 0.001f)
        {
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position, t_destPos, plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[p_num].position = t_destPos;
    }

    
}
