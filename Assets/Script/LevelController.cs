using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [SerializeField]
    List<Level> levels = new List<Level>();
    [SerializeField]
    private PlayerController player;
    private Level currentlevel;
    private int level;
    private Vector3 offset = new Vector3(0, 2.5f, 0);
    [SerializeField] private TMP_Text text;
    [SerializeField] private Animator animator;
    private int currentLevelScore = 0;
    private void Awake()
    {
        level = 1;
        StartCoroutine(NextLevel());
        Instance = this;
        UpdateUI(level);
    }
    public IEnumerator NextLevel()
    {   
        currentLevelScore = 0;
        if (currentlevel != null)
        {
            animator.gameObject.SetActive(true);
            animator.SetTrigger("End");
            yield return new WaitForSeconds(1);
            level++;
            Destroy(currentlevel.gameObject);
            animator.SetTrigger("Start");
        }
        currentlevel = Instantiate(levels[level - 1]);
        this.Onit();
        UpdateUI(level);

    }

    public void RePlayCurretLevel()
    {   
        player.SetScore(player.GetScore() - currentLevelScore);
        UIManager.Instance.SetScore(player.GetScore());
        level--;
        StartCoroutine(NextLevel());
        UIManager.Instance.DeactiveLoseUI();
    }
    private void Onit()
    {
        player.onInit();
        player.transform.position = currentlevel.GetStartingPoint().position + offset;
    }
    private void UpdateUI(int level)
    {
        text.text = level.ToString();
    }
    public int GetCurrentLevelScore()
    {
        return currentLevelScore;
    }
    public void SetCurrentLevelScore(int score)
    {
        this.currentLevelScore = score;
    }
}
