using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool canMove = true;


    [Header("속도 관련 변수")]
    [SerializeField] float movespeed;
    [SerializeField] float jetPackSpeed;
    Rigidbody myRigid;

    public bool IsJet { get; private set; } //Priperty는 속성이라 부르며, 변수의 참조와 수정을 따로 관리 가능하다
                                            //set은 privete 설정을 했기 때문에 PlayerController안에서만 IsJet의 값을 변경 가능하다
                                            //get 앞에는 publuc 가 생략되어 있음

    [Header("파티클 시스템(부스터)")]
    [SerializeField] ParticleSystem ps_LeftEngine;
    [SerializeField] ParticleSystem ps_RightEngine;

    AudioSource audioSource;

    JetEngineFuelManager theFuel;

    // Start is called before the first frame update
    void Start()
    {
        IsJet = false;
        myRigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        theFuel = FindObjectOfType<JetEngineFuelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        TryMove();
        TryJet();
        
           
    }


    void TryMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && canMove) // 상, 하(W, S) 방향키가 눌렸을 경우
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
        if (!IsJet)
        {
            ps_LeftEngine.Play();
            ps_RightEngine.Play();
            audioSource.Play();
            IsJet = true;
        }

        if(Input.GetKey(KeyCode.Space) && theFuel.isFuel && canMove)
        {
            myRigid.AddForce(Vector3.up * jetPackSpeed);    //(0, 1, 0)
        }
        else
        {
            if (IsJet)
            {
                ps_LeftEngine.Stop();
                ps_RightEngine.Stop();
                audioSource.Stop();
                IsJet = false;
            }

            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
}
