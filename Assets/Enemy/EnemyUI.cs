using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUI : MonoBehaviour
{
    Enemy enemy; // reference to the its enemy
    [SerializeField] TextMeshProUGUI healthTxt; // health text
    [SerializeField] Image enemyImage; // the image of the enemy
    [SerializeField] Image healthBar; // the green health bar
    [SerializeField] Image lostHealthBar; // the red health bar, the healt lost

    public void Init(Enemy enemy) // initialise
    {
        this.enemy = enemy;         // set the enemy
        UpdateHealthText();         // update the health text
        float percent = enemy.Health / enemy.MaxHealth;     // get the current health percentage
        healthBar.fillAmount = percent;             //set both health bars fill amount
        lostHealthBar.fillAmount = percent;
        enemyImage.color = enemy.enemyColor;        // set the enemy image color as the enemy color
    }

    void UpdateHealthText()
    {
        healthTxt.text = $"{enemy.Health}/{enemy.MaxHealth}";   //set the health text
    }

    public void OnDamageTaken()
    {
        UpdateHealthText();             // update health text
        float percent = enemy.Health / enemy.MaxHealth;     // get the health percentage
        healthBar.fillAmount = percent;     // set the bar fill amount base on the percentage
        
        lostHealthBar.DOKill();
        lostHealthBar.DOFillAmount(percent,0.15f).SetDelay(.4f);    // animate the red health bar using DOTween asset
    }

    // Called when the enemy dies
    public void OnDeath()
    {
        healthTxt.text = ""; // set the text as empty
        enemyImage.color = Color.grey;  // change the enemy image color to grey
        transform.SetAsLastSibling();   // set the object as the last sibling so in the UI its last on the list
    }
}
