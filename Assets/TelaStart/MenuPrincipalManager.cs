using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuPrincipalManager : MonoBehaviour
{

    [SerializeField] private string nomeDoLevelDeJogo;

    public void Jogar() {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void SairJogo(){
        Debug.Log("Você saiu do Jogo");
        Application.Quit();

    }


}
