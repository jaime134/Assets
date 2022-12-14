using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public Canvas doorCanvas;
    public Canvas pursueCanvas;
    public KeyManager KeyManager;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    void Start()
    {
        doorCanvas.enabled = false;
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player && KeyManager.hasKey)
        {
            m_IsPlayerAtExit = true;
            KeyManager.keyCanvas.enabled = false;
        }

        else if (other.gameObject == player && !KeyManager.hasKey) doorCanvas.enabled = true;
    }

    void OnTriggerExit (Collider other)
    {
        doorCanvas.enabled = false;
        pursueCanvas.enabled = false;
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

        if (Ghost0Movement.pursue) pursueCanvas.enabled = true;
        else pursueCanvas.enabled = false;
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}
