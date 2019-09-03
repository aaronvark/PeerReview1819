using UnityEngine;

public class InputHandeler : MonoBehaviour
{
    private Character character;

    private void Start() {
        character = GetComponent<Character>();
    }

    void Update() {
        character.Walking(Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)){
            character.Jump();
        } else if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.Space)) {
            character.CancelJump();
        }

        if (Input.GetButtonDown("Fire1")){
            character.Shoot();
        }

    }
}
