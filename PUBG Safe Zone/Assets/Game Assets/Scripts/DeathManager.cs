using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [Header("UI Objects")]

    public GameObject button;
    public TextMeshProUGUI respawnText;
    public GameObject youDiedText;

    [Header("Animation Objects")]

    private Animation animationObj;
    public AnimationClip youDiedClip;

    [Header("Character Controller Reference")]

    public CharacterControllerNew characterController;
    public float timer;

    private void Start()
    {
        characterController = GetComponent<CharacterControllerNew>();
        button.SetActive(false);
        youDiedText.SetActive(false);
    }

    private void Update()
    {
        if(characterController != null)
        {
            if (characterController.healthPlayer <= 0f)
            {
                HandleDeathSettings();
            }
        }
    }

    void HandleDeathSettings()
    {
        youDiedText.SetActive(true);
        button.SetActive(true);
        animationObj.Play(youDiedClip.name);
    }

    public void RespawnButton()
    {
        StartCoroutine(ReloadScene(5f));
    }

    private IEnumerator ReloadScene(float time)
    {
        float timeToRespawn = 0f;
        float timeLeftToRespawn = timer;
        while (timer > timeToRespawn)
        {
            timeLeftToRespawn -= Time.deltaTime;
            timeToRespawn += Time.deltaTime;
            respawnText.text = $"RESPAWNING IN {timeLeftToRespawn}";

            if (timeToRespawn >= timer)
            {
                timeToRespawn = timer;
                SceneManager.LoadScene(0);
                yield return null;
            }

        }
    }
}