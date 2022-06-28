using UnityEngine;
using UnityEngine.UI;


public class LetterFactory : MonoBehaviour
{
    [SerializeField] private Transform _panel;
    [SerializeField] private int _minFontSize;
    [SerializeField] private int _maxFontSize;
    [SerializeField] private Font _font;
    public LetterUnit SpawnLetter(Transform parent)
    {


        GameObject letterObject = new GameObject("Letter",typeof(Text));
        var letterUnit = letterObject.AddComponent<LetterUnit>();
        var letterText = letterObject.GetComponent<Text>();
        letterText.resizeTextMaxSize = _maxFontSize;
        letterText.resizeTextMinSize = _minFontSize;
        letterText.resizeTextForBestFit = true;
        letterText.font = _font;
        letterText.color = Color.black;
        letterText.alignment = TextAnchor.MiddleCenter;
        

        return letterUnit;

    }
}

