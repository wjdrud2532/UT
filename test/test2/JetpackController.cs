using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : FollowPlayer
{                                   //상속으로 FollowPlayer을 받는다. 

    [Header("제트 엔진 회전 속도")] [Range(0, 1)]
    [SerializeField] float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = tf_Player.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.position = Vector3.Lerp(transform.position, tf_Player.position - currentPos, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), spinSpeed);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.position = Vector3.Lerp(transform.position,
                tf_Player.position - new Vector3(currentPos.x, currentPos.y, -currentPos.z), speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-100, 0, 0), spinSpeed);

        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            transform.position = Vector3.Lerp(transform.position,
                tf_Player.position - new Vector3(currentPos.x, currentPos.y, 0f), speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-56, 0, 0), spinSpeed);

        }

    }
}
