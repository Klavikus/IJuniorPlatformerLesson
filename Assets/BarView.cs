using UnityEngine;
using UnityEngine.UI;

public class BarView : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void OnValueChanged(float value)
    {
        _image.fillAmount = value;
    }
}