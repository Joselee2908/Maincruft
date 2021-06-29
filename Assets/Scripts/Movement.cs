using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;
    private float directionY;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public int coins = 0;
    public Text score;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        //JUMP
        if (Input.GetKeyDown(KeyCode.Space)) {
            directionY = jumpSpeed;
        }

        //SPRINT
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            speed += 6f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            speed -= 6f;
        }

        //SHIFT
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            speed -= 3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed += 3f;
        }

        directionY -= gravity * Time.deltaTime;
        direction.y = (new Vector3(0f, directionY, 0f).normalized).y;

        controller.Move(direction.normalized * speed * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit col) {
        //Debug.Log("F");
        if (col.gameObject.tag == "Coin") {
            Debug.Log("okay");
            Destroy(col.gameObject);
            coins += 1;
            score.text = "Score: " + coins.ToString();
        }
    }
}
