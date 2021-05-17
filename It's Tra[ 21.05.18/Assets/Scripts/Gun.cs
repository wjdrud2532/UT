using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
        //열거된 데이터 중에서 하나를 선택할 수 있는 자료형
{       //끝날 땐 ; 가 아닌 , 으로 끝나야 함
        Normal_Gun,
}

public class Gun : MonoBehaviour
{

    [Header("총 유형")]
    public WeaponType weaponType;

    [Header("총 연사속도 조정")]
    public float fireRate;

    [Header("총알 개수")]
    public int bulletCount;
    public int maxBulletCount;

    [Header("총구 섬광")]
    public ParticleSystem ps_MuzzleFlash;

    [Header("총알 프리팹")]
    public GameObject go_Buller_Prefab;

    [Header("애니메이터")]
    public Animator animator;

    [Header("총알 스피드")]
    public float speed;

    [Header("총알 발사 사운드")]
    public string sound_Fire;

}
