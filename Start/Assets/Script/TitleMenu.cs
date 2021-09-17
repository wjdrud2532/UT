using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    //[SerializeField] AnimationClip FirstAnimation = null;

    [SerializeField] GameObject goStageUI = null;


    private void Start()
    {
        
    }

    public void BtnPlay()
    {
        goStageUI.SetActive(true);          //seri에 넣은 게임 오브젝트를 활성화 -> 스테이지 메뉴 켜짐
        this.gameObject.SetActive(false);   //현재의 게임 오브젝트를 비활성화 -> 타이틀 꺼
    }
}
