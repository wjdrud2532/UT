using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("피격 이벤트")]
    [SerializeField] GameObject go_RicochetEffect;

    [Header("총알 데미지")]
    [SerializeField] int damage;

    [Header("피격 효과음")]
    [SerializeField] string sound_Ricochet;

    void OnCollisionEnter(Collision other)
        //다른 컬라이더와 충돌하는 순간 호출되는 함수
        //충돌한 객체의 정보는 other에 저장됨
    {
        ContactPoint contactPoint = other.contacts[0];
        //충돌한 객체의 '접촉면'에 대한 정보가 담긴 클래스
        //contacts[0]에는 충돌한 객체의 접촉면 정보가 있다(부딪힌 객체의 가장 가까운 면)

        //SoundManager.instance.PlaySE("NormalGun_Ricochet"); 명칭을 그대로 사용함 - 하드코딩
        SoundManager.instance.PlaySE(sound_Ricochet);        //변수 이름들 사용함


        var clone = Instantiate(go_RicochetEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
                                              //충돌한 컬라이더 표면상의 position좌표
                                                                        //특정 방향을 바라보게 만드는 메소드, normal은 충돌한 컬라이더의 표면 방향 -> 부딪힌 표면의 방향을 알기 위함
                                                                                        //ex)아래를 보고 있는 천장에 부딪히면 아래를 바라보게 됨

        if(other.transform.CompareTag("Mine"))
        {
            other.transform.GetComponent<Mine>().Damaged(damage);
        }

        Destroy(clone, 0.5f);   //clone를 0.5초 뒤에 삭제(파티클 시스템 삭제)	
        Destroy(gameObject);    //총알 삭제
    }
}
