using UnityEngine;
using TMPro;

public class CameraTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _timeElapsed;

    void Update()
    {
        _timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(_timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(_timeElapsed % 60F);
        int milliseconds = Mathf.FloorToInt((_timeElapsed * 100F) % 100F);

        _timerText.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}
