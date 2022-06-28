using UnityEngine;
using UnityEngine.UI;
using System;

public class UIConstructor : MonoBehaviour
{

    [SerializeField] private Button _generateButton;
    [SerializeField] private Button _shuffleButton;
    [SerializeField] private InputField _letterWidthField;
    [SerializeField] private InputField _letterHeightField;
    [SerializeField] private Text _wrongMessage;
    [SerializeField] private LettersSystem _lettersSystem;
    [SerializeField] private int _maxLetters = 35;

    private void Start()
    {
        _generateButton.onClick.AddListener(() => InputCheck());
        _shuffleButton.onClick.AddListener(() => Shuffle());
    }

    private void InputCheck()
    {
        ErrorText("");

        if (Int32.TryParse(_letterHeightField.text, out int letterColumnCount) &&
        Int32.TryParse(_letterWidthField.text, out int letterRowCount))
        {
            if (letterColumnCount > _maxLetters || letterRowCount > _maxLetters)
            {
                ErrorText("Слишком большое число");
                return;
            }
            _lettersSystem.SpawnLetters(letterRowCount, letterColumnCount, new Action(() => EnableButtons()));
        }
        else
        {
            ErrorText("Не вернеые данные ввода");
        }
    }


    private void Shuffle()
    {
        if (_lettersSystem.LettersCount <= 0)
        {
            ErrorText("Сначала сгенерируйте таблицу");
            return;
        }
        ErrorText("");
        SwitchButtons(false);
        _lettersSystem.Shuffle(new Action(() => EnableButtons()));
    }

    private void ErrorText(string text)
    {
        _wrongMessage.text = text;
    }

    private void SwitchButtons(bool flag)
    {
        _generateButton.enabled = flag;
        _shuffleButton.enabled = flag;
    }
    private void EnableButtons()
    {
        SwitchButtons(true);
    }
}

