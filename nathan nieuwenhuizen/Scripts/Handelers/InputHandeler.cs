using UnityEngine;

/// <summary>
/// Handles all the input the player makes (exception to button presses)
/// </summary>
public class InputHandeler : MonoBehaviour
{
    private Character character;

    private readonly string fireKey = "Fire1";
    private readonly string walkKey = "Horizontal";
    private readonly string jumpKey = "Jump";

    private void Start()
    {
        character = GetComponent<Character>();
    }

    void FixedUpdate()
    {
        character.Walking(Input.GetAxis(walkKey));

        if (Input.GetButtonDown(jumpKey) || Input.GetKeyDown(KeyCode.Space)){
            character.Jump();
        } else if (Input.GetButtonUp(jumpKey) || Input.GetKeyUp(KeyCode.Space)) {
            character.CancelJump();
        }

        if (Input.GetButtonDown(fireKey))
        {
            character.Trigger();
        }
        if (Input.GetButton(fireKey))
        {
            character.TriggerHold();
        }
        if (Input.GetButtonUp(fireKey))
        {
            character.Untrigger();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.Pause(Time.timeScale == 1);
        }
    }
}
