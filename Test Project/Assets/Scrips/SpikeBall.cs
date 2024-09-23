using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpikeBall : MonoBehaviour
{
    public float moveSpeed;
    public Transform enemy;
    public GameObject player;
    public AudioSource loseSFX;
    // Patrol variables
    public Transform patrolPtA;
    public Transform patrolPtB;
    public Transform patrolPtC;
    public Transform patrolPtD;
    public Transform patrolTarget;
    public TextMeshProUGUI loseLabel;
    public PlayerMovement playerScript;
    
    public float minimumDistance;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerMovement>();
        
    }

    
    void Update()
    {
        // Move towards this position
        Vector3 targetPosition = patrolTarget.position;

        // To find the diurection from A to B = B - A
        Vector3 delta = targetPosition - enemy.position;
        Vector3 moveDirection = delta.normalized;

        enemy.position += moveDirection * moveSpeed * Time.deltaTime;

        // If we're REALLY close, change our target
        if (delta.magnitude < minimumDistance)
        {
            if (patrolTarget == patrolPtA)
            {
                patrolTarget = patrolPtB;
            }
            else if (patrolTarget == patrolPtB)
            {
                patrolTarget = patrolPtC;
            }
            else if (patrolTarget == patrolPtC)
            {
                patrolTarget = patrolPtD;
            }
            else if (patrolTarget == patrolPtD)
            {
                patrolTarget = patrolPtA;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player)
        {
            player = col.gameObject;
            loseSFX.Play();
            Debug.Log("Play Sound");
            playerScript.loseGame = true;
            Invoke(nameof(PlayerDeath), 0.1f);
            
        }
    }
    public void PlayerDeath()
    {
        
        player.gameObject.SetActive(false);
    }
}
