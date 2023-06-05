using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public delegate void ChoiceChangeDelegate(int choiceIndex);
    public static ChoiceChangeDelegate OnChoiceChange;

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
        choiceIndex++;
        if (choiceIndex < 0)
        {
            choiceIndex = playerPrefabs.Length - 1;
        }
        else if (choiceIndex >= playerPrefabs.Length)
        {
            choiceIndex = 0;
        }
        OnChoiceChange?.Invoke(choiceIndex);
    }

    public void PreviousChoice()
    {
        choiceIndex--;
        if(choiceIndex < 0)
        {
            choiceIndex = playerPrefabs.Length - 1;
        }
        else if (choiceIndex >= playerPrefabs.Length)
        {
            choiceIndex = 0;
        }
        OnChoiceChange?.Invoke(choiceIndex);
    }

    public GameObject GetPlayerPrefab()
    {
        return playerPrefabs[choiceIndex];
    }
}
