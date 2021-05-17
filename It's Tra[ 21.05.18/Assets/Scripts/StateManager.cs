using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  //씬 전환 사용 가능하도록 선언


public class StateManager : MonoBehaviour
{
    [SerializeField] float blinkSpeed;
    [SerializeField] int blinkCount;

    [SerializeField] MeshRenderer mesh_PlayerGraphics ;
    //점과 선, 면 등을 활성화, 비활성화를 반복하며 깜빡임을 연출함

    [SerializeField] int maxHP; //  체력 최대치
    int currentHP;  //현재 체력

    [SerializeField] Text[] txt_HPArray;

    bool isInvincibleMode = false; // 현재 무적상태인지

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPStatus();
    }

    void UpdateHPStatus()
    {
        for(int i = 0; i < txt_HPArray.Length; i ++)
        {
            if(i < currentHP)
            {
                txt_HPArray[i].gameObject.SetActive(true);
            }
            else
                txt_HPArray[i].gameObject.SetActive(false);

        }
    }

    public void DecreaseHp(int _num)
    {
        if (!isInvincibleMode)
        {
            currentHP -= _num;
            UpdateHPStatus();

            if (currentHP <= 0)
                Playerdead();

            SoundManager.instance.PlaySE("Hurt");
            StartCoroutine(BlinkCoroutune());
        }
    }

    IEnumerator BlinkCoroutune()    //일종의 병렬처리 기법, 대기 기능의 구현이 간단함
    {
        isInvincibleMode = true;
        for(int i = 0; i < blinkCount * 2; i ++)
        {
            mesh_PlayerGraphics.enabled = !mesh_PlayerGraphics.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            //yield return 코루틴은 반드시 대기 문법이 존재해야 한다
        }
        isInvincibleMode = false;
    }

    void Playerdead()
    {
        SceneManager.LoadScene("Title");
    }

    public void IncreaseHp(int _num)
    {
        if (currentHP == maxHP)
            return;

        currentHP += _num;
        if (currentHP > maxHP)
            currentHP = maxHP;
        UpdateHPStatus();
    }
}
