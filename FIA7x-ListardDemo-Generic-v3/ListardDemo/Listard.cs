/*
 * fn@gso-koeln.de 2018
 */
using System;
using System.Collections;

namespace ListardDemo
{
    ///*
    //public class ListardLong ... int values ==> long values 
    //public class ListardDateTime ... int values ==> DateTime values 
    //public class ListardPosition ... int values ==> Position values 
    //*/

    //public class Listard<T> {
    //    // statt int values ==> T 
    //}

    //Listard<int> iInt = new Listard<int>();
    //Listard<Position> iInt = new Listard<Position>();

    // multi purpose list class
    public class Listard<T> : IEnumerable
    {
        private T[] values = new T[0]; // internal buffer
        private int count = 0; // number of contained elements

        // constructor
        public Listard(int count = 0) {
            this.values = new T[count];
            this.count = count;
        }

        // getter - property to retrieve the number of contained elements
        public int Count { get { return count; } }

        // getter - property to retrieve the size currently reserved
        public int Size { get { return values.Length; } }

        // index based read/write access using []-operator overloading
        public T this[int index] {
            get { return Get(index); } // retrieve value at index
            set { Set(value, index); }    // set value at index
        }

        // index based read access
        public T Get(int index) {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            return this.values[index];
        }

        // index based write access
        public void Set(T value, int index) {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            this.values[index] = value;
        }

        // insert an element at the given index
        public void InsertAt(T value, int index) {
            InsertAt(new T[] { value }, index); // delegate to existing method
        }

        // insert multiple elements at the given index
        public void InsertAt(T[] values, int index) {
            if (index < 0 || index > count) throw new IndexOutOfRangeException();
            int shift = values.Length;
            GrowSize(Count + shift);

            // shift all upper elements tho the new end
            for (int i = count - 1; i >= index; i--) {
                this.values[i + shift] = this.values[i];
            }

            // fetch all given elements in between
            for (int i = 0; i < shift; i++) {
                this.values[index + i] = values[i];
            }

            count += values.Length;
        }

        
        // append element to the end
        public void Add(T value) {
            InsertAt(new T[] { value }, count); // delegate to existing method
        }

        // append multiple elements to the end
        public void Add(T[] values) {
            InsertAt(values, count); // delegate to existing method
        }

        // deletes the given number of elements at the given index
        public void Delete(int index, int count = 1) {
            if (index < 0 || index >= this.count) throw new IndexOutOfRangeException();
            if (count < 0 || count > this.count - index) throw new ArgumentException();

            // shift all upper elements downward
            for (int i = index; ; i++) {
                int j = i + count;
                if (j >= this.count) break;
                this.values[i] = this.values[j];
            }

            // fill unused range with default values
            for (int i = this.count - count; i < this.count; i++) {
                this.values[i] = default(T);
            }

            // set new count
            this.count -= count;
        }

        // grows the reserved buffer to required size.
        // for performance issues we at least double the buffer if growing.
        private void GrowSize(int minSize) {
            if (Size < minSize) {
                int bestSize = Math.Max(minSize, Size * 2);
                T[] temp = new T[bestSize];
                for (int i = 0; i < count; i++) temp[i] = values[i];
                values = temp;
            }
        }

        #region ***** foreach-loop support *****
        // creates a element iterator
        public IEnumerator GetEnumerator() {
            return new ListardEnumerator<T>(this);
        }

        // element iterator as nested class
        public class ListardEnumerator<T> : IEnumerator
        {
            private Listard<T> listard = null;
            private int index = -1;

            public ListardEnumerator(Listard<T> listard) {
                this.listard = listard;
                this.Reset();
            }

            public object Current {
                get {
                    if (index >= listard.count) throw new IndexOutOfRangeException();
                    return listard[index];
                }
            }

            public bool MoveNext() {
                if (index + 1 < listard.count) {
                    index++;
                    return true;
                } else {
                    return false;
                }
            }

            public void Reset() { index = -1; }
        }
        #endregion
    }
}
