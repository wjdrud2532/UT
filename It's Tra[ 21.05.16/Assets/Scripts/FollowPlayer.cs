using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("따라갈 대상 지정")]
    [SerializeField] protected Transform tf_Player;
    //객체의 위치값, 회전값, 크기값을 지닌 클래

    [Header("따라갈 속도 지정")] [Range(0, 1)] //반드시 0~1 사이의 값만을 입력해야합
    [SerializeField] protected float speed;
    //protected - 상속 클래스 안에서만 사용 가능하도록 함

    protected Vector3 currentPos; //플레이어와 총 간의 거리차이를 저장하기 위한 변수

    
    void Start()
    {
        currentPos = tf_Player.position - this.transform.position;
    }

     
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, tf_Player.position - currentPos, speed);
                                    //Lerp(PosA, PosB, speed) 위치 A와 위치 B사이의 값을 speed비율로 보간
                                    //자연스러운 감속 효과
    }
}
