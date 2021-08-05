using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       //곡의 정보를 텍스트로 보여주기 위해서 

[System.Serializable]
public class Song       //곡의 정보를 담고 있는 클래스
{
    public string name;         //이름
    public string composer;     //작곡가
    public int bgm;             //여기 지정된 bpm에 따라 곡의 빠르기가 결정되도록 한다.
    public Sprite sprite;       //이미지
}
public class StageMenu : MonoBehaviour
{
    [SerializeField] Song[] songList = null;

    [SerializeField] Text txtSongName = null;
    [SerializeField] Text txtSongComposer = null;
    [SerializeField] Image imgDisk = null;

    [SerializeField] GameObject TitleMenu = null;

    [SerializeField] Text txtSongScore = null;      //점수 채워넣을 부분 

    DatabaseManager theDatabase;

    int currentSong = 0;

    //   start 대신 
    void OnEnable()
    {
        if(theDatabase = null)
        theDatabase = FindObjectOfType<DatabaseManager>();

        SettingSong();
    }

    public void BtnNext()   //다음 버튼 누르면 다음 곡이 나타나도록 함 
    {
        AudioManager.instance.PlaySFX("Touch");     //Touch와 같은 이름을 가진 효과음을 AudioManager에서 찾아 재생시킨다.

        if (++currentSong > songList.Length - 1)    //다음 곡이 없다면
            currentSong = 0;                        //처음으로 돌아간다.

        SettingSong();
    }

    public void BtnPrior()   
    {
        AudioManager.instance.PlaySFX("Touch");

        if (--currentSong < 0)    
            currentSong = songList.Length - 1;      //이전 곡이 없다면 최대값으로 돌아간다.                         

        SettingSong();
    }

    void SettingSong()
    {
        txtSongName.text = songList[currentSong].name;
        txtSongComposer.text = songList[currentSong].composer;

        txtSongScore.text = string.Format("{0:#,##0}", theDatabase.score[currentSong]);
        imgDisk.sprite = songList[currentSong].sprite;

        //음악 선택화면에서 음악을 들을 수 있게 
        AudioManager.instance.PlayBGM("BGM" + currentSong); //오디오의 이름을 등록할 때 BGM0 ~ 3으로 설정했기 때문에 그에 맞는 음악이 나온다
     }

    public void BtnBack()
    {
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BtnPlay()
    {
        int t_bpm = songList[currentSong].bgm;

        //게임 시작시                    현재 음악과,     bpm을 넘겨준다
        GameManager.instance.GameStart(currentSong, t_bpm);
        this.gameObject.SetActive(false);
    }
}
