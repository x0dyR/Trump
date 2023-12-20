using UnityEngine;

namespace collegeGame
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
    public class SO : ScriptableObject
    {
        [field: SerializeField] public GroundedData GroundedData { get; private set; }
    }
}
