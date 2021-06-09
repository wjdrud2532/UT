using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    Vector3 destPos = new Vector3();

    //회전
    [SerializeField] float spinSpeed = 270;
    Vector3 rotDir = new Vector3();         //방향값
    Quaternion destRot = new Quaternion();  //회전값

    //반동
    [SerializeField] float recoilPosY = 0.25f;  //큐브가 얼마나 들썩일지
    [SerializeField] float recoilSpeed = 1.5f;   //큐브 들썩임의 속도


    bool canMove = true;

    //기타
    [SerializeReference] Transform fakeCube = null;     //가짜큐브를 먼저 돌려 놓고, 그 돌아간 만큼의 값을 목표 회전값으로 삼기 위함
    [SerializeReference] Transform realCube = null;

    TimingManager TheTimingManager;
    CamaraController theCam;

    // Start is called before the first frame update
    void Start()
    {
        TheTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CamaraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove)
            {
                //판정체크
                if (TheTimingManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }

    }
    void StartAction()
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
}
