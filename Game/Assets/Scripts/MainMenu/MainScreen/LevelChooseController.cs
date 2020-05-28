using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class LevelChooseController : MonoBehaviour
{
    private const float Speed = 50f;
    public GameObject[] fields;
    private int _fieldCheck, _sum;
    private bool _canMove = false, _next = false, _status4X4 = false, _status5X5 = false;
    private readonly Vector3 _target = new Vector3(0f, 0f, 0f);
    private Vector3 _preTarget;
    private float _timer = 0f;
    public TextMeshProUGUI fieldsName;

    private void Start()
    {
        _fieldCheck = PlayerPrefs.HasKey("LastFieldCheck") ? PlayerPrefs.GetInt("LastFieldCheck") : 0;
        fields[_fieldCheck].transform.position = _target;
    }

    private void FixedUpdate()
    {
        switch (_fieldCheck)
        {
            case 0:
                fieldsName.text = "3x3";
                break;
            case 1:
                fieldsName.text = "4x4";
                break;
            case 2:
                fieldsName.text = "5x5";
                break;
        }
        PlayerPrefs.SetInt("LastFieldCheck", _fieldCheck);
        Move();
        _timer -= Time.deltaTime;
    }

    public void NextField()
    {
        if (!(_timer <= 0)) return;
        if (_fieldCheck >= 2) return;
        _fieldCheck += 1;
        _next = true;
        _timer = 0.2f;
        StartMove(new Vector3(10.8f, 0f, 0f), new Vector3(-10.8f, 0f, 0f));
    }

    public void PreField()
    {
        if (!(_timer <= 0)) return;
        if (_fieldCheck <= 0) return;
        _fieldCheck -= 1;
        _next = false;
        _timer = 0.2f;
        StartMove(new Vector3(-10.8f, 0f, 0f), new Vector3(10.8f, 0f, 0f));
    }
    
    public void PlayField()
    {
        if (PlayerPrefs.GetInt("Energy") <= 0) return;
        PlayerPrefs.SetInt("Energy", PlayerPrefs.GetInt("Energy") - 1);
        switch (_fieldCheck)
        {
            case 0:
            {
                LoadGameScene(0);
                break;
            }
            case 1:
            {
                LoadGameScene(20);
                break;
            }
            default:
            {
                LoadGameScene(40);
                break;
            }
        }
    }

    public void Empty5X5()
    {
        PlayerPrefs.SetInt("Level", 50);
        SceneManager.LoadScene("5x5");
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
    
    private void StartMove(Vector3 spawn, Vector3 preTarget)
    {
        _canMove = true;
        fields[_fieldCheck].transform.position = spawn;
        _preTarget = preTarget;
    }
    
    private void Move()
    {
        if (!_canMove) return;
        if (_next)
            fields[_fieldCheck - 1].transform.position =
                Vector3.MoveTowards(fields[_fieldCheck - 1].transform.position, _preTarget, Speed * Time.deltaTime);
        else
            fields[_fieldCheck + 1].transform.position =
                Vector3.MoveTowards(fields[_fieldCheck + 1].transform.position, _preTarget, Speed * Time.deltaTime);

        fields[_fieldCheck].transform.position = Vector3.MoveTowards(fields[_fieldCheck].transform.position,
            _target, Speed * Time.deltaTime);


        if (fields[_fieldCheck].transform.position != _target) return;
        _canMove = false;
        if (_next)
            fields[_fieldCheck - 1].transform.position = new Vector3(0, 25f, 0);
        else
            fields[_fieldCheck + 1].transform.position = new Vector3(0, 25f, 0);
    }
}
