using UnityEngine;


/// Handles the physics/movement of a bullet projectile.
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    /// How fast the bullet travels.
    public float speed = 500.0f;

    /// The maximum amount of time the bullet will stay alive after being
    /// projected.
    public float maxLifetime = 10.0f;


    /// The rigidbody component attached to the bullet.
    public new Rigidbody2D rigidbody { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        // The bullet only needs a force to be added once since they have no
        // drag to make them stop moving
        this.rigidbody.AddForce(direction * this.speed);

        // Destroy the bullet after it reaches it max lifetime
        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the bullet as soon as it collides with anything
        Destroy(this.gameObject);
    }

}
