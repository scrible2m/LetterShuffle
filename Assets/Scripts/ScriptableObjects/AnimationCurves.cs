using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnimationCurves", order = 1)]
public class AnimationCurves : ScriptableObject
{
    public List<AnimationCurve> Curves;

}
