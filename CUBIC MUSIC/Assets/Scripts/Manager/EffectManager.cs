using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null;    //교체할 이미지 변수
    [SerializeField] Sprite[] judgementSprite = null;   //판정에 따라 다른 이미지를 출력하기 위한 스프라이트 저장 배열 

    public void JudgementEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num]; //넘어온 파라미터 num의 값에 따라 이미지를 변경시킨다.
        judgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
}
