using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        //카메라의 테크가 mainCamera인 객체의 값을 가져온다.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraPos.x));   
                               //카메라 화면상의 좌표를 실제 3d좌표로 치환시키는 메소드이다..
                                                                                    //카메라의 x좌표가 필요한 이유 - 3d죄표값을 위해서(카메라의 거리와
                                                                     //플레이어가 움직이는 공간의 거리를 채워주는 것 (총이 마우스를 따라다니게 할 때 이 값이 없으면 총구가 카메라를 바라보게 된다)
        Vector3 target = new Vector3(0f, mousePos.y, mousePos.z);
        transform.LookAt(target);
    }
}
