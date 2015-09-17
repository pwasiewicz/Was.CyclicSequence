namespace Was.CyclicSequence.Helpers.Enumerators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class CyclicSequenceSynchronizedEnumerator<T> : IEnumerator<T>
    {
        private readonly CyclicSequence<T> _cyclicSequence;

        public CyclicSequenceSynchronizedEnumerator(CyclicSequence<T> cyclicSequence)
        {
            _cyclicSequence = cyclicSequence;
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
            try
            {
                this.Current = this._cyclicSequence.Next();
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public void Reset()
        {
            throw new NotSupportedException("Synchronized sequence cannot be resetted.");
        }
    }
}
