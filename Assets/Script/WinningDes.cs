using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningDes : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparlke1;
    [SerializeField] private ParticleSystem sparlke2;
    [SerializeField] private GameObject chestOpen;
    [SerializeField] private GameObject chestClose;
    
    
    private void OnTriggerEnter(Collider collison)
    {
        if (collison.CompareTag("Player"))
        {
            sparlke1.Play();
            sparlke2.Play();
            //collison.gameObject.GetComponent<PlayerController>().Winning();
            collison.gameObject.GetComponent<PlayerController>().ClearBrickList();
            chestClose.SetActive(false);
            chestOpen.SetActive(true);
            Invoke(nameof(MoveToNextLevel), 5f);
        }
    }
    private void MoveToNextLevel()
    {
        StartCoroutine(LevelController.Instance.NextLevel()); ;
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Winning();
        }
    }
}
