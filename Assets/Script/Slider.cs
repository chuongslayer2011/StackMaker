using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    //[SerializeField] private GameObject brickOnSlider;
    [SerializeField] private Material passingColor;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().listBrick.removeBrick();
            transform.GetComponent<MeshRenderer>().material = passingColor;
            
            collision.gameObject.GetComponent<PlayerController>().SetMovingSpeed(6f);
            //brickOnSlider.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<PlayerController>().SetMovingSpeed(15f);
        }
    }
}
