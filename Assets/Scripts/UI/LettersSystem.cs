using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class LettersSystem : MonoBehaviour
{
    [SerializeField] private List<LetterUnit> _units = new List<LetterUnit>();
    [SerializeField] private List<Vector2> _unitsPosition = new List<Vector2>();
    public int LettersCount => _units.Count;
    [SerializeField] private RectTransform _letterPanel;
    [SerializeField] private LettersPool _lettersPool;
    [SerializeField] private float _shuffleTimer = 2f;

    [SerializeField] private AnimationCurves _curves;

    private Vector2 _asciiLetters = new Vector2(65, 90);

    public void SpawnLetters(uint letterRowCount, uint letterColumnCount, Action callback)
    {
        int letterSize;
        if (_units.Count > 0)
        {
           ResetLetterList();
        }
        if ((float)letterRowCount / (float)letterColumnCount > _letterPanel.rect.height / _letterPanel.rect.width)
        {
            letterSize = Convert.ToInt32(Math.Floor(_letterPanel.rect.height / letterRowCount));

        }
        else
        {
            letterSize = Convert.ToInt32(Math.Floor(_letterPanel.rect.width / letterColumnCount));
        }
        Vector2 offset = new Vector2(letterColumnCount - 1, letterRowCount - 1) * letterSize / 2;
        for (int i = 0; i < letterRowCount; i++)
        {
            for (int k = 0; k < letterColumnCount; k++)
            {
                var unitPosition = new Vector2(letterSize * k - offset.x, offset.y - letterSize * i);
                _units.Add(_lettersPool.BecomeLetter((int)Random.Range(_asciiLetters.x, _asciiLetters.y), unitPosition, letterSize,_letterPanel));
                _unitsPosition.Add(unitPosition);
            }
        }
        callback();
    }

    private void ResetLetterList()
    {
      
            foreach (LetterUnit unit in _units)
            {
                _lettersPool.Reset(unit) ;
              
            }
     
        _units.Clear();
        _unitsPosition.Clear();
        
    }

    public void Shuffle(Action callback)
    {
        List<Vector2> tempPositions = new List<Vector2>(_unitsPosition);
        
        foreach (LetterUnit unit in _units)
        {
            int i = Random.Range(0,tempPositions.Count);
            unit.NewPosition(tempPositions[i],_curves.Curves[Random.Range(0,_curves.Curves.Count)], _shuffleTimer);
            tempPositions.RemoveAt(i);
        }
        StartCoroutine(ShuffleUnits(callback));
       
    }

    private IEnumerator ShuffleUnits(Action callback)
    {
        float elapsedTime = 0f;
        float totalTime = _shuffleTimer;
        
        while (elapsedTime < totalTime)
        {
            
            foreach (LetterUnit unit in _units)
            {
                unit.Shuffle(elapsedTime);
               
            }
           
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        callback();
    }
}
