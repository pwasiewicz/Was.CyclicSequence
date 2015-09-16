namespace Was.CyclicSequence.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class CyclicSequenceEnumerator<T> : IEnumerator<T>
    {
        private readonly CyclicSequenceItem<T> _head;
        private readonly IDictionary<CyclicSequenceItem<T>, int> _occurencesDict;

        private readonly Func<int> _versionFetcher;

        private readonly int _currentVersion;

        private CyclicSequenceItem<T> _current;

        public CyclicSequenceEnumerator(CyclicSequenceItem<T> head,
            IDictionary<CyclicSequenceItem<T>, int> occurencesDict, Func<int> versionFetcher)
        {
            if (versionFetcher == null) throw new ArgumentNullException("versionFetcher");
            if (occurencesDict == null) throw new ArgumentNullException("occurencesDict");

            _head = head;

            _occurencesDict = occurencesDict;
            this._versionFetcher = versionFetcher;
            this._current = null;

            this._currentVersion = versionFetcher();
        }

        public T Current { get; private set; }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (this._currentVersion != this._versionFetcher())
                throw new InvalidOperationException("Sequence has changed.");

            if (this._head == null) return false;

            if (this._current == null)
            {
                this._current = this._head;
            }
            else
            {
                if (this._current.Occurences == 0)
                {
                    this._current.Occurences = this._occurencesDict[this._current];
                    this._current = this._current.Previous;
                }

                if (this._current == null)
                {
                    this._current = this._head;
                }
            }
            
            this._current.Occurences--;
            this.Current = this._current.Item;

            return true;
        }

        public void Reset()
        {
            if (this._current == null) return;

            this._current.Occurences = this._occurencesDict[this._current];
            this._current = null;
        }
    }
}
