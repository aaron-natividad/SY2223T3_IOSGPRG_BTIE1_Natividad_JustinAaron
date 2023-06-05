using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    public int choiceIndex = 0;
    public GameObject[] playerPrefabs;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NextChoice()
    {
        choiceIndex = (choiceIndex + 1) % playerPrefabs.Length;
    }

    public void PreviousChoice()
    {
        choiceIndex = (choiceIndex - 1) % playerPrefabs.Length;
    }

    public GameObject GetPlayerPrefab()
    {
        return playerPrefabs[choiceIndex];
    }
}
