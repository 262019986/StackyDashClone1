using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameFolders.Scripts.UI
{
    public class Finish : BaseBehaviour
    {
        #region FIELDS
        [SerializeField] private Image coin;
        [SerializeField] private RectTransform target;
        #endregion

        #region PROPERTIES
        #endregion

        #region MONOBHEAVIOR
        #endregion


        private void OnEnable()
        {
            PlayerInteractions.OnGameFinished += CreateCoins;
        }

        private void OnDisable()
        {
            PlayerInteractions.OnGameFinished -= CreateCoins;
        }

        private void CreateCoins()
        {
            for (int i = 0; i < 30; i++)
            {
                var image = Instantiate(coin,transform,false);
                image.rectTransform.anchoredPosition = new Vector2(Random.Range(200, 800), Random.Range(-1100, -500));

                image.rectTransform.DOAnchorPos(target.anchoredPosition, 1f).SetEase(Ease.InCirc);
            }
        }
    }
}
