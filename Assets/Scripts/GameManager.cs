using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                            var singleton = new GameObject("GameManager");
                            _instance = singleton.AddComponent<GameManager>();
                            //DontDestroyOnLoad(singleton);
                }
            }
                
            return _instance;
        }
    }

    #endregion
    [HideInInspector] public float ScreenWidth => Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x;

    [Header("Walls options")]
    [SerializeField] private BoxCollider2D LeftWallCollider;
    [SerializeField] private BoxCollider2D RightWallCollider;

    private void Start()
    {
        ChangeScreenSides();
    }

    public void ChangeScreenSides()
    {
        LeftWallCollider.transform.position = new Vector3 (-ScreenWidth - LeftWallCollider.size.x / 2f, 0f, 0f);
		RightWallCollider.transform.position = new Vector3 (ScreenWidth + RightWallCollider.size.x / 2f, 0f, 0f);
    }
}

