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
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Xml.Messages
{
    public class ShoppingCartContentDataContract:IDataContract<ShoppingCart>
    {
        public ShoppingCartContentDataContract()
        {
        }

        public ShoppingCartContentDataContract( ShoppingCart dataObject )
        {
            this.Id = TypeConverter.ShoppingCartId.ConvertFrom( dataObject.Id );
            this.SalesPointId = TypeConverter.SalesPointId.ConvertFrom( dataObject.SalesPointId );
            this.ViewPointId = TypeConverter.ViewPointId.ConvertFrom( dataObject.ViewPointId );
            this.SalesPersonId = TypeConverter.SalesPersonId.ConvertFrom( dataObject.SalesPersonId );
            this.CustomerId = TypeConverter.CustomerId.ConvertFrom( dataObject.CustomerId );
            this.Status = TypeConverter.ShoppingCartStatus.ConvertFrom( dataObject.Status );
        }

        [XmlAttribute]
        public String Id{ get; set; } = String.Empty;

        [XmlAttribute]
        public String SalesPointId{ get; set; } = String.Empty;

        [XmlAttribute]
        public String ViewPointId{ get; set; } = String.Empty;

        [XmlAttribute]
        public String SalesPersonId{ get; set; } = String.Empty;

        [XmlAttribute]
        public String CustomerId{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Status{ get; set; } = String.Empty;

        [XmlElement( ElementName = "Item" )]
        public ShoppingCartItemDataContract[]? Items{ get; set; }
        
        public ShoppingCart GetDataObject()
        {
            return new ShoppingCart(    TypeConverter.ShoppingCartId.ConvertTo( this.Id ),
                                        TypeConverter.SalesPointId.ConvertTo( this.SalesPointId ),
                                        TypeConverter.ViewPointId.ConvertTo( this.ViewPointId ),
                                        TypeConverter.SalesPersonId.ConvertTo( this.SalesPersonId ),
                                        TypeConverter.CustomerId.ConvertTo( this.CustomerId ),
                                        TypeConverter.ShoppingCartStatus.ConvertTo( this.Status )   );
        }
    }
}