using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public int energy = 0;
    public int CurrentEnergy => energy;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
        UIController.Instance.UpdateEnergyUI(energy);
    }

    public bool UseEnergy(int cost)
    {
        if (energy >= cost)
        {
            energy -= cost;
            UIController.Instance.UpdateEnergyUI(energy);
            return true;
        }
        return false;
    }

    public void ResetSession()
    {
        energy = 0;
        UIController.Instance.UpdateEnergyUI(energy);
    }
}
