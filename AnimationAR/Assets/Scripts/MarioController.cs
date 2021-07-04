using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarioController : MonoBehaviour
{
    private bool isCharacterSpawned;
    
    [SerializeField]
    private GameObject marioPrefab;
    
    
    // Update is called once per frame
    void Update()
    {
        if (PlaneFinder.isGameStarted && !isCharacterSpawned)
        {
            isCharacterSpawned = true;
            StartCoroutine(SpawnMario());
            
        }
    }

    IEnumerator SpawnMario()
    {
        
        Vector3 tappedPosition = PlaneFinder.placementPose.position;
        
        Instantiate(marioPrefab, tappedPosition,Quaternion.Euler(0, 180, 0));

        yield return new WaitForEndOfFrame();
    }
}
