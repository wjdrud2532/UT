using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxspeed;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()   //단발적인 키 입력에 유용(입력이 씹히는 경우가 없다)
    {
        //ㅡㅡㅡㅡStop Speed
        if(Input.GetButtonUp("Horizontal")) //x좌표 이동키의 입력이 사라졌을 때
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            //                           벡터 크기를 1로 만든 상태(단위 벡터)
        }

        //ㅡㅡㅡㅡ 이동을 끝마치는 경우 현재 이동속도를 1로 만들어 정지시키는데 사용된다.


        //방향 전환
        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
    }

    void FixedUpdate()  //지속적인 키 입력에 유용
        //설정에 따라 다르지만 1초에 약 50번 작동함(버튼을 누르고 있으면 1초에 50번 힘이 가해짐)
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if(rigid.velocity.x > maxspeed)                 //오른쪽 속도의 최대
            //해당 오브젝트의 속도 중 x 좌표의 속도가 넘는다면
        {
            rigid.velocity = new Vector2(maxspeed, rigid.velocity.y);
                                                    //y축 속도는 그대로
        }
        else if(rigid.velocity.x < maxspeed *(-1) )     //왼쪽 속도의 최대(-1을 곱한다.)
        {
            rigid.velocity = new Vector2(maxspeed*(-1), rigid.velocity.y);
        }
    }
}
