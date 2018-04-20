/*
 * fn@gso-koeln.de 2018
 */
using System;
using System.Reflection
;

namespace ListardDemo
{
    public class TestListardInt : UnitTest
    {
        public void Run() {
            Console.WriteLine("Starte Tests");
            try {
                // Alles Tests durchführen bis zum ersten Fehler
                //TestAdd();
                //TestInsertAt();

                // Keine Fehler
                Console.WriteLine("Tests erfolgreich");
            } catch (Exception exc) {
                // Ersten Fehler anzeigen
                Console.WriteLine("Fehler: " + exc.Message);
            }
        }

        public void TestNew() {
            // test a new Listard<int> with default count
            Listard<int> l = new Listard<int>();
            if (l.Count != 0) throw new Exception();
            if (l.Size != 0) throw new Exception();
            try {
                int test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                int test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestNewExt() {
            // test a new Listard<int> with initial count
            int[] list = { 1, 2, 3, 10, 100, 100 };
            foreach (int n in list) {
                Listard<int> l = new Listard<int>(n);
                if (l.Count != n) throw new Exception();
                if (l.Size != n) throw new Exception();
                for (int i = 0; i < n; i++) {
                    if (l[i] != 0) throw new Exception();
                }
                try {
                    int test = l[-1];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
                try {
                    int test = l[l.Count];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
            }
        }

        public void TestAdd() {
            Listard<int> l = new Listard<int>();
            l.Add(4711);
            if (l.Count != 1) throw new Exception();
            if (l.Size != 1) throw new Exception();
            if (l[0] != 4711) throw new Exception();
            try {
                int test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                int test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestAddBlock() {
            Listard<int> l = new Listard<int>();
            int[] block = new int[] { 4711, 4712, 4713 };
            l.Add(block);
            if (l.Count != block.Length) throw new Exception();
            for (int i = 0; i < block.Length; i++) {
                if (l[i] != block[i]) throw new Exception();
            }
            try {
                int test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                int test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestAddExt() {
            Listard<int> l = new Listard<int>();
            int cnt = 0;
            for (int v = 4711; v < 4722; v++) {
                cnt++;
                l.Add(v);
                if (l.Count != cnt) throw new Exception();
                if (l[l.Count - 1] != v) throw new Exception();
                try {
                    int test = l[-1];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
                try {
                    int test = l[l.Count];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
            }
        }

        public void TestGetSet() {
            Listard<int> l = new Listard<int>();
            int[] block = new int[] { 4711, 4712, 4713 };
            l.Add(block);
            l[0] = 10 * l[0];
            l.Set(10 * l.Get(1), 1);
            if (l[0] != 47110) throw new Exception();
            if (l[1] != 47120) throw new Exception();
            if (l.Get(2) != 4713) throw new Exception();
            try {
                int test = l.Get(-1);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                int test = l.Get(l.Count);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestDelete() {
            Listard<int> l = new Listard<int>();
            int[] block = new int[] { 4710, 4711, 4712, 4713, 4714, 4715 };
            l.Add(block);
            l.Delete(0, 1);
            if (l.Count != 5) throw new Exception();
            if (l[0] != 4711) throw new Exception();
            if (l[1] != 4712) throw new Exception();
            if (l[2] != 4713) throw new Exception();
            if (l[3] != 4714) throw new Exception();
            if (l[4] != 4715) throw new Exception();
            l.Delete(3, 2);
            if (l.Count != 3) throw new Exception();
            if (l[0] != 4711) throw new Exception();
            if (l[1] != 4712) throw new Exception();
            if (l[2] != 4713) throw new Exception();
            l.Delete(1, 0);
            if (l.Count != 3) throw new Exception();
            if (l[0] != 4711) throw new Exception();
            if (l[1] != 4712) throw new Exception();
            if (l[2] != 4713) throw new Exception();
            l.Delete(1, 1);
            if (l.Count != 2) throw new Exception();
            if (l[0] != 4711) throw new Exception();
            if (l[1] != 4713) throw new Exception();
            l.Delete(0);
            if (l.Count != 1) throw new Exception();
            if (l[0] != 4713) throw new Exception();

            try {
                l.Delete(-1);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                l.Delete(1);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                l.Delete(0, -1);
                throw new Exception();
            } catch (ArgumentException) { /* expected error */ }
            try {
                l.Delete(0, 2);
                throw new Exception();
            } catch (ArgumentException) { /* expected error */ }

            l.Delete(0, 1);
            if (l.Count != 0) throw new Exception();
        }

        public void TestInsert() {
            var l = new Listard<int>();
            l.Add(new int[] { 10, 20, 30 });
            l.InsertAt(new int[] { 300, 301 }, 3);
            l.InsertAt(new int[] { 200, 201 }, 2);
            l.InsertAt(new int[] { 100, 101 }, 1);
            l.InsertAt(new int[] { 000, 001 }, 0);

            int[] block = { 0, 1, 10, 100, 101, 20, 200, 201, 30, 300, 301 };
            for (int i = 0; i < block.Length; i++) {
                if (l[i] != block[i]) throw new Exception();
            }
        }

        public void TestBig() {
            int huge = 1000000;
            Listard<int> l = new Listard<int>(huge);

            l[0] = 4711;
            if (l[0] != 4711) throw new Exception();
            if (l[1] != 0) throw new Exception();

            l[l.Count - 1] = 4712;
            if (l[huge - 1] != 4712) throw new Exception();
            if (l[huge - 2] != 0) throw new Exception();
        }

        public void TestForEach() {
            // test iteration upon empty list
            Listard<int> l = new Listard<int>();
            foreach (int v in l) throw new Exception();

            // test iteration upon filled list
            int[] block = new int[] { 4711, 4712, 4713 };
            l.Add(block);
            if (l.Count != block.Length) throw new Exception();
            int i = 0;
            foreach (int v in l) {
                if (v != block[i++]) throw new Exception();
            }
        }
    }
}
