using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour
{
    private float elapsedTime = 0f; // Прошедшее время
    private bool isRunning = false; // Состояние секундомера (запущен или нет)
    public TextMeshProUGUI timerText; // UI-текст для отображения времени

    void Start()
    {
        isRunning = true;
    }
    void Update()
    {
        if (isRunning)
        {
            // Увеличиваем прошедшее время
            elapsedTime += Time.deltaTime;

            // Обновляем текст таймера
            UpdateTimerText();
        }
    }
    
    void UpdateTimerText()
    {
        // Преобразуем прошедшее время в минуты, секунды и миллисекунды
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // Форматируем строку и выводим ее на экран
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}