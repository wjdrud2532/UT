using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPresskey = true;

    //이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();
    Vector3 originPos = new Vector3();

    //회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();         //방향값
    Quaternion destRot = new Quaternion();  //회전값

    //반동
    [SerializeField] float recoilPosY = 0.25f;  //큐브가 얼마나 들썩일지
    [SerializeField] float recoilSpeed = 1.5f;   //큐브 들썩임의 속도


    bool canMove = true;
    bool isFalling = false;

    //기타
    [SerializeReference] Transform fakeCube = null;     //가짜큐브를 먼저 돌려 놓고, 그 돌아간 만큼의 값을 목표 회전값으로 삼기 위함
    [SerializeReference] Transform realCube = null;

    TimingManager TheTimingManager;
    CamaraController theCam;
    Rigidbody myRigid;              //낭떨어지에서 떨어지기 위해서는 중력옵션을 켜야한다.
    StatusManager theStaus;


    // Start is called before the first frame update
    void Start()
    {
        TheTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CamaraController>();
        myRigid = GetComponentInChildren<Rigidbody>();          //현재 객체에는 rigid가 없지만 자식 객체인 cube에는 존재, 그 값을 가져오기 위해 inchildren을 사용
        originPos = transform.position; //시작하자마자 자신의 위치를 저장
        theStaus = FindObjectOfType<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStartGame)
        {
            CheckFalling();

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
            {
                if (canMove && s_canPresskey && !isFalling)
                {
                    Calc();

                    //판정체크
                    if (TheTimingManager.CheckTiming())
                    {
                        StartAction();
                    }
                }
            }
        }
        

    }

    void Calc()
    {
        //방향계산
        //                                  상하 움직임 0
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        //GetAxisRaw("Vertical") 입력값 W or 위 방향키 = 1, S or 아래 방향키 = -1, 없을 시 0

        //이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);      //x축을 굴리면 오른쪽, 왼쪽으로 움직인다. z축도 마찬가지
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);        //해당 물체의 주변을 공전함
        destRot = fakeCube.rotation;

    }

    void StartAction()
    {
        
        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
        StartCoroutine(theCam.ZoomCam());
        //
    }


    IEnumerator MoveCo()
    {
        canMove = false;

                   //Distance - A좌표와 B좌표간의 거리차를 반환
        //while(Vector3.Distance(transform.position, destPos) != 0)
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f) //더 가벼움
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        //아주 근소한 차이가 있을 수 있기 때문에
        transform.position = destPos;
        canMove = true; 
    }

    IEnumerator SpinCo()
    {           //2개 값의 차이를 구한다                 이 둘의 차이가    이하일 때 
        while(Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;
    }

    IEnumerator RecoilCo()
    {
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while (realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }

    void CheckFalling()     //큐브 아래로 레이저를 쏴서 플레이트가 존재하는지 확인
    {                       //                              존재한다면 떨어지지 않고, 존재하지 않는다면 떨어진다.
        if(!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))    // ! 확인
            {
                Falling();
            }
        }
        
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
    }

    public void ResetFalling()
    {
        theStaus.DecreaseHp(1);     //큐브가 떨어질 경우 1만큼 체력을 감소시킨다.
        AudioManager.instance.PlaySFX("Falling");

        if (!theStaus.IsDead())       //체력이 감소된 뒤에도 체력이 남아있다면 위치를 되돌리고 계속 이어나간다
        {
            isFalling = false;
            myRigid.useGravity = false;
            myRigid.isKinematic = true;

            transform.position = originPos;
            realCube.localPosition = new Vector3(0, 0, 0);   //rigidbody가 없는 부모 객체는 낭떠러지 위에, 자식 객체만 추락중ㅈ
        }
        
    }

    public void Initialized()   //게임 재시작시 초기 함수
    {
        transform.position = Vector3.zero;  //위치 초기	
        destPos = Vector3.zero; 
        realCube.localPosition = Vector3.zero;
        canMove = true;
        s_canPresskey = true;   //키 입력 가능하도록 
        isFalling = false;
        myRigid.useGravity = false;
        myRigid.isKinematic = true;
    }
}
