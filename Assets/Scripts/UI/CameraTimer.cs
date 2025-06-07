using UnityEngine;
using TMPro;
using System.Text;

public class CameraTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float _timeElapsed;
    private StringBuilder _sb = new();

    void Update()
    {
        _timeElapsed += Time.deltaTime;
        int seconds = Mathf.FloorToInt(_timeElapsed % 60f);
        int milliseconds = Mathf.FloorToInt((_timeElapsed % 1f) * 100f);
        int minutes = Mathf.FloorToInt(_timeElapsed / 60f);
        _sb.Clear();
        _sb.AppendFormat("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        _timerText.text = _sb.ToString();
    }
}
