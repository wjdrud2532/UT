using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    [SerializeField] GameObject goTitleMenu = null;

    public void BtnBack()
    {
        goTitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
