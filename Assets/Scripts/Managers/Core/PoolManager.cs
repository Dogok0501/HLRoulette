using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolManager 
{
    class Pool
    {
        public GameObject Prefab { get; private set; }
        public Transform Root { get; set; }

        Queue<Figure> _figurePoolQueue = new Queue<Figure>();
        Queue<Poolable> _poolablePoolQueue = new Queue<Poolable>();

        public void Init(int index)
        {
            Prefab = AddressableManager.handle[index - 1000].Result;
            Root = new GameObject().transform;
            Root.name = Managers.Data.figureDataDict[index].name.ToString() + "_Root_Figure";

            for (int i = 0; i < Define.FIGURE_POOL_SIZE; i++)
            {
                Push(FigureCreate());
            }
        }

        public void Init(GameObject prefab)
        {
            switch(prefab.name)
            {
                case "Star":
                    Prefab = Managers.Resource.GetMouseEffectPrefab();
                    break;

                case "Ball":
                    Prefab = Managers.Resource.GetBallPrefab();
                    break;

                case "PopupText":
                    Prefab = Managers.Resource.GetTextPrefab();
                    break;
            }
            
            Root = new GameObject().transform;
            Root.name = prefab.name + "_Root";

            for (int i = 0; i < Define.POOLABLE_POOL_SIZE; i++)
            {
                Push(PoolableCreate());
            }
        }

        Figure FigureCreate()
        {
            GameObject go = Object.Instantiate<GameObject>(Prefab);
            Figure figure = go.GetComponent<Figure>();
            figure.Init();
            return figure;
        }

        Poolable PoolableCreate()
        {
            GameObject go = Object.Instantiate<GameObject>(Prefab);
            Poolable poolable = go.GetComponent<Poolable>();
            poolable.Init();
            return poolable;
        }

        public void Push(Figure figure)
        {
            if (figure == null)
                return;

            figure.myTransform.SetParent(Root);
            figure.myGameObject.SetActive(false);

            _figurePoolQueue.Enqueue(figure);
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.myTransform.SetParent(Root);
            poolable.myGameObject.SetActive(false);
            poolable.isUsing = false;

            _poolablePoolQueue.Enqueue(poolable);
        }

        public Figure FigurePop(Transform parent)
        {
            Figure figure;

            if (_figurePoolQueue.Count > 0)
                figure = _figurePoolQueue.Dequeue();
            else
                figure = FigureCreate();            

            figure.Init();            
            figure.myGameObject.SetActive(true);
            figure.myTransform.SetParent(parent);

            return figure;
        }

        public Poolable PoolablePop(Transform parent)
        {
            Poolable poolable;

            if (_poolablePoolQueue.Count > 0)
                poolable = _poolablePoolQueue.Dequeue();
            else
                poolable = PoolableCreate();

            poolable.Init();
            poolable.myGameObject.SetActive(true);
            poolable.myTransform.SetParent(parent);
            poolable.isUsing = true;

            return poolable;
        }
    }

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(int index)
    {
        Pool pool = new Pool();
        pool.Init(index);
        pool.Root.SetParent(_root);

        _pool.Add(Managers.Data.figureDataDict[index].name, pool);
    }

    public void CreatePool(GameObject prefab)
    {
        Pool pool = new Pool();
        pool.Init(prefab);
        pool.Root.SetParent(_root);
        
        _pool.Add(prefab.name, pool);
    }

    public void Push(Figure figure)
    {
        string name = figure.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            Addressables.ReleaseInstance(figure.gameObject);
            return;
        }

        _pool[name].Push(figure);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name.Replace("(Clone)", "").Trim();
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Figure Pop(int index, Transform parent = null)
    {
        if (_pool.ContainsKey(Managers.Data.figureDataDict[index].name) == false)
            CreatePool(index);

        return _pool[Managers.Data.figureDataDict[index].name].FigurePop(parent);
    }

    public Poolable Pop(GameObject prefab, Transform parent = null)
    {
        if (_pool.ContainsKey(prefab.name) == false)
            CreatePool(prefab);

        return _pool[prefab.name].PoolablePop(parent);
    }

    public void FigureClear()
    {
        foreach (Transform root in _root)
        {
            if(root.name.Contains("_Figure"))
            {
                foreach (GameObject obj in root)
                    Addressables.ReleaseInstance(obj);

                GameObject.Destroy(root.gameObject);
            }                
        }            

        _pool.Clear();
    }

    public void Clear()
    {
        foreach (Transform root in _root)
            GameObject.Destroy(root.gameObject);

        _pool.Clear();
    }
}
