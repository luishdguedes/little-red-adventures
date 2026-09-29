using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int totalCoins = 3;
    private int collectedCoins = 0;

    public GameObject door;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método chamado quando uma moeda é coletada
    public void CheckCoins()
    {
        // Incrementa o contador de moedas coletadas
        collectedCoins++;

        // Verifica se todas as moedas foram coletadas
        if (collectedCoins >= totalCoins)
        {
            // Se todas as moedas foram coletadas, destrói a porta
            if (door != null)
            {
                Door doorScript = door.GetComponent<Door>();
                if (doorScript != null)
                {
                    doorScript.DestroyDoor();
                }
            }
        }
    }
}