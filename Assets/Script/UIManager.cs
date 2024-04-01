using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject LoseUI;
    private void Awake()
    {
        Instance = this;
    }
    public void SetScore(int score)
    {
        
        scoreText.text = score.ToString();
    }
    public void SetLevel(int level)
    {
        levelText.text = level.ToString(); 
    }
    public void EnanbleLoseUI()
    {
        
        LoseUI.SetActive(true);
    }
    public void DeactiveLoseUI()
    {
        LoseUI.SetActive(false);    
    }
}
