using System.Collections.Generic;
using UnityEngine;
using System;

public class LettersPool : MonoBehaviour
{
    [SerializeField] private List<LetterUnit> _lettersPool = new List<LetterUnit>();
    [SerializeField] private int _maxLetterCount;
    [SerializeField] private LetterFactory _factory;

    private void Start()
    {
        for (int i = 0; i < _maxLetterCount; i++)
        {
            _lettersPool.Add(_factory.SpawnLetter(transform));
        }
    }
    public LetterUnit BecomeLetter(int letter, Vector2 position, int size, RectTransform panel)
    {
        LetterUnit unit = null;
        if (_lettersPool.Count > 0)
        {
            unit = _lettersPool[0];
            _lettersPool.RemoveAt(0);
        }
        else
        {
            unit = _factory.SpawnLetter(transform);
        }
        var tempLetter = Convert.ToChar(letter).ToString();
        unit.Init(tempLetter, position, size, panel); ;
        return unit;
    }
    public void Reset(LetterUnit unit)
    {
        _lettersPool.Add(unit);
        unit.Reset(this.transform);
    }
}
