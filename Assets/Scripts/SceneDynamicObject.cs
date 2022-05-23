using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDynamicObject : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Rigidbody2D _objRigidbody2D;
    private Transform _objTransform;

    private void Awake()
    {
        _objRigidbody2D = GetComponent<Rigidbody2D>();
        _objTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        CheckDownBorder();
    }

    private void FixedUpdate()
    {
        MoveObj();
    }

    private void MoveObj()
    {
        _objRigidbody2D.velocity = new Vector2(0, GameplaySceneController.main.sceneSpeed * _speed) * Time.timeScale;
    }

    private void CheckDownBorder()
    {
        if(_objTransform.position.y <= GameplaySceneController.main.ReturnDownBoundary())
        {
            DeactivateObj();
        }
    }

    public void Generate(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        gameObject.SetActive(true);
    }

    public void DeactivateObj()
    {
        gameObject.SetActive(false);
    }
}
