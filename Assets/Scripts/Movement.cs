using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    // [SerializeField] private float moveSpeed = 10f;
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] float rocketShipAngle = 5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrusterLeft;
    [SerializeField] ParticleSystem thrusterRight;
    [SerializeField] ParticleSystem rocketJet;

    Rigidbody rb;
    AudioSource audioSource;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!rocketJet.isPlaying)
        {
            rocketJet.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        rocketJet.Stop();
    }


    private void RotateLeft()
    {
        if (!thrusterLeft.isPlaying)
        {
            thrusterLeft.Play();
        }
        ApplyRotation(-rocketShipAngle);
    }

    private void RotateRight()
    {
        if (!thrusterRight.isPlaying)
        {
            thrusterRight.Play();
        }
        ApplyRotation(rocketShipAngle);
    }


    private void StopRotating()
    {
        thrusterLeft.Stop();
        thrusterRight.Stop();
    }


    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation so the system can take over.
    }
}