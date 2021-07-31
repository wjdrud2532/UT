using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] GameObject goStageUI = null;

    public void BtnPlay()
    {
        goStageUI.SetActive(true);  //stage UI가 나타남
        this.gameObject.SetActive(false);   //Title UI가 사라짐
    }
}
