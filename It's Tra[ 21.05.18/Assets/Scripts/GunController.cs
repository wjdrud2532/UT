using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("현재 장착된 총")]
    [SerializeField] Gun currentGun;

    float currentFireRate;

    [SerializeField] Text txt_CurrentGunBullet;


        /*
         * currentFireRate가 1초에 1씩 감소하다가 currentFireRate <= 0 이면 발사 가능
         * 이후 currentFireRate를 FireRate 값으로 리셋한다.
         */

    //[Header("총구 섬광")]
    //[SerializeField] ParticleSystem ps_MuzzleFlash;

    //[Header("총알 프리팹")]
    //[SerializeField] GameObject go_Bullet_Prefab;


    //[Header("총알 스피드")]
    //[SerializeField] float bulletSpeed;

    // Start is called before the first frame update

    public void BulletUISetting()
    {
        txt_CurrentGunBullet.text = "x " + currentGun.bulletCount;

    }

    void Start()
    {

        currentFireRate = 0;        //시작하자마자 바로 발사 가능
        BulletUISetting();
     }

    // Update is called once per frame
    void Update()
    {
        FireRateCalc();
        TryFire();
        LockOnMouse();
    }

    void FireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    void TryFire()
    {
        if(Input.GetButton("Fire1") && currentGun.bulletCount > 0)        //Fire1은 마우스 왼쪽 클릭 감지(디폴트값) 이다.
        {
            if(currentFireRate <= 0)
            {
                Fire();
                currentFireRate = currentGun.fireRate;  //다시 0.2의 값이 들어가게 됨
                
            }
        }
    }

    void Fire()
    {
        //Debug.Log("총알 발사");
        currentGun.bulletCount--;
        BulletUISetting();
        currentGun.animator.SetTrigger("GunFire");

        SoundManager.instance.PlaySE(currentGun.sound_Fire);
        //SoundManager.instance.PlaySE("NormalGun_Fire");
        currentGun.ps_MuzzleFlash.Play();

        var clone = Instantiate(currentGun.go_Buller_Prefab, currentGun.ps_MuzzleFlash.transform.position, Quaternion.identity);      //프리팹을 생성(총알 생성)
                                                                                                                //생성된 총알을 변수로 관리하기 위한 var clone

        clone.GetComponent<Rigidbody>().AddForce(transform.forward * currentGun.speed);
            //총알이 담겨있는 clone으로 rigidbodt를 꺼내와 addforce로 날려보낸다.

    }

    void LockOnMouse()
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
