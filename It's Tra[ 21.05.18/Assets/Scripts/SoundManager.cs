using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]       //클래스 자체를 인스펙터 창에서 선택할 수 있게 
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;    //static 선언으로 공유 변수로 만든다(어디서든 참조, 변경이 가능)
                                            //클래스 자체를 &&&&&&&&&&


    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("bgm 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;         //배열로 선언 -> 효과음은 동시에 여러개가 출려되기 때문

    // Start is called before the first frame update
    void Start()
    {
        instance = this;                   //&&&&&&&&&&&
        PlayerRandomBGM();
    }

    public void PlayerRandomBGM()
    {
        int random = Random.Range(0, 2);    //랜덤으로 설정하고 싶은 값이 정수일 경우 0, 2를 하면 0 ~ 1사이의 값을 무작위로 설정하게 된다
        bgmPlayer.clip = bgmSounds[random].clip;        //넣어놓은 clip중 무작위 하나가 설정됨
        bgmPlayer.Play();

    }

    public void PlaySE(string _soundName)
    {
        for(int i = 0; i < sfxSounds.Length; i ++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                for(int x = 0; x < sfxPlayer.Length; x ++)
                {
                    if(!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다!!");
                return;
            }
        }
        Debug.Log("등록된 효과음이 없습니다");
    }

}
