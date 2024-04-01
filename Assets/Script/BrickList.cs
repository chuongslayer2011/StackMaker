using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickList : MonoBehaviour
{
    public List<GameObject> brickList;
    private int indexTopBrick;
    private GameObject topBrick;
    [SerializeField] private PlayerController player;
    public void addBrick(GameObject brick)
    {       
        brickList.Add(brick);
    }
    public void removeBrick()
    {
        if (brickList.Count <= 0)
        {   
            player.Lose();
            return;
        }
        else
        {   
            indexTopBrick = brickList.Count - 1;
            topBrick = brickList[indexTopBrick];
            if (topBrick != null)
            {
                brickList.RemoveAt(indexTopBrick);
                player.SetPlayerPosition(topBrick.transform.position);
                Destroy(topBrick);
            } 
            
        }
        
    }
}
