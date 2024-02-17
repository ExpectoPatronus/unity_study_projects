using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Управляет звуковыми настройками в игре, воспроизводит звуки и музыку.
/// </summary>
public class SoundManager : MonoBehaviour
{
    public AudioMixer masterMixer;   // Мастер-микшер звуковых настроек.
    public static SoundManager soundManagerInstance;  // Ссылка на экземпляр класса.

    [SerializeField] private Image imageSFX, imageMusic;  // Графические элементы для отображения состояния звука и музыки.
    [SerializeField] private Sprite[] spriteSFX, spriteMusic;  // Спрайты для отображения состояния звука и музыки.

    [SerializeField] private AudioClip[] clipSFX; // Звуковые эффекты.
    [SerializeField] private AudioClip[] clipMusic; // Музыкальные композиции.

    private AudioSource _sourceSFX; // Компонент AudioSource для воспроизведения звуковых эффектов.
    private AudioSource _sourceMusic;  // Компонент AudioSource для воспроизведения музыки.

    private float _volumeSFX = 1f;  // Громкость звуковых эффектов.
    private float _volumeMusic = 1f; // Громкость музыки.
    private bool _muteSFX, _muteMusic; // Флаги отключения звука и музыки.

    private void Awake()
    {
        soundManagerInstance = this; // Устанавливаем ссылку на экземпляр класса.
        _sourceSFX = gameObject.AddComponent<AudioSource>(); // Создаем компонент AudioSource для звуковых эффектов.
        _sourceMusic = gameObject.AddComponent<AudioSource>(); // Создаем компонент AudioSource для музыки.
        _sourceMusic.volume = _volumeMusic; // Устанавливаем громкость музыки.
        _sourceSFX.volume = _volumeSFX; // Устанавливаем громкость звуковых эффектов.
    }

    /// <summary>
    /// Воспроизводит звуковой эффект.
    /// </summary>
    /// <param name="clip">Звуковой эффект для воспроизведения.</param>
    public void PlaySFX(AudioClip clip)
    {
        if (!_sourceSFX.isPlaying)
        {
            _sourceSFX.clip = clip;
            _sourceSFX.PlayOneShot(_sourceSFX.clip);
        }
    }

    /// <summary>
    /// Воспроизводит звуковой эффект по индексу из массива clip_SFX.
    /// </summary>
    /// <param name="i">Индекс звукового эффекта в массиве clip_SFX.</param>
    public void PlaySFX(int i)
    {
        _sourceSFX.clip = clipSFX[i];
        _sourceSFX.PlayOneShot(_sourceSFX.clip);
    }

    /// <summary>
    /// Воспроизводит музыкальную композицию по индексу из массива clip_Music.
    /// </summary>
    /// <param name="i">Индекс музыкальной композиции в массиве clip_Music.</param>
    public void PlayMusic(int i)
    {
        _sourceMusic.clip = clipMusic[i];
        _sourceMusic.PlayOneShot(_sourceMusic.clip);
    }

    /// <summary>
    /// Переключает режим отключения/включения музыки.
    /// </summary>
    public void MuteMusic()
    {
        _muteMusic = !_muteMusic;
        imageMusic.sprite = spriteMusic[_muteMusic ? 1 : 0];
        masterMixer.SetFloat("VolumeMusic", _muteMusic ? -80f : _volumeMusic);
    }

    /// <summary>
    /// Переключает режим отключения/включения звуковых эффектов.
    /// </summary>
    public void MuteSFX()
    {
        _muteSFX = !_muteSFX;
        imageSFX.sprite = spriteSFX[_muteSFX ? 1 : 0];
        masterMixer.SetFloat("VolumeSFX", _muteSFX ? -80f : _volumeSFX);
    }
}
