using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score,
    NormalGUn_Bullet,
}

public class Item : MonoBehaviour
{
    public ItemType itemtype;

    public int extraScore;
    public int extraBullet;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
