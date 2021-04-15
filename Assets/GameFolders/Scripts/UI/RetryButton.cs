using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameFolders.Scripts.UI
{
    public class RetryButton : BaseBehaviour , IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
