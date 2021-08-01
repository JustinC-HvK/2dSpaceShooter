using UnityEngine;
using UnityEngine.UI;


/// Manages the state of the game such as scoring, dying, and game over.
public class GameManager : MonoBehaviour
{

    public Player player;
    public ParticleSystem explosionEffect;
    public int score = 0;
    public int lives = 3;
    public float respawnDelay = 3.0f;
    public float respawnInvulTime = 3.0f;


    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), this.respawnInvulTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();

        // Score is increased based on the size of the asteroid
        if (asteroid.size < 0.7f) {
            score += 100; // small asteroid
        } else if (asteroid.size < 1.4f) {
            score += 50; // medium asteroid
        } else {
            score += 25; // large asteroid
        }
    }

    public void PlayerDeath(Player player)
    {
        this.explosionEffect.transform.position = player.transform.position;
        this.explosionEffect.Play();

        this.lives--;

        if (this.lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }

    public void GameOver()
    {
        this.lives = 3;
        this.score = 0;
    }

}
