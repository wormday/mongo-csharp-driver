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
using System.Reflection;
using System.Text;

namespace MongoDB.Bson.Serialization.Conventions
{
    /// <summary>
    /// A convention that finds the id member by name.
    /// </summary>
    public class NamedIdMemberConvention : ConventionBase, IClassMapConvention
    {
        private readonly IEnumerable<string> _names;
        private readonly MemberTypes _memberTypes;
        private readonly BindingFlags _bindingFlags;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIdMemberConvention" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        public NamedIdMemberConvention(IEnumerable<string> names)
            : this(names, BindingFlags.Instance | BindingFlags.Public)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIdMemberConvention" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="memberTypes">The member types.</param>
        public NamedIdMemberConvention(IEnumerable<string> names, MemberTypes memberTypes)
            : this(names, memberTypes, BindingFlags.Instance | BindingFlags.Public)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIdMemberConvention" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        public NamedIdMemberConvention(IEnumerable<string> names, BindingFlags bindingFlags)
            : this(names, MemberTypes.Field | MemberTypes.Property, bindingFlags)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedIdMemberConvention" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="memberTypes">The member types.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public NamedIdMemberConvention(IEnumerable<string> names, MemberTypes memberTypes, BindingFlags bindingFlags)
        {
            if (names == null)
            {
                throw new ArgumentNullException("names");
            }

            _names = names;
            _memberTypes = memberTypes;
            _bindingFlags = bindingFlags | BindingFlags.DeclaredOnly;
        }

        /// <summary>
        /// Applies a modification to the class map.
        /// </summary>
        /// <param name="classMap">The class map.</param>
        public void Apply(BsonClassMap classMap)
        {
            foreach (var name in _names)
            {
                var member = classMap.ClassType.GetMember(name, _memberTypes, _bindingFlags).SingleOrDefault();

                if (member != null)
                {
                    classMap.MapIdMember(member);
                    return;
                }
            }
        }
    }
}