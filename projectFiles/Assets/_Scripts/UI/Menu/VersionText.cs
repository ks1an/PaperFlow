using TMPro;
using UnityEngine;

public sealed class VersionText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = Application.version + "    ";
    }
}
