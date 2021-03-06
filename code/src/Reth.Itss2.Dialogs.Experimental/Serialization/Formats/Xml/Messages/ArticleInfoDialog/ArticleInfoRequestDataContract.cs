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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages;

using ArticleInfoRequestArticle = Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo.ArticleInfoRequestArticle;
using ArticleInfoRequestArticleDataContract = Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.ArticleInfo.ArticleInfoRequestArticleDataContract;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages.ArticleInfo
{
    public class ArticleInfoRequestDataContract:SubscribedRequestDataContract<ArticleInfoRequest>
    {
        public ArticleInfoRequestDataContract()
        {
            this.Articles = new ArticleInfoRequestArticleDataContract[]{};
        }

        public ArticleInfoRequestDataContract( ArticleInfoRequest dataObject )
        :
            base( dataObject )
        {
            this.Articles = TypeConverter.ConvertFromDataObjects<ArticleInfoRequestArticle, ArticleInfoRequestArticleDataContract>( dataObject.GetArticles() );
            this.IncludeCrossSellingArticles = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IncludeCrossSellingArticles );
            this.IncludeAlternativeArticles = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IncludeAlternativeArticles );
            this.IncludeAlternativePackSizeArticles = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IncludeAlternativePackSizeArticles );
        }

        [XmlElement( ElementName = "Article" )]
        public ArticleInfoRequestArticleDataContract[] Articles{ get; set; }

        [XmlAttribute]
        public String? IncludeCrossSellingArticles{ get; set; }
        
        [XmlAttribute]
        public String? IncludeAlternativeArticles{ get; set; }
        
        [XmlAttribute]
        public String? IncludeAlternativePackSizeArticles{ get; set; }
        
        public override ArticleInfoRequest GetDataObject()
        {
            return new ArticleInfoRequest( TypeConverter.MessageId.ConvertTo( this.Id ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                            TypeConverter.ConvertToDataObjects<ArticleInfoRequestArticle, ArticleInfoRequestArticleDataContract>( this.Articles ),
                                            TypeConverter.Boolean.ConvertNullableTo( this.IncludeCrossSellingArticles ),
                                            TypeConverter.Boolean.ConvertNullableTo( this.IncludeAlternativeArticles ),
                                            TypeConverter.Boolean.ConvertNullableTo( this.IncludeAlternativePackSizeArticles ) );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( ArticleInfoRequestEnvelopeDataContract );
        }
    }
}
