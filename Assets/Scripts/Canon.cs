using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private Bullet m_bulletShoot;
    [SerializeField] private Transform m_shootingPoint;
    [SerializeField] private Transform m_bulletParent;
    [SerializeField] private KeyCode shootKeyCode;


    private void Update()
    {
        if (Input.GetKeyDown(shootKeyCode))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(m_bulletShoot, m_shootingPoint.position, m_shootingPoint.rotation, m_bulletParent);
        Debug.Log("Shoot");
    }
}