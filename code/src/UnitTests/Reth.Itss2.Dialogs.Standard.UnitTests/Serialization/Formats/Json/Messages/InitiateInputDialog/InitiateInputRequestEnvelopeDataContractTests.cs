﻿// Implementation of the WWKS2 protocol.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.InitiateInputDialog
{
    [TestClass]
    public class InitiateInputRequestEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Request
        {
            get
            {
                ( int InputSource, int InputPoint ) details = ( 1, 2 );

                (   ArticleId Id,
                    String FMDId    ) article = ( new ArticleId( "1985" ), "FMD-23" );

                (   int Index,
                    String ScanCode,
                    String DeliveryNumber,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    PackDate ExpiryDate,
                    int SubItemQuantity,
                    String MachineLocation,
                    StockLocationId StockLocationId,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    PackShape Shape ) pack = (  42,
                                                "0101010",
                                                "4711",
                                                "0815",
                                                "EXT-1",
                                                "SER-3",
                                                new PackDate( 2021, 5, 5 ),
                                                30,
                                                "main",
                                                new StockLocationId( "default" ),
                                                100,
                                                50,
                                                24,
                                                153,
                                                PackShape.Cuboid    );

                bool isNewDelivery = false;
                bool setPickingIndicator = true;

                return (    $@" {{
                                    ""InitiateInputRequest"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IsNewDelivery"": ""{ isNewDelivery }"",
                                        ""SetPickingIndicator"": ""{ setPickingIndicator }"",
                                        ""Details"":
                                        {{
                                            ""InputSource"": ""{ details.InputSource }"",
                                            ""InputPoint"": ""{ details.InputPoint }""
                                        }},
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""FMDId"": ""{ article.FMDId }"",
                                                ""Pack"":
                                                [
                                                    {{
                                                        ""ScanCode"": ""{ pack.ScanCode }"",
                                                        ""DeliveryNumber"": ""{ pack.DeliveryNumber }"",
                                                        ""BatchNumber"": ""{ pack.BatchNumber }"",
                                                        ""ExternalId"": ""{ pack.ExternalId }"",
                                                        ""SerialNumber"": ""{ pack.SerialNumber }"",
                                                        ""MachineLocation"": ""{ pack.MachineLocation }"",
                                                        ""StockLocationId"": ""{ pack.StockLocationId }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }"",
                                                        ""Depth"": ""{ pack.Depth }"",
                                                        ""Width"": ""{ pack.Width }"",
                                                        ""Height"": ""{ pack.Height }"",
                                                        ""Weight"": ""{ pack.Weight }"",
                                                        ""Shape"": ""{ pack.Shape }""
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new InitiateInputRequest(   JsonMessageTests.MessageId,
                                                                                JsonMessageTests.Source,
                                                                                JsonMessageTests.Destination,
                                                                                new( details.InputSource, details.InputPoint ),
                                                                                new InitiateInputRequestArticle[]
                                                                                {
                                                                                    new(    article.Id,
                                                                                            article.FMDId,
                                                                                            new InitiateInputRequestPack[]
                                                                                            {
                                                                                                new(    pack.ScanCode,
                                                                                                        pack.DeliveryNumber,
                                                                                                        pack.BatchNumber,
                                                                                                        pack.ExternalId,
                                                                                                        pack.SerialNumber,
                                                                                                        pack.MachineLocation,
                                                                                                        pack.StockLocationId,
                                                                                                        pack.ExpiryDate,
                                                                                                        pack.Index,
                                                                                                        pack.SubItemQuantity,
                                                                                                        pack.Depth,
                                                                                                        pack.Width,
                                                                                                        pack.Height,
                                                                                                        pack.Weight,
                                                                                                        pack.Shape  )
                                                                                            }   )
                                                                                },
                                                                                isNewDelivery,
                                                                                setPickingIndicator ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( InitiateInputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( InitiateInputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}