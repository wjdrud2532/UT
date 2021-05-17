using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 이게 있어야 다음 씬으로 넘어갈 수 있다
public class Title : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("InGameScenes");

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
