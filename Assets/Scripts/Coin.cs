using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto colidiu: " + other.name); 
        if (other.CompareTag("Player"))
        {
            Debug.Log("Moeda coletada!"); 
            Destroy(gameObject); // Destroi a moeda
            CoinManager.Instance.CheckCoins();
        }
    }
}