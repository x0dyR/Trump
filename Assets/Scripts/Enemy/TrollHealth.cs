using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trump
{

    public class TrollHealth : MonoBehaviour
    {
        private int HP = 100;
        public Animator animator;
        public Slider HealthBar;

        void Update(){
            HealthBar.value = HP;
        }
        public void TakeDamage(int damageAmount){
            HP -= damageAmount;
            if(HP <= 0){
                animator.SetTrigger("Dead");
                GetComponent<Collider>().enabled = false;
                HealthBar.gameObject.SetActive(false);

            }
        }   
    }
}