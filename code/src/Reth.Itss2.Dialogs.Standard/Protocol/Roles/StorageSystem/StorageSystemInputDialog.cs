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

using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InputDialog;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Roles.StorageSystem
{
    public class StorageSystemInputDialog:Dialog, IStorageSystemInputDialog
    {
        public StorageSystemInputDialog( IDialogProvider dialogProvider )
        :
            base( "Input", dialogProvider )
        {
        }

        public InputResponse SendRequest( InputRequest request )
        {
            return base.SendRequest<InputRequest, InputResponse>( request );
        }
        
        public Task<InputResponse> SendRequestAsync( InputRequest request )
        {
            return base.SendRequestAsync<InputRequest, InputResponse>( request );
        }

        public Task<InputResponse> SendRequestAsync( InputRequest request, CancellationToken cancellationToken )
        {
            return base.SendRequestAsync<InputRequest, InputResponse>( request, cancellationToken );
        }

        public void SendMessage( InputMessage message )
        {
            base.SendMessage( message );
        }
        
        public Task SendMessageAsync( InputMessage message )
        {
            return base.SendMessageAsync( message );
        }
    }
}