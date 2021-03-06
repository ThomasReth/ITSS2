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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockDeliveryInfo
{
    public class StockDeliveryInfoResponseDataContract:SubscribedResponseDataContract<StockDeliveryInfoResponse>
    {
        public StockDeliveryInfoResponseDataContract()
        {
            this.Task = new StockDeliveryInfoResponseTaskDataContract();
        }

        public StockDeliveryInfoResponseDataContract( StockDeliveryInfoResponse dataObject )
        :
            base( dataObject )
        {
            this.Task = TypeConverter.ConvertFromDataObject<StockDeliveryInfoResponseTask, StockDeliveryInfoResponseTaskDataContract>( dataObject.Task );
        }

        public StockDeliveryInfoResponseTaskDataContract Task{ get; set; }

        public override StockDeliveryInfoResponse GetDataObject()
        {
            return new StockDeliveryInfoResponse(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                    TypeConverter.ConvertToDataObject<StockDeliveryInfoResponseTask, StockDeliveryInfoResponseTaskDataContract>( this.Task )    );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( StockDeliveryInfoResponseEnvelopeDataContract );
        }
    }
}
