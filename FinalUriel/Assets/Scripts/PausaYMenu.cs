using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(AudioSource))]
public class PausaYMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject panelInicial;
    [SerializeField] private GameObject fondoMenu;
    public AudioSource audioMenu;
    public AudioSource audioGame;


    bool panelInicialDesactivado = false;

    void Start()
    {
        PauseGame();
        pausePanel.SetActive(false);
    }
    

    //Probé poner en Awake y en Start pero siguen desapareciendo los AudioSource, Si se arregla se pueden voler a poner el Stop y el Player de abajo
    void Awake()
    {
        audioMenu = GetComponent<AudioSource>();
        audioGame = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!panelInicialDesactivado)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                fondoMenu.SetActive(false);
                panelInicial.SetActive(false);
                panelInicialDesactivado = true;
                //audioMenu.Stop();
                //audioGame.Play();
                ContinueGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!pausePanel.activeInHierarchy)
                {
                    PauseGame();
                }
                else if (pausePanel.activeInHierarchy)
                {
                    ContinueGame();
                }
            }
        }

        if(transform.position.y < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}
