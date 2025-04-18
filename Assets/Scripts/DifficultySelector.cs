using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    public Toggle easyToggle;
    public Toggle brutalToggle;

    private void Start()
    {
        easyToggle.onValueChanged.AddListener((isOn) => {
            if (isOn) AIController.Instance.SetDifficulty(false);
        });

        brutalToggle.onValueChanged.AddListener((isOn) => {
            if (isOn) AIController.Instance.SetDifficulty(true);
        });

        easyToggle.isOn = true;
    }
}
