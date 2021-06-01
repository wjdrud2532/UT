using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    public float NoteSpeed = 400;

    UnityEngine.UI.Image noteImage;

    private void OnEnable() //객체가 활성화 될 때마다 실행 -> 노트가 끝날 때 비활성화 되었던 이미지를 다시 활성화하는 역할을 함
    {
        if(noteImage == null)
;        noteImage = GetComponent<UnityEngine.UI.Image>();

        noteImage.enabled = true;
    }



    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.right * NoteSpeed * Time.deltaTime;
        //그냥 Position하면 canvars안에서 움직이는 것이 아니라 전체 월드 내에서 움직이기 때문에
        //지정된 canvars안에서만 움직일수 있도록 localPosition을 하는 것
    }

    public void HideNode()
    {
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
