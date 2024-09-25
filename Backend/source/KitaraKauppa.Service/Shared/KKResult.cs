using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaraKauppa.Service.Shared
{
    [Serializable]
    public sealed class KKResult<T>
    {
        public string? Message { get; private set; }
        public T? Value { get; private set; }
        public bool Succeeded { get; private set; } = false;

        public KKResult<T> Fail(string? message)
        {
            Message = message;
            return this;
        }

        public KKResult<T> SucceededWithValue(T value)
        {
            ArgumentNullException.ThrowIfNull(value);

            Message = string.Empty;

            Value = value;
            Succeeded = true;

            return this;
        }
    }
}
