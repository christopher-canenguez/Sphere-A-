using UnityEngine;

public enum AudioType
{
    JUMP,
    LANDING,
    GAMEOVER
} // End enum.

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    bool _musicEnable = true;
    bool _fxEnable = true;

    [Space(10)]
    [Range(0, 1)] [SerializeField] float _musicVolume = 1f;
    [Range(0, 1)] [SerializeField] float _fxVolume = 1f;

    [Space(10)]
    [SerializeField] AudioSource _bgMusicSource;
    [SerializeField] AudioClip _bgMusicClip;

    [Header("Sound Effect Clip :")]
    [SerializeField] public AudioClip _jump;
    [SerializeField] public AudioClip _landing;
    [SerializeField] public AudioClip _gameover;

    GameObject oneShotGameObject;
    AudioSource oneShotAudioSource;

    private void Start()
    {
        _fxEnable = PlayerPrefs.GetInt("sfxState") == 0;
        _musicEnable = PlayerPrefs.GetInt("musicState") == 0;

        if (_musicEnable)
        {
            PlayBackgroundMusic(_bgMusicClip);
        } // End if.
    } // End Start.

    public void PlayAudio(AudioType type)
    {
        // Return if audio fx is disable.
        if (!_fxEnable)
        {
            return;
        } // End if.

        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        } // End if.

        AudioClip clip = GetClip(type);

        oneShotAudioSource.volume = _fxVolume;
        oneShotAudioSource.PlayOneShot(clip);
    } // End PlayAudio.

    public void ToggleMusic(ref bool state)
    {
        _musicEnable = !_musicEnable;

        UpdateMusic();

        state = _musicEnable;

        PlayerPrefs.SetInt("musicState", _musicEnable ? 0 : 1);
    } // End ToggleMusic.

    public void ToggleFX(ref bool state)
    {
        _fxEnable = !_fxEnable;

        state = _fxEnable;

        PlayerPrefs.SetInt("sfxState", _fxEnable ? 0 : 1);
    } // End ToggleFX.

    void UpdateMusic()
    {
        if (!_musicEnable)
        {
            _bgMusicSource.Stop();
        } // End if.
        else
        {
            PlayBackgroundMusic(_bgMusicClip);
        } // End else.
    } // End UpdateMusic.

    private AudioClip GetClip(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.JUMP:
                return _jump;
            case AudioType.LANDING:
                return _landing;
            case AudioType.GAMEOVER:
                return _gameover;
            default:
                return null;
        } // End switch.
    } // End GetClip.

    private void PlayBackgroundMusic(AudioClip clip)
    {
        _bgMusicSource.Stop();
        _bgMusicSource.clip = clip;
        _bgMusicSource.volume = _musicVolume;
        _bgMusicSource.Play();
    } // End PlayBackgroundMusic.
} // End script.
