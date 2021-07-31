using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]       //밖에서 수정이 가능하도록 
public class Sound      //sound 정보를 담고있는 클래스 생성     
{
    public string name;
    public AudioClip clip;      //음악 파일을 담는 클립
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    //플레이어를 언제 어디서든 호출할 수 있도록 자기 자신을 인스펙터로 만든다.

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    private void Start()
    {
        instance = this;    //시작과 동시에 자기 자신을 저장 
    }

    public void PlayBGM(string p_bgmName)
    {
        for(int i = 0; i < bgm.Length; i ++)        //bgm개수만큼 반복
        {
            if(p_bgmName == bgm[i].name)            //인자로 받은 이름과 같은 이름을 가진 bgm을 찾는다
            {
                bgmPlayer.clip = bgm[i].clip;       //찾은 클립을 플레이어에 저장한다
                bgmPlayer.Play();                   //재생한다.
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();                           //재생 정지
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)        //sfx중에서
        {
            if (p_sfxName == sfx[i].name)           //같은 이름을 찾고
            {
                for(int x = 0; x < sfxPlayer.Length; x ++)  //sfx플레이어에서
                {
                    if(!sfxPlayer[x].isPlaying)             //사용중이 아닌 플레이어를 찾아서
                    {
                        sfxPlayer[x].clip = sfx[i].clip;    //재생하고자 하는 클립을 넣는다.
                        sfxPlayer[x].Play();                //그리고 재생한다.
                        return;
                    }
                }
                //만약 모든 플레이어가 사용중이라면
                Debug.Log("모든 오디오 플레이어가 재생중");
                return;
            }
        }

        Debug.Log(p_sfxName + "에 해당하는 효과음이 없습니다.");
    }
}
