using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;  
    [SerializeField] private Transform direction;
    [SerializeField] private Vector3 directionVector;
    [SerializeField] private float m_damage;

    void Update()
    {
        directionVector = direction.position;
        transform.position += speed * Time.deltaTime * -directionVector;
    }


}
