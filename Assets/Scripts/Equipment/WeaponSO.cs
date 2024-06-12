using System;
using UnityEngine;

namespace Trump
{
    [CreateAssetMenu(fileName = "weaponSO", menuName = "Weapon/WeaponConfig")]
    public class WeaponSO : ScriptableObject
    {
        [field: SerializeField,Range(0, 512)] private UInt32 _damage;
        [field: SerializeField] private string _name;
        [field: SerializeField] private GameObject _gameObject;
        [field: SerializeField] private BoxCollider _collider;

        public float Damage => _damage;

        public string Name => _name;

        public GameObject GameObject => _gameObject;

        public BoxCollider BoxCollider => _collider;

        public string Path => $"Weapon/{_name}";

        private void OnValidate()
        {
            if (_gameObject == null)
                throw new ArgumentException(nameof(_gameObject));
            if (_collider == null)
                throw new ArgumentException(nameof(_collider));
        }
    }
}