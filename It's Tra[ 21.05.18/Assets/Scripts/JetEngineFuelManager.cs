using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetEngineFuelManager : MonoBehaviour
{

    [SerializeField] float maxFuel;     //최대 연료
    float currentFuel;  //현재 연료

    [SerializeField] Slider slider_JetEngine;
    [SerializeField] Text txt_JetEngine;

    PlayerController thePC;

    public bool isFuel { get; private set; }

    [SerializeField] float waitRechargeFuel;    //재충전 대기시간
    float currentWaitRechargeFuel;  //계산에 사용

    [SerializeField] float rechargespeed;   //재충전속도

    // Start is called before the first frame update
    void Start()
    {
        currentFuel = maxFuel;
        slider_JetEngine.maxValue = maxFuel;
        slider_JetEngine.value = currentFuel;
        thePC = FindObjectOfType<PlayerController>();   //해당 타입이 있는지 하이라케를 찾아봄, public으로 설정된 것들을 사용 가능해짐
    }

    // Update is called once per frame
    void Update()
    {
        CheckFuel();

        UsedFuel();

        slider_JetEngine.value = currentFuel;
        txt_JetEngine.text = Mathf.Round(currentFuel / maxFuel * 100f).ToString() + " %";
    }

    void FuelRecharge()
    {
        if(currentFuel < maxFuel)
        {
            currentWaitRechargeFuel += Time.deltaTime;
            if(currentWaitRechargeFuel >= waitRechargeFuel)
            {
                currentFuel += rechargespeed * Time.deltaTime;
            }
        }
        else
        {       //현재 연료가 100이상일 경우
            slider_JetEngine.gameObject.SetActive(false);    //액티브 비활성화(UI안보임)

        }
    }

    void CheckFuel()
    {
        if (currentFuel > 0)
            isFuel = true;
        else
            isFuel = false;
    }

    void UsedFuel()
    {

        if (thePC.IsJet)
        {
            slider_JetEngine.gameObject.SetActive(true);    //액티브 활성화(UI보임)
            currentFuel -= Time.deltaTime;

            currentWaitRechargeFuel = 0;

            if (currentFuel <= 0)
                currentFuel = 0;


        }
        else
        {
            FuelRecharge();
        }

    }
}
