using TMPro;
using UnityEngine;

public sealed class ViewFPS : MonoBehaviour
{
    [SerializeField, Range(0,10)] int _viewUpdateInterval;
    [SerializeField] float _updateInterval = 0.5f;
    [SerializeField] TextMeshProUGUI _text;

    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;

    void Start()
    {
        timeleft = _updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            fps = (accum / frames);
            timeleft = _updateInterval;
            accum = 0.0f;
            frames = 0;
        }

        if(Time.frameCount % _viewUpdateInterval == 0)
        _text.text = fps.ToString();
    }
}
