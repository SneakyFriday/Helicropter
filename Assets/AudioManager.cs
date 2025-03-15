using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private Slider volumeSlider;
    
    private float _previousVolume = 1f;
    private bool _isMuted = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Load saved volume
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 0.75f);
        _isMuted = PlayerPrefs.GetInt("GameMuted", 0) == 1;
        
        // Init volume slider
        if (volumeSlider != null)
        {
            volumeSlider.value = _isMuted ? 0f : savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        
        // Set initial volume
        AudioListener.volume = _isMuted ? 0f : savedVolume;
        _previousVolume = savedVolume;
    }
    
    public void SetVolume(float volume)
    {
        _previousVolume = volume;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
        
        if (volume <= 0.01f && !_isMuted)
        {
            _isMuted = true;
            PlayerPrefs.SetInt("GameMuted", 1);
        }
        else if (volume > 0.01f && _isMuted)
        {
            _isMuted = false;
            PlayerPrefs.SetInt("GameMuted", 0);
        }
        
        PlayerPrefs.Save();
    }
}