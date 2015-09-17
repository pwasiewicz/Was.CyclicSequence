namespace Was.CyclicSequence.Helpers.Enumerators
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class CyclicSequenceSynchronizedEnumerator<T> : IEnumerator<T>
    {
        public T Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
