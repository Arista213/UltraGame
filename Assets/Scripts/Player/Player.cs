using System;
using Assets.Scripts;
using General;
using Turrets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class Player : Damageable
    {
        public override void TakeDamage(float damage)
        {
            Health -= damage;
            healthBar.fillAmount = Health / MaxHealth;
            if (Health <= 0)
            {
                Destroy(DamageableGameobject);
                SceneManager.LoadScene("Menu");
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Mouse1))
                BuildManager.ClearTurret();
        }
    }
}