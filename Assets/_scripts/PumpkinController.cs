using UnityEngine;

public class PumpkinController : MonoBehaviour
{
    public float speed = 5f;
    public float offset = 1f;
    public ParticleSystem particleSystem = new ParticleSystem();
    public ParticleSystem constantParticleSystem = new ParticleSystem();
    public AudioClip cropClip;
    private AudioSource m_AudioSource;
    private float m_leftEdge = -10f;
    
    private void Start()
    {
        if (Camera.main != null) m_leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x - offset;
        m_AudioSource = GetComponent<AudioSource>();
        constantParticleSystem.Play();
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
    
    public void SetPumpkinCropped()
    {
        particleSystem.Play();
        m_AudioSource.PlayOneShot(cropClip);
        constantParticleSystem.Stop();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
