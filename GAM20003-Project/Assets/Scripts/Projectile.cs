using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float velocity;
    private float lifeSpan = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeSpan);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
    }

    void DestroyProjectile() {
        Destroy(gameObject);
    }
}
