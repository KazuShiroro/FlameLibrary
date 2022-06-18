using System;
using UniRx;

namespace Flame.UniRx
{
    /// <summary>
    /// ラップされたAsyncSubject
    /// </summary>
    public class WrappedAsyncSubject : IObservable<Unit>, IDisposable
    {
        public readonly AsyncSubject<Unit> asyncSubject = new AsyncSubject<Unit>();
        private readonly object lockObject = new object();

        public void Done()
        {
            if (asyncSubject.IsCompleted) return;

            asyncSubject.OnNext(Unit.Default);
            asyncSubject.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<Unit> observer)
        {
            lock (lockObject)
            {
                return asyncSubject.Subscribe(observer);
            }
        }

        public void Dispose()
        {
            lock (lockObject)
            {
                asyncSubject.Dispose();
            }
        }
    }
}