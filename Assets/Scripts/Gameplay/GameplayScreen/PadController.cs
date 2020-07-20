using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay
{
    public class PadController : MonoBehaviour
    {
        private SessionData _sessionData;
        private Functions _functions;

        public int checkForField;
        public Sprite on; public Sprite off;
        [SerializeField] private PadController[] padsIteractWith;
        public bool isTurn = false;
        
        private float _timer = 0.2f;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _functions = FindObjectOfType<Functions>();
        }
        
        private void FixedUpdate()
        {
            _timer -= Time.deltaTime;
        }
        private void OnMouseDown()
        {
            if (_timer >= 0 || _sessionData.sessionSave.pause) return;
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
