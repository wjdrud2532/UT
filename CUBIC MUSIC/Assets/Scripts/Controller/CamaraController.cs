
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField] Transform thePlayer = null; //카메라가 따라갈 대상
    [SerializeField] float followSpeed = 15;    //카메라의 이동 속도

    Vector3 playerDistance = new Vector3(); //카메라와 큐브의 거리 차이를 기억시키기 위한 벡터

    float hitDistance = 0;
    [SerializeField] float zoomDistance = -1.25f;

    // Start is called before the first frame update
    void Start()
    {
        playerDistance = transform.position - thePlayer.position;   //카메라의 위치에서 플레이어의 위치를 뺀다
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
                            //Lerp(A, B, C) A와 B사이의 값에서 C비율의 값을 추출  (0, 10, 0.5) => 5
    }

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f); //0,.15f초 뒤에 원상복귀되

        hitDistance = 0;
    }
}
