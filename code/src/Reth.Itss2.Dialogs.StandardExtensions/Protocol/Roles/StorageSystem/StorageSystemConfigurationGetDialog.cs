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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGetDialog;

namespace Reth.Itss2.Dialogs.StandardExtensions.Protocol.Roles.StorageSystem
{
    public class StorageSystemConfigurationGetDialog:Dialog, IStorageSystemConfigurationGetDialog
    {
        public event EventHandler<MessageReceivedEventArgs>? RequestReceived;

        public StorageSystemConfigurationGetDialog( IDialogProvider dialogProvider )
        :
            base( Dialogs.ConfigurationGet, dialogProvider )
        {
        }

        protected void OnRequestReceived( ConfigurationGetRequest request )
        {
            if( this.RequestReceived is not null )
            {
                this.RequestReceived.Invoke( this, new MessageReceivedEventArgs( request, this.DialogProvider ) );
            }
        }

        public override void Connect( IMessageTransmitter messageTransmitter )
        {
            base.Connect(   messageTransmitter,
                            ( IMessage message ) =>
                            {
                                this.Dispatch<ConfigurationGetRequest>( message, this.OnRequestReceived );
                            }   );
        }

        public void SendResponse( ConfigurationGetResponse response )
        {
            base.SendResponse( response );
        }
        
        public Task SendResponseAsync( ConfigurationGetResponse response, CancellationToken cancellationToken = default )
        {
            return base.SendResponseAsync( response, cancellationToken );
        }
    }
}
