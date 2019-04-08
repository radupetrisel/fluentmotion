using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace FluentMotion.helpers
{
    public static class DisposableHelpers
    {
        public static T DisposedBy<T>(this T disposable, CompositeDisposable compositeDisposable) where T : IDisposable
        {
            compositeDisposable.Add(disposable);
            return disposable;
        }

    }
}
