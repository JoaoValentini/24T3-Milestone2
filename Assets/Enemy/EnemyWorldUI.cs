using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class EnemyWorldUI : MonoBehaviour
{
    Enemy enemy;
    Transform camTransform;
    [SerializeField] TMP_Text healthTxt;
    [SerializeField] Image healthBar;
    [SerializeField] Image healthLostBar;

    void Start()
    {
        camTransform = Camera.main.transform; // get a reference to the camera transform
    }

    void Update()
    {
        // make the UI look at the camera
        transform.LookAt(camTransform, Vector3.right);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x,0,0);
    }

    // initialise the ui
    public void Init(Enemy enemy)
    {
        this.enemy = enemy;     // set the enemy
        UpdateHealthText();     // update the health text
        float percent = enemy.Health / enemy.MaxHealth; // get the health percentage
        healthBar.fillAmount = percent;         // update the health bars
        healthLostBar.fillAmount = percent;
    }
    
    void UpdateHealthText()
    {
        healthTxt.text = $"{enemy.Health}/{enemy.MaxHealth}";
    }

    // called when the enemy takes damage
    public void OnDamageTaken()
    {
        UpdateHealthText();     
        float percent = enemy.Health / enemy.MaxHealth;     //get the health percentage
        healthBar.fillAmount = percent;     //set the green health bar size
        
        healthLostBar.DOKill(); // cancel previous animation
        // set the red health bar as the same size as the green one over time, with a delay
        healthLostBar.DOFillAmount(percent,0.15f).SetDelay(.4f);    
    }

    public void OnDeath() // deactive the ui when this enemy dies
    {
        gameObject.SetActive(false);

    }
}
