using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("따라갈 플레이어 설정")]
    [SerializeField] Transform tf_Player;

    [Header("따라갈 스피드 지정")]
    [Range(0, 1f)] [SerializeField] float chaseSpeed;

    float comNormalXPos;

    [SerializeField] [Header("부스터시 떨어질 거리 x")]
    float comJetPos;
    float camcurrentXPos;       //부스터 사용시 JetX
                                //평상시 NormlX 좌표만큼 거리가 떨어진다.


    PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = tf_Player.GetComponent<PlayerController>();
        comNormalXPos = transform.position.x;
        camcurrentXPos = comNormalXPos;
    }

    // Update is called once per frame
    void Update()
    {

        if (thePlayer.IsJet)
        {
            camcurrentXPos = comJetPos;
        }
        else
            camcurrentXPos = comNormalXPos;

        Vector3 movePos = Vector3.Lerp(transform.position, tf_Player.position, chaseSpeed);

        float cameraPosX = Mathf.Lerp(transform.position.x, camcurrentXPos, chaseSpeed);
        transform.position = new Vector3(cameraPosX, movePos.y, movePos.z);
                //일정한 x좌표만큼 떨어져서 플레이어를 계속 따라다니게 f
    }
}
