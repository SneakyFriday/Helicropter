using UnityEngine;

public class ScarecrowController : MonoBehaviour
{
    public float speed = 5f;
    public float offset = 1f;
    private float m_leftEdge = -10f;
    
    private void Start()
    {
        if (Camera.main != null) m_leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero).x - offset;
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
}
