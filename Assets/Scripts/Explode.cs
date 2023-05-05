using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private float timeToExplode = 2;
    [SerializeField] private float timeRemaining;
    [SerializeField] private GameObject explosionPrefab;

    [SerializeField] private Transform m_explotePoint;
    [SerializeField] private Transform m_exploteParent;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timeToExplode;
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(timeRemaining <= 0 ) 
        {
            bombExplode();
        }
    }
    void bombExplode()
       
    {
        Instantiate(explosionPrefab,  m_explotePoint.position, m_explotePoint.rotation, m_exploteParent);

        Destroy(gameObject);
    }
}
