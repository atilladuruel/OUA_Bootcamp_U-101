using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    //Click sesi ayarları-----
    public AudioClip clickSound;
    private AudioSource audioSource;

    //Buton ayarları-------
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    public GameObject backButton;
    public GameObject menuButton;

    //Panel ayarları---------
    public GameObject confirmationPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        menuPanel.SetActive(SceneManager.GetActiveScene().name == "GameScene" ? false : true);
        settingsPanel.SetActive(false);
        confirmationPanel.SetActive(false);
        creditsPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        //Eğer bir audiosource bileşeni yoksa, oluşturulup clicksoundu atayım
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clickSound;
        }

    }

    //Sahne yüklenmeden play  butonunun click sesinin duyulması için------
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        //Sesin uzunluğu kadar bekleme süresi alıyorum.
        yield return new WaitForSeconds(delay);
        //Ses çalıyor sonra da sahne yükleniyor
        SceneManager.LoadScene("GameScene");
    }

    //Butonlar--------
    public void PlayButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        StartCoroutine(LoadSceneAfterDelay(audioSource.clip.length));


    }
    public void SettingsButtonClicked()
    {

        settingsPanel.SetActive(true);
        backButton.SetActive(true);
        menuPanel.SetActive(false);
        audioSource.PlayOneShot(clickSound);
    }

    public void MenuButtonClicked()
    {

        menuPanel.SetActive(true);
        audioSource.PlayOneShot(clickSound);
    }

    public void BackButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
        backButton.SetActive(false);


    }
    public void QuitButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        confirmationPanel.SetActive(true);
        menuPanel.SetActive(false);

    }
    public void YesButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        Application.Quit();
        Debug.Log("Quit");
    }
    public void NoButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        confirmationPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void CreditsButtonClicked()
    {
        audioSource.PlayOneShot(clickSound);
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
        backButton.SetActive(true);
    }

}
