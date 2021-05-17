using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] Gun[] guns;

    GunController theGC;

    private void Start()
    {
        theGC = FindObjectOfType<GunController>();

    }

    const int NORMAL_GUN = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            //item에 부딪힌 Item의 정보가 들어감

            int extra = 0;

            if(item.itemtype == ItemType.Score)
            {
                SoundManager.instance.PlaySE("Score");
                extra = item.extraScore;
                ScoreManager.exterScore += extra;
            }
            else if (item.itemtype == ItemType.NormalGUn_Bullet)
            {
                SoundManager.instance.PlaySE("Bullet");
                extra = item.extraBullet;
                guns[NORMAL_GUN].bulletCount += extra;
                theGC.BulletUISetting();
            }
            string message = "+" + extra;
                                                            //other.transform.position
            FloatingTextManager.instace.CreateFloatingText(new Vector3(0, 1, 7), message);

            Destroy(other.gameObject);
        }
    }
}
