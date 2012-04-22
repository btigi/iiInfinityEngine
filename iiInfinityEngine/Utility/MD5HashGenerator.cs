using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

public class MD5HashGenerator
{
    private static readonly Object locker = new Object();

    internal static String GenerateKey(Object sourceObject)
    {
        String hashString = "";

        if (sourceObject == null)
        {
            throw new ArgumentNullException("sourceObject");
        }
        else
        {
            try
            {
                hashString = ComputeHash(ObjectToByteArray(sourceObject));
                return hashString;
            }
            catch (AmbiguousMatchException ame)
            {
                throw new ApplicationException("Object is not serializable. " + ame.Message);
            }
        }
    }

    private static byte[] ObjectToByteArray(Object objectToSerialize)
    {
        using (MemoryStream fs = new MemoryStream())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                lock (locker)
                {
                    formatter.Serialize(fs, objectToSerialize);
                }
                return fs.ToArray();
            }
            catch (SerializationException se)
            {
                Console.WriteLine("An error occured during serialization. " + se.Message);
                return null;
            }
        }
    }

    private static string ComputeHash(byte[] objectAsBytes)
    {
        using (MD5 md5 = new MD5CryptoServiceProvider())
        {
            try
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                // Build the final string by converting each byte into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Hash has not been generated.");
                return null;
            }
        }
    }
}