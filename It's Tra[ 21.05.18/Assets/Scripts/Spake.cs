using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spake : MonoBehaviour
{

    [SerializeField] int damage;

    [SerializeField] float force;

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Player"))    //부딪힌 객체(other)가 Player일 경우 실행
        {
            Debug.Log(damage + "를 플레이어에게 입혔습니다");
            other.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);     //폭발 반경 내에 있는 다른 rigidbodt를 날려보냄
            other.transform.GetComponent<StateManager>().DecreaseHp(damage);
        }
    }
}
