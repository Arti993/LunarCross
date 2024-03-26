using System;
using UnityEngine;
using IJunior.TypedScenes;
using TMPro;

public class LevelButton : MonoBehaviour
{
    private TMP_Text _buttonTextField;

    private void Awake()
    {
        _buttonTextField = GetComponentInChildren<TMP_Text>();
    }

    public void OnClick()
    {
        SaveSelectedLevelNumber();

        Gameplay.Load();
    }

    private void SaveSelectedLevelNumber()
    {
        if (int.TryParse(_buttonTextField.text, out int clickedButtonNumber) == false)
            throw new InvalidOperationException();

        PlayerPrefs.SetInt("SelectedLevelNumber", clickedButtonNumber);
    }
}