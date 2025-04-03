using UnityEngine;
public class CharacterControllerNew : MonoBehaviour
{
    [Header("Movement Values")]
    public float healthPlayer;
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public CharacterController playerController;
    public float fixSpeed = 1f;

    [Header("Animator")]
    public Animator playerAnim;

    [Header("Booleans")]
    public bool canRun;
    public bool canWalk;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        currentSpeed = runSpeed;
    }

    private void Update()
    {
        InputControls();
    }

    void InputControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward * vertical * fixSpeed;
        Vector3 right = transform.right * horizontal * fixSpeed;
        playerController.Move((forward + right) * currentSpeed * Time.deltaTime);

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
            playerAnim.SetBool("isPosing",true);
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
