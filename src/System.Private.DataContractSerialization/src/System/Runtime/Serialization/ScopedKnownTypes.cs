﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//------------------------------------------------------------
//------------------------------------------------------------

namespace System.Runtime.Serialization
{
    using System;
    using System.Xml;
    using DataContractDictionary = System.Collections.Generic.Dictionary<System.Xml.XmlQualifiedName, DataContract>;

    internal struct ScopedKnownTypes
    {
        internal DataContractDictionary[] dataContractDictionaries;
        private int _count;
        internal void Push(DataContractDictionary dataContractDictionary)
        {
            if (dataContractDictionaries == null)
                dataContractDictionaries = new DataContractDictionary[4];
            else if (_count == dataContractDictionaries.Length)
                Array.Resize<DataContractDictionary>(ref dataContractDictionaries, dataContractDictionaries.Length * 2);
            dataContractDictionaries[_count++] = dataContractDictionary;
        }

        internal void Pop()
        {
            _count--;
        }

        internal DataContract GetDataContract(XmlQualifiedName qname)
        {
            for (int i = (_count - 1); i >= 0; i--)
            {
                DataContractDictionary dataContractDictionary = dataContractDictionaries[i];
                DataContract dataContract;
                if (dataContractDictionary.TryGetValue(qname, out dataContract))
                    return dataContract;
            }
            return null;
        }
    }
}
