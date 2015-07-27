/// Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


namespace PipelineBuilderPkg
{
    [Serializable]
    internal class CheckSumValidator
    {
        Dictionary<String, byte[]> _files;
        string _path;
        private CheckSumValidator(String path)
        {
            _path = path;
            _files = new Dictionary<string, byte[]>();
            string[] fileList = Directory.GetFiles(_path, "*.cs");
            foreach (String file in fileList)
            {
                byte[] hash = System.Security.Cryptography.SHA256.Create().ComputeHash(File.ReadAllBytes(file));
                 _files.Add(file, hash);
            }
        }

        private void WriteResults()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path+"\\checksum",FileMode.Create);
            formatter.Serialize(stream,this);
            stream.Close();
        }

        public static bool ValidateCheckSum(string path)
        {
            CheckSumValidator current = new CheckSumValidator(path);
            CheckSumValidator original = GetInfo(path);
            if (original == null)
            {
                //There is no pre-computed check-sum. 
                return true;
            }
            current.CheckForEdits(original);
            return true;
        }

        public static void StoreCheckSum(string path)
        {
            new CheckSumValidator(path).WriteResults();
        }

        private void CheckForEdits(CheckSumValidator original)
        {
            foreach (string s in original._files.Keys)
            {
                byte[] currentHash;
                if (_files.TryGetValue(s, out currentHash))
                {
                    byte[] originalHash = original._files[s];
                    if (currentHash.Length != originalHash.Length)
                    {
                        throw new InvalidOperationException("File has been edited and left in the ''Generated Files'' folder. Please move it from this folder to prevent these changes from being lost. File: " + s);
                    }
                    for (int i = 0; i < originalHash.Length; i++)
                    {
                        if (!originalHash[i].Equals(currentHash[i]))
                        {
                            throw new InvalidOperationException("File has been edited and left in the ''Generated Files'' folder. Please move it from this folder to prevent these changes from being lost. File: " + s);
                        }
                    }
                }
            }
            foreach (string s in _files.Keys)
            {
                if (!original._files.ContainsKey(s))
                {
                    throw new InvalidOperationException("File has been added to the ''Generated Files'' folder manually. Please move it from this folder to prevent this file from being lost. File: " + s);
                }
            }
        }


        private static CheckSumValidator GetInfo(string path)
        {
            if (!File.Exists(path+"\\checksum"))
            {
                return null;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + "\\checksum", FileMode.Open);
            CheckSumValidator result = (CheckSumValidator)formatter.Deserialize(stream);
            stream.Close();
            return result;

        }
    }
}