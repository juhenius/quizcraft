using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuizCraft
{
  public class UniqueKeyGenerator
  {
    public static string GenerateKey<T>(T obj)
    {
      return GenerateMD5Hash(GetUniqueName(obj));
    }

    public static string GenerateKey<T>(List<T> objects)
    {
      var typeNames = objects.Select(GetUniqueName).Distinct().OrderBy(name => name);
      string concatenatedTypes = string.Join(",", typeNames);
      return GenerateMD5Hash(concatenatedTypes);
    }

    public static string CreateUniqueName<T>(T obj, params object[] values)
    {
      var builder = new StringBuilder();
      builder.Append(GetDefaultUniqueName(obj));
      builder.Append("(");
      builder.AppendJoin(",", values.Select(GetUniqueName));
      builder.Append(")");
      return builder.ToString();
    }

    public static string JaggedArrayToString<T>(T[][] value)
    {
      return $"[{string.Join(", ", value.Select(innerArray => $"[{string.Join(", ", innerArray)}]"))}]";
    }

    private static string GetUniqueName<T>(T obj)
    {
      if (obj is IUniqueKeyProvider provider)
      {
        return provider.GetUniqueName();
      }

      if (IsSimpleType(obj))
      {
        return $"{obj}";
      }

      return GetDefaultUniqueName(obj);
    }

    private static string GetDefaultUniqueName<T>(T obj)
    {
      return obj.GetType().FullName;
    }

    private static string GenerateMD5Hash(string input)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
          sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString().Substring(0, 8);
      }
    }

    private static bool IsSimpleType(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      Type type = obj.GetType();
      return type.IsPrimitive ||
             type == typeof(string) ||
             type == typeof(decimal) ||
             type == typeof(DateTime) ||
             type.IsEnum;
    }
  }

  public interface IUniqueKeyProvider
  {
    string GetUniqueName();
  }
}
