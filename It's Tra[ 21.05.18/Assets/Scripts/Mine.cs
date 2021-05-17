using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] GameObject go_EffectPrefab;

    [SerializeField] int hp;

    [SerializeField] float verticalDistance; //수직 움직임
    [SerializeField] float horizontalDistance; //수평 움직임

    [Range(0, 1)]
    [SerializeField] float moveSpeed;       //움직일 스피드

    Vector3 endPos1;
    Vector3 endPos2;
    Vector3 currentDestination;

    private void Start()
    {
        Vector3 originPos = transform.position; //처음 자기 자신의 위치를 기억
        endPos1.Set(originPos.x, originPos.y + verticalDistance, originPos.z + horizontalDistance);
        endPos2.Set(originPos.x, originPos.y - verticalDistance, originPos.z - horizontalDistance);
        currentDestination = endPos1;
    }

    private void Update()
    {
        if((transform.position - endPos1).sqrMagnitude <= 0.1f) // 계산 결과의 제곱을 반환하는 sqrmagnitude
        //vector3.distance 나 magniude 로 거리 계산이 가능하지만 성능이 나빠진다 (대신 정확함)
        {
            currentDestination = endPos2;
        }
        else if ((transform.position - endPos2).sqrMagnitude <= 0.1f) 
            currentDestination = endPos1;

        transform.position = Vector3.Lerp(transform.position, currentDestination, moveSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.name == "Player")
            //하이라케에 있는 에셋들의 이름과 같으면
        {
            other.transform.GetComponent<StateManager>().DecreaseHp(damage);

            Explosion();
        }
    }
    
    public void Damaged( int _num)
    {
        hp -= _num;
        if(hp <= 0)
        {
            Explosion();
        }
    }

    void Explosion()
    {
        SoundManager.instance.PlaySE("Mine");
        GameObject clone = Instantiate(go_EffectPrefab, transform.position, Quaternion.identity);


        Destroy(clone, 2f); //2초 후에 사라짐
        Destroy(gameObject);
    }
}
