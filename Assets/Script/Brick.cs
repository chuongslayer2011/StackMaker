using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject BrickPrefab;
    private GameObject topBrick;
    private Vector3 currentPlayerPosition;
    private Vector3 offset = new Vector3(0, 0.4f, 0);
    private void Start()
    {  
              
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            int score = collision.GetComponent<PlayerController>().GetScore() + 1;
            collision.GetComponent<PlayerController>().SetScore(score);
            transform.gameObject.SetActive(false);           
            currentPlayerPosition = collision.GetComponent<PlayerController>().GetCurrentPlayerPosition();
            topBrick = Instantiate(BrickPrefab, currentPlayerPosition , Quaternion.Euler(-90, 0, 180), collision.transform);
            collision.GetComponent<BrickList>().addBrick(topBrick);
            collision.GetComponent<PlayerController>().Up();
            currentPlayerPosition.y = topBrick.transform.position.y + 0.3f;
            collision.GetComponent<PlayerController>().SetPlayerPosition(currentPlayerPosition);
            UIManager.Instance.SetScore(score);
            LevelController.Instance.SetCurrentLevelScore(LevelController.Instance.GetCurrentLevelScore() + 1);                    
        }
        
    }
    
}
