using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointLightTest : MonoBehaviour
{

    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject go_Cube;

    float CountTime = 0;

    private void Start()
    {

        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CountTime += (Time.deltaTime);
        this.transform.RotateAround(go_Cube.transform.position, Vector3.up, 100 * Time.deltaTime);

        if(CountTime >= 5.0)
        {
            this.gameObject.SetActive(false);
            Title.SetActive(true);
        }
    }
}
