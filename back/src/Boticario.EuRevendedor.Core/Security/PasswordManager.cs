using Flunt.Notifications;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Boticario.EuRevendedor.Core.Security
{
    public sealed class PasswordManager : Notifiable, IEquatable<PasswordManager>, IEquatable<string>
    {
        public string Encoded { get; }

        public PasswordManager(string password) : base()
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                AddNotification("Senha", "A senha não pode ficar vazia!");
                return;
            }

            Encoded = EncodePassword(password);
        }

        public static implicit operator PasswordManager(string password) => new PasswordManager(password);

        public static implicit operator string(PasswordManager value) => value.Encoded;

        private static string EncodePassword(string password)
        {
            string result;
            var bytes = Encoding.Unicode.GetBytes(password);

            using (var stream = new MemoryStream())
            {
                stream.WriteByte(0);

                using (var sha256 = new SHA256Managed())
                {
                    var hash = sha256.ComputeHash(bytes);
                    stream.Write(hash, 0, hash.Length);

                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes);
                }

            }
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PasswordManager;

            return other != null ? Equals(other) : Equals(obj as string);
        }

        public bool Equals(PasswordManager other) => other != null && Encoded == other.Encoded;

        public bool Equals(string other) => Encoded == other;

        public static bool operator == (PasswordManager a, PasswordManager b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((a is null) || (b is null)) return false;

            return a.Encoded == b.Encoded;
        }

        public static bool operator !=(PasswordManager a, PasswordManager b) => !(a == b);

        public override int GetHashCode() => Encoded.GetHashCode();

        public override string ToString() => Encoded;
    }
}
