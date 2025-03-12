using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MauiBlazor.Shared.Helper;

public static class PasswordHasher
{
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;
    private static readonly KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
    private const string Delimiter = "$";

    public static string HashPassword(string password)
    {
        // ソルトを作成する
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        // パスワードをハッシュ化する
        byte[] hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            prf: Prf,
            iterationCount: Iterations,
            numBytesRequested: KeySize);

        // ソルトとハッシュをBase64文字列に変換
        string saltBase64 = Convert.ToBase64String(salt);
        string hashBase64 = Convert.ToBase64String(hash);

        // ソルトとハッシュを連結して保存する
        return $"{saltBase64}{Delimiter}{hashBase64}";
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        try
        {
            // 保存されたハッシュを分割
            string[] parts = storedHash.Split(Delimiter);
            if (parts.Length != 2)
            {
                return false;
            }

            string saltBase64 = parts[0];
            string hashBase64 = parts[1];

            // Base64文字列からバイト配列に変換する
            byte[] salt = Convert.FromBase64String(saltBase64);
            byte[] storedHashBytes = Convert.FromBase64String(hashBase64);


            // 入力されたパスワードを同じソルトでハッシュ化する
            byte[] enteredHash = KeyDerivation.Pbkdf2(
                enteredPassword,
                salt,
                prf: Prf,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            // 保存されたハッシュと入力されたパスワードのハッシュを比較する
            return CryptographicOperations.FixedTimeEquals(enteredHash, storedHashBytes);
        }
        catch (Exception)
        {
            // ハッシュの形式が不正な場合や、Base64デコードに失敗した場合など
            return false;
        }
    }
}