using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] InputReader m_InputReader;
    [SerializeField] Sprite[] sprites;
    [SerializeField] ParticleSystem particleSystem = new ParticleSystem();
    [SerializeField] ParticleSystem explosionSystem = new ParticleSystem();
    [SerializeField] AudioClip heliSound = null;
    [SerializeField] AudioClip explosionSound = null;
    public float speed = 5f;
    public float jumpForce = 10f;
    public static UnityEvent OnCropped = new UnityEvent();
    public static UnityEvent OnPumpkinCropped = new UnityEvent();
    private float gravity = -9.8f;
    private Vector3 direction = Vector3.zero;
    private SpriteRenderer spriteRenderer;
    private int spriteIndex = 0;
    private AudioSource m_AudioSource;
    private Vector3 m_PlayerPosition;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_AudioSource = GetComponent<AudioSource>();
        m_PlayerPosition = transform.position;
    }
    
    // private void OnEnable()
    // {
    //     m_InputReader.Jump += OnJump;
    // }
    //
    // private void OnDisable()
    // {
    //     m_InputReader.Jump -= OnJump;
    // }

    private void Update()
    {
        // When Space is pressed, invoke the OnJump 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnJump();
        }
    }

    private void OnJump()
    {
        direction = Vector3.up * jumpForce;
    }
    
    public void ResetPlayerPosition()
    {
        transform.position = m_PlayerPosition;
    }

    void Start()
    {
       InvokeRepeating(nameof(AnimateSprite), 0f, 0.1f);
       GameManager.Instance.RestartGameEvent.AddListener(ResetPlayerPosition);
       gameObject.SetActive(true);
       particleSystem.Play();
    }
    
    private void AnimateSprite()
    {
        if(GameManager.IsGameRunning == false) return;
        if(!m_AudioSource.isPlaying) m_AudioSource.PlayOneShot(heliSound);
        spriteIndex++;
        if(spriteIndex >= sprites.Length)
            spriteIndex = 0;
        
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    void FixedUpdate()
    {
        AdjustPlayerPosition();
    }
    
    private void AdjustPlayerPosition()
    {
        if(GameManager.IsGameRunning == false) return;
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<KornController>())
        {
            var corn = other.GetComponent<KornController>();
            corn.SetCornCropped();
            OnCropped.Invoke();
        }

        if (other.GetComponent<ScarecrowController>() || other.CompareTag("Ground"))
        {
            particleSystem.Stop();
            GameManager.IsGameRunning = false;
            explosionSystem.Play();
            m_AudioSource.Stop();
            m_AudioSource.PlayOneShot(explosionSound);
            gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
        
        if (other.GetComponent<PumpkinController>())
        {
            var pumpkin = other.GetComponent<PumpkinController>();
            pumpkin.SetPumpkinCropped();
            OnPumpkinCropped.Invoke();
        }
    }
}
