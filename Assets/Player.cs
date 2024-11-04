using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    GameManager manager; 
    public float speed = 2f;
    float attackDistance = 0.5f;    // distance that the player can attack an enemy from
    float attackDamage = 15f;       // damage to deal to enemies
    public int targetIndex = 0;     // the index of the current target ( Enemies list in manager )
    public RectTransform targetUI;  // the target RectTransform

    void Start()
    {
        manager = FindObjectOfType<GameManager>();  // get a reference to the manager
    }

    public void ChaseNextTarget()   // Chase the enemy 
    {
        if(manager.Enemies.Count == 0) // Check if there is no enemy left
            return;
        
        if(targetIndex >= manager.Enemies.Count) // if the current target index is greater than enemies count, set to 0
            targetIndex = 0;

        UpdateTargetUI();   // update target ui
        StopAllCoroutines(); // stop all routines
        StartCoroutine(MoveToEnemyRoutine(manager.Enemies[targetIndex])); // start the routine to move the player to target
    }

    public void UpdateTargetUI()
    {
        // Enables the target UI and set it to the position as the current target
        targetUI.gameObject.SetActive(true);
        targetUI.position = manager.Enemies[targetIndex].UI.transform.position;
    }

    // Routine to move the player to an enemy, when the player reaches the enemy
    // they will attack the enemy
    IEnumerator MoveToEnemyRoutine(Enemy enemy)
    {
        // check if player is within attack distance from the enemy
        while (Vector3.Distance(transform.position, enemy.transform.position) > attackDistance)
        {
            Vector3 enemyPos = enemy.transform.position;
            Vector3 moveDir = (enemyPos - transform.position).normalized; // get the movement direction
            transform.position += moveDir * speed * Time.deltaTime; // move towards enemy
            yield return null;  // Wait for next frame
        }

        //Reached the enemy, attack
        Attack(enemy);
        targetIndex++; // increase the index to target next enemy
        if (targetIndex >= manager.Enemies.Count) // if index is greater than enemies, set it to 0
            targetIndex = 0;

        targetUI.gameObject.SetActive(false); // disable the target UI
    }

    void Attack(Enemy enemy)
    {
        enemy.Damage(attackDamage); // do damage to enemy
    }
}
