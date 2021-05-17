using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play();
        Destroy(gameObject, destroyTime);
    }
}
