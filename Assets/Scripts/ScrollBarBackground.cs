using UnityEngine;
using UnityEngine.UI;

public class ScrollBarBackground : MonoBehaviour
{
    [SerializeField] private RectTransform HandlerRect;    // RectTransform хэндлера
    [SerializeField] private RectTransform FillRect;       // RectTransform заливки
    [SerializeField] private RectTransform BackgroundRect; // RectTransform фона (контейнера)

    private void Update()
    {
        if (HandlerRect == null || FillRect == null || BackgroundRect == null) return;

        // Вычисляем ширину заливки на основе позиции хэндлера
        float normalizedPosition = Mathf.InverseLerp(
            BackgroundRect.rect.xMin,
            BackgroundRect.rect.xMax,
            HandlerRect.anchoredPosition.x
        );

        // Устанавливаем ширину заливки
        FillRect.sizeDelta = new Vector2(normalizedPosition * BackgroundRect.rect.width, FillRect.sizeDelta.y);
    }
}
