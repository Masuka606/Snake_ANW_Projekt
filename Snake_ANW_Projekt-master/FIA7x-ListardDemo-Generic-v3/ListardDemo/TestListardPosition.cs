/*
 * fn@gso-koeln.de 2018
 */
using System;
using System.Reflection
;

namespace ListardDemo
{
    public class TestListardPosition : UnitTest
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
            // test a new Listard<Position> with default count
            Listard<Position> l = new Listard<Position>();
            if (l.Count != 0) throw new Exception();
            if (l.Size != 0) throw new Exception();
            try {
                Position test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                Position test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestNewExt() {
            // test a new Listard<Position> with initial count
            int[] list = { 1, 2, 3, 10, 100, 100 };
            foreach (int n in list) {
                Listard<Position> l = new Listard<Position>(n);
                if (l.Count != n) throw new Exception();
                if (l.Size != n) throw new Exception();
                for (int i = 0; i < n; i++) {
                    if (l[i] != null) throw new Exception();
                }
                try {
                    Position test = l[-1];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
                try {
                    Position test = l[l.Count];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
            }
        }

        public void TestAdd() {
            Listard<Position> l = new Listard<Position>();
            l.Add(new Position("47|11"));
            if (l.Count != 1) throw new Exception();
            if (l.Size != 1) throw new Exception();
            if (l[0].Text != "47|11") throw new Exception();
            try {
                Position test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                Position test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestAddBlock() {
            Listard<Position> l = new Listard<Position>();
            Position[] block = new Position[] { new Position("47|11"), new Position("47|12"), new Position("48|13") };
            l.Add(block);
            if (l.Count != block.Length) throw new Exception();
            for (int i = 0; i < block.Length; i++) {
                if (l[i] != block[i]) throw new Exception();
            }
            try {
                Position test = l[-1];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                Position test = l[l.Count];
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestAddExt() {
            Listard<Position> l = new Listard<Position>();
            int cnt = 0;
            for (int i = 0; i < 100; i++) {
                Position v = new Position(i * 2, 1 * 3);
                cnt++;
                l.Add(v);
                if (l.Count != cnt) throw new Exception();
                if (l[l.Count - 1] != v) throw new Exception();
                try {
                    Position test = l[-1];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
                try {
                    Position test = l[l.Count];
                    throw new Exception();
                } catch (IndexOutOfRangeException) { /* expected error */ }
            }
        }

        public void TestGetSet() {
            Listard<Position> l = new Listard<Position>();
            Position[] block = new Position[] { new Position("47|11"), new Position("47|12"), new Position("48|13") };
            l.Add(block);
            l[0].X = 10 * l[0].X;
            l.Set(new Position(l.Get(1).X, l.Get(1).Y * 10), 1);
            if (l[0].Text != "470|11") throw new Exception();
            if (l[1].Text != "47|120") throw new Exception();
            if (l.Get(2).Text != "48|13") throw new Exception();
            try {
                Position test = l.Get(-1);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
            try {
                Position test = l.Get(l.Count);
                throw new Exception();
            } catch (IndexOutOfRangeException) { /* expected error */ }
        }

        public void TestDelete() {
            Listard<Position> l = new Listard<Position>();
            Position[] block = new Position[] { 
                new Position("47|11"), new Position("47|12"), 
                new Position("48|13"), new Position("48|14"), new Position("48|15") };
            l.Add(block);
            l.Delete(3, 2);
            if (l.Count != 3) throw new Exception();
            if (l[0].Text != "47|11") throw new Exception();
            if (l[1].Text != "47|12") throw new Exception();
            if (l[2].Text != "48|13") throw new Exception();
            l.Delete(1, 0);
            if (l.Count != 3) throw new Exception();
            if (l[0].Text != "47|11") throw new Exception();
            if (l[1].Text != "47|12") throw new Exception();
            if (l[2].Text != "48|13") throw new Exception();
            l.Delete(1, 1);
            if (l.Count != 2) throw new Exception();
            if (l[0].Text != "47|11") throw new Exception();
            if (l[1].Text != "48|13") throw new Exception();
            l.Delete(0);
            if (l.Count != 1) throw new Exception();
            if (l[0].Text != "48|13") throw new Exception();

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
            var l = new Listard<Position>();
            Position[] block = new Position[] { new Position("47|11"), new Position("47|12"), new Position("48|13") };
            l.Add(block);
            l.InsertAt(new Position[] { new Position("57|14"), new Position("57|15") }, 3);
            l.InsertAt(new Position[] { new Position("57|16"), new Position("57|17") }, 2);
            l.InsertAt(new Position[] { new Position("57|18"), new Position("57|19") }, 1);
            l.InsertAt(new Position[] { new Position("57|20"), new Position("57|21") }, 0);

            string[] check = { 
                "57|20", "57|21", "47|11", 
                "57|18", "57|19", "47|12", 
                "57|16", "57|17", "48|13", 
                "57|14", "57|15" };
            for (int i = 0; i < check.Length; i++) {
                if (l[i].Text != check[i]) throw new Exception();
            }
        }

        public void TestBig() {
            int huge = 1000000;
            Listard<Position> l = new Listard<Position>(huge);

            l[0] = new Position("47|11");
            if (l[0].Text != "47|11") throw new Exception();
            if (l[1] != null) throw new Exception();

            l[l.Count - 1] = new Position("47|12");
            if (l[huge - 1].Text != "47|12") throw new Exception();
            if (l[huge - 2] != null) throw new Exception();
        }

        public void TestForEach() {
            // test iteration upon empty list
            Listard<Position> l = new Listard<Position>();
            foreach (Position v in l) throw new Exception();

            // test iteration upon filled list
            Position[] block = new Position[] { new Position("47|11"), new Position("47|12"), new Position("48|13") };
            l.Add(block);
            if (l.Count != block.Length) throw new Exception();
            int i = 0;
            foreach (Position v in l) {
                // equal by reference?
                if (v != block[i]) throw new Exception();
                // equal by text representation?
                if (v.Text != block[i++].Text) throw new Exception();
            }
        }
    }
}
