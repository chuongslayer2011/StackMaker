using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    
    public Transform GetStartingPoint()
    {
        return startingPoint;
    }
    
}
