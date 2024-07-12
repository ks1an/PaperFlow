using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ModalWindowPanel : MonoBehaviour
{
    [SerializeField] Transform _box;

    [Header("Header")]
    [SerializeField] Transform _headerArea;
    [SerializeField] TextMeshProUGUI _title;

    [Header("Content")]
    [SerializeField] Transform _contentArea;

    [Header("HorizontalContainer")]
    [SerializeField] TextMeshProUGUI _horizontalContainerText;
    [SerializeField] Transform _horizontalLayoutArea, _iconContainer;
    [SerializeField] Image _iconImage;

    [Header("Footer")]
    [SerializeField] Transform _footerArea;
    [SerializeField] Button _confirmBttn, _declineBttn, _altBttn;
    [SerializeField] TextMeshProUGUI _confirmTxt, _declineTxt, _altTxt;

    Action onAlternateAction, onDeclineAction, onConfirmAction;

    public void ShowHorizontal(string title, string message, string confirmTxt, string declineTxt, Action confirmAction, Action declineAction, Sprite icon = null, string altTxt = null, Action altAction = null)
    {
        _horizontalLayoutArea.gameObject.SetActive(true);

        #region Header

        _headerArea.gameObject.SetActive(!string.IsNullOrEmpty(title));
        _title.text = title;

        #endregion

        #region Content

        if (icon == null)
            _iconContainer.gameObject.SetActive(false);
        else
        {
            _iconContainer.gameObject.SetActive(true);
            _iconImage.sprite = icon;
        }

        _horizontalContainerText.text = message;

        #endregion

        #region Footer

        onConfirmAction = confirmAction;
        _confirmTxt.text = confirmTxt;

        if (declineAction != null)
        {
            _declineBttn.gameObject.SetActive(true);
            _declineTxt.text = declineTxt;
            onDeclineAction = declineAction;
        }
        else
            _declineBttn.gameObject.SetActive(false);

        if (altAction != null)
        {
            _altBttn.gameObject.SetActive(true);
            _altTxt.text = altTxt;
            onAlternateAction = altAction;
        }
        else
            _altBttn.gameObject.SetActive(false);

        #endregion
    }

    #region HorizontallSamples

    public void ShowHorizontallNoChoice(string title, string message, Action confirmAction, Sprite imageToShow = null)
    {
        ShowHorizontal(title, message, "Далее", null, confirmAction, null, imageToShow);
    }

    #endregion

    public void Confirm() => onConfirmAction?.Invoke();
    public void Alternate() => onAlternateAction?.Invoke();
    public void Decline() => onDeclineAction?.Invoke();
    public void CloseModalWindow() => gameObject.SetActive(false);
}
