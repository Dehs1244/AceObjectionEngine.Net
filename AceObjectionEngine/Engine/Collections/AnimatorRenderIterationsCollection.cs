using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Exceptions;

namespace AceObjectionEngine.Engine.Collections
{
    public class AnimatorRenderIterationsCollection<T> : ICollection<T>
    {
        private ICollection<int> _endedIterations = new List<int>();
        private int _currentIteration = -1;
        private Dictionary<int, List<T>> _iterations = new Dictionary<int, List<T>>();

        public int Count => _iterations[_currentIteration].Count;

        public bool IsReadOnly => _currentIteration == -1;

        public void StartIteration(int iteration)
        {
            if (_endedIterations.Contains(iteration)) throw new ObjectionIterationEndedException(iteration);
            _currentIteration = iteration;
            _iterations.Add(iteration, new List<T>());
        }

        public void Add(T obj)
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();
            _iterations[_currentIteration].Add(obj);
        }

        public void EndIteration()
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            _endedIterations.Add(_currentIteration);
            _currentIteration = -1;
        }

        public IEnumerable<T> MapAll() => _iterations.SelectMany((x, y) => x.Value);

        public void Clear()
        {
            _iterations.Clear();
            _endedIterations.Clear();
        }

        public bool Contains(T item)
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            return _iterations[_currentIteration].Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            _iterations[_currentIteration].CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            return _iterations[_currentIteration].Remove(item);
        }

        public void RemoveAt(int index)
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            _iterations[_currentIteration].RemoveAt(index);
        }

        public void RemoveLast() => RemoveAt(Count - 1);

        public IEnumerator<T> GetEnumerator()
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            return _iterations[_currentIteration].GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_currentIteration == -1) throw new ObjectionRenderIteratorNotStartedException();

            return _iterations[_currentIteration].GetEnumerator();
        }
    }
}
