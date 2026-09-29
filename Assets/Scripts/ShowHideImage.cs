using UnityEngine;

public class ShowHideImage : MonoBehaviour
{
    public GameObject player;  // O player
    public GameObject image;   // A imagem que deseja mostrar/ocultar
    public float showDistance = 5.0f;  // Distância para mostrar a imagem

    private void Update()
    {
        // Verifica se o player e a imagem estão atribuídos
        if (player == null || image == null)
        {
            Debug.LogWarning("Player ou Image não atribuídos no Inspector.");
            return;
        }

        // Calcula a distância entre o player e a imagem
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Se a distância for menor ou igual à distância especificada, mostra a imagem
        if (distance <= showDistance)
        {
            image.SetActive(true);
        }
        else  // Caso contrário, oculta a imagem
        {
            image.SetActive(false);
        }
    }
}
