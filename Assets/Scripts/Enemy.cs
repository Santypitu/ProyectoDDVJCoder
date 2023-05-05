using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Enemy : MonoBehaviour
{
    public enum EnemyTypes
    {
        EnemySmall,
        EnemyGiant
    }

    [SerializeField] private Transform player;
    [SerializeField] private float distanceMaxToChase;
    [SerializeField] private float speed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private EnemyTypes enemyType;

    void Move(Vector3 direction) 
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Chase1()
    {
        var l_diffVector= player.position-transform.position;
        if(distanceMaxToChase<l_diffVector.magnitude) 
        {
            speed =5f;
            Quaternion rotation = Quaternion.LookRotation(l_diffVector.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
            Move(l_diffVector.normalized);
        }
    }    void Chase2()
    {
        var l_diffVector= player.position-transform.position;
        if(distanceMaxToChase<l_diffVector.magnitude) 
        {
            Quaternion rotation = Quaternion.LookRotation(l_diffVector.normalized);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
            Move(l_diffVector.normalized);
        }
    }

    //void LookAt()
    //{
    //    var l_diffVector = player.position - transform.position;

    //        Quaternion rotation = Quaternion.LookRotation(l_diffVector.normalized);
    //        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
        
    //}


    // Update is called once per frame
    void Update()
    {
       
        switch (enemyType)
            {
                case EnemyTypes.EnemySmall:
                    Chase1();
                    break;
                case EnemyTypes.EnemyGiant:
                    Chase2();
                    break;

            }
        
    }


}
