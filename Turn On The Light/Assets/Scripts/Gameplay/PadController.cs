using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PadController : MonoBehaviour
    {
        public Sprite on; public Sprite off;
        [SerializeField] private PadController[] padsIteractWith;
        public bool isTurn = false;

        private float _timer = 0.2f;

        private void FixedUpdate()
        {
            _timer -= Time.deltaTime;
        }
        private void OnMouseDown()
        {
            if (_timer >= 0) return;
            foreach(var padController in padsIteractWith)
            {
                padController.isTurn = !padController.isTurn;
                padController.GetComponent<SpriteRenderer>().sprite = padController.isTurn ? on : off;
            }

            FindObjectOfType<ProcessController>().CheckForWin();
            _timer = 0.2f;
        }
    }
}
