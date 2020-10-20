using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperCourse.FirstLesson
{
    public class StructCollection<T> : IEnumerable<T> where T : struct
    {
        #region Feilds

        private readonly T[] _values;

        private uint _version;

        #endregion

        #region Props

        public int Length => _values.Length;

        #endregion

        #region Constructors

        public StructCollection() : this(new T[0])
        {
        }

        public StructCollection(IEnumerable<T>? values) : this(values?.ToArray())
        {
        }

        public StructCollection(T[]? values)
        {
            _values = (T[]) (values?.Clone() ?? new T[0]);
        }

        #endregion

        #region Implement IEnumerable Methods

        public IEnumerator<T> GetEnumerator() => new StructCollectionEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => new StructCollectionEnumerator(this);

        #endregion

        #region Indexers

        public T this[uint index]
        {
            get
            {
                if (index >= _values.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _values[index];
            }
            set
            {
                if (index >= _values.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                _values[index] = value;

                _version++;
            }
        }

        public IEnumerable<T> this[params uint[] indexes]
        {
            get
            {
                foreach (var index in indexes)
                {
                    if (index >= _values.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    yield return _values[index];
                }
            }
            set
            {
                var dictionary = indexes.Zip(value, (i, v) => new
                    {
                        i, v
                    })
                    .ToDictionary(x => x.i, x => x.v);

                foreach (var (index, obj) in dictionary)
                {
                    if (index >= _values.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    _values[index] = obj;

                    _version++;
                }
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return string.Join(", ", _values);
        }
        
        #endregion

        private class StructCollectionEnumerator : IEnumerator<T>
        {
            #region Feilds

            private readonly StructCollection<T> _collection;

            private readonly uint _version;

            private int _position;

            private T _currentValue;

            #endregion

            #region Constructors

            public StructCollectionEnumerator(StructCollection<T> collection)
            {
                _collection = collection;
                _position = -1;
                _currentValue = default(T);
                _version = collection._version;
            }

            #endregion

            #region Implement IEnumerator Methods

            public bool MoveNext()
            {
                if (_version == _collection._version && _position < _collection._values.Length - 1)
                {
                    _position++;
                    _currentValue = _collection._values[_position];

                    return true;
                }

                Reset();

                return false;
            }

            public void Reset()
            {
                if (_version != _collection._version)
                {
                    throw new InvalidOperationException();
                }

                _position = -1;
                _currentValue = default;
            }

            public T Current => _currentValue;

            object IEnumerator.Current
            {
                get
                {
                    if (_position < 0 || _position >= _collection._values.Length)
                    {
                        throw new IndexOutOfRangeException();
                    }

                    return Current;
                }
            }

            public void Dispose()
            {
            }

            #endregion
        }
    }
}