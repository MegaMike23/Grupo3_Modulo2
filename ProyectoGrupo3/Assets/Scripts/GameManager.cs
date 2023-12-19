using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private int pots = 0;

    private int lives;

    public EventHandler OnChangeAddPot;

    public EventHandler<int> OnChangeLives;

    public EventHandler<bool> OnPausePressed;



    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        lives = LoadLives();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (AudioManager.Instance == null)
        {
            Debug.Log("No hay INSTANCE de audio manager previamente creado!");
        }
        else
        {
            AudioManager.Instance.PlayMusic("Background");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Pausa o Continuar
        if (Input.GetKeyDown((KeyCode)'p'))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;//Pausar el tiempo
                OnPausePressed?.Invoke(this, true);
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;//Continuar el tiempo
                OnPausePressed?.Invoke(this, false);
            }
            
        }
    }

    public void AddPot()
    {
        pots++;
        OnChangeAddPot?.Invoke(this, EventArgs.Empty); //Lanza llamada de Evento para quen quiera recibirla que se sume un pot
    }

    public int GetPot()
    {
        return pots;
    }

    public void ChangeLives(int numberLessLives)
    {
       
        lives -= numberLessLives;
        if (lives < 0)
        {
            lives = 0;
            SaveLives(3);
        }
        else
        {
            SaveLives(lives);
        }

        OnChangeLives?.Invoke(this, lives);
    }

    private void SaveLives(int _lives)
    {
        PlayerPrefs.SetInt("Lives", _lives);
    }

    private int LoadLives()
    {
        return PlayerPrefs.GetInt("Lives", 3);
    }

    public int GetLives()
    {
        return lives;
    }
}
