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
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Dialogs.Standard.Protocol;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.UnprocessedDialog;

namespace Reth.Itss2.Workflows.Standard.StorageSystem.UnprocessedDialog
{
    public interface IUnprocessedWorkflow:IWorkflow
    {
        event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        void SendMessage( UnprocessedMessage message );
        Task SendMessageAsync( UnprocessedMessage message, CancellationToken cancellationToken = default );

        UnprocessedMessage CreateUnprocessedMessage(    SubscriberId localSubscriberId,
                                                        SubscriberId remoteSubscriberId,
                                                        IMessage message,
                                                        String? text,
                                                        UnprocessedReason? reason    );
        
        UnprocessedMessage CreateUnprocessedMessage(    SubscriberId localSubscriberId,
                                                        SubscriberId remoteSubscriberId,
                                                        String message,
                                                        String? text,
                                                        UnprocessedReason? reason   );
    }
}