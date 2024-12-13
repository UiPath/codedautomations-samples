using System;
using System.Collections.Generic;
using UiPath.CodedWorkflows;
using System.Security.Cryptography;

namespace GenerateTwoFactorAuthenticationCode
{
    public class Workflow : CodedWorkflow
    {
        [Workflow]
        public string Execute(string secretKey)
        {
            return AuthenticationCode(secretKey);
        }

        private string AuthenticationCode(string secret)
        {
            // Step 1: Decode the base32 secret key
            byte[] key = Base32Decode(secret);

            // Step 2: Calculate the time step (current time divided by 30 seconds)
            long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long timeStep = unixTime / 30;

            // Step 3: Convert time step to byte array (8 bytes, big-endian)
            byte[] timeBytes = BitConverter.GetBytes(timeStep);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(timeBytes);

            // Step 4: Generate HMAC-SHA1 hash using the time step as message and secret key
            using (HMACSHA1 hmac = new HMACSHA1(key))
            {
                byte[] hash = hmac.ComputeHash(timeBytes);

                // Step 5: Extract dynamic binary code (4 bytes) from the hash
                int offset = hash[hash.Length - 1] & 0x0F;
                int binaryCode = (hash[offset] & 0x7F) << 24
                               | (hash[offset + 1] & 0xFF) << 16
                               | (hash[offset + 2] & 0xFF) << 8
                               | (hash[offset + 3] & 0xFF);

                // Step 6: Modulo to get a 6-digit code
                int otp = binaryCode % 1_000_000;

                // Return the OTP as a zero-padded 6-digit string
                return otp.ToString("D6");
            }
        }

        // Base32 decoding function to get the byte array from the base32-encoded key
        private static byte[] Base32Decode(string base32)
        {
            // Decode Base32-encoded string to byte array
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            int bitBuffer = 0;
            int bitBufferLen = 0;
            var result = new List<byte>();

            foreach (char c in base32.ToUpper())
            {
                if (c == '=') break;

                int index = base32Chars.IndexOf(c);
                if (index < 0) throw new ArgumentException("Invalid Base32 character");

                bitBuffer = (bitBuffer << 5) | index;
                bitBufferLen += 5;

                if (bitBufferLen >= 8)
                {
                    result.Add((byte)(bitBuffer >> (bitBufferLen - 8)));
                    bitBufferLen -= 8;
                }
            }

            return result.ToArray();
        }
    }
}