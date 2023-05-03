using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateCamara : MonoBehaviour
{
    [SerializeField] private GameObject camera1,camera2;
  

    private void AlternateCameras()
    {
        camera1.SetActive(!camera1.activeSelf);
        camera2.SetActive(!camera2.activeSelf);
    }

    // Start is called before the first frame update
    void Start()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AlternateCameras();
        }

    }

}
