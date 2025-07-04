using System.Security.Cryptography;
using System.Text;

namespace Utilities.Constants;

public class EncryptionHelpers
{
    //Define the triple des provider:
    private TripleDESCryptoServiceProvider m_des = new TripleDESCryptoServiceProvider();

    //Define the string handler:
    private UTF8Encoding m_utf8 = new UTF8Encoding();

    //Define the local property arrays:
    private byte[] m_key;

    private byte[] m_iv;

    //Define the local key and vector byte arrays:        
    //Private readonly byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
    private readonly byte[] key =
        { 4, 2, 3, 4, 1, 3, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 0, 5, 20, 21, 22, 23, 24 };

    private readonly byte[] iv = { 8, 7, 6, 5, 4, 3, 2, 1 };

    public EncryptionHelpers()
    {
        m_key = key;
        m_iv = iv;
    }

    public byte[] Encrypt(byte[] input)
    {
        return Transform(input, m_des.CreateEncryptor(m_key, m_iv));
    }

    public byte[] Decrypt(byte[] input)
    {
        return Transform(input, m_des.CreateDecryptor(m_key, m_iv));
    }

    public string Encrypt(string text)
    {
        byte[] input = m_utf8.GetBytes(text);
        byte[] output = Transform(input, m_des.CreateEncryptor(m_key, m_iv));

        return Convert.ToBase64String(output);
    }

    public string Decrypt(string text)
    {
        byte[] input = Convert.FromBase64String(text);
        byte[] output = Transform(input, m_des.CreateDecryptor(m_key, m_iv));

        return m_utf8.GetString(output);
    }

    private byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
    {
        //Create the necessary streams
        MemoryStream memStream = new MemoryStream();
        CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);

        //Transform the bytes as requested
        cryptStream.Write(input, 0, input.Length);
        cryptStream.FlushFinalBlock();

        //Read the memory stream and convert it back into byte array
        memStream.Position = 0;
        byte[] result = new byte[Convert.ToInt32(memStream.Length - 1) + 1];

        memStream.Read(result, 0, Convert.ToInt32(result.Length));

        //Close and release the streams
        memStream.Close();
        cryptStream.Close();

        //Hand back the encrypted buffer
        return result;
    }
}