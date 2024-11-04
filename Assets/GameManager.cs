using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Enemy> Enemies = new List<Enemy>(); // List of enemies
    [SerializeField] Enemy enemyPrefab;   // prefab of the enemy
    public RectTransform enemyUIsParent;    // the UI parent of the enemies list
    public Player player; 

    void Awake()
    {
        // Find all enemies in the level and set it to list
        Enemies = FindObjectsOfType<Enemy>().ToList();  
    }

    // Loop through all the enemies and damage each one a random value
    public void DamageAllEnemiesRandomly()
    {
        for (int i = Enemies.Count - 1; i >= 0 ; i--)
        {
            Enemies[i].Damage(Random.Range(1,30));
        }
    }

    // Spawn an enemy and add it to list
    public void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        Enemies.Add(enemy);
    }

    public void SortEnemiesByLowestHealth()
    {
        // Loop through each index, starting with 1,
        // dont need to compare the first index
        
        for (int i = 1; i < Enemies.Count; i++) 
        {
            int indexToCheck = i; // store the index to check

            Enemy currentEnemy = Enemies[indexToCheck]; // store the current enemy
            Enemy previousEnemy = Enemies[indexToCheck - 1]; // store the previous enemy

            // check if current enemy health is lower than the previous one
            while (currentEnemy.Health < previousEnemy.Health)
            {
                // if it is lower, switch them
                Enemies[indexToCheck] = previousEnemy;
                Enemies[indexToCheck - 1] = currentEnemy;

                // reduce 1 from the index, so we keep checking until the current
                // enemy health is higher than the previous one
                indexToCheck--;
                
                // check if got to the start of the list and exit if we did
                if(indexToCheck <= 0)
                    break;
                
                //set the new previous value, so we can compare it
                previousEnemy = Enemies[indexToCheck - 1];

            }
        }


        // after sorting, rearrange the enemy UIs and reset the target index
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].UI.transform.SetSiblingIndex(i);
        }
        player.targetIndex = 0;
    }
}
