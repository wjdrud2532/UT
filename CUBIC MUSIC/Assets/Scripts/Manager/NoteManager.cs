using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{

    public int bpm = 0;
    double currentTime = 0d;    //리듬게임이라 정확도가 중요하기 때문에 int가 아닌 double을 사


    [SerializeField] Transform tfNoteAppear = null;
    //[SerializeField] GameObject goNote = null;

    TimingManager TheTimingManager;
    EffectManager theEffectManager;

    private void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        TheTimingManager = GetComponent<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)     //120bpm이라면 0.5초가 지난 뒤 노트를 생성하게 됨
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();    //공유자원인 ObjectPool의 instance에서 안에 담긴 객체를 가져온다
            t_note.transform.position = tfNoteAppear.position;      //이 객체에 적절한 위치값을 준 뒤
            t_note.SetActive(true);     //활성화 상태로 만든다.

            //GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            ////t_note.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);        //0.6f로 해야 1.xx 로 나온다. 왜??
            //t_note.transform.SetParent(this.transform);

            TheTimingManager.boxNoteList.Add(t_note);       //타이밍 매니저에 해당 노트를 추가

            currentTime -= 60d / bpm;
            //currentTime = 0을 하지 않는 이유는 0.51005551... 같이 정확히 0.5초가 아니기 때문에 오차를 줄이기 위해서 -60d/bpm을 한다.

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())   //이미지의 활성상태를 리턴한다 -> 노트판정이 완료되어 이미지를 지웠다면 끝에 다다랐을 때 miss를 활성화하지 않는다.
                theEffectManager.JudgementEffect(3);
            TheTimingManager.boxNoteList.Remove(collision.gameObject);  //노드가 파괴될ㄷ 때 해당 노트를 삭

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);     //가져온 객체를 반납   
            collision.gameObject.SetActive(false);  //이후 비활성화

            // Destroy(collision.gameObject);
        }
    }
}


/*
 * 오브젝트 풀링이 필요한 이유
 * 객체 생성과 객체 파괴는 꽤 소모적인 일이기 때문에 사양이 낮은 환경(모바일같은)에서 원활하게 작동시키기 위함
 * 
 * 작동 원리
 * 미리 객체를 생성하고 필요할 때 마다 가져다 쓰는 
 */