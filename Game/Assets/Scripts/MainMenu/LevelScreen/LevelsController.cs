using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class LevelsController : MonoBehaviour
{
    public void GoToLevel()
    {
        if (PlayerPrefs.GetInt("Energy") <= 0) return;
        var level = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        Debug.Log(level);
        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
        PlayerPrefs.SetInt("Level", level);
        LoadGameScene(level);
    }

    private static void LoadGameScene(int level)
    {
        if (level < 21)
            SceneManager.LoadScene("3x3");
        else if (level < 42)
            SceneManager.LoadScene("4x4");
        else
            SceneManager.LoadScene("5x5");

    }
    
    private const float Speed = 50f;
    public TextMeshProUGUI _stage;
    public GameObject[] blocks;
    public GameObject[] buttons;
    private int _blockCheck = 0;
    private bool _canMove = false;
    private bool _next = false;
    private readonly Vector3 _target = new Vector3(10.8f, 0f, 0f);
    private Vector3 _preTarget;
    private float _timer = 0f;

    private void Start()
    {
        _blockCheck = PlayerPrefs.HasKey("LastBlockCheck") ? PlayerPrefs.GetInt("LastBlockCheck") : 0;
        _stage.text = (_blockCheck + 1).ToString();
        blocks[_blockCheck].transform.position = _target;
    }

    private void FixedUpdate()
    {
        Move();
        PlayerPrefs.SetInt("LastBlockCheck", _blockCheck);
        switch (_blockCheck)
        {
            case 0:
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                break;
            case 1:
                buttons[0].SetActive(true);
                buttons[1].SetActive(true);
                break;
            case 2:
                buttons[0].SetActive(true);
                buttons[1].SetActive(false);
                break;
        }
        _stage.text = (_blockCheck + 1).ToString();
        _timer -= Time.deltaTime;
    }

    public void NextBlock()
    {
        if (!(_timer <= 0)) return;
        if (_blockCheck >= 2) return;
        _blockCheck += 1;
        _next = true;
        _timer = 0.2f;
        StartMove(new Vector3(21.6f, 0f, 0f), new Vector3(0f, 0f, 0f));
    }

    public void PreBlock()
    {
        if (!(_timer <= 0)) return;
        if (_blockCheck <= 0) return;
        _blockCheck -= 1;
        _next = false;
        _timer = 0.2f;
        StartMove(new Vector3(0f, 0f, 0f), new Vector3(21.6f, 0f, 0f));
    }
    
    private void StartMove(Vector3 spawn, Vector3 preTarget)
    {
        _canMove = true;
        blocks[_blockCheck].transform.position = spawn;
        _preTarget = preTarget;
    }
    
    private void Move()
    {
        if (!_canMove) return;
        if (_next)
            blocks[_blockCheck - 1].transform.position =
                Vector3.MoveTowards(blocks[_blockCheck - 1].transform.position, _preTarget, Speed * Time.deltaTime);
        else
            blocks[_blockCheck + 1].transform.position =
                Vector3.MoveTowards(blocks[_blockCheck + 1].transform.position, _preTarget, Speed * Time.deltaTime);

        blocks[_blockCheck].transform.position = Vector3.MoveTowards(blocks[_blockCheck].transform.position,
            _target, Speed * Time.deltaTime);


        if (blocks[_blockCheck].transform.position != _target) return;
        _canMove = false;
        if (_next)
            blocks[_blockCheck - 1].transform.position = new Vector3(0, 25f, 0);
        else
            blocks[_blockCheck + 1].transform.position = new Vector3(0, 25f, 0);
    }
}
    
