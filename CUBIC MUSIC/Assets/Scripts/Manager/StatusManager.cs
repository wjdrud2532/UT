using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f; //    0.1초마다 깜빡거리도록 
    [SerializeField] int blinkCount = 10;       //10번 깜빡거린다
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShhield = 0;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;

    [SerializeField] int shieldIncreaseCombo = 5;   //5콤보가 찰 때마다 쉴드가 생김
    int currentShiledCombo = 0;
    [SerializeField] Image shieldGauge = null;      //게이지를 조정하기 위함 

    Result theResult;
    NoteManager theNote;
    [SerializeField] MeshRenderer playerMesh = null;   //meshrenderer를 켜고 끄는 것을 통해 깜빡임을 구현 

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    public void CheckShield()
    {
        currentShiledCombo++;

        if(currentShiledCombo >= shieldIncreaseCombo)   //현재콤보가 지정된 값보다 크다면 쉴드를 증가시킨다.
        {
            currentShiledCombo = 0;                     //그리고 다시 0으로 초기화
            IncreaseShield();
        }

        shieldGauge.fillAmount = (float) currentShiledCombo / shieldIncreaseCombo;
    }

    public void ResetShieldCombo()
    {
        currentShiledCombo = 0;
        shieldGauge.fillAmount = (float)currentShiledCombo / shieldIncreaseCombo;

    }

    public void IncreaseShield()
    {
        currentShhield++;

        if (currentShhield >= maxShield)
            currentShhield = maxShield;

        SettingShiledImage();
    }

    public void IncreaseHP(int p_num)
    {
        currentHp += p_num;

        if (currentHp >= maxHp)
            currentHp = maxHp;

        SettingHPImage();
    }

    public void DecreasShiled(int p_num)
    {
        currentShhield -= p_num;

        if (currentShhield <= 0)
            currentShhield = 0;

        SettingShiledImage();
    }

    public void DecreaseHp(int p_num)
    {
        if (!isBlink)   //연속으로 체력이 빠지지 않도록 깜빡이는 중에는 무적처리를 한다.
        {
            if (currentShhield > 0)
                DecreasShiled(p_num);
            else
            {
                currentHp -= p_num;

                if (currentHp <= 0)      //현재 체력이 0 이하라면
                {
                    //Debug.Log("die");
                    isDead = true;          //상태를 변경하고
                    theResult.ShowResult(); //결과창을 나타낸다.
                    theNote.RemoveNote();   //노트들을 지운다.
                }
                else
                {
                    StartCoroutine(BlinkCo());  //체력이 1 이상일 경우에만
                }

                SettingHPImage();
            }
            
        }
        
    }

    void SettingHPImage()
    {
        for(int i = 0; i < hpImage.Length; i ++)
        {
            if(i < currentHp)       //체력 개수 확인
            {
                hpImage[i].gameObject.SetActive(true);  //하트 표시
            }
            else
            {
                hpImage[i].gameObject.SetActive(false); //하트 삭제
            }
        }
    }

    void SettingShiledImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShhield)       
            {
                shieldImage[i].gameObject.SetActive(true);  
            }
            else
            {
                shieldImage[i].gameObject.SetActive(false); 
            }
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCo()
    {
        isBlink = true; //무적처리

        while(currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;   //반대 상태로 전환(깜빡임)
            yield return new WaitForSeconds(blickSpeed);   //0.1 초단위로 깜빡임
            currentBlinkCount++;
        }

        playerMesh.enabled = true; //깜빡임 원위치(나타난 상태로 전환)
        isBlink = false;    //무적 해제
        currentBlinkCount = 0;  //깜빡임 초기화
    }

    public void Initialized()   //게임 재시작시 초기 함수
    {
        currentHp = maxHp;
        currentShhield = 0;
        currentShiledCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPImage();       //하트랑 쉴드 다시 3개씩 보임
        SettingShiledImage();
    }
}
