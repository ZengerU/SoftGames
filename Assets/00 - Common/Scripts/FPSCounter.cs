using System.Globalization;
using TMPro;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class FPSCounter : MonoBehaviour
    {
        TextMeshProUGUI _text;

        void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            var fps = 1.0f / Time.unscaledDeltaTime;

            // Update the TMP text with the current FPS
            _text.SetText($"FPS: {Mathf.Ceil(fps).ToString(CultureInfo.InvariantCulture)}");
        }
    }
}