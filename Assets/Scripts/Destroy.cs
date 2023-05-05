using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    void Update()
    {
        DestroyExplote();
    }

    void DestroyExplote()
    {
        Destroy(gameObject,2f);
    }
}
