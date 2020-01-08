﻿using System;
using System.Xml;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Serialization;
using Reth.Protocols.Serialization.Xml;

namespace Reth.Itss2.Standard.Serialization.Xml.DataContracts.Storage.InitiateInput
{
    internal class InitiateInputMessageDetailsDataContract<TTypeMappings>:XmlSerializable<InitiateInputMessageDetails, TTypeMappings>
        where TTypeMappings:ITypeMappings
    {
        public InitiateInputMessageDetailsDataContract()
        {
        }

        public InitiateInputMessageDetailsDataContract( InitiateInputMessageDetails dataObject )
        :
            base( dataObject )
        {
        }
        
        public override void ReadXml( XmlReader reader )
        {      
            int inputSource = base.Serializer.ReadMandatoryInteger( reader, nameof( this.DataObject.InputSource ) );
            InitiateInputMessageStatus status = base.Serializer.ReadMandatoryEnum<InitiateInputMessageStatus>( reader, nameof( this.DataObject.Status ) );
            Nullable<int> inputPoint = base.Serializer.ReadOptionalInteger( reader, nameof( this.DataObject.InputPoint ) );

            this.DataObject = new InitiateInputMessageDetails(  inputSource,
                                                                status,
                                                                inputPoint );
        }

        public override void WriteXml( XmlWriter writer )
        {
            InitiateInputMessageDetails dataObject = this.DataObject;

            base.Serializer.WriteMandatoryInteger( writer, nameof( dataObject.InputSource ), dataObject.InputSource );
            base.Serializer.WriteMandatoryEnum<InitiateInputMessageStatus>( writer, nameof( dataObject.Status ), dataObject.Status );
            base.Serializer.WriteOptionalInteger( writer, nameof( dataObject.InputPoint ), dataObject.InputPoint );
        }
    }
}