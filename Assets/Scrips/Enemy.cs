using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreValue = 12;

    ScoreBoard scoreBoard;
    [SerializeField] int hits = 3;
    // Start is called before the first frame update
    void Start()
    {
        addNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void addNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <= 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scoreValue);
        hits--;
    }

    private void KillEnemy()
    {
        GameObject Fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        Fx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
