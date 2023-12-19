using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private TextMeshProUGUI textNumberPots;
    [SerializeField] private Image[] heartSprites;
    [SerializeField] private GameObject cuadroPausa;//Menu de pausa en el canvas

    private void Start()
    {
        if (GameManager.Instance != null) { 
            GameManager.Instance.OnChangeAddPot += GameManager_OnChangeAddPot; //Se le asigna la funcion asociada a la llamada
            GameManager.Instance.OnChangeLives += GameManager_OnChangeLives;
            GameManager.Instance.OnPausePressed += GameManager_OnPausePressed;
        }

        int corazonesactivos = GameManager.Instance.LoadLive();
        Debug.Log(corazonesactivos);
        HeartUIUpdate(corazonesactivos);
    }

    private void GameManager_OnPausePressed(object sender, bool e)
    {
        TogglePauseUI(e);
    }

    private void HeartUIUpdate(int numberHearts)
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            heartSprites[i].enabled = false;

        }

        for (int i = 0; i < numberHearts; i++)
        {
            heartSprites[i].enabled = true;
        }
    }

    private void GameManager_OnChangeLives(object sender, int e)
    {
        HeartUIUpdate(e);
    }

    private void GameManager_OnChangeAddPot(object sender, EventArgs e) //Funcion asociada a la llamada del evento
    {
        textNumberPots.text = GameManager.Instance.GetPot().ToString();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        AudioManager.Instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }

    //Activar/Desactivar el menú de pausa
    private void TogglePauseUI(bool isPaused)
    {
        cuadroPausa.SetActive(isPaused);
    }

}
