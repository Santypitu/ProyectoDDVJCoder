using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreManager m_scoreManager;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore(int p_score)
    {
        m_scoreManager.Add(p_score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
