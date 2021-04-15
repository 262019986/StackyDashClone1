using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.UI
{
    public class ScoreHolder : BaseBehaviour
    {
        #region FIELDS
        [SerializeField] private int orderID;
        private Text _scoreText;
        private int _scoreCount;
        #endregion

        #region PROPERTIES
        #endregion

        #region MONOBHEAVIOR
        #endregion

        private void OnEnable() => PlayerInteractions.OnCubeCollected += IncreaseCount;

        private void OnDisable() => PlayerInteractions.OnCubeCollected -= IncreaseCount;

        public override void Subscribe()
        {
            Executor.behaviours.Add(orderID,this);
        }

        public override void BaseAwake()
        {
            _scoreText = GetComponent<Text>();
            _scoreText.text = $"{_scoreCount}";
        }

        private void IncreaseCount()
        {
            _scoreCount++;
            _scoreText.text = $"{_scoreCount}";
        }
    }
}
