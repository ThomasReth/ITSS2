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
using System.Text.Json.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockLocationInfo
{
    public class StockLocationInfoResponseDataContract:SubscribedResponseDataContract<StockLocationInfoResponse>
    {
        public StockLocationInfoResponseDataContract()
        {
            this.StockLocations = new StockLocationDataContract[]{};
        }

        public StockLocationInfoResponseDataContract( StockLocationInfoResponse dataObject )
        :
            base( dataObject )
        {
            this.StockLocations = TypeConverter.ConvertFromDataObjects<StockLocation, StockLocationDataContract>( dataObject.GetStockLocations() );
        }

        [JsonPropertyName( nameof( StockLocation ) )]
        public StockLocationDataContract[]? StockLocations{ get; set; }

        public override StockLocationInfoResponse GetDataObject()
        {
            return new StockLocationInfoResponse(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                    TypeConverter.ConvertToDataObjects<StockLocation, StockLocationDataContract>( this.StockLocations )   );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( StockLocationInfoResponseEnvelopeDataContract );
        }
    }
}
