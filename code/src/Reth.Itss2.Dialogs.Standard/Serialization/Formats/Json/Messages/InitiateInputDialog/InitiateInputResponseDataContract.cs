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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.InitiateInput
{
    public class InitiateInputResponseDataContract:SubscribedResponseDataContract<InitiateInputResponse>
    {
        public InitiateInputResponseDataContract()
        {
            this.Details = new InitiateInputResponseDetailsDataContract();
            this.Articles = new InitiateInputResponseArticleDataContract[]{};
        }

        public InitiateInputResponseDataContract( InitiateInputResponse dataObject )
        :
            base( dataObject )
        {
            this.Details = TypeConverter.ConvertFromDataObject<InitiateInputResponseDetails, InitiateInputResponseDetailsDataContract>( dataObject.Details );
            this.Articles = TypeConverter.ConvertFromDataObjects<InitiateInputResponseArticle, InitiateInputResponseArticleDataContract>( dataObject.GetArticles() );
            this.IsNewDelivery = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IsNewDelivery );
            this.SetPickingIndicator = TypeConverter.Boolean.ConvertNullableFrom( dataObject.SetPickingIndicator );
        }

        public InitiateInputResponseDetailsDataContract Details{ get; set; }

        public String? IsNewDelivery{ get; set; }

        public String? SetPickingIndicator{ get; set; }

        [JsonPropertyName( "Article" )]
        public InitiateInputResponseArticleDataContract[]? Articles{ get; set; }

        public override InitiateInputResponse GetDataObject()
        {
            return new InitiateInputResponse(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                TypeConverter.ConvertToDataObject<InitiateInputResponseDetails, InitiateInputResponseDetailsDataContract>( this.Details ),
                                                TypeConverter.ConvertToDataObjects<InitiateInputResponseArticle, InitiateInputResponseArticleDataContract>( this.Articles ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.IsNewDelivery ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.SetPickingIndicator ) );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( InitiateInputResponseEnvelopeDataContract );
        }
    }
}
