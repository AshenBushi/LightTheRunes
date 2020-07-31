using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay
{
    public class PadController : MonoBehaviour
    {
        private SessionData _sessionData;
        private ProgressData _progressData;
        
        public Sprite on; public Sprite off;
        [SerializeField] private PadController[] padsIteractWith;
        public bool isTurn = false;
        
        private float _timer = 0.2f;
        private AudioSource _runeClickSound;

        private void Start()
        {
            _sessionData = FindObjectOfType<SessionData>();
            _progressData = FindObjectOfType<ProgressData>();
            
            _runeClickSound = GetComponent<AudioSource>();
        }
        
        private void FixedUpdate()
        {
            _timer -= Time.deltaTime;
        }
        private void OnMouseDown()
        {
            if (_sessionData.sessionSave.magicScrollUse)
            {
                isTurn = !isTurn;
                gameObject.GetComponent<SpriteRenderer>().sprite = isTurn ? on : off;
                _sessionData.sessionSave.magicScrollUse = false;
                FindObjectOfType<ProcessController>().CheckForWin();
                _timer = 0.2f;
                return;
            }
            if (_timer >= 0 || _sessionData.sessionSave.pause) return;
            foreach(var padController in padsIteractWith)
            {
                padController.isTurn = !padController.isTurn;
                padController.GetComponent<SpriteRenderer>().sprite = padController.isTurn ? on : off;
                if(_progressData.progressSave.sound)
                    _runeClickSound.Play();
            }

            FindObjectOfType<ProcessController>().CheckForWin();
            _timer = 0.2f;
        }
    }
}
