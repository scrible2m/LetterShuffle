
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LetterUnit : MonoBehaviour
{
    private Text _letter;
    private RectTransform _rectTransform;
    private Vector2 _targetPosition;
    private Vector2 _oldPosition;
    private AnimationCurve _animationCurve;
    private float _shuffleOffset;
    private float _letterAnimationTime;

    public void Init(string letter, Vector2 position, int size, RectTransform panel)
    {
        StopAllCoroutines();
        _letter = GetComponent<Text>();
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.SetParent(panel);
        _rectTransform.anchoredPosition = position;
        _rectTransform.localScale = Vector2.zero;
        _rectTransform.sizeDelta = new Vector2(size, size);
        _letter.text = letter;
        _shuffleOffset = _rectTransform.sizeDelta.x / 5;
        StartCoroutine(ScaleTo(Vector2.zero, Vector2.one));

    }

    public void Reset(Transform transform)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(_rectTransform.localScale, Vector2.zero));
        _rectTransform.SetParent(transform);

    }

    private IEnumerator ScaleTo(Vector2 fromScale, Vector2 targetScale)
    {

        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        float elapsedTime = 0f;
        float totalTime = .5f;
        while (elapsedTime < totalTime)
        {
            _rectTransform.localScale = Vector3.Lerp(fromScale, targetScale, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    public void Shuffle(float time)
    {
        Vector2 tempPosition = _rectTransform.anchoredPosition;
        _rectTransform.anchoredPosition = Vector2.Lerp(_oldPosition, _targetPosition, time/_letterAnimationTime) + Vector2.one * _animationCurve.Evaluate(time) * _shuffleOffset;

    }

    public void NewPosition(Vector2 position, AnimationCurve curve, float letterTime)
    {
        _targetPosition = position;
        _animationCurve = curve;
        _oldPosition = _rectTransform.anchoredPosition;
        _letterAnimationTime = letterTime;
    }
}
