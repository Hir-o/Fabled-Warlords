using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDetails", menuName = "LevelDetails/LevelDetailsData", order = 0)]
public class LevelDetails : ScriptableObject
{
    [Scene] [SerializeField] private int _levelIndex;
    [SerializeField] private Sprite _levelIcon;
    [SerializeField] private string _levelName;

    [TextAreaAttribute(15, 10)] [SerializeField]
    private string _levelDescription;

    public int LevelIndex => _levelIndex;
    public Sprite LevelIcon => _levelIcon;
    public string LevelName => _levelName;
    public string LevelDescription => _levelDescription;
}