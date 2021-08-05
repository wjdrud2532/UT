using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{

    //AudioSource myAudio;
    bool musicStart = false;

    public string bgmName = "";

    // Start is called before the first frame update
    //void Start()
    //{
    //    myAudio = GetComponent<AudioSource>();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                //myAudio.Play();
                AudioManager.instance.PlayBGM(bgmName);  //BGM0의 이름을 가진 음악을 재생
               
                musicStart = true; 
            }
        }
    }

    public void ResetMusic()        //재시작시 호출
    {
        musicStart = false;
    }
}
