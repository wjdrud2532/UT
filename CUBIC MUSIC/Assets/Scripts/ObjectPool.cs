using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public class ObjectInfo
{
    public GameObject goPrefab; //goPrefab를 필요한 만큼 생성시킨다
    public int count;   //필요한 양
    public Transform tfPoolParent; //어느 부모로 생성시킬지
}

public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    public static ObjectPool instance;      //언제 어디서든 가져오고 반납할 수 있도록 공유자원 으로 만듦 -> 어디서든 public 변수, 함수에 접근 가능
                                            //public static 은 공유자원이라는 의미

    public Queue<GameObject> noteQueue = new Queue<GameObject>();   //선입선출 알고리즘

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(objectInfo[0]);
        //noteQueue = InsertQueue(objectInfo[1]);       //또 다른, 객체의 생성과 삭제가 많이 이루어 지는 객체가 있을 경우 index를 늘려가며 다른 Queue를 만들어 활용한다
        //noteQueue = InsertQueue(objectInfo[2]);
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)  //GameObject의 타입을 가지고 있는 Queue를 리턴시키는 함수, 오브젝트 정보를 인자로 받는다
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();    //임시 Queue
        for (int i = 0; i < p_objectInfo.count; i++)    //카운트 개수만큼 반복(프리팹 생성)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefab, transform.position, Quaternion.identity);
            t_clone.SetActive(false);       //객체를 생성 후 비활성화

            if (p_objectInfo.tfPoolParent != null)  //부모설정  
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);     //부모 객체가 존재한다면 그것을 부모로 하고
            else
                t_clone.transform.SetParent(this.transform);        //아니라면 이 스크립트가 붙어 있는 객체를 부모로 한다.

            t_queue.Enqueue(t_clone);
        }
        return t_queue;     //count 만큼의 객체가 담겨있는 임시 Queue를 반환한다.
    }
}
