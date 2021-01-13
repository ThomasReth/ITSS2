// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Reflection;

using Reth.Itss2.Dialogs.Standard.Diagnostics;
using Reth.Itss2.Dialogs.Standard.Protocol;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    public class DataContractResolver:IDataContractResolver
    {
        public DataContractResolver( Type dialogProvider )
        {
            this.DataContracts = this.ResolveContracts( dialogProvider );
        }

        public DataContractResolver( IDialogProvider dialogProvider )
        {
            this.DataContracts = this.ResolveContracts( dialogProvider );
        }

        private IReadOnlyDictionary<String, Type> DataContracts
        {
            get;
        }

        private List<Assembly> GetAssemblies( Type type )
        {
            List<Assembly> result = new List<Assembly>();

            if( type.IsInterface == false &&
                type.Equals( typeof( Object ) ) == false  )
            {
                result.AddRange( this.GetAssemblies( type.BaseType ) );
                result.Add( type.Assembly );
            }

            return result;
        }

        private IReadOnlyDictionary<String, Type> ResolveContracts( IDialogProvider dialogProvider )
        {
            return this.ResolveContracts( dialogProvider.GetType() );
        }

        private IReadOnlyDictionary<String, Type> ResolveContracts( Type dialogProvider )
        {
            Dictionary<String, Type> result = new Dictionary<String, Type>();

            List<Assembly> assemblies = this.GetAssemblies( dialogProvider );

            foreach( Assembly assembly in assemblies )
            {
                Type[] types = assembly.GetTypes();

                foreach( Type type in types )
                {
                    IEnumerable<DataContractMappingAttribute> attributes = type.GetCustomAttributes<DataContractMappingAttribute>();

                    foreach( DataContractMappingAttribute attribute in attributes )
                    {
                        String name = attribute.TypeMapping.Name;

                        if( result.ContainsKey( name ) == true )
                        {
                            result[ name ] = type;
                        }else
                        {
                            result.Add( name, type );
                        }
                    }
                }
            }

            return result;
        }

        public Type ResolveContract( String messageName )
        {
            if( this.DataContracts.TryGetValue( messageName, out Type result ) )
            {
                return result;
            }else
            {
                throw Assert.Exception( new MessageNotSupportedException( $"No data contract found for message '{ messageName }'." ) );
            }
        }
    }
}