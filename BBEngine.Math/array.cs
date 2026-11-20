
using System;
using System.Collections;
using System.Collections.Generic;

namespace BBEngine.Math
{
    public class array<T> :
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable,
        IList<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        ICollection,
        IList
    {
        private readonly List<T> _items = new();

        // Constructor
        public array() { }

        public array(int capacity)
        {
            _items = new List<T>(capacity);
        }

        public array(IEnumerable<T> collection)
        {
            _items = new List<T>(collection);
        }

        // Properties
        public int Count => _items.Count;

        public int Capacity
        {
            get => _items.Capacity;
            set => _items.Capacity = value;
        }

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => ((ICollection)_items).SyncRoot;

        // Indexer
        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        T IList<T>.this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        object? IList.this[int index]
        {
            get => this[index];
            set => this[index] = ConvertValue(value);
        }

        // Add
        public void Add(T item)
        {
            _items.Add(item);
        }

        int IList.Add(object? value)
        {
            _items.Add(ConvertValue(value));
            return Count - 1;
        }

        // Insert
        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
        }

        void IList.Insert(int index, object? value)
        {
            _items.Insert(index, ConvertValue(value));
        }

        // Remove
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        void IList.Remove(object? value)
        {
            if (IsCompatibleObject(value))
                _items.Remove((T)value!);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        // Clear
        public void Clear()
        {
            _items.Clear();
        }

        // Contains
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        bool IList.Contains(object? value)
        {
            return IsCompatibleObject(value) && Contains((T)value!);
        }

        // IndexOf
        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        int IList.IndexOf(object? value)
        {
            return IsCompatibleObject(value)
                ? IndexOf((T)value!)
                : -1;
        }

        // Copy
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)_items).CopyTo(array, index);
        }

        // Enumeration
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Capacity
        public void EnsureCapacity(int capacity)
        {
            _items.EnsureCapacity(capacity);
        }

        public void TrimExcess()
        {
            _items.TrimExcess();
        }

        // Helpers
        private static bool IsCompatibleObject(object? value)
        {
            if (value is T)
                return true;

            return value == null && default(T) == null;
        }

        private static T ConvertValue(object? value)
        {
            if (IsCompatibleObject(value))
                return (T)value!;

            throw new ArgumentException(
                $"Value must be of type {typeof(T)}.",
                nameof(value)
            );
        }

        // Add Operator
        public static array<T> operator +(array<T> array, T item)
        {
            array.Add(item);
            return array;
        }

        public static array<T> operator -(array<T> array, T item)
        {
            array.Remove(item);
            return array;
        }
    }
}
