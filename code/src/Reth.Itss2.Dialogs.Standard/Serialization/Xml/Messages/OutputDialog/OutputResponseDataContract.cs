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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages.OutputDialog
{
    public class OutputResponseDataContract:SubscribedResponseDataContract<OutputResponse>
    {
        public OutputResponseDataContract()
        {
            this.Details = new OutputResponseDetailsDataContract();
            this.Criterias = new OutputCriteriaDataContract[]{};
        }

        public OutputResponseDataContract( OutputResponse dataObject )
        :
            base( dataObject )
        {
            this.Details = TypeConverter.ConvertFromDataObject<OutputResponseDetails, OutputResponseDetailsDataContract>( dataObject.Details );
            this.BoxNumber = dataObject.BoxNumber;
            this.Criterias = TypeConverter.ConvertFromDataObjects<OutputCriteria, OutputCriteriaDataContract>( dataObject.GetCriterias() );
        }

        [XmlElement]
        public OutputResponseDetailsDataContract Details{ get; set; }

        [XmlAttribute]
        public String? BoxNumber{ get; set; }

        [XmlElement( ElementName = "Criteria" )]
        public OutputCriteriaDataContract[] Criterias{ get; set; }

        public override OutputResponse GetDataObject()
        {
            return new OutputResponse(  TypeConverter.MessageId.ConvertTo( this.Id ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                        TypeConverter.ConvertToDataObject<OutputResponseDetails, OutputResponseDetailsDataContract>( this.Details ),
                                        this.BoxNumber,
                                        TypeConverter.ConvertToDataObjects<OutputCriteria, OutputCriteriaDataContract>( this.Criterias )    );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( OutputResponseEnvelopeDataContract );
        }
    }
}