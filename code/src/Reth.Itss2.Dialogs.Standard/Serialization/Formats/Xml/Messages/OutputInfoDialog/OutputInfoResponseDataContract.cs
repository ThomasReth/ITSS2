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
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.OutputInfo
{
    public class OutputInfoResponseDataContract:SubscribedResponseDataContract<OutputInfoResponse>
    {
        public OutputInfoResponseDataContract()
        {
            this.Task = new OutputInfoResponseTaskDataContract();
        }

        public OutputInfoResponseDataContract( OutputInfoResponse dataObject )
        :
            base( dataObject )
        {
            this.Task = TypeConverter.ConvertFromDataObject<OutputInfoResponseTask, OutputInfoResponseTaskDataContract>( dataObject.Task );
        }

        [XmlElement]
        public OutputInfoResponseTaskDataContract Task{ get; set; }

        public override OutputInfoResponse GetDataObject()
        {
            return new OutputInfoResponse(  TypeConverter.MessageId.ConvertTo( this.Id ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                            TypeConverter.ConvertToDataObject<OutputInfoResponseTask, OutputInfoResponseTaskDataContract>( this.Task )  );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( OutputInfoResponseEnvelopeDataContract );
        }
    }
}
