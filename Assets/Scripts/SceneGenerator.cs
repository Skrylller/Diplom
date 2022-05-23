using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneGenerator : MonoBehaviour
{
    public static SceneGenerator main;

    [System.Serializable]
    public class GeneratedObj
    {
        [SerializeField] private SceneDynamicObject _objPrefab;
        [SerializeField] private Vector2 _timeRangeGenerate;
        [SerializeField] private int _numPositionsX;
        [SerializeField] private float _borderPositionX;

        private float _timeLastGenerate;
        private float _timeNewGenerate;

        [SerializeField] private List<SceneDynamicObject> _objsBufer = new List<SceneDynamicObject>(20);

        public void InicializeObjsBufer()
        {
            for (int i = 0; i < _objsBufer.Count; i++)
            {
                _objsBufer[i] = Instantiate(_objPrefab, SceneGenerator.main.transform);
                _objsBufer[i].DeactivateObj();
            }
        }
        public void DestroyAllBufer()
        {
            for (int i = 0; i < _objsBufer.Count; i++)
            {
                _objsBufer[i].DeactivateObj();
            }
        }

        public void CalculateGenerateObjs()
        {
            if (Time.time >= _timeLastGenerate + _timeNewGenerate)
            {
                GenerateObj();
            }
        }

        public void GenerateObj()
        {
            Vector2 position;

            if (_numPositionsX < 2)
                position = new Vector2(0, GameplaySceneController.main.ReturnUpBoundary());
            else
                position = new Vector2(-_borderPositionX + _borderPositionX * 2 / (_numPositionsX - 1) * Random.Range(0, _numPositionsX), GameplaySceneController.main.ReturnUpBoundary());

            try
            {
                _objsBufer.Where(x => x.gameObject.activeSelf == false).ToList().First().Generate(position);
            }
            catch { }

            _timeLastGenerate = _timeLastGenerate + _timeNewGenerate;
            _timeNewGenerate = Random.Range(_timeRangeGenerate.x, _timeRangeGenerate.y);
        }
    }

    [SerializeField] private List<GeneratedObj> _generatedObjs = new List<GeneratedObj>();


    private void Awake()
    {
        main = this;
        InicializeObjsBufer();
    }

    private void Update()
    {
        if (GameplaySceneController.main.isStartGameplay)
        {
            foreach (GeneratedObj generatedObj in _generatedObjs)
            {
                generatedObj.CalculateGenerateObjs();
            }
        }
    }

    private void InicializeObjsBufer()
    {
        foreach (GeneratedObj generatedObj in _generatedObjs)
        {
            generatedObj.InicializeObjsBufer();
        }
    }

    public void DestroyAll()
    {
        foreach (GeneratedObj generatedObj in _generatedObjs)
        {
            generatedObj.DestroyAllBufer();
        }
    }

}
