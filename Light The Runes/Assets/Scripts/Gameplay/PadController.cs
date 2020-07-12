using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class PadController : MonoBehaviour
    {
        private SaveData _saveData;
        private Functions _functions;
        
        public Sprite on; public Sprite off;
        [SerializeField] private PadController[] padsIteractWith;
        public bool isTurn = false;

        private float _timer = 0.2f;

        private void Start()
        {
            _saveData = FindObjectOfType<SaveData>();
            _functions = FindObjectOfType<Functions>();
        }
        
        private void FixedUpdate()
        {
            _timer -= Time.deltaTime;
        }
        private void OnMouseDown()
        {
            if (_timer >= 0 || _saveData.save.pause) return;
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
