using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Этот скрипт отвечает за воспроизведение звука и изменение масштаба объекта(кнопки) при взаимодействии с ним.
/// </summary>
public class SoundOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField] public AudioSource source;  // Ссылка на компонент AudioSource для воспроизведения звука.
    private const float _defaultScaleChange = 1.0f; // Значение по умолчанию
    private float _scaleChange = 1.1f;  // Множитель для изменения масштаба при наведении.

    /// <summary>
    /// Вызывается при наведении указателя мыши на объект.
    /// </summary>
    /// <param name="eventData">Данные о событии указателя.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Увеличиваем масштаб объекта при наведении.
        transform.localScale *= _scaleChange;

        // Проверяем наличие аудиоклипа и воспроизводим его.
        if (source.clip != null)
        {
            source.PlayOneShot(source.clip);
        }

    }

    /// <summary>
    /// Вызывается при уходе указателя мыши с объекта.
    /// </summary>
    /// <param name="eventData">Данные о событии указателя.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        // Возвращаем масштаб объекта к значению по умолчанию.
        transform.localScale = new Vector3(_defaultScaleChange, _defaultScaleChange, _defaultScaleChange);
    }

    /// <summary>
    /// Вызывается при отпускании кнопки мыши над объектом.
    /// </summary>
    /// <param name="eventData">Данные о событии указателя.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        // Возвращаем масштаб объекта к значению по умолчанию.
        transform.localScale = new Vector3(_defaultScaleChange, _defaultScaleChange, _defaultScaleChange);
    }
}
