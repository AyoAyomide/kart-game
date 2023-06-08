using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CarController : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public Transform carModel;
    public float groundCheck;
    public TextMeshProUGUI scoreText;

    Vector3 modelOffset;
     float lastGroundCheck;
     float yCurrentRot;

    // the varable that hold input value
     bool accelerateInput;
     float turnInput;

    public TrackZone curTrackZone;
    public int zonePassed;
    public int racePosition;
    public int curLap;
    public bool canControl;

    public Rigidbody carRig;

    void Start()
    {
        modelOffset = carModel.transform.localPosition;
        GameManager.instance.cars.Add(this);
        transform.position = GameManager.instance.spawnPoint[GameManager.instance.cars.Count - 1].position;
    }
    void Update()
    {
        if (!canControl) return;

        float turnRate = Vector3.Dot(carRig.velocity.normalized, carModel.forward);
        turnRate = Mathf.Abs(turnRate);

        yCurrentRot += turnInput * turnRate * turnSpeed * Time.deltaTime;

        carModel.position = transform.position + modelOffset;
        checkGround();
        scoreText.text = zonePassed.ToString();
    }

    private void FixedUpdate()
    {
        if (!canControl) return;
        if (accelerateInput)
        {
            carRig.AddForce(carModel.forward * moveSpeed, ForceMode.Acceleration);
        }
    }

    public void accelerateStart(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            accelerateInput = true;
        else
            accelerateInput = false;
    }

    public void turnStart(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    void checkGround()
    {
        Ray ray = new Ray(transform.position + new Vector3(0,-1f,0), Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1.0f))
        {
            carModel.up = hit.normal;
        }
        else
        {
            carModel.up = Vector3.up;
        }

        carModel.Rotate(new Vector3(0, yCurrentRot, 0), Space.Self);
    }
}
