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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDB.Driver
{
    /// <summary>
    /// Represents the result of an isMaster command.
    /// </summary>
    [Serializable]
    public class IsMasterResult : CommandResult
    {
        // public properties
        /// <summary>
        /// Gets the arbiters.
        /// </summary>
        public IEnumerable<MongoServerAddress> Arbiters
        {
            get { return GetInstanceAddressesFromNamedResponseElement("arbiters"); }
        }

        /// <summary>
        /// Gets the hosts.
        /// </summary>
        public IEnumerable<MongoServerAddress> Hosts
        {
            get { return GetInstanceAddressesFromNamedResponseElement("hosts"); }
        }

        /// <summary>
        /// Gets whether the server is an arbiter.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the server is an arbiter; otherwise, <c>false.</c>.
        /// </value>
        public bool IsArbiterOnly
        {
            get { return Response.GetValue("arbiterOnly", false).ToBoolean(); }
        }

        /// <summary>
        /// Gets whether the server is the primary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the server is the primary; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimary
        {
            get { return Response.GetValue("ismaster", false).ToBoolean(); }
        }

        /// <summary>
        /// Gets whether the server is a passive member.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the server is a passive member; otherwise, <c>false</c>.
        /// </value>
        public bool IsPassive
        {
            //!ArbiterOnly is a workaround for CSHARP-273
            get { return Response.GetValue("passive", false).ToBoolean() && !IsArbiterOnly; }
        }

        /// <summary>
        /// Gets whether the server is secondary.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the server is secondary; otherwise, <c>false</c>.
        /// </value>
        public bool IsSecondary
        {
            get { return Response.GetValue("secondary", false).ToBoolean(); }
        }

        /// <summary>
        /// Gets the size of the max BSON object.
        /// </summary>
        /// <value>
        /// The size of the max BSON object.
        /// </value>
        public int MaxBsonObjectSize
        {
            get { return Response.GetValue("maxBsonObjectSize", MongoDefaults.MaxDocumentSize).ToInt32(); }
        }

        /// <summary>
        /// Gets the length of the max message.
        /// </summary>
        /// <value>
        /// The length of the max message.
        /// </value>
        public int MaxMessageLength
        {
            get { return Math.Max(MongoDefaults.MaxMessageLength, MaxBsonObjectSize + 1024); }
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message
        {
            get 
            {
                if (Response.Contains("msg"))
                {
                    return Response["msg"].AsString;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        public MongoServerAddress MyAddress
        {
            get
            {
                MongoServerAddress address;
                if (Response.Contains("me") && MongoServerAddress.TryParse(Response["me"].AsString, out address))
                {
                    return address;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the passives.
        /// </summary>
        public IEnumerable<MongoServerAddress> Passives
        {
            get { return GetInstanceAddressesFromNamedResponseElement("passives"); }
        }

        /// <summary>
        /// Gets the primary.
        /// </summary>
        public MongoServerAddress Primary
        {
            get
            {
                MongoServerAddress address;
                if(Response.Contains("primary") && MongoServerAddress.TryParse(Response["primary"].AsString, out address))
                {
                    return address;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the name of the replica set.
        /// </summary>
        /// <value>
        /// The name of the replica set.
        /// </value>
        public string ReplicaSetName
        {
            get 
            {
                if (Response.Contains("setName"))
                {
                    return Response["setName"].AsString;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the tags.
        /// </summary>
        public ReplicaSetTagSet Tags
        {
            get
            {
                var tagSet = new ReplicaSetTagSet();
                if (Response.Contains("tags"))
                {
                    var tags = Response["tags"].AsBsonDocument;
                    foreach (var tag in tags)
                    {
                        tagSet.Add(tag.Name, tag.Value.ToString());
                    }
                }

                return tagSet;
            }
        }

        // private methods
        private IEnumerable<MongoServerAddress> GetInstanceAddressesFromNamedResponseElement(string elementName)
        {
            if (!Response.Contains(elementName))
            {
                return Enumerable.Empty<MongoServerAddress>();
            }

            List<MongoServerAddress> instanceAddresses = new List<MongoServerAddress>();
            foreach (var hostName in Response[elementName].AsBsonArray)
            {
                var address = MongoServerAddress.Parse(hostName.AsString);
                instanceAddresses.Add(address);
            }

            return instanceAddresses;
        }
    }
}
