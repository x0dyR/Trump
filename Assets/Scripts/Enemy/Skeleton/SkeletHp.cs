using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace collegeGame
{
    public class SkeletHp : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        [SerializeField] private SkeletConfig _skeletConfig;

        public void UpdateHealthBar(float currentHealth)
        {
            _healthbarSprite.fillAmount = currentHealth / _skeletConfig.Health;
            transform.LookAt(Camera.main.transform);
        }
    }
}