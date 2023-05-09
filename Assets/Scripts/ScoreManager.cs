using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int m_currentScore;
    void Start()
    {
        
    }

    public void Add(int p_score)
    {
        m_currentScore += p_score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
