using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrokerClient
{
    internal class NetworkProxy : DispatchProxy
    {
        private void WriteString(string str, byte[] arr, ref int index,bool includeTerminator = false)
        {
            foreach (var chr in str)
            {
                arr[index++] = (byte)chr;
            }

            if (includeTerminator)
                arr[index++] = 0;
        }


        private void WriteNumber(int number, byte[] arr, ref int index)
        {
            var bytes = BitConverter.GetBytes(number);

            foreach (var _byte in bytes)
            {
                arr[index++] = _byte;
            }

            for (int i = 0; i < 4-bytes.Length; i++)
            {
                arr[index++] = 0;
            }
        }

        private void WriteNumber(float number, byte[] arr, ref int index)
        {
            var bytes = BitConverter.GetBytes(number);

            foreach (var _byte in bytes)
            {
                arr[index++] = _byte;
            }

            for (int i = 0; i < 4 - bytes.Length; i++)
            {
                arr[index++] = 0;
            }
        }


        private byte[] CreatePacket(MethodInfo methodInfo, object?[]? args)
        {
            var index = 0;
            var bytes = new byte[1024];

            WriteString(methodInfo.DeclaringType.Name,bytes,ref index);
            WriteString("/", bytes, ref index);
            WriteString(methodInfo.Name, bytes, ref index);
            WriteString("\n",bytes,ref index);

            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg.GetType() == typeof(string))
                        WriteString(arg as string,bytes,ref index,true);
                    else if (arg.GetType() == typeof(int)) 
                        WriteNumber((int)arg,bytes,ref index);
                    else if (arg.GetType() == typeof(float))
                        WriteNumber((float)arg, bytes, ref index);
                }
            }


            bytes[index] = 3;




            return bytes;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            var response = NetworkHandler.SendAndReceive(CreatePacket(targetMethod, args));

            if (targetMethod.ReturnType == typeof(string))
                return Encoding.ASCII.GetString(response);
            if ((targetMethod.ReturnType == typeof(int)))
                return BitConverter.ToInt32(response);
            if (targetMethod.ReturnType == typeof(float))
                return BitConverter.ToSingle(response);

            return null;
        }
    }
}
