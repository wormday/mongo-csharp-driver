﻿/* Copyright 2010-2012 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver
{
    /// <summary>
    /// Represents a BSON document that can be used where an IMongoIndexKeys is expected.
    /// </summary>
    public class IndexKeysDocument : BsonDocument, IMongoIndexKeys
    {
        // constructors
        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class.
        /// </summary>
        public IndexKeysDocument()
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class specifying whether duplicate element names are allowed
        /// (allowing duplicate element names is not recommended).
        /// </summary>
        /// <param name="allowDuplicateNames">Whether duplicate element names are allowed.</param>
        public IndexKeysDocument(bool allowDuplicateNames)
            : base(allowDuplicateNames)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and adds one element.
        /// </summary>
        /// <param name="element">An element to add to the document.</param>
        public IndexKeysDocument(BsonElement element)
            : base(element)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and adds new elements from a dictionary of key/value pairs.
        /// </summary>
        /// <param name="dictionary">A dictionary to initialize the document from.</param>
        public IndexKeysDocument(Dictionary<string, object> dictionary)
            : base(dictionary)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and adds new elements from a dictionary of key/value pairs.
        /// </summary>
        /// <param name="dictionary">A dictionary to initialize the document from.</param>
        public IndexKeysDocument(IEnumerable<KeyValuePair<string, object>> dictionary)
            : base(dictionary)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and adds new elements from a dictionary of key/value pairs.
        /// </summary>
        /// <param name="dictionary">A dictionary to initialize the document from.</param>
        public IndexKeysDocument(IDictionary dictionary)
            : base(dictionary)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and adds new elements from a list of elements.
        /// </summary>
        /// <param name="elements">A list of elements to add to the document.</param>
        public IndexKeysDocument(IEnumerable<BsonElement> elements)
            : base(elements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IndexKeysDocument class and creates and adds a new element.
        /// </summary>
        /// <param name="name">The name of the element to add to the document.</param>
        /// <param name="value">The value of the element to add to the document.</param>
        public IndexKeysDocument(string name, BsonValue value)
            : base(name, value)
        {
        }
    }
}
