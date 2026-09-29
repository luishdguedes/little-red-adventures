using UnityEngine;

public class Door : MonoBehaviour
{
    // Método chamado para destruir a porta
    public void DestroyDoor()
    {
        // Destroi a porta
        Destroy(gameObject);
    }
}