using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("속도 관련 변수")]
    [SerializeField] float movespeed;
    [SerializeField] float jetPackSpeed;
    Rigidbody myRigid;

    
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TryMove();
        TryJet();
        
           
    }

    void TryMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0) // 상, 하(W, S) 방향키가 눌렸을 경우
                                                 //GetAxisRaw 특정 키가 눌렸을 때 1과 -1을 반환해주는 메소드
                                                 //Horizontal : 좌(A) 방향키 = -1, 우(D) 방향키 = +1을 반환
                                                 //Vertical : 하(S) 방향키 = -1, 상(W) 방향키 = +1을 반환한다.
        {
            Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
            myRigid.AddForce(moveDir * movespeed);    //특정 방향으로 힘을 가하는 메소드
        }
    }

    void TryJet()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            myRigid.AddForce(Vector3.up * jetPackSpeed);    //(0, 1, 0)
        }
        else
        {
            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
}
