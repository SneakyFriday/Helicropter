using UnityEngine;

public class KornController : MonoBehaviour
{
    public float speed = 5f;
    public float offset = 1f;
    public ParticleSystem particleSystem = null;
    public AudioClip cropClip;
    private AudioSource m_AudioSource;
    private float m_leftEdge = -10f;
    private SpriteRenderer m_SpriteRenderer;

    private void Start()
    {
        if (Camera.main != null) m_leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x - offset;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(GameManager.IsGameRunning == false) return;
        transform.position += Vector3.left * (speed * Time.deltaTime);
        if (transform.position.x < m_leftEdge)
        {
            Destroy(gameObject);
        }
    }

    public void SetCornCropped()
    {
        m_SpriteRenderer.enabled = false;
        particleSystem.Play();
        m_AudioSource.PlayOneShot(cropClip);
    }
}
