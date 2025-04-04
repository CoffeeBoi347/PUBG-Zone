using TMPro;
using UnityEngine;
public class CharacterControllerNew : MonoBehaviour
{
    [Header("Movement Values")]

    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public Rigidbody playerController;
    public float fixSpeed = 1f;

    [Header("Health Manager")]

    public Transform healthImg;
    public float healthPlayer;
    private const float healthDeduction = 0.1f;
    public Sprite[] healthImgArrays;

    [Header("Animator")]

    public Animator playerAnim;

    [Header("Booleans")]

    public bool canRun;
    public bool canWalk;
    public bool canDeductHealth = true;
    public bool insideSafeZone;
    public bool canDamagePlayer;
    public bool canControlPlayer = true;

    [Header("Texts")]

    public TMP_Text healthText;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        currentSpeed = runSpeed;
        insideSafeZone = false;
        canDamagePlayer = false;
    }

    private void Update()
    {
        HealthConditions();
        if (!insideSafeZone)
        {
            canDamagePlayer = true;
        }

        if (canDamagePlayer && !insideSafeZone && canDeductHealth)
        {
            healthPlayer -= healthDeduction;
        }
        InputControls();
    }

    void InputControls()
    {
        if (canControlPlayer)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = transform.forward * vertical * fixSpeed;
            Vector3 right = transform.right * horizontal * fixSpeed;
            playerController.velocity = (forward + right) * currentSpeed;
            if (Input.GetKey(KeyCode.W))
            {
                playerAnim.SetBool("isRunning", true);
                currentSpeed = runSpeed;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerAnim.SetBool("isRunning", true);
                forward = -forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerAnim.SetBool("isRunning", true);
                right = -right;
                right *= 0.5f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerAnim.SetBool("isRunning", true);
                right *= 0.5f;
            }
            else
            {
                playerAnim.SetBool("isRunning", false);
                currentSpeed = runSpeed;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                playerAnim.SetBool("isAttacking", true);
                currentSpeed = 0;
            }
            else
            {
                playerAnim.SetBool("isAttacking", false);
            }

            if (Input.GetKey(KeyCode.P))
            {
                playerAnim.SetBool("isPosing", true);
            }
            else
            {
                playerAnim.SetBool("isPosing", false);

            }

            if (Input.GetKey(KeyCode.T))
            {
                playerAnim.SetBool("isTaunting", true);
            }
            else
            {
                playerAnim.SetBool("isTaunting", false);
            }
        }
    }

    void HealthConditions()
    {
        healthText.text = "Health: " + healthPlayer;

        if (healthPlayer > 80 && healthPlayer <= 100)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[0];
        }

        else if (healthPlayer > 60 && healthPlayer <= 80)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[1];
        }
        else if (healthPlayer > 40 && healthPlayer <= 60)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[2];
        }
        else if (healthPlayer > 20 && healthPlayer <= 40)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[3];
        }
        else if (healthPlayer > 10 && healthPlayer <= 20)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[4];
        }
        else if (healthPlayer > 0 && healthPlayer <= 10)
        {
            healthImg.GetComponent<SpriteRenderer>().sprite = healthImgArrays[5];
        }
        else
        {
            healthPlayer = 0;
            playerAnim.SetTrigger("isDeath");
            canDeductHealth = false;
            canControlPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            insideSafeZone = true;
            Debug.Log("DONT DEDUCT HEALTH!");

            Debug.Log("YAY YOU ARE SAFE..!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            insideSafeZone = false;
        }
    }
}
