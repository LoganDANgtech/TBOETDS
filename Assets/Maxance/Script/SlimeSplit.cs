using UnityEngine;

public class SlimeSplit : MonoBehaviour
{
    public int health;
    public bool canSplit = true;

    public int maxHealth = 10;
    public PlayerDamage playerDamage;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    void Split()
    {
        if (canSplit)
        {
            health = 5;
            // Instantiate new slime GameObjects
            GameObject newSlime1 = Instantiate(gameObject, transform.position + Vector3.right, Quaternion.identity);
            GameObject newSlime2 = Instantiate(gameObject, transform.position + Vector3.left, Quaternion.identity);

            // Set canSplit to false to prevent further splitting
            canSplit = false;

            // Set canSplit to false for the newly instantiated slimes
            newSlime1.GetComponent<SlimeSplit>().canSplit = false;
            newSlime2.GetComponent<SlimeSplit>().canSplit = false;
            newSlime1.GetComponent<SlimeSplit>().health = 5;
            newSlime2.GetComponent<SlimeSplit>().health = 5;

            // Adjust the scale of the new slimes to make them smaller
            float scaleFactor = 0.6f; // Adjust the scale factor as needed
            newSlime1.transform.localScale *= scaleFactor;
            newSlime2.transform.localScale *= scaleFactor;

            // Destroy the current slime GameObject
            Destroy(gameObject);
        }
        if (canSplit == false)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "weapon")
        {
            TakeDamage(playerDamage.damage);
        }
    }



    public void TakeDamage(int damage)
    {
        health -= damage;

        // Vérifie si la santé est inférieure ou égale à zéro et déclenche la division
        if (health <= 0)
        {
            Split();
        }
    }


    // Implement collision or damage handling methods as needed
}
