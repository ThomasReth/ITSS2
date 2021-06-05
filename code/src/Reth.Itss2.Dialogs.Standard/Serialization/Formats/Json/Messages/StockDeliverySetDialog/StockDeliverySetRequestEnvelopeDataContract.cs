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

using System.Text.Json.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockDeliverySet
{
    [JsonDataContractMapping( typeof( StockDeliverySetRequest ), typeof( StockDeliverySetRequestDataContract ) )]
    public class StockDeliverySetRequestEnvelopeDataContract:MessageEnvelopeDataContract
    {
        public StockDeliverySetRequestEnvelopeDataContract()
        {
            this.StockDeliverySetRequest = new StockDeliverySetRequestDataContract();
        }

        public StockDeliverySetRequestEnvelopeDataContract( IMessageEnvelope dataObject )
        :
            base( dataObject )
        {
            this.StockDeliverySetRequest = new StockDeliverySetRequestDataContract( ( StockDeliverySetRequest )( dataObject.Message ) );
        }

        [JsonIgnore]
        public override IMessage Message => this.StockDeliverySetRequest.GetDataObject();

        public StockDeliverySetRequestDataContract StockDeliverySetRequest
        {
            get; set;
        }
    }
}
