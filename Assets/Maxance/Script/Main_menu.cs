using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject Menuprincipale;

    public float waitTime;
    public int SceneNumber = 1;
    public Animator musicAnim;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Menuprincipale.SetActive(true);
    }
    public void PlayGame()
    {
        SceneNumber = 1;
        StartCoroutine(ChangeScene());
    }
    public void MainMenu(){
        SceneNumber = 0;
        StartCoroutine(ChangeScene());
    }
    //private void Update(){ if (Input.GetKeyDown(KeyCode.Space)){StartCoroutine(ChangeScene());}}



    IEnumerator ChangeScene()
    {
        audioManager.PlaySFX(audioManager.Playbutton);
        //musicAnim.SetTrigger("Fade Out");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(SceneNumber);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
