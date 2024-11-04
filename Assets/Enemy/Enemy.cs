using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    float health; // make health private so only this script can change it
    public float Health => health; // public getter for the health
    [SerializeField] float maxHealth = 100;
    public float MaxHealth => maxHealth;
    [SerializeField] float speed = 5f; // movement speed
    EnemyWorldUI worldUI; // health bar on top of model
    [SerializeField] EnemyUI enemyUIPrefab; // ui prefab
    public EnemyUI UI; // ui on the overlay canvas
    public Color enemyColor; // color for the model
    MeshRenderer meshRenderer; // model mesh renderer


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();  // get the game manager
        health = maxHealth;         // set the health to the mex health
        worldUI = GetComponentInChildren<EnemyWorldUI>();  // get the model ui on the child of this object
        worldUI.Init(this);     // initialise the world ui

        StartCoroutine("MoveToRandomPositionRoutine");  // start the move routine
        AssignRandomColor();  // assign a random color to this enemy 

        InstantiateUI();    // instantiate the enemy ui
    }

    void InstantiateUI()
    {
        UI = Instantiate(enemyUIPrefab,gameManager.enemyUIsParent); // instantiate the enemy ui
        UI.transform.SetAsFirstSibling();   // set it on top of list
        UI.Init(this);                      // initialise UI
    }

    void AssignRandomColor()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();  // get the mesh renderer
        enemyColor = Random.ColorHSV(0,1,.2f,1,.25f,1);         // get a random color
        meshRenderer.material.color = enemyColor;               // assign the color to the mesh renderer
    }
    
    // Do damage to the enemy
    public void Damage(float damage)
    {
        if(health <= 0)  // check if health is already 0, if it is return
            return;

        // subtract the damage from the health and clamp it from 0 to max health
        health = Mathf.Clamp(health - damage, 0, maxHealth); 


        // update both UIs
        worldUI.OnDamageTaken();
        UI.OnDamageTaken();
        
        StartCoroutine(TakeDamageAnimationRoutine()); // start damage animation
        
        if (health <= 0) // kill the enemy if health is 0
            Kill();
    }

    void Kill()
    {
        gameManager.Enemies.Remove(this); // remove the enemy from the enemies list
        
        // update the UIs
        worldUI.OnDeath();
        UI.OnDeath();
  
        // rotate de enemy 90 degrees, so it is laying down on the ground 
        transform.Rotate(transform.forward, 90);         
        StopCoroutine("MoveToRandomPositionRoutine"); // stop the move routine       

        // check if there is no more enemies left and disable the target UI
        if(gameManager.Enemies.Count == 0)
            gameManager.player.targetUI.gameObject.SetActive(false);       
    }

    Vector3 GetRandomPosition() // Generates a random position within the bounds
    {
        float boundsExtent = 10;
        return new Vector3(Random.Range(-boundsExtent,boundsExtent),0, Random.Range(-boundsExtent,boundsExtent));
    }

    // Routine to move the enemy around
    IEnumerator MoveToRandomPositionRoutine()
    {
        Vector3 pos = GetRandomPosition();  // get random position
        float distanceThreshold = 0.05f;    // set a threshold for the position

        // while enemy is not within the position threshold, move towards the position
        while(Vector3.Distance(pos,transform.position) > distanceThreshold)
        {
            Vector3 moveDir = (pos - transform.position).normalized; // get the movement direction
            transform.position += moveDir * speed * Time.deltaTime; // move over time
            yield return null; // wait for next frame
        }

        float waitTime = Random.Range(.5f,2f); // wait for a random amount of time
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("MoveToRandomPositionRoutine"); // start moving again
    }

    IEnumerator TakeDamageAnimationRoutine()
    {
        meshRenderer.sharedMaterial.color = Color.red; // set the enemy color to red
        yield return new WaitForSeconds(0.15f); // wait a little
        
        // if the player died, set the color to grey, if not set back to the original color
        if(health <= 0)
            meshRenderer.sharedMaterial.color = Color.grey;
        else
            meshRenderer.sharedMaterial.color = enemyColor;
        
    }

}
