using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Helpers;

namespace AceObjectionEngine.Engine.Collections
{
    public class AnimatorLayersCollection<T> : IDictionary<int, AnimatorObjectsCollection<T>>, IEnumerable<IAnimationObject>, IEnumerator<AnimatorObjectsCollection<T>>
        where T : class, IAnimatorHierarchy<T>
    {
        private readonly IDictionary<int, AnimatorObjectsCollection<T>> _dict = new SortedDictionary<int, AnimatorObjectsCollection<T>>();
        public T Parent;
        public AnimatorObjectsCollection<T> this[int key] { get 
            {
                if (!_dict.ContainsKey(key)) AddSimpleLayer(key);
                return _dict[key];
            } set => _dict[key] = value; }

        public ICollection<int> Keys => _dict.Keys;

        public ICollection<AnimatorObjectsCollection<T>> Values => _dict.Values;

        public int Count => _dict.Count;

        public bool IsReadOnly => false;

        public int _position = -1;
        public AnimatorObjectsCollection<T> Current => _dict[_position];

        object IEnumerator.Current => Current;

        private int _layerIndexer = 0;

        public AnimatorLayersCollection(T parent)
        {
            Parent = parent;
        }

        public void Add(int layer, AnimatorObjectsCollection<T> value)
        {
            if (_dict.ContainsKey(layer))
            {
                foreach (var animationObject in value)
                {
                    _dict[layer].Add(animationObject);
                }
                return;
            }
            _dict.Add(layer, value);
        }

        public void Add(int layer, IAnimationObject value)
        {
            if (value != null && TypeHelper.GetExtensiveAttribute<FloatyIndexInvoker>(value) != null) _layerIndexer++;

            if (value is IFloatyLayerIndex objectLayerIndexer) objectLayerIndexer.LayerIndexer = _layerIndexer;

            if (_dict.ContainsKey(layer))
            {
                _dict[layer].Add(value);
                return;
            }
            AddSimpleLayer(layer);
            Add(layer, value);
        }

        public int FindLayerWithObjects<TObject>() where TObject : IAnimationObject
        {
            var layer = _dict.Where(x => x.Value.Any(y => y is TObject));

            return layer.Any() ? layer.First().Key : -1;
        }

        public int FindLayerWithObjects(Type objectType)
        {
            var layer = _dict.Where(x => x.Value.Any(y => TypeHelper.AreSame(y.GetType(), objectType)));

            return layer.Any() ? layer.First().Key : -1;
        }

        public bool Any(Func<KeyValuePair<int, AnimatorObjectsCollection<T>>, bool> predicate) => 
            _dict.Any(predicate);

        public void AddSimpleLayer(int layer) => Add(layer, new AnimatorObjectsCollection<T>(Parent));

        public void Add(KeyValuePair<int, AnimatorObjectsCollection<T>> item) => Add(item.Key, item.Value);

        public void Clear() => _dict.Clear();

        public bool Contains(KeyValuePair<int, AnimatorObjectsCollection<T>> item) => _dict.Contains(item);

        public bool ContainsKey(int layer) => _dict.ContainsKey(layer);

        public void CopyTo(KeyValuePair<int, AnimatorObjectsCollection<T>>[] array, int arrayIndex)
        {
            foreach(var entry in _dict)
            {
                array[arrayIndex] = new KeyValuePair<int, AnimatorObjectsCollection<T>>(entry.Key, (AnimatorObjectsCollection<T>)entry.Value.Clone());
            }
        }

        public IEnumerator<KeyValuePair<int, AnimatorObjectsCollection<T>>> GetEnumerator() => _dict.GetEnumerator();

        public bool Remove(int layer)
        {
            if (!_dict.ContainsKey(layer)) return false;
            return _dict.Remove(layer);
        }

        public bool Remove(KeyValuePair<int, AnimatorObjectsCollection<T>> item)
        {
            if (!_dict.Contains(item)) return false;
            return _dict.Remove(item.Key);
        }

        //public IEnumerable<TAnimationLayerObject> SelectAlongLayerObjects<TAnimationLayerObject>(int layer) 
        //    where TAnimationLayerObject : IAnimationObject
        //{
        //    foreach(var layer in _dict)
        //    {
        //        foreach(var animationObject in layer.Value)
        //        {
        //            if(animationObject is TAnimationLayerObject) yield return (TAnimationLayerObject)animationObject;
        //        }
        //    }
        //}

        public bool TryGetValue(int layer, out AnimatorObjectsCollection<T> value) => _dict.TryGetValue(layer, out value);

        IEnumerator IEnumerable.GetEnumerator() => _dict.GetEnumerator();

        public IEnumerable<IAnimationObject> AsAnimationObjectEnumerable() => this;

        IEnumerator<IAnimationObject> IEnumerable<IAnimationObject>.GetEnumerator() => _dict.Values
            .SelectMany(x => x.Select(y => y)).GetEnumerator();

        public void Dispose()
        {
            _dict.Clear();
            _position = -1;
        }

        public bool MoveNext()
        {
            if (_position >= _dict.Count) return false;
            _position++;
            return true;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}
