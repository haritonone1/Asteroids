using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    
    [SerializeField] private Transform _playerTransform;
    private Camera _camera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public Transform ReturnPlayer()
    {
        return _playerTransform;
    }
    
    private void ReturnPlayerIfIsOutOfCamera()
    {
        if (_playerTransform == null)
            return;
        
        var playerPosOnScreen = _camera.WorldToScreenPoint(_playerTransform.position);

        if (playerPosOnScreen.y < 0)
        {
            _playerTransform.position = _camera.ScreenToWorldPoint(new Vector3(playerPosOnScreen.x, Screen.height, playerPosOnScreen.z));
            return;
        }

        if (playerPosOnScreen.y > Screen.height)
        {
            _playerTransform.position =
                _camera.ScreenToWorldPoint(new Vector3(playerPosOnScreen.x, 0f, playerPosOnScreen.z));
            return;
        }
        
        if (playerPosOnScreen.x < 0)
        {
            _playerTransform.position =
                _camera.ScreenToWorldPoint(new Vector3(Screen.width, playerPosOnScreen.y, playerPosOnScreen.z));
            return;
        }
        
        if (playerPosOnScreen.x > Screen.width)
        {
            _playerTransform.position =
                _camera.ScreenToWorldPoint(new Vector3(0f, playerPosOnScreen.y, playerPosOnScreen.z));
            return;
        }
    }
    
    void Update()
    {
        ReturnPlayerIfIsOutOfCamera();
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
}
