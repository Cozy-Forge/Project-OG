using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityChest : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        InfinityTimeManager.Instance.bGameStart = true;
        InfinityModeManager.Instance.Spawn();
        GetComponent<RandomSpawner>().OnInteract();
        gameObject.SetActive(false);
    }
}