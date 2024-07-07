//using DG.Tweening;
//using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.UI.Menu
{
    public class MenuUI : MonoBehaviour
    {
        //[SerializeField] float _leftPosX = -1000, _rightPosX = 0;
        //[SerializeField] float _tweenDurationIntro = 3, _tweenDurationOutro = 4;
        [SerializeField] GameObject _menu;
        //[SerializeField] RectTransform _rectTransform;

        public void MenuIntro()
        {
            _menu.SetActive(true);
            //DoTweenAnimIntro();
        }

        public void MenuOutro() // public async void MenuOutro()
        {
           //await DoTweenAnimaOutro();
           _menu.SetActive(false);
        }

        //void DoTweenAnimIntro() => _rectTransform.DOAnchorPosX(_rightPosX, _tweenDurationIntro).SetUpdate(true);

        //async Task DoTweenAnimaOutro() => await _rectTransform.DOAnchorPosX(_leftPosX, _tweenDurationOutro).SetUpdate(true).AsyncWaitForCompletion();
    }
}