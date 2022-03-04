using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using Revendor.Domain.Common;
using Revendor.Domain.ValueObjects;

namespace Revendor.Application.Authentication
{
    public interface ITokenIssuer
    {
        IDictionary<string, object> ExtractClaims(string encryptedToken);
        string IssueTokenForUser(Credentials credentials, string tenant);
    }

    public class TokenIssuer : ITokenIssuer
    {
        private readonly IJwtEncoder jwtEncoder;

        public TokenIssuer()
        {
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var base64Encoder = new JwtBase64UrlEncoder();
            jwtEncoder = new JwtEncoder(algorithm, serializer, base64Encoder);
        }

        public IDictionary<string, object> ExtractClaims(string encryptedToken)
        {
            return new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(Constants.SecretKey)
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(Decrypt(encryptedToken));
        }

        public string IssueTokenForUser(Credentials credentials, string tenant)
        {
            var claims = new Dictionary<string, object>
            {
                { "username", credentials.User },
                { "role", credentials.Role },
                { "tenant", tenant },
                //{"exp",  }
            };

            var token = Encrypt(jwtEncoder.Encode(claims, Constants.SecretKey));

            return token;
        }
        

        private static string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(Constants.EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(clearBytes, 0, clearBytes.Length);
                cs.Close();
            }

            clearText = Convert.ToBase64String(ms.ToArray());

            return clearText;
        }

        private static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(cipherText);
            using var encryptor = Aes.Create();
            var pdb = new Rfc2898DeriveBytes(Constants.EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipherBytes, 0, cipherBytes.Length);
                cs.Close();
            }

            cipherText = Encoding.Unicode.GetString(ms.ToArray());

            return cipherText;
        }
    }

    public class Credentials
    {
        public Credentials(string user, Role role)
        {
            User = user;
            Role = role.ToString();
        }

        public string User { get; set; }

        public string Role { get; set; }
    }

    public class AccessResponse
    {
        public AccessResponse(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; }
    }
}