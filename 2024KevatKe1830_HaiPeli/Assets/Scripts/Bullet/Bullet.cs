using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float currentSpeed = 25f;
    private float lifespan = 3f;
    private float lifeTimer;

    private void OnEnable(){
        lifeTimer = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * -1 * currentSpeed * Time.deltaTime);
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0){
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            return;
        }
        IDamageable damageable = other.GetComponent<IDamageable>();
        if(damageable != null){
            damageable.TakeDamage(1);
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }
}
