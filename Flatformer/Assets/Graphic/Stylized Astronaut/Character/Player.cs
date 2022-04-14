using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI UIShardsValue;
    public CharacterController Controller;
    public Transform Step;
    public Animator anim;
    public float Speed = 5f;
    public float JumpHeight = 2f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f;
    public float DashDistance = 5f;
    public int JumpCounter;
    public int RequiredShards;
    public LayerMask Ground;
    public Vector3 Drag;
    public AudioSource ShardCollectSource;
    public GameObject World;
    
    private int _spaceShards;
    private int _jumpsRemaining;
    private Vector3 _velocity;
    private bool _isGrounded = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        ManageUI();
    }

    private void ManageUI()
    {
        UIShardsValue.text = $"Shards: {_spaceShards}/{RequiredShards}";
    }
    
    private void CalculateMovement()
    {
        _isGrounded = Physics.CheckSphere(Step.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        if (_isGrounded && _velocity.y < 0)
        {   
            _velocity.y = 0f;
            _jumpsRemaining = JumpCounter;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Controller.Move(move * Time.deltaTime * Speed);
        if (move != Vector3.zero)
            transform.forward = move;

        if (Input.GetButtonDown("Jump") && _jumpsRemaining > 0)
        {
            _velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            _jumpsRemaining -= 1;
        }
        
        if (Input.GetButtonDown("Dash"))
        {
            Debug.Log("Dash");
            _velocity += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        }


        _velocity.y += Gravity * Time.deltaTime;

        _velocity.x /= 1 + Drag.x * Time.deltaTime;
        _velocity.y /= 1 + Drag.y * Time.deltaTime;
        _velocity.z /= 1 + Drag.z * Time.deltaTime;

        Controller.Move(_velocity * Time.deltaTime);
    }
    
    public void CollectSpaceShard()
    {
        _spaceShards += 1;
        ShardCollectSource.Play();
        if (_spaceShards == RequiredShards) WorldGeneration();
    }

    public void WorldGeneration()
    {
        World.SetActive(true);
    }
}
