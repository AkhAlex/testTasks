using System.Collections.Generic;
using System.Collections;
using System;

namespace MyList
{
    public class MyList<T> : IList<T>
    {
        private T[] _list;
        private int _size;

        public MyList(){
            _list = new T[0];
            _size = 0;
        }

        public MyList(int size){            
            if(size < 0)
                throw new ArgumentException(); 
            if(size == 0)
                _list = new T[0];
            else
                _list = new T[size];
                _size = size;
        }

        public int Count { get {
                return _size;
            }
        }

        public bool IsReadOnly { get {  
                return false;
            }
        }

        public T this[int index] { 
            get{
                if (index >= _size || index < 0) {
                    throw new ArgumentOutOfRangeException();
                }
                return _list[index];
            } 
            set{
                if (index >= _size || index < 0) {
                    throw new ArgumentOutOfRangeException();
                }
                _list[index] = value;
            }
        }

        public void Add (T item) {
            T[] newList = new T[_size+1];
            Array.Copy(_list, 0, newList, 0, _size);
            newList[_size] = item;
            _list = newList;
            _size++;
        }

        public void Clear (){
            _size = 0;
        }

        public bool Contains (T item){
            bool isThere = false;
            for(int i = 0; i < _size; ++i) {
                if(Object.Equals(item, _list[i])) {
                    isThere = true;
                    break;
                }            
            }
            return isThere;
        }

        public void CopyTo (T[] array, int arrayIndex){
            if(array == null)
                throw new NullReferenceException();
            if(arrayIndex < 0)
                throw new ArgumentOutOfRangeException();
            if(array.Length - arrayIndex < this.Count)
                throw new ArgumentException();
            Array.Copy(_list, 0, array, arrayIndex, _size);
        }


        public IEnumerable<T> MyIterator()
        {
            for(int i = 0; i < _size; ++i){
                yield return _list[i];
            }
            yield break; 
        }

        public IEnumerator<T> GetEnumerator()
        {
            return MyIterator().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf (T item) 
        {
            return Array.IndexOf(_list, item);
        }

        public void Insert (int index, T item) {
            if(index >= _size || index < 0)
                throw new ArgumentOutOfRangeException();
            _list[index] = item;
        }

        public bool Remove (T item) 
        { 
            bool isThere = false;
            int i = 0;
            for(; i < _size; ++i) {
                if(Object.Equals(item, _list[i])) {
                    isThere = true;
                    break;
                }            
            }
            if(isThere) {
                T[] _newList = new T[_size-1];
                for(int j = 0; j < _size-1; ++j){
                    if(j==i)
                        continue;
                    _newList[j] = _list[j];
                }
                _list = _newList;
                _size--;
            }
            return isThere;
        }

        public void RemoveAt (int index) {
            if(index >= _size || index < 0)
                throw new ArgumentOutOfRangeException();
            T[] _newList = new T[_size-1] ;
            for(int j = 0; j < _size-1; ++j){
                if(j==index)
                    continue;
                _newList[j] = _list[j];
            } 
            _list = _newList;
            _size--;       
        }
    }
}